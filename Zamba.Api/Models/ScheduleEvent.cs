namespace Zamba.Api.Models;

/// <summary>
/// Represents a scheduled event/rule that will be executed on a recurring schedule.
/// </summary>
public class ScheduleEvent
{
    /// <summary>
    /// Unique identifier for the schedule event.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Display name of the schedule event.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description of the schedule event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The rule ID or identifier that will be executed by the scheduler.
    /// This corresponds to a rule in the business logic.
    /// </summary>
    public required string RuleId { get; set; }

    /// <summary>
    /// Optional override of the endpoint to call. If not specified, the default ExecuteRule endpoint is used.
    /// </summary>
    public string? EndpointOverride { get; set; }

    /// <summary>
    /// Whether this schedule event is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date and time when this schedule event was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when this schedule event was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// User or system that created this schedule event.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// User or system that last updated this schedule event.
    /// </summary>
    public string? UpdatedBy { get; set; }

    // Navigation properties
    /// <summary>
    /// Configuration for this schedule event's recurrence.
    /// </summary>
    public ScheduleConfig? ScheduleConfig { get; set; }

    /// <summary>
    /// Execution logs for this schedule event.
    /// </summary>
    public ICollection<ScheduleExecutionLog> ExecutionLogs { get; set; } = new List<ScheduleExecutionLog>();
}
