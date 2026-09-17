using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface IMemoryContextService
{
    Task<CopilotPromptResponse> AskAsync(CopilotPromptRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Builds a "memory context" from an uploaded file (chunking + embeddings, cached in a local vector
/// store) and uses it to ground a prompt sent to the GitHub Copilot chat API.
/// </summary>
public class MemoryContextService(
    IFileTextExtractor textExtractor,
    ITextChunker textChunker,
    IEmbeddingClient embeddingClient,
    IVectorStore vectorStore,
    ICopilotChatClient chatClient,
    IOptions<VectorStoreOptions> vectorStoreOptions,
    IOptions<FileProcessingOptions> fileProcessingOptions,
    IOptions<GitHubCopilotOptions> copilotOptions) : IMemoryContextService
{
    private readonly VectorStoreOptions _vectorStoreOptions = vectorStoreOptions.Value;
    private readonly FileProcessingOptions _fileProcessingOptions = fileProcessingOptions.Value;
    private readonly GitHubCopilotOptions _copilotOptions = copilotOptions.Value;

    public async Task<CopilotPromptResponse> AskAsync(CopilotPromptRequest request, CancellationToken cancellationToken)
    {
        var totalStopwatch = Stopwatch.StartNew();
        var stats = new PromptStats { EmbeddingProvider = _copilotOptions.EmbeddingProvider };

        string documentId;
        string fileName;
        bool isIndexed;

        if (!string.IsNullOrWhiteSpace(request.FileBase64))
        {
            byte[] fileBytes;
            try
            {
                fileBytes = Convert.FromBase64String(request.FileBase64);
            }
            catch (FormatException)
            {
                throw new ArgumentException("FileBase64 is not a valid base64 string.");
            }

            if (fileBytes.LongLength > _fileProcessingOptions.MaxFileSizeBytes)
            {
                throw new ArgumentException(
                    $"File exceeds the maximum allowed size of {_fileProcessingOptions.MaxFileSizeBytes} bytes.");
            }

            documentId = ComputeDocumentId(fileBytes);
            fileName = request.FileName!;
            isIndexed = await vectorStore.DocumentExistsAsync(documentId, cancellationToken);

            if (!isIndexed && textExtractor.TryExtractText(fileBytes, fileName, out var text))
            {
                var chunks = textChunker.Chunk(text, _vectorStoreOptions.ChunkSize, _vectorStoreOptions.ChunkOverlap);
                var embeddedChunks = new List<DocumentChunk>(chunks.Count);

                var embeddingStopwatch = Stopwatch.StartNew();
                for (var i = 0; i < chunks.Count; i++)
                {
                    var embedding = await embeddingClient.GetEmbeddingAsync(chunks[i], cancellationToken);
                    embeddedChunks.Add(new DocumentChunk(i, chunks[i], embedding));
                }
                embeddingStopwatch.Stop();
                stats.EmbeddingDurationMs = embeddingStopwatch.ElapsedMilliseconds;
                stats.ChunksIndexed = embeddedChunks.Count;

                if (embeddedChunks.Count > 0)
                {
                    await vectorStore.SaveChunksAsync(documentId, fileName, embeddedChunks, cancellationToken);
                    isIndexed = true;
                }
            }
        }
        else
        {
            documentId = request.DocumentId!;
            isIndexed = await vectorStore.DocumentExistsAsync(documentId, cancellationToken);
            if (!isIndexed)
            {
                throw new ArgumentException($"No document found for DocumentId '{documentId}'. Send FileBase64/FileName to index it first.");
            }

            fileName = await vectorStore.GetFileNameAsync(documentId, cancellationToken) ?? "unknown";
        }

        stats.DocumentAlreadyIndexed = isIndexed;

        var retrievalStopwatch = Stopwatch.StartNew();
        var relevantChunks = isIndexed
            ? await RetrieveRelevantChunksAsync(documentId, request.Prompt, cancellationToken)
            : [];
        retrievalStopwatch.Stop();
        stats.RetrievalDurationMs = retrievalStopwatch.ElapsedMilliseconds;

        var systemContext = BuildSystemContext(fileName, relevantChunks);

        var chatStopwatch = Stopwatch.StartNew();
        var completion = await chatClient.GetChatCompletionAsync(systemContext, request.Prompt, cancellationToken);
        chatStopwatch.Stop();
        stats.ChatDurationMs = chatStopwatch.ElapsedMilliseconds;

        if (completion.Usage is { } usage)
        {
            stats.ChatModel = usage.Model;
            stats.InputTokens = usage.InputTokens;
            stats.OutputTokens = usage.OutputTokens;
            stats.CacheReadTokens = usage.CacheReadTokens;
            stats.CacheWriteTokens = usage.CacheWriteTokens;
            stats.EstimatedCost = usage.Cost;
        }

        totalStopwatch.Stop();
        stats.TotalDurationMs = totalStopwatch.ElapsedMilliseconds;

        return new CopilotPromptResponse
        {
            Response = completion.Response,
            DocumentId = documentId,
            ChunksUsed = relevantChunks.Count,
            Stats = stats
        };
    }

    private async Task<IReadOnlyList<DocumentChunk>> RetrieveRelevantChunksAsync(
        string documentId, string prompt, CancellationToken cancellationToken)
    {
        var queryEmbedding = await embeddingClient.GetEmbeddingAsync(prompt, cancellationToken);
        return await vectorStore.FindMostRelevantChunksAsync(
            documentId, queryEmbedding, _vectorStoreOptions.TopK, cancellationToken);
    }

    private static string BuildSystemContext(string fileName, IReadOnlyList<DocumentChunk> relevantChunks)
    {
        if (relevantChunks.Count == 0)
        {
            return $"The user attached a file named '{fileName}' but no text content could be extracted from it " +
                   "(it may be binary or empty). Answer the user's prompt as best you can without file context.";
        }

        var builder = new StringBuilder();
        builder.AppendLine($"The following are the most relevant excerpts from the file '{fileName}' for the user's prompt.");
        builder.AppendLine("Use them as context to answer accurately; do not invent information that is not present.");
        builder.AppendLine();

        foreach (var chunk in relevantChunks.OrderBy(c => c.Index))
        {
            builder.AppendLine($"--- Excerpt {chunk.Index} ---");
            builder.AppendLine(chunk.Content);
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static string ComputeDocumentId(byte[] fileBytes)
    {
        var hash = SHA256.HashData(fileBytes);
        return Convert.ToHexString(hash);
    }
}
