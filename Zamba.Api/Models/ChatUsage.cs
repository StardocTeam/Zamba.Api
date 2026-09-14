namespace Zamba.Api.Models;

/// <summary>Token/cost usage reported by the Copilot runtime for a chat completion call.</summary>
public class ChatUsage
{
    public string Model { get; set; } = string.Empty;
    public long InputTokens { get; set; }
    public long OutputTokens { get; set; }
    public long CacheReadTokens { get; set; }
    public long CacheWriteTokens { get; set; }
    public long ReasoningTokens { get; set; }
    public double? Cost { get; set; }
    public long DurationMs { get; set; }
}

/// <summary>Wraps a chat completion's text together with the usage metrics for that call.</summary>
public record ChatCompletionResult(string Response, ChatUsage? Usage);
