namespace Zamba.Api.Models;

public sealed class TraceSource
{
    public int Id { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string FolderPath { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<TraceRecord> Traces { get; set; } = new List<TraceRecord>();
}