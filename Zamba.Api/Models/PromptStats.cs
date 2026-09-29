namespace Zamba.Api.Models;

/// <summary>
/// Diagnostics for a single /api/copilot/ask call, useful for tuning chunk size, cache hit rate,
/// and tracking token/cost usage over time.
/// </summary>
public class PromptStats
{
    /// <summary>Whether the document's embeddings were already cached from a previous request.</summary>
    public bool DocumentAlreadyIndexed { get; set; }

    /// <summary>"Local" (offline hashing-trick) or "GitHubModels".</summary>
    public string EmbeddingProvider { get; set; } = string.Empty;

    /// <summary>Number of chunks embedded and stored for this document (0 when already indexed).</summary>
    public int ChunksIndexed { get; set; }

    public long EmbeddingDurationMs { get; set; }

    public long RetrievalDurationMs { get; set; }

    public long ChatDurationMs { get; set; }

    public long TotalDurationMs { get; set; }

    public string? ChatModel { get; set; }

    public long? InputTokens { get; set; }

    public long? OutputTokens { get; set; }

    public long? TotalTokens => InputTokens.HasValue || OutputTokens.HasValue
        ? (InputTokens ?? 0) + (OutputTokens ?? 0)
        : null;

    /// <summary>Token-derived credits; 1 credit represents 1,000 input and output tokens combined.</summary>
    public double? CopilotCreditsUsed =>
        Services.CopilotPlanCostEstimator.CalculateCreditsUsed(InputTokens, OutputTokens);

    /// <summary>Equivalent usage cost at the overage rate, before applying the monthly included quota.</summary>
    public double? EstimatedCostAtOverageRateUsd =>
        Services.CopilotPlanCostEstimator.CalculateCostAtOverageRateUsd(InputTokens, OutputTokens);

    public long? CacheReadTokens { get; set; }

    public long? CacheWriteTokens { get; set; }

    public double? EstimatedCost { get; set; }
}
