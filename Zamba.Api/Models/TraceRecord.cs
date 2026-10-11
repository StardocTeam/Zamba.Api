namespace Zamba.Api.Models;

public sealed class TraceRecord
{
    public long Id { get; set; }
    public int TraceSourceId { get; set; }
    public TraceSource TraceSource { get; set; } = null!;
    public string RelativePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTimeOffset TraceTimestampUtc { get; set; }
    public string? UserId { get; set; }
    public string? TraceConfig { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset RegisteredAtUtc { get; set; } = DateTimeOffset.UtcNow;
}