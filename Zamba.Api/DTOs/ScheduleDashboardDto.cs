namespace Zamba.Api.DTOs;

public class ScheduleDashboardDto
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int TotalEvents { get; set; }
    public int ActiveEvents { get; set; }
    public int TotalExecutions { get; set; }
    public int RunningExecutions { get; set; }
    public int SuccessfulExecutions { get; set; }
    public int FailedExecutions { get; set; }
    public int TimedOutExecutions { get; set; }
    public int SkippedExecutions { get; set; }
    public double SuccessRate { get; set; }
    public double? AverageDurationMs { get; set; }
    public List<ScheduleDashboardDayDto> Trend { get; set; } = new();
    public List<ScheduleDashboardEventDto> TopEvents { get; set; } = new();
    public List<ScheduleExecutionLogDto> ActiveExecutions { get; set; } = new();
    public ScheduleWorkerStatusDto? Worker { get; set; }
}

public class ScheduleDashboardDayDto
{
    public DateTime Date { get; set; }
    public int Success { get; set; }
    public int Failed { get; set; }
    public int Running { get; set; }
    public int TimedOut { get; set; }
    public int Skipped { get; set; }
}

public class ScheduleDashboardEventDto
{
    public int ScheduleEventId { get; set; }
    public required string Name { get; set; }
    public int Executions { get; set; }
    public int Failures { get; set; }
    public double? AverageDurationMs { get; set; }
}

public class ScheduleWorkerStatusDto
{
    public required string WorkerInstanceId { get; set; }
    public required string HostName { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime LastHeartbeatAtUtc { get; set; }
    public DateTime? LastSyncAtUtc { get; set; }
    public int RegisteredJobCount { get; set; }
    public int ActiveJobCount { get; set; }
    public string? LastError { get; set; }
    public bool IsOnline { get; set; }
}

public class ScheduleWorkerHeartbeatDto
{
    public required string WorkerInstanceId { get; set; }
    public required string HostName { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime? LastSyncAtUtc { get; set; }
    public int RegisteredJobCount { get; set; }
    public int ActiveJobCount { get; set; }
    public string? LastError { get; set; }
}