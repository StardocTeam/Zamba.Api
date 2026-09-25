namespace Zamba.Api.Models;

/// <summary>
/// Enum representing the type of recurrence for a schedule.
/// </summary>
public enum RecurrenceType
{
    /// <summary>Execute once at a specific date/time.</summary>
    Once = 0,

    /// <summary>Execute every N minutes.</summary>
    Minutely = 1,

    /// <summary>Execute every N hours.</summary>
    Hourly = 2,

    /// <summary>Execute every day at a specific time.</summary>
    Daily = 3,

    /// <summary>Execute on specific days of the week at a specific time.</summary>
    Weekly = 4,

    /// <summary>Execute on a specific day of the month at a specific time.</summary>
    Monthly = 5,

    /// <summary>Execute according to a custom Cron expression.</summary>
    CronExpression = 6
}

/// <summary>
/// Enum representing what action to take if a scheduled job is missed (e.g., server downtime).
/// </summary>
public enum MisfireInstruction
{
    /// <summary>Fire the job immediately when the scheduler resumes.</summary>
    FireNow = 0,

    /// <summary>Do nothing; skip the missed execution.</summary>
    DoNothing = 1,

    /// <summary>Fire once now and then resume normal schedule.</summary>
    FireOnceNow = 2
}

/// <summary>
/// Represents the recurrence configuration for a ScheduleEvent.
/// This is a 1-to-1 relationship with ScheduleEvent.
/// </summary>
public class ScheduleConfig
{
    /// <summary>
    /// Unique identifier for the schedule configuration.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key to the associated ScheduleEvent.
    /// </summary>
    public int ScheduleEventId { get; set; }

    /// <summary>
    /// Type of recurrence (Once, Minutely, Hourly, Daily, Weekly, Monthly, or CronExpression).
    /// </summary>
    public RecurrenceType RecurrenceType { get; set; }

    /// <summary>
    /// The interval value for the recurrence (e.g., every X minutes/hours).
    /// Applicable for Minutely and Hourly recurrence types.
    /// </summary>
    public int? IntervalValue { get; set; }

    /// <summary>
    /// Comma-separated list of days of the week (e.g., "MON,WED,FRI").
    /// Applicable for Weekly recurrence type.
    /// </summary>
    public string? DaysOfWeek { get; set; }

    /// <summary>
    /// Day of the month (1-31) for Monthly recurrence.
    /// Applicable for Monthly recurrence type.
    /// </summary>
    public int? DayOfMonth { get; set; }

    /// <summary>
    /// Time of day (as TimeSpan) when the execution should occur.
    /// Applicable for Daily, Weekly, and Monthly recurrence types.
    /// </summary>
    public TimeSpan? TimeOfDay { get; set; }

    /// <summary>
    /// The start date and time for the schedule. Null means start immediately.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The end date and time for the schedule. Null means no end date (indefinite).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Custom Cron expression for advanced scheduling.
    /// Applicable only when RecurrenceType is CronExpression.
    /// </summary>
    public string? CronExpression { get; set; }

    /// <summary>
    /// Action to take if a scheduled execution is missed (e.g., server downtime).
    /// </summary>
    public MisfireInstruction MisfireInstruction { get; set; } = MisfireInstruction.FireNow;

    /// <summary>
    /// Time zone identifier for schedule calculations (e.g., "Argentina Standard Time").
    /// Default is "Argentina Standard Time" (UTC-3 / UTC-2 during DST).
    /// </summary>
    public string TimeZone { get; set; } = "Argentina Standard Time";

    // Navigation property
    /// <summary>
    /// The associated ScheduleEvent.
    /// </summary>
    public ScheduleEvent? ScheduleEvent { get; set; }
}
