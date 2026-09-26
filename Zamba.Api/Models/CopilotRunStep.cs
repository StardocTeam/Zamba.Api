namespace Zamba.Api.Models;

public sealed class CopilotRunStep
{
    public long Id { get; set; }
    public Guid RunId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "Running";
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public long? DurationMs { get; set; }
    public string? Detail { get; set; }
    public string? ErrorMessage { get; set; }
    public CopilotRun Run { get; set; } = null!;
}
