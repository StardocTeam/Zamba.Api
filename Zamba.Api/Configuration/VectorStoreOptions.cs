namespace Zamba.Api.Configuration;

/// <summary>
/// Settings for the local SQLite-backed embedding/vector store used as the file's memory context.
/// </summary>
public class VectorStoreOptions
{
    public const string SectionName = "VectorStore";

    public string DatabasePath { get; set; } = "Data/vectorstore.db";
    public int ChunkSize { get; set; } = 1000;
    public int ChunkOverlap { get; set; } = 200;
    public int TopK { get; set; } = 5;
}
