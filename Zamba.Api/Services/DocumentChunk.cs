namespace Zamba.Api.Services;

/// <summary>A single piece of a document along with its embedding vector.</summary>
public record DocumentChunk(int Index, string Content, float[] Embedding);
