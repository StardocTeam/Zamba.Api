using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;

namespace Zamba.Api.Controllers;

public sealed class WorkflowDashboardTaskQuery
{
    public decimal? WorkflowId { get; set; }
    public decimal? StepId { get; set; }
    public decimal? DocumentTypeId { get; set; }
    public decimal? AssigneeId { get; set; }
    public decimal[]? AssigneeIds { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string DateField { get; set; } = "checkin";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}

[ApiController]
[Route("api/workflow-dashboard")]
public sealed class WorkflowDashboardController(ZambaDbContext dbContext) : ControllerBase
{
    private const string WorkflowIdColumn = "work_id";
    private const string StepIdColumn = "step_Id";
    private const string DocumentTypeIdColumn = "DOC_TYPE_ID";
    private const string DocumentIdColumn = "Doc_ID";
    private const string IndexIdColumn = "INDEX_ID";

    [HttpGet("filters")]
    public async Task<ActionResult<object>> GetFilters(CancellationToken cancellationToken)
    {
        var connection = dbContext.Database.GetDbConnection();
        await EnsureOpenAsync(connection, cancellationToken);
        var schema = await DatabaseSchema.LoadAsync(connection, cancellationToken);
        if (!schema.TryGetTable("WFWorkflow", out var workflowTable)
            || !schema.TryGetTable("WFStep", out var stepTable)
            || !schema.TryGetTable("DOC_TYPE", out var documentTypeTable))
        {
            return Problem("Faltan tablas base para consultar el catálogo de workflows.", statusCode: 503);
        }

        var workflows = await ReadCatalogAsync(connection, workflowTable,
            new[] { WorkflowIdColumn, "Name" }, cancellationToken);
        var steps = await ReadCatalogAsync(connection, stepTable,
            new[] { StepIdColumn, WorkflowIdColumn, "Name" }, cancellationToken);
        var documentTypes = await ReadCatalogAsync(connection, documentTypeTable,
            new[] { DocumentTypeIdColumn, "DOC_TYPE_NAME" }, cancellationToken);
        var states = schema.TryGetTable("WFStepStates", out var stateTable)
            ? await ReadCatalogAsync(connection, stateTable,
                new[] { "Doc_State_ID", "Step_Id", "Name", "Description" }, cancellationToken)
            : Array.Empty<Dictionary<string, object?>>();
        var principals = schema.TryGetTable("z_user_or_group", out var principalTable)
            ? await ReadPrincipalsAsync(connection, principalTable, cancellationToken)
            : Array.Empty<Dictionary<string, object?>>();

        return Ok(new { workflows, steps, states, documentTypes, principals });
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<object>> GetTasks(
        [FromQuery] WorkflowDashboardTaskQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = dbContext.Database.GetDbConnection();
        await EnsureOpenAsync(connection, cancellationToken);
        var schema = await DatabaseSchema.LoadAsync(connection, cancellationToken);
        if (!schema.TryGetTable("WFDocument", out var taskTable))
        {
            return Problem("No se encontró la tabla WFDocument.", statusCode: 503);
        }

        var documentId = taskTable.FindColumn(DocumentIdColumn);
        var workflowColumn = taskTable.FindColumn(WorkflowIdColumn);
        var stepColumn = taskTable.FindColumn(StepIdColumn);
        var documentTypeColumn = taskTable.FindColumn(DocumentTypeIdColumn);
        var assigneeColumn = taskTable.FindColumn("User_Asigned");
        var checkInColumn = taskTable.FindColumn("CheckIn");
        if (documentId is null || workflowColumn is null || stepColumn is null || documentTypeColumn is null)
        {
            return Problem("WFDocument no contiene las columnas requeridas para monitorear tareas.", statusCode: 503);
        }

        query.Page = Math.Max(1, query.Page);
        query.PageSize = Math.Clamp(query.PageSize, 1, 100);
        var useCreatedDate = string.Equals(query.DateField, "created", StringComparison.OrdinalIgnoreCase);
        var dateColumn = useCreatedDate
            ? taskTable.FindColumn("Date_Asigned_By") ?? taskTable.FindColumn("LastUpdateDate") ?? checkInColumn
            : checkInColumn ?? taskTable.FindColumn("Date_Asigned_By");
        var conditions = new List<string>();
        var parameters = new List<(string Name, object? Value)>();
        AddFilter(query.WorkflowId, workflowColumn, "workflowId", conditions, parameters);
        AddFilter(query.StepId, stepColumn, "stepId", conditions, parameters);
        AddFilter(query.DocumentTypeId, documentTypeColumn, "documentTypeId", conditions, parameters);
        if (query.AssigneeIds is { Length: > 0 })
        {
            AddInFilter(query.AssigneeIds, assigneeColumn, conditions, parameters);
        }
        else
        {
            AddFilter(query.AssigneeId, assigneeColumn, "assigneeId", conditions, parameters);
        }
        if (query.From.HasValue && dateColumn is not null)
        {
            conditions.Add($"d.{Quote(dateColumn.Name)} >= @fromDate");
            parameters.Add(("fromDate", query.From.Value.Date));
        }
        if (query.To.HasValue && dateColumn is not null)
        {
            conditions.Add($"d.{Quote(dateColumn.Name)} < @toDate");
            parameters.Add(("toDate", query.To.Value.Date.AddDays(1)));
        }

        var sqlParts = new TaskSqlParts();
        sqlParts.Select.AddRange(new[]
        {
            SelectColumn(taskTable, "d", DocumentIdColumn, "documentId"),
            SelectColumn(taskTable, "d", "Task_ID", "taskId"),
            SelectColumn(taskTable, "d", "Name", "taskName"),
            SelectColumn(taskTable, "d", DocumentTypeIdColumn, "documentTypeId"),
            SelectColumn(taskTable, "d", WorkflowIdColumn, "workflowId"),
            SelectColumn(taskTable, "d", StepIdColumn, "stepId"),
            SelectColumn(taskTable, "d", "Do_State_ID", "stateId"),
            SelectColumn(taskTable, "d", "Task_State_ID", "taskStateId"),
            SelectColumn(taskTable, "d", "User_Asigned", "assigneeId"),
            SelectColumn(taskTable, "d", "User_Asigned_By", "assignedById"),
            SelectColumn(taskTable, "d", "Date_Asigned_By", "createdAt"),
            SelectColumn(taskTable, "d", "CheckIn", "checkIn"),
            SelectColumn(taskTable, "d", "ExpireDate", "expiresAt"),
            SelectColumn(taskTable, "d", "Comments", "comments"),
            SelectColumn(taskTable, "d", "Tag", "tag"),
        });
        AddCatalogJoin(schema, "WFWorkflow", "w", WorkflowIdColumn, workflowColumn, "workflowName", sqlParts);
        AddCatalogJoin(schema, "WFStep", "s", StepIdColumn, stepColumn, "stepName", sqlParts);
        AddStateJoin(schema, taskTable, sqlParts);
        AddDocumentTypeJoin(schema, taskTable, sqlParts);
        AddCreatorJoin(schema, taskTable, sqlParts);
        AddPrincipalJoins(schema, taskTable, sqlParts);

        var fromSql = $"FROM {Quote(taskTable.Schema)}.{Quote(taskTable.Name)} d {string.Join(" ", sqlParts.Joins)}";
        var whereSql = conditions.Count == 0 ? string.Empty : " WHERE " + string.Join(" AND ", conditions);
        var countSql = $"SELECT COUNT_BIG(1) {fromSql}{whereSql};";
        var count = Convert.ToInt64(await ScalarAsync(connection, countSql, parameters, cancellationToken));
        var expirationColumn = taskTable.FindColumn("ExpireDate");
        var overdueExpression = expirationColumn is null
            ? "0"
            : $"SUM(CASE WHEN d.{Quote(expirationColumn.Name)} < GETDATE() THEN 1 ELSE 0 END)";
        var summarySql = $"SELECT " +
            $"{overdueExpression} AS overdueCount, " +
            $"SUM(CASE WHEN {(assigneeColumn is null ? "1" : $"d.{Quote(assigneeColumn.Name)} IS NULL")} THEN 1 ELSE 0 END) AS unassignedCount " +
            $"{fromSql}{whereSql};";
        var summary = await ReadSingleAsync(connection, summarySql, parameters, cancellationToken);
        var pagedSql = $"SELECT {string.Join(", ", sqlParts.Select)} {fromSql}{whereSql} " +
            $"ORDER BY {(dateColumn is null ? $"d.{Quote(documentId.Name)}" : $"d.{Quote(dateColumn.Name)}")} DESC, d.{Quote(documentId.Name)} DESC " +
            "OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";
        var pageParameters = new List<(string Name, object? Value)>(parameters)
        {
            ("offset", (query.Page - 1) * query.PageSize),
            ("pageSize", query.PageSize)
        };
        var items = await ReadRowsAsync(connection, pagedSql, pageParameters, cancellationToken);
        return Ok(new
        {
            page = query.Page,
            pageSize = query.PageSize,
            total = count,
            summary = new
            {
                total = count,
                overdue = ToInt64(summary.GetValueOrDefault("overdueCount")),
                unassigned = ToInt64(summary.GetValueOrDefault("unassignedCount"))
            },
            items
        });
    }

    [HttpGet("tasks/{taskId}/detail")]
    public async Task<ActionResult<object>> GetTaskDetail(string taskId, CancellationToken cancellationToken)
    {
        if (!decimal.TryParse(taskId, NumberStyles.Number, CultureInfo.InvariantCulture, out var id))
        {
            return BadRequest("El identificador de tarea no es válido.");
        }

        var connection = dbContext.Database.GetDbConnection();
        await EnsureOpenAsync(connection, cancellationToken);
        var schema = await DatabaseSchema.LoadAsync(connection, cancellationToken);
        if (!schema.TryGetTable("WFDocument", out var taskTable)
            || taskTable.FindColumn(DocumentTypeIdColumn) is not { } documentTypeColumn
            || taskTable.FindColumn(DocumentIdColumn) is not { } documentIdColumn)
        {
            return Problem("No se encontró el esquema de tareas necesario para el detalle.", statusCode: 503);
        }

        var taskKey = taskTable.FindColumn("Task_ID") ?? documentIdColumn;
        var taskRow = await ReadSingleAsync(connection,
            $"SELECT {Quote(documentIdColumn.Name)} AS [documentId], {Quote(documentTypeColumn.Name)} AS [documentTypeId] " +
            $"FROM {Quote(taskTable.Schema)}.{Quote(taskTable.Name)} WHERE {Quote(taskKey.Name)} = @taskId;",
            new[] { ("taskId", (object?)id) }, cancellationToken);
        if (taskRow.Count == 0)
        {
            return NotFound();
        }

        var documentId = Convert.ToString(taskRow["documentId"], CultureInfo.InvariantCulture) ?? string.Empty;
        var documentTypeId = Convert.ToString(taskRow["documentTypeId"], CultureInfo.InvariantCulture) ?? string.Empty;
        var attributes = await ReadDocumentAttributesAsync(
            connection, schema, documentTypeId, documentId, cancellationToken);
        return Ok(new { documentId, documentTypeId, attributes });
    }

    private static async Task EnsureOpenAsync(DbConnection connection, CancellationToken cancellationToken)
    {
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }
    }

    private static void AddFilter(
        decimal? value,
        DatabaseColumn? column,
        string parameter,
        ICollection<string> conditions,
        ICollection<(string Name, object? Value)> parameters)
    {
        if (value.HasValue && column is not null)
        {
            conditions.Add($"d.{Quote(column.Name)} = @{parameter}");
            parameters.Add((parameter, value.Value));
        }
    }

    private static void AddInFilter(
        IReadOnlyCollection<decimal> values,
        DatabaseColumn? column,
        ICollection<string> conditions,
        ICollection<(string Name, object? Value)> parameters)
    {
        if (values.Count == 0 || column is null) return;

        var parameterNames = new List<string>(values.Count);
        var index = 0;
        foreach (var value in values)
        {
            var name = $"assigneeId{index++}";
            parameterNames.Add("@" + name);
            parameters.Add((name, value));
        }
        conditions.Add($"d.{Quote(column.Name)} IN ({string.Join(", ", parameterNames)})");
    }

    private static string SelectColumn(DatabaseTable table, string alias, string requested, string resultName)
    {
        var column = table.FindColumn(requested);
        return column is null ? $"NULL AS {Quote(resultName)}" : $"{alias}.{Quote(column.Name)} AS {Quote(resultName)}";
    }

    private static void AddCatalogJoin(
        DatabaseSchema schema,
        string tableName,
        string alias,
        string keyName,
        DatabaseColumn taskKey,
        string resultName,
        TaskSqlParts sqlParts)
    {
        if (!schema.TryGetTable(tableName, out var table)
            || table.FindColumn(keyName) is not { } key
            || table.FindColumn("Name") is not { } name)
        {
            return;
        }
        sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} {alias} ON {alias}.{Quote(key.Name)} = d.{Quote(taskKey.Name)}");
        sqlParts.Select.Add($"{alias}.{Quote(name.Name)} AS {Quote(resultName)}");
    }

    private static void AddStateJoin(DatabaseSchema schema, DatabaseTable taskTable, TaskSqlParts sqlParts)
    {
        if (!schema.TryGetTable("WFStepStates", out var table)
            || table.FindColumn("Doc_State_ID") is not { } key
            || taskTable.FindColumn("Do_State_ID") is not { } taskKey)
        {
            return;
        }
        sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} st ON st.{Quote(key.Name)} = d.{Quote(taskKey.Name)}");
        sqlParts.Select.Add(SelectColumn(table, "st", "Name", "stateName"));
        sqlParts.Select.Add(SelectColumn(table, "st", "Description", "stateDescription"));
    }

    private static void AddDocumentTypeJoin(DatabaseSchema schema, DatabaseTable taskTable, TaskSqlParts sqlParts)
    {
        if (!schema.TryGetTable("DOC_TYPE", out var table)
            || table.FindColumn(DocumentTypeIdColumn) is not { } key
            || taskTable.FindColumn(DocumentTypeIdColumn) is not { } taskKey)
        {
            return;
        }
        sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} dt ON dt.{Quote(key.Name)} = d.{Quote(taskKey.Name)}");
        sqlParts.Select.Add(SelectColumn(table, "dt", "DOC_TYPE_NAME", "documentTypeName"));
    }

    private static void AddCreatorJoin(DatabaseSchema schema, DatabaseTable taskTable, TaskSqlParts sqlParts)
    {
        if (taskTable.FindColumn(DocumentTypeIdColumn) is not { } typeKey || taskTable.FindColumn(DocumentIdColumn) is not { } documentKey)
        {
            return;
        }
        var creatorUnion = BuildCreatorUnion(schema);
        if (creatorUnion.Length == 0) return;
        sqlParts.Joins.Add($"LEFT JOIN ({creatorUnion}) creator ON creator.docTypeId = d.{Quote(typeKey.Name)} AND creator.docId = d.{Quote(documentKey.Name)}");
        sqlParts.Select.Add("creator.creatorId AS [creatorId]");
    }

    private static void AddPrincipalJoins(DatabaseSchema schema, DatabaseTable taskTable, TaskSqlParts sqlParts)
    {
        if (!schema.TryGetTable("z_user_or_group", out var table)
            || table.FindColumn("id", "USER_ID", "GROUP_ID", "USER_OR_GROUP_ID") is not { } key
            || table.FindColumn("name", "USER_NAME", "GROUP_NAME", "DESCRIPTION") is not { } name)
        {
            return;
        }
        if (taskTable.FindColumn("User_Asigned") is { } assignee)
        {
            sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} assignee ON assignee.{Quote(key.Name)} = d.{Quote(assignee.Name)}");
            sqlParts.Select.Add($"assignee.{Quote(name.Name)} AS [assigneeName]");
        }
        if (taskTable.FindColumn("User_Asigned_By") is { } assignedBy)
        {
            sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} assigner ON assigner.{Quote(key.Name)} = d.{Quote(assignedBy.Name)}");
            sqlParts.Select.Add($"assigner.{Quote(name.Name)} AS [assignedByName]");
        }
        if (BuildCreatorUnion(schema).Length > 0)
        {
            sqlParts.Joins.Add($"LEFT JOIN {Quote(table.Schema)}.{Quote(table.Name)} creatorPrincipal ON creatorPrincipal.{Quote(key.Name)} = creator.creatorId");
            sqlParts.Select.Add($"creatorPrincipal.{Quote(name.Name)} AS [creatorName]");
        }
    }

    private static string BuildCreatorUnion(DatabaseSchema schema)
    {
        var sources = schema.Tables
            .Where(table => Regex.IsMatch(table.Name, "^DOC_T[0-9]+$", RegexOptions.IgnoreCase))
            .Select(table => new
            {
                Table = table,
                TypeId = Regex.Match(table.Name, "[0-9]+$").Value,
                DocumentId = table.FindColumn("DOC_ID"),
                PlatterId = table.FindColumn("PLATTER_ID")
            })
            .Where(source => source.DocumentId is not null && source.PlatterId is not null)
            .ToArray();
        return string.Join(" UNION ALL ", sources.Select(source =>
            $"SELECT CAST({source.TypeId} AS decimal(18,0)) AS docTypeId, " +
            $"t.{Quote(source.DocumentId!.Name)} AS docId, t.{Quote(source.PlatterId!.Name)} AS creatorId " +
            $"FROM {Quote(source.Table.Schema)}.{Quote(source.Table.Name)} t"));
    }

    private static async Task<List<object>> ReadDocumentAttributesAsync(
        DbConnection connection,
        DatabaseSchema schema,
        string documentTypeId,
        string documentId,
        CancellationToken cancellationToken)
    {
        if (!Regex.IsMatch(documentTypeId, "^[0-9]+$", RegexOptions.CultureInvariant))
        {
            return [];
        }

        var indexRows = await ReadIndexDefinitionsAsync(connection, schema, documentTypeId, cancellationToken);
        if (indexRows.Count == 0)
        {
            return [];
        }

        var dataTable = schema.TryGetTable("DOC_I" + documentTypeId, out var dynamicTable)
            ? dynamicTable
            : null;
        var storedValues = await ReadDocumentValuesAsync(connection, dataTable, documentId, cancellationToken);

        var result = new List<object>(indexRows.Count);
        foreach (var row in indexRows)
        {
            var currentIndexId = Convert.ToString(row.GetValueOrDefault("indexId"), CultureInfo.InvariantCulture) ?? string.Empty;
            result.Add(await BuildAttributeAsync(connection, schema, dataTable, storedValues, row,
                currentIndexId, cancellationToken));
        }
        return result;
    }

    private static async Task<List<Dictionary<string, object?>>> ReadIndexDefinitionsAsync(
        DbConnection connection,
        DatabaseSchema schema,
        string documentTypeId,
        CancellationToken cancellationToken)
    {
        if (!schema.TryGetTable("INDEX_R_DOC_TYPE", out var relation)
            || !schema.TryGetTable("DOC_INDEX", out var indexTable)
            || relation.FindColumn(IndexIdColumn) is not { } relationIndexId
            || relation.FindColumn(DocumentTypeIdColumn) is not { } relationTypeId
            || indexTable.FindColumn(IndexIdColumn) is not { } indexId
            || indexTable.FindColumn("INDEX_NAME") is not { } indexName)
        {
            return [];
        }

        var relationOrder = relation.FindColumn("ORDEN");
        var indexSql = $"SELECT r.{Quote(relationIndexId.Name)} AS [indexId], i.{Quote(indexName.Name)} AS [name], " +
            $"{SelectOptional(indexTable, "i", "INDEX_TYPE")} AS [type], " +
            $"{SelectOptional(indexTable, "i", "DROPDOWN")} AS [dropdown], " +
            $"{SelectOptional(indexTable, "i", "DROPDOWNTYPE")} AS [dropdownType] " +
            $"FROM {Quote(relation.Schema)}.{Quote(relation.Name)} r " +
            $"INNER JOIN {Quote(indexTable.Schema)}.{Quote(indexTable.Name)} i ON i.{Quote(indexId.Name)} = r.{Quote(relationIndexId.Name)} " +
            $"WHERE r.{Quote(relationTypeId.Name)} = @documentTypeId " +
            (relationOrder is null ? $"ORDER BY r.{Quote(relationIndexId.Name)}" : $"ORDER BY r.{Quote(relationOrder.Name)}");
        return await ReadRowsAsync(connection, indexSql,
            new[] { ("documentTypeId", (object?)decimal.Parse(documentTypeId, CultureInfo.InvariantCulture)) }, cancellationToken);
    }

    private static string SelectOptional(DatabaseTable table, string alias, string columnName) =>
        table.FindColumn(columnName) is { } column ? $"{alias}.{Quote(column.Name)}" : "NULL";

    private static async Task<DocumentAttributeValues> ReadDocumentValuesAsync(
        DbConnection connection,
        DatabaseTable? dataTable,
        string documentId,
        CancellationToken cancellationToken)
    {
        var wideValues = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        var eavValues = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        if (dataTable?.FindColumn("DOC_ID") is not { } documentKey) return new(wideValues, eavValues);

        var dataRows = await ReadRowsAsync(connection,
            $"SELECT * FROM {Quote(dataTable.Schema)}.{Quote(dataTable.Name)} WHERE {Quote(documentKey.Name)} = @documentId;",
            new[] { ("documentId", (object?)decimal.Parse(documentId, CultureInfo.InvariantCulture)) }, cancellationToken);
        if (dataRows.Count == 0) return new(wideValues, eavValues);
        foreach (var pair in dataRows[0]) wideValues[pair.Key] = pair.Value;

        var eavIndexColumn = dataTable.FindColumn(IndexIdColumn, "DOC_INDEX", "INDEXID");
        var eavValueColumn = dataTable.FindColumn("VALUE", "DATA", "INDEX_VALUE", "VALOR");
        if (eavIndexColumn is null || eavValueColumn is null) return new(wideValues, eavValues);
        foreach (var row in dataRows)
        {
            var key = Convert.ToString(row.GetValueOrDefault(eavIndexColumn.Name), CultureInfo.InvariantCulture);
            if (key is not null) eavValues[key] = row.GetValueOrDefault(eavValueColumn.Name);
        }
        return new(wideValues, eavValues);
    }

    private static async Task<object> BuildAttributeAsync(
        DbConnection connection,
        DatabaseSchema schema,
        DatabaseTable? dataTable,
        DocumentAttributeValues storedValues,
        IReadOnlyDictionary<string, object?> definition,
        string indexId,
        CancellationToken cancellationToken)
    {
        var value = storedValues.EntityAttributeValues.GetValueOrDefault(indexId);
        if (value is null && dataTable is not null)
        {
            var attributeName = Convert.ToString(definition.GetValueOrDefault("name"), CultureInfo.InvariantCulture) ?? string.Empty;
            var valueColumn = dataTable.FindColumn(indexId, "I" + indexId, "INDEX_" + indexId, attributeName);
            if (valueColumn is not null) value = storedValues.WideValues.GetValueOrDefault(valueColumn.Name);
        }

        var isDropdown = ToInt64(definition.GetValueOrDefault("dropdown")) > 0
            || ToInt64(definition.GetValueOrDefault("dropdownType")) > 0;
        var choices = isDropdown
            ? await ReadIndexChoicesAsync(connection, schema, indexId, cancellationToken)
            : [];
        var matchingChoice = choices.FirstOrDefault(choice =>
            string.Equals(Convert.ToString(choice.GetValueOrDefault("code"), CultureInfo.InvariantCulture),
                Convert.ToString(value, CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase));
        var displayValue = matchingChoice?.GetValueOrDefault("description") ?? value;
        return new
        {
            indexId,
            name = definition.GetValueOrDefault("name"),
            type = definition.GetValueOrDefault("type"),
            value = FormatDatabaseValue(value),
            displayValue = FormatDatabaseValue(displayValue),
            choices
        };
    }

    private static object? FormatDatabaseValue(object? value) => value is decimal numeric
        ? numeric.ToString(CultureInfo.InvariantCulture)
        : value;

    private static async Task<List<Dictionary<string, object?>>> ReadIndexChoicesAsync(
        DbConnection connection,
        DatabaseSchema schema,
        string indexId,
        CancellationToken cancellationToken)
    {
        if (!Regex.IsMatch(indexId, "^[0-9]+$", RegexOptions.CultureInvariant)
            || !schema.TryGetTable("SLST_S" + indexId, out var listTable)
            || listTable.FindColumn("Codigo", "Code", "ID") is not { } code
            || listTable.FindColumn("Descripcion", "Description", "Name") is not { } description)
        {
            return [];
        }
        return await ReadRowsAsync(connection,
            $"SELECT {Quote(code.Name)} AS [code], {Quote(description.Name)} AS [description] FROM {Quote(listTable.Schema)}.{Quote(listTable.Name)} ORDER BY {Quote(code.Name)};",
            Array.Empty<(string, object?)>(), cancellationToken);
    }

    private static async Task<Dictionary<string, object?>[]> ReadCatalogAsync(
        DbConnection connection,
        DatabaseTable table,
        string[] desiredColumns,
        CancellationToken cancellationToken)
    {
        var key = table.FindColumn(desiredColumns[0]);
        var label = table.FindColumn(desiredColumns[^1]);
        if (key is null || label is null)
        {
            return Array.Empty<Dictionary<string, object?>>();
        }
        var columns = desiredColumns
            .Select(name => table.FindColumn(name))
            .Where(column => column is not null)
            .DistinctBy(column => column!.Name, StringComparer.OrdinalIgnoreCase)
            .Select(column => Quote(column!.Name));
        return (await ReadRowsAsync(connection,
            $"SELECT {string.Join(", ", columns)} FROM {Quote(table.Schema)}.{Quote(table.Name)} ORDER BY {Quote(key.Name)};",
            Array.Empty<(string, object?)>(), cancellationToken)).ToArray();
    }

    private static async Task<Dictionary<string, object?>[]> ReadPrincipalsAsync(
        DbConnection connection,
        DatabaseTable table,
        CancellationToken cancellationToken)
    {
        var id = table.FindColumn("id", "USER_ID", "GROUP_ID", "USER_OR_GROUP_ID");
        var name = table.FindColumn("name", "USER_NAME", "GROUP_NAME", "DESCRIPTION");
        if (id is null || name is null)
        {
            return Array.Empty<Dictionary<string, object?>>();
        }
        var type = table.FindColumn("TYPE", "USER_OR_GROUP", "IS_GROUP", "GROUP_FLAG");
        var typeSelect = type is null ? "NULL AS [principalType]" : $"{Quote(type.Name)} AS [principalType]";
        return (await ReadRowsAsync(connection,
            $"SELECT {Quote(id.Name)} AS [id], {Quote(name.Name)} AS [name], {typeSelect} FROM {Quote(table.Schema)}.{Quote(table.Name)} ORDER BY {Quote(name.Name)};",
            Array.Empty<(string, object?)>(), cancellationToken)).ToArray();
    }

    private static async Task<List<Dictionary<string, object?>>> ReadRowsAsync(
        DbConnection connection,
        string sql,
        IEnumerable<(string Name, object? Value)> parameters,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var rows = new List<Dictionary<string, object?>>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (var index = 0; index < reader.FieldCount; index++)
            {
                var value = await reader.IsDBNullAsync(index, cancellationToken) ? null : reader.GetValue(index);
                object? normalizedValue = value;
                if (value is DBNull) normalizedValue = null;
                else if (value is decimal numeric) normalizedValue = numeric.ToString(CultureInfo.InvariantCulture);
                row[reader.GetName(index)] = normalizedValue;
            }
            rows.Add(row);
        }
        return rows;
    }

    private static async Task<Dictionary<string, object?>> ReadSingleAsync(
        DbConnection connection,
        string sql,
        IEnumerable<(string Name, object? Value)> parameters,
        CancellationToken cancellationToken) =>
        (await ReadRowsAsync(connection, sql, parameters, cancellationToken)).FirstOrDefault()
        ?? new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

    private static async Task<object?> ScalarAsync(
        DbConnection connection,
        string sql,
        IEnumerable<(string Name, object? Value)> parameters,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters);
        return await command.ExecuteScalarAsync(cancellationToken);
    }

    private static void AddParameters(DbCommand command, IEnumerable<(string Name, object? Value)> parameters)
    {
        foreach (var (name, value) in parameters)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }
    }

    private static string Quote(string identifier) => $"[{identifier.Replace("]", "]]", StringComparison.Ordinal)}]";

    private static long ToInt64(object? value) => value is null or DBNull ? 0 : Convert.ToInt64(value);

    private sealed class TaskSqlParts
    {
        public List<string> Select { get; } = [];
        public List<string> Joins { get; } = [];
    }

    private sealed record DocumentAttributeValues(
        Dictionary<string, object?> WideValues,
        Dictionary<string, object?> EntityAttributeValues);

    private sealed class DatabaseSchema(List<DatabaseTable> tables)
    {
        public IReadOnlyList<DatabaseTable> Tables { get; } = tables;

        public bool TryGetTable(string name, out DatabaseTable table)
        {
            table = Tables.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))!;
            return table is not null;
        }

        public static async Task<DatabaseSchema> LoadAsync(DbConnection connection, CancellationToken cancellationToken)
        {
            var rows = await ReadRowsAsync(connection,
                "SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS ORDER BY TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION;",
                Array.Empty<(string, object?)>(), cancellationToken);
            var tables = rows
                .GroupBy(row => ($"{row["TABLE_SCHEMA"]}", $"{row["TABLE_NAME"]}"))
                .Select(group => new DatabaseTable(group.Key.Item1, group.Key.Item2,
                    group.Select(row => $"{row["COLUMN_NAME"]}").ToArray()))
                .ToList();
            return new DatabaseSchema(tables);
        }
    }

    private sealed class DatabaseTable(string schema, string name, string[] columns)
    {
        public string Schema { get; } = schema;
        public string Name { get; } = name;
        public IReadOnlyList<DatabaseColumn> Columns { get; } = columns.Select(column => new DatabaseColumn(column)).ToArray();
        public DatabaseColumn? FindColumn(params string[] candidates) =>
            Columns.FirstOrDefault(column => candidates.Any(candidate =>
                string.Equals(column.Name, candidate, StringComparison.OrdinalIgnoreCase)));
    }

    private sealed record DatabaseColumn(string Name);
}