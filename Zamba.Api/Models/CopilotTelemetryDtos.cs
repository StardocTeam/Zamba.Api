namespace Zamba.Api.Models;

public sealed class CopilotTelemetrySummaryDto
{
    public int TotalRuns { get; set; }
    public int SucceededRuns { get; set; }
    public int FailedRuns { get; set; }
    public int RatedRuns { get; set; }
    public int NegativeRuns { get; set; }
    public int PositiveVotes { get; set; }
    public int NegativeVotes { get; set; }
    public double NegativeVotePercent { get; set; }
    public double NegativeRunPercent { get; set; }
    public double? AverageTotalDurationMs { get; set; }
    public double? AverageInputTokens { get; set; }
    public double? AverageEstimatedCost { get; set; }
}

public sealed class CopilotNegativeRunsPageDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRuns { get; set; }
    public IReadOnlyList<CopilotNegativeRunDto> Items { get; set; } = [];
}

public sealed class CopilotNegativeRunDto
{
    public Guid RunId { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? DocumentTypeCode { get; set; }
    public string? FileName { get; set; }
    public string PromptPreview { get; set; } = string.Empty;
    public string? ResponsePreview { get; set; }
    public string? ChatModel { get; set; }
    public long? TotalDurationMs { get; set; }
    public long? InputTokens { get; set; }
    public long? OutputTokens { get; set; }
    public int NegativeVoteCount { get; set; }
    public string? LatestFeedbackComment { get; set; }
}

public sealed class CopilotRunDetailDto
{
    public Guid RunId { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Prompt { get; set; } = string.Empty;
    public string? Response { get; set; }
    public string? FileName { get; set; }
    public string? DocumentId { get; set; }
    public int? DocumentTypeCode { get; set; }
    public string? SessionId { get; set; }
    public string? EmbeddingProvider { get; set; }
    public string? ChatModel { get; set; }
    public bool? DocumentAlreadyIndexed { get; set; }
    public int ChunksIndexed { get; set; }
    public int ChunksUsed { get; set; }
    public long? EmbeddingDurationMs { get; set; }
    public long? RetrievalDurationMs { get; set; }
    public long? ChatDurationMs { get; set; }
    public long? TotalDurationMs { get; set; }
    public long? InputTokens { get; set; }
    public long? OutputTokens { get; set; }
    public long? CacheReadTokens { get; set; }
    public long? CacheWriteTokens { get; set; }
    public double? EstimatedCost { get; set; }
    public string? ErrorMessage { get; set; }
    public IReadOnlyList<CopilotRunStepDto> Steps { get; set; } = [];
    public IReadOnlyList<CopilotFeedbackDto> Feedback { get; set; } = [];
}

public sealed class CopilotRunStepDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public long? DurationMs { get; set; }
    public string? Detail { get; set; }
    public string? ErrorMessage { get; set; }
}

public sealed class CopilotFeedbackDto
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
