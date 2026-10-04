namespace Zamba.Api.Models;

public class ScheduleWorkerStatus
{
    public required string WorkerInstanceId { get; set; }
    public required string HostName { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime LastHeartbeatAtUtc { get; set; }
    public DateTime? LastSyncAtUtc { get; set; }
    public int RegisteredJobCount { get; set; }
    public int ActiveJobCount { get; set; }
    public string? LastError { get; set; }
}