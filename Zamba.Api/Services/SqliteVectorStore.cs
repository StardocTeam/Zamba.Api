using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using Zamba.Api.Configuration;

namespace Zamba.Api.Services;

public interface IVectorStore
{
    Task<bool> DocumentExistsAsync(string documentId, CancellationToken cancellationToken);

    Task<bool> HasChunksAsync(string documentId, CancellationToken cancellationToken);

    Task<string?> GetFileNameAsync(string documentId, CancellationToken cancellationToken);

    Task<byte[]?> GetFileBytesAsync(string documentId, CancellationToken cancellationToken);

    Task SaveFileAsync(string documentId, string fileName, byte[] fileBytes, CancellationToken cancellationToken);

    Task SaveChunksAsync(string documentId, string fileName, IReadOnlyList<DocumentChunk> chunks, CancellationToken cancellationToken);

    Task<IReadOnlyList<DocumentChunk>> FindMostRelevantChunksAsync(
        string documentId, float[] queryEmbedding, string queryText, int topK, CancellationToken cancellationToken);
}

/// <summary>
/// Minimal local vector database backed by SQLite. Stores document chunks and their embeddings so
/// repeated prompts against the same file reuse previously computed embeddings, and lets the prompt
/// be answered using only the most relevant chunks instead of the whole file.
/// </summary>
public class SqliteVectorStore : IVectorStore
{
    private readonly string _connectionString;

    public SqliteVectorStore(IOptions<VectorStoreOptions> options)
    {
        var databasePath = options.Value.DatabasePath;
        var directory = Path.GetDirectoryName(databasePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        _connectionString = new SqliteConnectionStringBuilder { DataSource = databasePath }.ToString();
        EnsureSchema();
    }

    private void EnsureSchema()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Documents (
                DocumentId TEXT PRIMARY KEY,
                FileName TEXT NOT NULL,
                CreatedAtUtc TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Chunks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DocumentId TEXT NOT NULL,
                ChunkIndex INTEGER NOT NULL,
                Content TEXT NOT NULL,
                Embedding BLOB NOT NULL,
                FOREIGN KEY (DocumentId) REFERENCES Documents (DocumentId)
            );

            CREATE TABLE IF NOT EXISTS DocumentFiles (
                DocumentId TEXT PRIMARY KEY,
                FileData BLOB NOT NULL,
                FOREIGN KEY (DocumentId) REFERENCES Documents (DocumentId)
            );

            CREATE INDEX IF NOT EXISTS IX_Chunks_DocumentId ON Chunks (DocumentId);
            """;
        command.ExecuteNonQuery();
    }

    public async Task<bool> DocumentExistsAsync(string documentId, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM Documents WHERE DocumentId = $documentId";
        command.Parameters.AddWithValue("$documentId", documentId);

        var count = (long)(await command.ExecuteScalarAsync(cancellationToken) ?? 0L);
        return count > 0;
    }

    public async Task<bool> HasChunksAsync(string documentId, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT EXISTS(SELECT 1 FROM Chunks WHERE DocumentId = $documentId)";
        command.Parameters.AddWithValue("$documentId", documentId);

        return Convert.ToInt64(await command.ExecuteScalarAsync(cancellationToken)) != 0;
    }

    public async Task<string?> GetFileNameAsync(string documentId, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT FileName FROM Documents WHERE DocumentId = $documentId";
        command.Parameters.AddWithValue("$documentId", documentId);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result as string;
    }

    public async Task<byte[]?> GetFileBytesAsync(string documentId, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT FileData FROM DocumentFiles WHERE DocumentId = $documentId";
        command.Parameters.AddWithValue("$documentId", documentId);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result is byte[] bytes ? bytes : null;
    }

    public async Task SaveFileAsync(
        string documentId, string fileName, byte[] fileBytes, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        using (var documentCommand = connection.CreateCommand())
        {
            documentCommand.Transaction = transaction;
            documentCommand.CommandText =
                "INSERT OR REPLACE INTO Documents (DocumentId, FileName, CreatedAtUtc) " +
                "VALUES ($documentId, $fileName, $createdAtUtc)";
            documentCommand.Parameters.AddWithValue("$documentId", documentId);
            documentCommand.Parameters.AddWithValue("$fileName", fileName);
            documentCommand.Parameters.AddWithValue("$createdAtUtc", DateTime.UtcNow.ToString("O"));
            await documentCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        using (var fileCommand = connection.CreateCommand())
        {
            fileCommand.Transaction = transaction;
            fileCommand.CommandText =
                "INSERT OR REPLACE INTO DocumentFiles (DocumentId, FileData) VALUES ($documentId, $fileData)";
            fileCommand.Parameters.AddWithValue("$documentId", documentId);
            fileCommand.Parameters.AddWithValue("$fileData", fileBytes);
            await fileCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task SaveChunksAsync(
        string documentId, string fileName, IReadOnlyList<DocumentChunk> chunks, CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        using (var documentCommand = connection.CreateCommand())
        {
            documentCommand.Transaction = transaction;
            documentCommand.CommandText =
                """
                INSERT OR REPLACE INTO Documents (DocumentId, FileName, CreatedAtUtc)
                VALUES ($documentId, $fileName, $createdAtUtc)
                """;
            documentCommand.Parameters.AddWithValue("$documentId", documentId);
            documentCommand.Parameters.AddWithValue("$fileName", fileName);
            documentCommand.Parameters.AddWithValue("$createdAtUtc", DateTime.UtcNow.ToString("O"));
            await documentCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var chunk in chunks)
        {
            using var chunkCommand = connection.CreateCommand();
            chunkCommand.Transaction = transaction;
            chunkCommand.CommandText =
                """
                INSERT INTO Chunks (DocumentId, ChunkIndex, Content, Embedding)
                VALUES ($documentId, $chunkIndex, $content, $embedding)
                """;
            chunkCommand.Parameters.AddWithValue("$documentId", documentId);
            chunkCommand.Parameters.AddWithValue("$chunkIndex", chunk.Index);
            chunkCommand.Parameters.AddWithValue("$content", chunk.Content);
            chunkCommand.Parameters.AddWithValue("$embedding", EmbeddingToBytes(chunk.Embedding));
            await chunkCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DocumentChunk>> FindMostRelevantChunksAsync(
        string documentId, float[] queryEmbedding, string queryText, int topK, CancellationToken cancellationToken)
    {
        if (topK <= 0)
        {
            return [];
        }

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText =
            "SELECT ChunkIndex, Content, Embedding FROM Chunks WHERE DocumentId = $documentId";
        command.Parameters.AddWithValue("$documentId", documentId);

        var scored = new List<(DocumentChunk Chunk, double Score)>();
        using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var index = reader.GetInt32(0);
                var content = reader.GetString(1);
                var embeddingBytes = (byte[])reader["Embedding"];
                var embedding = BytesToEmbedding(embeddingBytes);

                var score = CosineSimilarity(queryEmbedding, embedding);
                scored.Add((new DocumentChunk(index, content, embedding), score));
            }
        }

        var lexicalScores = CalculateBm25Scores(scored, queryText);
        var lexicalRanks = lexicalScores
            .Where(item => item.Value > 0)
            .OrderByDescending(item => item.Value)
            .ThenBy(item => item.Key)
            .Select((item, rank) => (item.Key, rank))
            .ToDictionary(item => item.Key, item => item.rank);
        var vectorRanks = scored
            .OrderByDescending(item => item.Score)
            .ThenBy(item => item.Chunk.Index)
            .Select((item, rank) => (item.Chunk.Index, rank))
            .ToDictionary(item => item.Index, item => item.rank);

        return scored
            .Select(item => (
                item.Chunk,
                Score: ReciprocalRankFusionScore(item.Chunk.Index, vectorRanks, lexicalRanks, item.Score)))
            .OrderByDescending(item => item.Score)
            .ThenByDescending(item => item.Chunk.Index)
            .Take(topK)
            .Select(item => item.Chunk)
            .ToList();
    }

    private static Dictionary<int, double> CalculateBm25Scores(
        IReadOnlyList<(DocumentChunk Chunk, double Score)> chunks, string queryText)
    {
        var queryTerms = Tokenize(queryText)
            .GroupBy(term => term)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);
        if (queryTerms.Count == 0 || chunks.Count == 0)
        {
            return new Dictionary<int, double>();
        }

        var documentTerms = chunks
            .Select(item => Tokenize(item.Chunk.Content)
                .GroupBy(term => term)
                .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal))
            .ToArray();
        var documentFrequencies = queryTerms.Keys.ToDictionary(
            term => term,
            term => documentTerms.Count(terms => terms.ContainsKey(term)),
            StringComparer.Ordinal);
        var documentLengths = documentTerms.Select(terms => terms.Values.Sum()).ToArray();
        var averageDocumentLength = Math.Max(1, documentLengths.Average());
        var scores = new Dictionary<int, double>(chunks.Count);

        for (var documentIndex = 0; documentIndex < chunks.Count; documentIndex++)
        {
            var score = 0d;
            foreach (var queryTerm in queryTerms)
            {
                var termFrequency = documentTerms[documentIndex].GetValueOrDefault(queryTerm.Key);
                if (termFrequency == 0)
                {
                    continue;
                }

                var documentFrequency = documentFrequencies[queryTerm.Key];
                var inverseDocumentFrequency = Math.Log(
                    1 + (chunks.Count - documentFrequency + 0.5) / (documentFrequency + 0.5));
                var lengthNormalization = 1 - 0.75 + 0.75 * documentLengths[documentIndex] / averageDocumentLength;
                var termScore = inverseDocumentFrequency *
                    (termFrequency * 2.2) / (termFrequency + 1.2 * lengthNormalization);
                score += termScore * (1 + Math.Log(queryTerm.Value));
            }

            scores[chunks[documentIndex].Chunk.Index] = score;
        }

        return scores;
    }

    private static double ReciprocalRankFusionScore(
        int chunkIndex,
        IReadOnlyDictionary<int, int> vectorRanks,
        IReadOnlyDictionary<int, int> lexicalRanks,
        double vectorScore)
    {
        const double rankConstant = 60;
        var score = 0.65 / (rankConstant + vectorRanks[chunkIndex]);
        if (lexicalRanks.TryGetValue(chunkIndex, out var lexicalRank))
        {
            score += 0.35 / (rankConstant + lexicalRank);
        }

        return score + Math.Max(0, vectorScore) * 0.000001;
    }

    private static string[] Tokenize(string text) =>
        Regex.Matches(text.ToLowerInvariant(), @"[\p{L}\p{N}]{2,}")
            .Select(match => match.Value)
            .ToArray();

    private static byte[] EmbeddingToBytes(float[] embedding)
    {
        var bytes = new byte[embedding.Length * sizeof(float)];
        Buffer.BlockCopy(embedding, 0, bytes, 0, bytes.Length);
        return bytes;
    }

    private static float[] BytesToEmbedding(byte[] bytes)
    {
        var embedding = new float[bytes.Length / sizeof(float)];
        Buffer.BlockCopy(bytes, 0, embedding, 0, bytes.Length);
        return embedding;
    }

    private static double CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length != b.Length || a.Length == 0)
        {
            return 0;
        }

        double dot = 0, magA = 0, magB = 0;
        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }

        if (magA == 0 || magB == 0)
        {
            return 0;
        }

        return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
    }
}
