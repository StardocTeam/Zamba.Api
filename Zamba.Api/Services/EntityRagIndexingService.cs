using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;
using Zamba.Api.Data;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface IEntityRagIndexingService
{
    Task<IReadOnlyList<EntityRagIndexingConfigDto>> GetConfigurationsAsync(
        CancellationToken cancellationToken
    );
    Task<EntityRagIndexingConfigDto?> GetConfigurationAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    );
    Task<EntityRagIndexingConfigDto> SaveConfigurationAsync(
        decimal documentTypeId,
        UpdateEntityRagIndexingConfigDto dto,
        CancellationToken cancellationToken
    );
    Task<bool> DeleteConfigurationAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    );
    Task<EntityRagIndexingRunDto> RunBatchAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    );
    Task<EntityRagIndexingRunDto> ProcessOnInsertPendingAsync(CancellationToken cancellationToken);
    Task<EntityRagIndexingRunDto> IndexInsertedDocumentAsync(
        decimal documentTypeId,
        decimal documentId,
        CancellationToken cancellationToken
    );
}

public sealed class EntityRagIndexingService(
    ZambaDbContext dbContext,
    IVectorStore vectorStore,
    IFileTextExtractor textExtractor,
    ITextChunker textChunker,
    IEmbeddingClient embeddingClient,
    IOptions<VectorStoreOptions> vectorStoreOptions,
    IOptions<FileProcessingOptions> fileProcessingOptions
) : IEntityRagIndexingService
{
    private const int BatchSize = 100;
    private const int OnInsertBatchSize = 25;
    private DatabaseSqlDialect SqlDialect =>
        DatabaseSqlDialect.FromProviderName(dbContext.Database.ProviderName);
    private readonly VectorStoreOptions _vectorOptions = vectorStoreOptions.Value;
    private readonly FileProcessingOptions _fileOptions = fileProcessingOptions.Value;

    public async Task<IReadOnlyList<EntityRagIndexingConfigDto>> GetConfigurationsAsync(
        CancellationToken cancellationToken
    )
    {
        var connection = dbContext.Database.GetDbConnection();
        var schema = await LoadSchemaAsync(connection, cancellationToken);
        var docTypeTable =
            schema.FindTable("DOC_TYPE")
            ?? throw new InvalidOperationException("The DOC_TYPE table was not found.");
        var idColumn =
            docTypeTable.FindColumn("DOC_TYPE_ID")
            ?? throw new InvalidOperationException("DOC_TYPE does not contain DOC_TYPE_ID.");
        var nameColumn = docTypeTable.FindColumn("DOC_TYPE_NAME");
        var configs = await dbContext
            .EntityRagIndexingConfigs.AsNoTracking()
            .ToDictionaryAsync(config => config.DocumentTypeId, cancellationToken);
        var openHere = connection.State != ConnectionState.Open;
        if (openHere)
        {
            await connection.OpenAsync(cancellationToken);
        }

        try
        {
            await using var command = connection.CreateCommand();
            var dialect = SqlDialect;
            command.CommandText = dialect.PrepareCommandText(
                $"SELECT {Quote(idColumn)}, {(nameColumn is null ? "NULL" : Quote(nameColumn))} FROM {Quote(docTypeTable.Schema)}.{Quote(docTypeTable.Name)} ORDER BY {Quote(idColumn)};"
            );
            DatabaseSqlDialect.ConfigureCommand(command);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            var results = new List<EntityRagIndexingConfigDto>();
            while (await reader.ReadAsync(cancellationToken))
            {
                var id = Convert.ToDecimal(reader.GetValue(0), CultureInfo.InvariantCulture);
                configs.TryGetValue(id, out var config);
                results.Add(
                    new EntityRagIndexingConfigDto
                    {
                        DocumentTypeId = id,
                        DocumentTypeName = reader.IsDBNull(1)
                            ? null
                            : Convert
                                .ToString(reader.GetValue(1), CultureInfo.InvariantCulture)
                                ?.Trim(),
                        IsSupported = IsSupported(schema, id),
                        IsEnabled = config?.IsEnabled ?? false,
                        IndexFromDate = config?.IndexFromDate,
                        IndexMode = config?.IndexMode ?? EntityRagIndexMode.Batch,
                        CreatedAtUtc = config?.CreatedAtUtc,
                        UpdatedAtUtc = config?.UpdatedAtUtc,
                    }
                );
            }

            return results;
        }
        finally
        {
            if (openHere)
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<EntityRagIndexingConfigDto?> GetConfigurationAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    )
    {
        var config = await dbContext
            .EntityRagIndexingConfigs.AsNoTracking()
            .FirstOrDefaultAsync(item => item.DocumentTypeId == documentTypeId, cancellationToken);
        if (config is null)
        {
            return null;
        }

        var catalogItem = (await GetConfigurationsAsync(cancellationToken)).FirstOrDefault(item =>
            item.DocumentTypeId == documentTypeId
        );
        return catalogItem;
    }

    public async Task<EntityRagIndexingConfigDto> SaveConfigurationAsync(
        decimal documentTypeId,
        UpdateEntityRagIndexingConfigDto dto,
        CancellationToken cancellationToken
    )
    {
        if (!Enum.IsDefined(dto.IndexMode))
        {
            throw new ArgumentException("Unknown entity indexing mode.");
        }

        var schema = await LoadSchemaAsync(dbContext.Database.GetDbConnection(), cancellationToken);
        if (schema.FindTable("DOC_TYPE") is null || schema.FindEntityTables(documentTypeId) is null)
        {
            throw new KeyNotFoundException(
                $"Document type {documentTypeId} is not available for indexing."
            );
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(
            cancellationToken
        );
        var config = await dbContext.EntityRagIndexingConfigs.FirstOrDefaultAsync(
            item => item.DocumentTypeId == documentTypeId,
            cancellationToken
        );
        var now = DateTime.UtcNow;
        if (config is null)
        {
            config = new EntityRagIndexingConfig
            {
                DocumentTypeId = documentTypeId,
                CreatedAtUtc = now,
            };
            dbContext.EntityRagIndexingConfigs.Add(config);
        }

        config.IsEnabled = dto.IsEnabled;
        config.IndexFromDate = dto.IndexFromDate;
        config.IndexMode = dto.IndexMode;
        config.UpdatedAtUtc = now;
        await dbContext.SaveChangesAsync(cancellationToken);

        await SynchronizeScheduleEventAsync(config, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        var item = (await GetConfigurationsAsync(cancellationToken)).First(configItem =>
            configItem.DocumentTypeId == documentTypeId
        );
        return item;
    }

    public async Task<bool> DeleteConfigurationAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    )
    {
        var config = await dbContext.EntityRagIndexingConfigs.FirstOrDefaultAsync(
            item => item.DocumentTypeId == documentTypeId,
            cancellationToken
        );
        if (config is null)
        {
            return false;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(
            cancellationToken
        );
        await RemoveScheduleEventAsync(documentTypeId, cancellationToken);
        dbContext.EntityRagIndexingConfigs.Remove(config);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return true;
    }

    public async Task<EntityRagIndexingRunDto> RunBatchAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    )
    {
        var config = await dbContext
            .EntityRagIndexingConfigs.AsNoTracking()
            .FirstOrDefaultAsync(item => item.DocumentTypeId == documentTypeId, cancellationToken);
        if (config is null || !config.IsEnabled || config.IndexMode != EntityRagIndexMode.Batch)
        {
            throw new InvalidOperationException(
                "Batch indexing is not enabled for this document type."
            );
        }

        return await ProcessAsync(
            documentTypeId,
            config.IndexFromDate,
            null,
            BatchSize,
            cancellationToken
        );
    }

    public async Task<EntityRagIndexingRunDto> ProcessOnInsertPendingAsync(
        CancellationToken cancellationToken
    )
    {
        var configs = await dbContext
            .EntityRagIndexingConfigs.AsNoTracking()
            .Where(config => config.IndexMode == EntityRagIndexMode.OnInsert)
            .ToListAsync(cancellationToken);
        var total = new EntityRagIndexingRunDto();
        foreach (var config in configs.Where(config => config.IsEnabled))
        {
            var result = await ProcessAsync(
                config.DocumentTypeId,
                config.IndexFromDate,
                null,
                OnInsertBatchSize,
                cancellationToken
            );
            total = total with
            {
                Attempted = total.Attempted + result.Attempted,
                Indexed = total.Indexed + result.Indexed,
                Skipped = total.Skipped + result.Skipped,
                Failed = total.Failed + result.Failed,
                HasMore = total.HasMore || result.HasMore,
            };
        }

        return total;
    }

    public async Task<EntityRagIndexingRunDto> IndexInsertedDocumentAsync(
        decimal documentTypeId,
        decimal documentId,
        CancellationToken cancellationToken
    )
    {
        var config = await dbContext
            .EntityRagIndexingConfigs.AsNoTracking()
            .FirstOrDefaultAsync(item => item.DocumentTypeId == documentTypeId, cancellationToken);
        if (config is null || !config.IsEnabled || config.IndexMode != EntityRagIndexMode.OnInsert)
        {
            throw new InvalidOperationException(
                "Insert-time indexing is not enabled for this document type."
            );
        }

        return await ProcessAsync(
            documentTypeId,
            config.IndexFromDate,
            documentId,
            1,
            cancellationToken
        );
    }

    private async Task<EntityRagIndexingRunDto> ProcessAsync(
        decimal documentTypeId,
        DateTime? indexFromDate,
        decimal? onlyDocumentId,
        int batchSize,
        CancellationToken cancellationToken
    )
    {
        var schema = await LoadSchemaAsync(dbContext.Database.GetDbConnection(), cancellationToken);
        var tables =
            schema.FindEntityTables(documentTypeId)
            ?? throw new InvalidOperationException(
                $"DOC_T{FormatId(documentTypeId)} or DOC_I{FormatId(documentTypeId)} was not found."
            );
        var candidates = await ReadCandidatesAsync(
            schema,
            tables,
            documentTypeId,
            indexFromDate,
            onlyDocumentId,
            batchSize,
            cancellationToken
        );
        var processCount = onlyDocumentId.HasValue
            ? candidates.Count
            : Math.Min(batchSize, candidates.Count);
        var result = new EntityRagIndexingRunDto
        {
            DocumentTypeId = documentTypeId,
            HasMore = candidates.Count > processCount,
        };

        foreach (var candidate in candidates.Take(processCount))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result = await ProcessCandidateAsync(
                documentTypeId,
                candidate,
                result,
                cancellationToken
            );
        }

        return result;
    }

    private async Task<EntityRagIndexingRunDto> ProcessCandidateAsync(
        decimal documentTypeId,
        DocumentCandidate candidate,
        EntityRagIndexingRunDto result,
        CancellationToken cancellationToken
    )
    {
        var fileName = candidate.FileName?.Trim();
        var state = await dbContext.EntityRagDocumentStates.FirstOrDefaultAsync(
            item =>
                item.DocumentTypeId == documentTypeId && item.DocumentId == candidate.DocumentId,
            cancellationToken
        );
        state ??= new EntityRagDocumentState
        {
            DocumentTypeId = documentTypeId,
            DocumentId = candidate.DocumentId,
        };
        if (
            state.Status == EntityRagDocumentIndexStatus.Indexed
            || state.Status == EntityRagDocumentIndexStatus.Skipped
        )
        {
            return result;
        }

        state.FileName = fileName;
        state.UpdatedAtUtc = DateTime.UtcNow;
        state.LastError = null;
        result = result with { Attempted = result.Attempted + 1 };

        if (
            string.IsNullOrWhiteSpace(fileName) || !fileName.Contains('.', StringComparison.Ordinal)
        )
        {
            state.Status = EntityRagDocumentIndexStatus.Skipped;
            state.LastError = "The source document has no file extension.";
            result = result with { Skipped = result.Skipped + 1 };
            await SaveStateAsync(state, cancellationToken);
            return result;
        }

        try
        {
            if (!string.Equals(Path.GetFileName(fileName), fileName, StringComparison.Ordinal))
            {
                throw new InvalidDataException("The source filename contains a path separator.");
            }

            var filePath = Path.GetFullPath(
                Path.Combine(
                    candidate.VolumePath
                        ?? throw new FileNotFoundException(
                            "The document volume path was not found."
                        ),
                    FormatId(documentTypeId),
                    candidate.Offset?.ToString(CultureInfo.InvariantCulture) ?? "",
                    fileName
                )
            );
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "The source document file was not found.",
                    filePath
                );
            }

            var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
            if (bytes.LongLength > _fileOptions.MaxFileSizeBytes)
            {
                throw new InvalidDataException(
                    $"The source file exceeds the {_fileOptions.MaxFileSizeBytes}-byte indexing limit."
                );
            }

            var vectorDocumentId = await IndexFileAsync(bytes, fileName, cancellationToken);
            state.Status = EntityRagDocumentIndexStatus.Indexed;
            state.VectorDocumentId = vectorDocumentId;
            state.IndexedAtUtc = DateTime.UtcNow;
            result = result with { Indexed = result.Indexed + 1 };
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            state.Status = EntityRagDocumentIndexStatus.Failed;
            state.LastError =
                exception.Message.Length <= 4000 ? exception.Message : exception.Message[..4000];
            result = result with { Failed = result.Failed + 1 };
        }

        await SaveStateAsync(state, cancellationToken);
        return result;
    }

    private async Task<string> IndexFileAsync(
        byte[] bytes,
        string fileName,
        CancellationToken cancellationToken
    )
    {
        var documentId = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
        if (
            await vectorStore.DocumentExistsAsync(documentId, cancellationToken)
            && await vectorStore.HasChunksAsync(documentId, cancellationToken)
        )
        {
            return documentId;
        }

        var text = await textExtractor.ExtractTextAsync(bytes, fileName, cancellationToken);
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidDataException("No text could be extracted from the source document.");
        }

        var textChunks = textChunker.Chunk(
            text,
            _vectorOptions.ChunkSize,
            _vectorOptions.ChunkOverlap
        );
        var chunks = new List<DocumentChunk>(textChunks.Count);
        for (var index = 0; index < textChunks.Count; index++)
        {
            var embedding = await embeddingClient.GetEmbeddingAsync(
                textChunks[index],
                cancellationToken
            );
            chunks.Add(new DocumentChunk(index, textChunks[index], embedding));
        }

        if (chunks.Count == 0)
        {
            throw new InvalidDataException("No text chunks were produced for the source document.");
        }

        await vectorStore.SaveChunksAsync(documentId, fileName, chunks, cancellationToken);
        return documentId;
    }

    private async Task<List<DocumentCandidate>> ReadCandidatesAsync(
        SchemaSnapshot schema,
        EntityTables tables,
        decimal documentTypeId,
        DateTime? indexFromDate,
        decimal? onlyDocumentId,
        int batchSize,
        CancellationToken cancellationToken
    )
    {
        var diskVolume =
            schema.FindTable("DISK_VOLUME")
            ?? throw new InvalidOperationException("The DISK_VOLUME table was not found.");
        var volumeId =
            diskVolume.FindColumn("DISK_VOL_ID")
            ?? throw new InvalidOperationException("DISK_VOLUME does not contain DISK_VOL_ID.");
        var volumePath =
            diskVolume.FindColumn("DISK_VOL_PATH")
            ?? throw new InvalidOperationException("DISK_VOLUME does not contain DISK_VOL_PATH.");
        var documentId = tables.Document.FindColumn("DOC_ID")!;
        var documentVolume = tables.Document.FindColumn("VOL_ID");
        var documentOffset = tables.Document.FindColumn("OFFSET");
        var documentFile = tables.Document.FindColumn("DOC_FILE");
        var indexDocumentId = tables.Index.FindColumn("DOC_ID")!;
        var createdDate =
            tables.Index.FindColumn("crdate")
            ?? throw new InvalidOperationException($"{tables.Index.Name} does not contain crdate.");
        if (documentVolume is null || documentOffset is null || documentFile is null)
        {
            throw new InvalidOperationException(
                $"{tables.Document.Name} must contain VOL_ID, OFFSET, and DOC_FILE."
            );
        }

        var conditions = new List<string>();
        var parameters = new List<(string Name, object Value)>();
        if (indexFromDate.HasValue)
        {
            conditions.Add($"i.{Quote(createdDate)} >= @indexFromDate");
            parameters.Add(("indexFromDate", indexFromDate.Value));
        }
        if (onlyDocumentId.HasValue)
        {
            conditions.Add($"d.{Quote(documentId)} = @documentId");
            parameters.Add(("documentId", onlyDocumentId.Value));
        }

        var stateEntity = dbContext.Model.FindEntityType(typeof(EntityRagDocumentState))!;
        var stateTableName = stateEntity.GetTableName()!;
        var stateTable = string.IsNullOrEmpty(stateEntity.GetSchema())
            ? Quote(stateTableName)
            : $"{Quote(stateEntity.GetSchema()!)}.{Quote(stateTableName)}";
        var skipCompleted = onlyDocumentId.HasValue
            ? ""
            : $"AND NOT EXISTS (SELECT 1 FROM {stateTable} s WHERE s.{Quote(nameof(EntityRagDocumentState.DocumentTypeId))} = @documentTypeId AND s.{Quote(nameof(EntityRagDocumentState.DocumentId))} = d.{Quote(documentId)} AND s.{Quote(nameof(EntityRagDocumentState.Status))} IN (1, 2))";
        parameters.Add(("documentTypeId", documentTypeId));
        var whereSql = conditions.Count == 0 ? "" : "AND " + string.Join(" AND ", conditions);
        var joinVolume =
            $"LEFT JOIN {Quote(diskVolume.Schema)}.{Quote(diskVolume.Name)} v ON v.{Quote(volumeId)} = d.{Quote(documentVolume)}";
        var dialect = SqlDialect;
        var sql =
            $"{dialect.SelectWithLimit("take")}d.{Quote(documentId)}, d.{Quote(documentOffset)}, d.{Quote(documentFile)}, v.{Quote(volumePath)} "
            + $"FROM {Quote(tables.Document.Schema)}.{Quote(tables.Document.Name)} d "
            + $"INNER JOIN {Quote(tables.Index.Schema)}.{Quote(tables.Index.Name)} i ON i.{Quote(indexDocumentId)} = d.{Quote(documentId)} "
            + $"{joinVolume} WHERE 1 = 1 {whereSql} {skipCompleted} "
            + $"ORDER BY i.{Quote(createdDate)}, d.{Quote(documentId)}{dialect.FetchFirst("take")};";

        var connection = dbContext.Database.GetDbConnection();
        var openHere = connection.State != ConnectionState.Open;
        if (openHere)
        {
            await connection.OpenAsync(cancellationToken);
        }

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = dialect.PrepareCommandText(sql);
            DatabaseSqlDialect.ConfigureCommand(command);
            command.CommandTimeout = 60;
            AddParameter(command, dialect, "take", onlyDocumentId.HasValue ? 1 : batchSize + 1);
            foreach (var (name, value) in parameters)
            {
                AddParameter(command, dialect, name, value);
            }

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            var rows = new List<DocumentCandidate>();
            while (await reader.ReadAsync(cancellationToken))
            {
                rows.Add(
                    new DocumentCandidate(
                        Convert.ToDecimal(reader.GetValue(0), CultureInfo.InvariantCulture),
                        reader.IsDBNull(1)
                            ? null
                            : Convert.ToString(reader.GetValue(1), CultureInfo.InvariantCulture),
                        reader.IsDBNull(2)
                            ? null
                            : Convert.ToString(reader.GetValue(2), CultureInfo.InvariantCulture),
                        reader.IsDBNull(3)
                            ? null
                            : Convert.ToString(reader.GetValue(3), CultureInfo.InvariantCulture)
                    )
                );
            }

            return rows;
        }
        finally
        {
            if (openHere)
            {
                await connection.CloseAsync();
            }
        }
    }

    private async Task SaveStateAsync(
        EntityRagDocumentState state,
        CancellationToken cancellationToken
    )
    {
        if (dbContext.Entry(state).State == EntityState.Detached)
        {
            dbContext.EntityRagDocumentStates.Add(state);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SynchronizeScheduleEventAsync(
        EntityRagIndexingConfig config,
        CancellationToken cancellationToken
    )
    {
        var scheduleEvent = await dbContext
            .ScheduleEvents.Include(item => item.ScheduleConfig)
            .FirstOrDefaultAsync(
                item =>
                    item.ExecutionType == ScheduleExecutionType.EmbeddingRag
                    && item.RagDocumentTypeId == config.DocumentTypeId,
                cancellationToken
            );
        if (!config.IsEnabled || config.IndexMode != EntityRagIndexMode.Batch)
        {
            if (scheduleEvent is not null)
            {
                dbContext.ScheduleEvents.Remove(scheduleEvent);
            }
            return;
        }

        var now = DateTime.UtcNow;
        if (scheduleEvent is null)
        {
            scheduleEvent = new ScheduleEvent
            {
                Name = $"RAG indexing: DOC_TYPE {FormatId(config.DocumentTypeId)}",
                RuleId = $"entity-rag-{FormatId(config.DocumentTypeId)}",
                ExecutionType = ScheduleExecutionType.EmbeddingRag,
                RagDocumentTypeId = config.DocumentTypeId,
                IsActive = true,
                CreatedAt = now,
                CreatedBy = "EntityRagIndexingConfig",
            };
            dbContext.ScheduleEvents.Add(scheduleEvent);
        }

        scheduleEvent.Description = "Indexes unprocessed documents for this entity type.";
        scheduleEvent.IsActive = true;
        scheduleEvent.RagDocumentTypeId = config.DocumentTypeId;
        scheduleEvent.UpdatedAt = now;
        scheduleEvent.UpdatedBy = "EntityRagIndexingConfig";
        var schedule = scheduleEvent.ScheduleConfig;
        if (schedule is null)
        {
            schedule = new ScheduleConfig();
            scheduleEvent.ScheduleConfig = schedule;
        }

        schedule.RecurrenceType = RecurrenceType.Hourly;
        schedule.IntervalValue = 1;
        schedule.StartDate = config.IndexFromDate ?? now;
        schedule.TimeZone = "UTC";
        schedule.MisfireInstruction = MisfireInstruction.FireOnceNow;
    }

    private async Task RemoveScheduleEventAsync(
        decimal documentTypeId,
        CancellationToken cancellationToken
    )
    {
        var scheduleEvent = await dbContext.ScheduleEvents.FirstOrDefaultAsync(
            item =>
                item.ExecutionType == ScheduleExecutionType.EmbeddingRag
                && item.RagDocumentTypeId == documentTypeId,
            cancellationToken
        );
        if (scheduleEvent is not null)
        {
            dbContext.ScheduleEvents.Remove(scheduleEvent);
        }
    }

    private static async Task<SchemaSnapshot> LoadSchemaAsync(
        DbConnection connection,
        CancellationToken cancellationToken
    )
    {
        var openHere = connection.State != ConnectionState.Open;
        if (openHere)
        {
            await connection.OpenAsync(cancellationToken);
        }

        try
        {
            await using var command = connection.CreateCommand();
            var dialect = DatabaseSqlDialect.FromConnection(connection);
            command.CommandText = dialect.ColumnsQuery;
            DatabaseSqlDialect.ConfigureCommand(command);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            var tables = new Dictionary<(string Schema, string Table), List<string>>();
            while (await reader.ReadAsync(cancellationToken))
            {
                var key = (reader.GetString(0), reader.GetString(1));
                if (!tables.TryGetValue(key, out var columns))
                {
                    columns = [];
                    tables.Add(key, columns);
                }
                columns.Add(reader.GetString(2));
            }

            return new SchemaSnapshot(
                tables
                    .Select(item => new TableInfo(item.Key.Schema, item.Key.Table, item.Value))
                    .ToArray()
            );
        }
        finally
        {
            if (openHere)
            {
                await connection.CloseAsync();
            }
        }
    }

    private static void AddParameter(
        DbCommand command,
        DatabaseSqlDialect dialect,
        string name,
        object value
    )
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = dialect.CommandParameterName(name);
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }

    private static string FormatId(decimal id) => id.ToString("0", CultureInfo.InvariantCulture);

    private static bool IsSupported(SchemaSnapshot schema, decimal documentTypeId)
    {
        var diskVolume = schema.FindTable("DISK_VOLUME");
        return schema.FindEntityTables(documentTypeId) is not null
            && diskVolume?.FindColumn("DISK_VOL_ID") is not null
            && diskVolume.FindColumn("DISK_VOL_PATH") is not null;
    }

    private static string Quote(string identifier) =>
        $"[{identifier.Replace("]", "]]", StringComparison.Ordinal)}]";

    private sealed record DocumentCandidate(
        decimal DocumentId,
        string? Offset,
        string? FileName,
        string? VolumePath
    );

    private sealed record EntityTables(TableInfo Document, TableInfo Index);

    private sealed class SchemaSnapshot(IReadOnlyList<TableInfo> tables)
    {
        public TableInfo? FindTable(string name) =>
            tables.FirstOrDefault(table =>
                string.Equals(table.Name, name, StringComparison.OrdinalIgnoreCase)
            );

        public EntityTables? FindEntityTables(decimal id)
        {
            var suffix = FormatId(id);
            var docTable = tables.FirstOrDefault(table =>
                string.Equals(table.Name, $"DOC_T{suffix}", StringComparison.OrdinalIgnoreCase)
            );
            var indexTable = tables.FirstOrDefault(table =>
                docTable is not null
                && string.Equals(table.Schema, docTable.Schema, StringComparison.OrdinalIgnoreCase)
                && string.Equals(table.Name, $"DOC_I{suffix}", StringComparison.OrdinalIgnoreCase)
            );
            if (
                docTable is null
                || indexTable is null
                || docTable.FindColumn("DOC_ID") is null
                || docTable.FindColumn("VOL_ID") is null
                || docTable.FindColumn("OFFSET") is null
                || docTable.FindColumn("DOC_FILE") is null
                || indexTable.FindColumn("DOC_ID") is null
                || indexTable.FindColumn("crdate") is null
            )
            {
                return null;
            }

            return new EntityTables(docTable, indexTable);
        }
    }

    private sealed class TableInfo(string schema, string name, IReadOnlyList<string> columns)
    {
        public string Schema { get; } = schema;
        public string Name { get; } = name;

        public string? FindColumn(params string[] names) =>
            columns.FirstOrDefault(column =>
                names.Any(name => string.Equals(column, name, StringComparison.OrdinalIgnoreCase))
            );
    }
}
