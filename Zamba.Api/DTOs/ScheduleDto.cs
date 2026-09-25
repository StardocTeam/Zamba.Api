using Zamba.Api.Models;

namespace Zamba.Api.DTOs;

/// <summary>
/// DTO for creating or updating a schedule event with its configuration.
/// </summary>
public class CreateOrUpdateScheduleEventDto
{
    /// <summary>
    /// Display name of the schedule event (required).
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description of the schedule event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The rule ID or identifier to be executed (required).
    /// </summary>
    public required string RuleId { get; set; }

    /// <summary>
    /// Optional override of the endpoint to call.
    /// </summary>
    public string? EndpointOverride { get; set; }

    /// <summary>
    /// Whether this schedule event is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Configuration for the schedule recurrence (required).
    /// </summary>
    public required ScheduleConfigDto ScheduleConfig { get; set; }
}

/// <summary>
/// DTO for schedule configuration.
/// </summary>
public class ScheduleConfigDto
{
    /// <summary>
    /// Type of recurrence.
    /// </summary>
    public RecurrenceType RecurrenceType { get; set; }

    /// <summary>
    /// Interval value for Minutely/Hourly recurrence.
    /// </summary>
    public int? IntervalValue { get; set; }

    /// <summary>
    /// Comma-separated days of week (e.g., "MON,WED,FRI") for Weekly recurrence.
    /// </summary>
    public string? DaysOfWeek { get; set; }

    /// <summary>
    /// Day of month (1-31) for Monthly recurrence.
    /// </summary>
    public int? DayOfMonth { get; set; }

    /// <summary>
    /// Time of day as a string in "HH:mm:ss" format (e.g., "14:30:00").
    /// </summary>
    public string? TimeOfDay { get; set; }

    /// <summary>
    /// Start date and time for the schedule.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End date and time for the schedule (null = indefinite).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Custom Cron expression for CronExpression recurrence type.
    /// </summary>
    public string? CronExpression { get; set; }

    /// <summary>
    /// Action to take if execution is missed.
    /// </summary>
    public MisfireInstruction MisfireInstruction { get; set; } = MisfireInstruction.FireNow;

    /// <summary>
    /// Time zone identifier.
    /// </summary>
    public string TimeZone { get; set; } = "Argentina Standard Time";
}

/// <summary>
/// DTO for schedule event details (read operation).
/// </summary>
public class ScheduleEventDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Display name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Rule identifier.
    /// </summary>
    public required string RuleId { get; set; }

    /// <summary>
    /// Optional endpoint override.
    /// </summary>
    public string? EndpointOverride { get; set; }

    /// <summary>
    /// Whether the event is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last update timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Creator identifier.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Last updater identifier.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Associated schedule configuration.
    /// </summary>
    public ScheduleConfigDto? ScheduleConfig { get; set; }
}

/// <summary>
/// DTO for execution log details.
/// </summary>
public class ScheduleExecutionLogDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Schedule event identifier.
    /// </summary>
    public int ScheduleEventId { get; set; }

    /// <summary>
    /// When the execution was scheduled.
    /// </summary>
    public DateTime ScheduledAt { get; set; }

    /// <summary>
    /// When the execution started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// When the execution finished (null if still running).
    /// </summary>
    public DateTime? FinishedAt { get; set; }

    /// <summary>
    /// Execution status.
    /// </summary>
    public ExecutionStatus Status { get; set; }

    /// <summary>
    /// HTTP status code from the executed endpoint.
    /// </summary>
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// Response body (truncated).
    /// </summary>
    public string? ResponseBody { get; set; }

    /// <summary>
    /// Error message if execution failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Execution duration in milliseconds.
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Number of retries.
    /// </summary>
    public int RetryCount { get; set; }
}

/// <summary>
/// DTO for paginated list responses.
/// </summary>
public class PagedResultDto<T>
{
    /// <summary>
    /// List of items.
    /// </summary>
    public required List<T> Items { get; set; } = new();

    /// <summary>
    /// Current page number (1-indexed).
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Total count of items (across all pages).
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;

    /// <summary>
    /// Whether there's a next page.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Whether there's a previous page.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;
}

/// <summary>
/// DTO for API error response.
/// </summary>
public class ErrorResponseDto
{
    /// <summary>
    /// Error code or type.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Error message.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Detailed information (for development).
    /// </summary>
    public string? Detail { get; set; }

    /// <summary>
    /// Validation errors if applicable.
    /// </summary>
    public Dictionary<string, List<string>>? ValidationErrors { get; set; }
}
