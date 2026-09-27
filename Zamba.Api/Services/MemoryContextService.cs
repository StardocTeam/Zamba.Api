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
    ICopilotTelemetryService telemetry,
    IInvoiceQrCodeExtractor invoiceQrCodeExtractor,
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
        var run = await telemetry.StartRunAsync(request, cancellationToken);
        string? invoiceQrContext = null;

        try
        {
            string documentId;
            string fileName;
            bool isIndexed;

            if (!string.IsNullOrWhiteSpace(request.FileBase64))
            {
                var fileBytes = await TrackStepAsync(run.Id, "document_decode", () => Task.FromResult(DecodeFile(request.FileBase64)), cancellationToken);
                await TrackStepAsync(
                    run.Id,
                    "file_size_validation",
                    () => Task.FromResult(ValidateFileSize(fileBytes)),
                    cancellationToken,
                    _ => $"File bytes: {fileBytes.LongLength}");

                documentId = ComputeDocumentId(fileBytes);
                fileName = request.FileName!;
                if (request.DocumentTypeCode == 26)
                {
                    var qrCodes = await TrackStepAsync(
                        run.Id,
                        "invoice_qr_extraction",
                        () => invoiceQrCodeExtractor.ExtractAsync(fileBytes, fileName, cancellationToken),
                        cancellationToken,
                        result => $"QR codes found: {result.Count}");
                    invoiceQrContext = System.Text.Json.JsonSerializer.Serialize(qrCodes);
                }

                isIndexed = await TrackStepAsync(
                    run.Id,
                    "document_cache_check",
                    async () => await vectorStore.DocumentExistsAsync(documentId, cancellationToken)
                        && await vectorStore.HasChunksAsync(documentId, cancellationToken),
                    cancellationToken);
                stats.DocumentAlreadyIndexed = isIndexed;

                if (!isIndexed)
                {
                    var text = await TrackStepAsync(
                        run.Id,
                        "text_extraction_ocr",
                        () => textExtractor.ExtractTextAsync(fileBytes, fileName, cancellationToken),
                        cancellationToken,
                        extracted => extracted is null ? "No text extracted" : $"Characters extracted: {extracted.Length}");
                    if (text is not null)
                    {
                        var chunks = await TrackStepAsync(
                            run.Id,
                            "chunking",
                            () => Task.FromResult(textChunker.Chunk(text, _vectorStoreOptions.ChunkSize, _vectorStoreOptions.ChunkOverlap)),
                            cancellationToken,
                            result => $"Chunks created: {result.Count}");
                        var embeddingStopwatch = Stopwatch.StartNew();
                        var embeddedChunks = await TrackStepAsync(
                            run.Id,
                            "chunk_embedding",
                            async () =>
                            {
                                var result = new List<DocumentChunk>(chunks.Count);
                                for (var i = 0; i < chunks.Count; i++)
                                {
                                    var embedding = await embeddingClient.GetEmbeddingAsync(chunks[i], cancellationToken);
                                    result.Add(new DocumentChunk(i, chunks[i], embedding));
                                }

                                return result;
                            },
                            cancellationToken,
                            result => $"Chunks embedded: {result.Count}");
                        embeddingStopwatch.Stop();
                        stats.EmbeddingDurationMs = embeddingStopwatch.ElapsedMilliseconds;
                        stats.ChunksIndexed = embeddedChunks.Count;

                        if (embeddedChunks.Count > 0)
                        {
                            await TrackStepAsync(
                                run.Id,
                                "index_persist",
                                async () =>
                                {
                                    await vectorStore.SaveChunksAsync(documentId, fileName, embeddedChunks, cancellationToken);
                                    return true;
                                },
                                cancellationToken,
                                _ => $"Chunks saved: {embeddedChunks.Count}");
                            isIndexed = true;
                        }
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
                isIndexed = await TrackStepAsync(
                    run.Id,
                    "document_cache_check",
                    () => vectorStore.DocumentExistsAsync(documentId, cancellationToken),
                    cancellationToken);
                if (!isIndexed)
                {
                    throw new ArgumentException($"No document found for DocumentId '{documentId}'. Send FileBase64/FileName to index it first.");
                }
                stats.DocumentAlreadyIndexed = true;

                fileName = await TrackStepAsync(
                    run.Id,
                    "document_metadata",
                    async () => await vectorStore.GetFileNameAsync(documentId, cancellationToken) ?? "unknown",
                    cancellationToken);
                isIndexed = await TrackStepAsync(
                    run.Id,
                    "document_chunks_check",
                    () => vectorStore.HasChunksAsync(documentId, cancellationToken),
                    cancellationToken);
                if (!isIndexed)
                {
                    throw new ArgumentException($"Document '{documentId}' has no indexed text. Re-upload it after enabling text extraction or OCR.");
                }
            }

            var retrievalStopwatch = Stopwatch.StartNew();
            var relevantChunks = await TrackStepAsync(
                run.Id,
                "retrieval_rerank",
                () => RetrieveRelevantChunksAsync(documentId, request.Prompt, cancellationToken),
                cancellationToken,
                result => $"Chunks selected: {result.Count}");
            retrievalStopwatch.Stop();
            stats.RetrievalDurationMs = retrievalStopwatch.ElapsedMilliseconds;

            var promptWithContext = await TrackStepAsync(
                run.Id,
                "prompt_build",
                () => Task.FromResult(BuildPromptWithContext(
                    fileName,
                    relevantChunks,
                    documentSkillProvider.GetInstructions(request.DocumentTypeCode),
                    invoiceQrContext,
                    request.Prompt)),
                cancellationToken,
                result => $"Prompt characters: {result.Length}");

            var chatStopwatch = Stopwatch.StartNew();
            var completion = await TrackStepAsync(
                run.Id,
                "copilot_completion",
                () => chatClient.GetChatCompletionAsync(promptWithContext, cancellationToken),
                cancellationToken,
                result => $"Response characters: {result.Response.Length}");
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

            var response = new CopilotPromptResponse
            {
                RunId = run.Id,
                Response = completion.Response,
                SessionId = completion.SessionId,
                DocumentId = documentId,
                ChunksUsed = relevantChunks.Count,
                Stats = stats
            };
            await telemetry.CompleteRunAsync(run, response, cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            totalStopwatch.Stop();
            try
            {
                await telemetry.FailRunAsync(run, ex, totalStopwatch.ElapsedMilliseconds, CancellationToken.None);
            }
            catch
            {
                // Preserve the original pipeline failure if telemetry persistence is unavailable.
            }

            throw;
        }
    }

    private async Task<T> TrackStepAsync<T>(
        Guid runId,
        string name,
        Func<Task<T>> action,
        CancellationToken cancellationToken,
        Func<T, string?>? detailFactory = null)
    {
        var step = await telemetry.StartStepAsync(runId, name, cancellationToken);
        try
        {
            var result = await action();
            await telemetry.CompleteStepAsync(step, detailFactory?.Invoke(result), cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            await telemetry.FailStepAsync(step, ex, CancellationToken.None);
            throw;
        }
    }

    private static byte[] DecodeFile(string fileBase64)
    {
        try
        {
            return Convert.FromBase64String(fileBase64);
        }
        catch (FormatException)
        {
            throw new ArgumentException("FileBase64 is not a valid base64 string.");
        }
    }

    private bool ValidateFileSize(byte[] fileBytes)
    {
        if (fileBytes.LongLength > _fileProcessingOptions.MaxFileSizeBytes)
        {
            throw new ArgumentException(
                $"File exceeds the maximum allowed size of {_fileProcessingOptions.MaxFileSizeBytes} bytes.");
        }

        return true;
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
        string? invoiceQrContext,
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

        if (!string.IsNullOrWhiteSpace(invoiceQrContext))
        {
            builder.AppendLine("Decoded invoice QR data:");
            builder.AppendLine(invoiceQrContext);
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
