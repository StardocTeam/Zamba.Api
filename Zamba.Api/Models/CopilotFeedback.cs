namespace Zamba.Api.Models;

public sealed class CopilotFeedback
{
    public long Id { get; set; }
    public Guid RunId { get; set; }
    public string VoterId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public CopilotRun Run { get; set; } = null!;
}
