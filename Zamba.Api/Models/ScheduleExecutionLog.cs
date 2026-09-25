namespace Zamba.Api.Models;

/// <summary>
/// Enum representing the execution status of a schedule run.
/// </summary>
public enum ExecutionStatus
{
    /// <summary>Execution is currently running.</summary>
    Running = 0,

    /// <summary>Execution completed successfully.</summary>
    Success = 1,

    /// <summary>Execution failed with an error.</summary>
    Failed = 2,

    /// <summary>Execution was skipped (not executed).</summary>
    Skipped = 3,

    /// <summary>Execution exceeded the timeout duration.</summary>
    Timeout = 4
}

/// <summary>
/// Represents a log record of an execution of a ScheduleEvent.
/// This tracks the history of each scheduled rule execution.
/// </summary>
public class ScheduleExecutionLog
{
    /// <summary>
    /// Unique identifier for the execution log.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key to the associated ScheduleEvent.
    /// </summary>
    public int ScheduleEventId { get; set; }

    /// <summary>
    /// The date and time when this execution was scheduled to run.
    /// </summary>
    public DateTime ScheduledAt { get; set; }

    /// <summary>
    /// The date and time when this execution actually started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// The date and time when this execution finished. Null if still running.
    /// </summary>
    public DateTime? FinishedAt { get; set; }

    /// <summary>
    /// The current status of this execution.
    /// </summary>
    public ExecutionStatus Status { get; set; }

    /// <summary>
    /// The HTTP status code returned by the executed endpoint (if applicable).
    /// </summary>
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// The response body from the executed endpoint. Truncated to N characters to avoid excessive storage.
    /// </summary>
    public string? ResponseBody { get; set; }

    /// <summary>
    /// Error message if the execution failed or timed out.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Duration of the execution in milliseconds.
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Number of retries attempted for this execution.
    /// </summary>
    public int RetryCount { get; set; } = 0;

    // Navigation property
    /// <summary>
    /// The associated ScheduleEvent.
    /// </summary>
    public ScheduleEvent? ScheduleEvent { get; set; }
}
