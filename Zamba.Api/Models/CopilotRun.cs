namespace Zamba.Api.Models;

public sealed class CopilotRun
{
    public Guid Id { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public string Status { get; set; } = "Running";
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
    public ICollection<CopilotRunStep> Steps { get; set; } = new List<CopilotRunStep>();
    public ICollection<CopilotFeedback> Feedback { get; set; } = new List<CopilotFeedback>();
}
