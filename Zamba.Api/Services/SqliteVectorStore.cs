using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;

namespace Zamba.Api.Services;

public interface IVectorStore
{
    Task<bool> DocumentExistsAsync(string documentId, CancellationToken cancellationToken);

    Task SaveChunksAsync(string documentId, string fileName, IReadOnlyList<DocumentChunk> chunks, CancellationToken cancellationToken);

    Task<IReadOnlyList<DocumentChunk>> FindMostRelevantChunksAsync(
        string documentId, float[] queryEmbedding, int topK, CancellationToken cancellationToken);
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
        string documentId, float[] queryEmbedding, int topK, CancellationToken cancellationToken)
    {
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

        return scored
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .Select(x => x.Chunk)
            .ToList();
    }

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
