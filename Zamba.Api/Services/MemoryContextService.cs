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
    IDocumentSkillProvider documentSkillProvider,
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
            isIndexed = await vectorStore.DocumentExistsAsync(documentId, cancellationToken)
                && await vectorStore.HasChunksAsync(documentId, cancellationToken);

            if (!isIndexed && await textExtractor.ExtractTextAsync(fileBytes, fileName, cancellationToken) is { } text)
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

            if (!isIndexed)
            {
                throw new ArgumentException("No text could be extracted from the document. OCR processing is required for this file.");
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
            isIndexed = await vectorStore.HasChunksAsync(documentId, cancellationToken);
            if (!isIndexed)
            {
                throw new ArgumentException($"Document '{documentId}' has no indexed text. Re-upload it after enabling text extraction or OCR.");
            }
        }

        stats.DocumentAlreadyIndexed = isIndexed;

        var retrievalStopwatch = Stopwatch.StartNew();
        var relevantChunks = isIndexed
            ? await RetrieveRelevantChunksAsync(documentId, request.Prompt, cancellationToken)
            : [];
        retrievalStopwatch.Stop();
        stats.RetrievalDurationMs = retrievalStopwatch.ElapsedMilliseconds;

        var promptWithContext = BuildPromptWithContext(
            fileName,
            relevantChunks,
            documentSkillProvider.GetInstructions(request.DocumentTypeCode),
            request.Prompt);

        var chatStopwatch = Stopwatch.StartNew();
        var completion = await chatClient.GetChatCompletionAsync(promptWithContext, cancellationToken);
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
            SessionId = completion.SessionId,
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
            documentId, queryEmbedding, prompt, _vectorStoreOptions.TopK, cancellationToken);
    }

    private static string BuildPromptWithContext(
        string fileName,
        IReadOnlyList<DocumentChunk> relevantChunks,
        string? documentInstructions,
        string userPrompt)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Follow the document-type instructions and answer using only the relevant excerpts below. Do not invent document facts.");
        builder.AppendLine($"Document: {fileName}");
        builder.AppendLine();

        if (!string.IsNullOrWhiteSpace(documentInstructions))
        {
            builder.AppendLine("Document-type instructions:");
            builder.AppendLine(documentInstructions);
            builder.AppendLine();
        }

        if (relevantChunks.Count == 0)
        {
            builder.AppendLine("No text excerpts were retrieved for this document.");
        }
        else
        {
            foreach (var chunk in relevantChunks.OrderBy(c => c.Index))
            {
                builder.AppendLine($"--- Excerpt {chunk.Index} ---");
                builder.AppendLine(chunk.Content);
            }

            builder.AppendLine();
        }

        builder.AppendLine("User request:");
        builder.AppendLine(userPrompt);
        return builder.ToString();
    }

    private static string ComputeDocumentId(byte[] fileBytes)
    {
        var hash = SHA256.HashData(fileBytes);
        return Convert.ToHexString(hash);
    }
}
