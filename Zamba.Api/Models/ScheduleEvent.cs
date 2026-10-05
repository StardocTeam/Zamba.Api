namespace Zamba.Api.Models;

/// <summary>
/// The action performed when a schedule event runs.
/// </summary>
public enum ScheduleExecutionType
{
    ExecuteRule = 0,
    ExecuteQuery = 1,
    ExecuteEndPoint = 2
}

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
    /// Optional response body or query result expected from this schedule action.
    /// </summary>
    public string? ExpectedResult { get; set; }

    /// <summary>
    /// The rule ID or identifier that will be executed by the scheduler.
    /// This corresponds to a rule in the business logic.
    /// </summary>
    public required string RuleId { get; set; }

    /// <summary>
    /// Action to perform. Existing schedules default to ExecuteRule.
    /// </summary>
    public ScheduleExecutionType ExecutionType { get; set; } = ScheduleExecutionType.ExecuteRule;

    /// <summary>
    /// Optional override of the endpoint to call. If not specified, the default ExecuteRule endpoint is used.
    /// </summary>
    public string? EndpointOverride { get; set; }

    /// <summary>
    /// SQL query to run for ExecuteQuery schedules.
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// Absolute URL used by ExecuteEndPoint schedules.
    /// </summary>
    public string? EndpointUrl { get; set; }

    /// <summary>
    /// HTTP method used by ExecuteEndPoint schedules.
    /// </summary>
    public string? EndpointMethod { get; set; }

    /// <summary>
    /// JSON object containing query string parameters for ExecuteEndPoint schedules.
    /// </summary>
    public string? EndpointParameters { get; set; }

    /// <summary>
    /// Request body used by ExecuteEndPoint schedules.
    /// </summary>
    public string? EndpointBody { get; set; }

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
