using FluentValidation;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Validation;

/// <summary>
/// Validator for CreateOrUpdateScheduleEventDto.
/// </summary>
public class CreateOrUpdateScheduleEventValidator : AbstractValidator<CreateOrUpdateScheduleEventDto>
{
    public CreateOrUpdateScheduleEventValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters");

        RuleFor(x => x.ExecutionType)
            .IsInEnum().WithMessage("ExecutionType must be a valid enum value");

        RuleFor(x => x.RuleId)
            .NotEmpty().WithMessage("RuleId is required for ExecuteRule schedules")
            .MaximumLength(255).WithMessage("RuleId must not exceed 255 characters")
            .When(x => x.ExecutionType == ScheduleExecutionType.ExecuteRule);

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.EndpointOverride)
            .MaximumLength(500).WithMessage("EndpointOverride must not exceed 500 characters")
            .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.EndpointOverride))
            .WithMessage("EndpointOverride must be a valid URL if provided");

        RuleFor(x => x.Query)
            .NotEmpty().WithMessage("Query is required for ExecuteQuery schedules")
            .When(x => x.ExecutionType == ScheduleExecutionType.ExecuteQuery);

        RuleFor(x => x.EndpointUrl)
            .NotEmpty().WithMessage("EndpointUrl is required for ExecuteEndPoint schedules")
            .Must(BeValidUrl).WithMessage("EndpointUrl must be an absolute HTTP or HTTPS URL")
            .When(x => x.ExecutionType == ScheduleExecutionType.ExecuteEndPoint);

        RuleFor(x => x.EndpointMethod)
            .Must(method => string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase)
                || string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
            .WithMessage("EndpointMethod must be GET or POST")
            .When(x => x.ExecutionType == ScheduleExecutionType.ExecuteEndPoint);

        RuleFor(x => x.EndpointParameters)
            .Must(BeJsonObject).WithMessage("EndpointParameters must be a JSON object")
            .When(x => !string.IsNullOrWhiteSpace(x.EndpointParameters));

        RuleFor(x => x.EndpointUrl).MaximumLength(2048);
        RuleFor(x => x.EndpointBody).MaximumLength(100000);

        RuleFor(x => x.ScheduleConfig)
            .NotNull().WithMessage("ScheduleConfig is required")
            .SetValidator(new ScheduleConfigValidator());
    }

    private static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url)) return true;
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool BeJsonObject(string? value)
    {
        try
        {
            using var document = System.Text.Json.JsonDocument.Parse(value!);
            return document.RootElement.ValueKind == System.Text.Json.JsonValueKind.Object;
        }
        catch (System.Text.Json.JsonException)
        {
            return false;
        }
    }
}

/// <summary>
/// Validator for ScheduleConfigDto.
/// </summary>
public class ScheduleConfigValidator : AbstractValidator<ScheduleConfigDto>
{
    public ScheduleConfigValidator()
    {
        // RecurrenceType validation
        RuleFor(x => x.RecurrenceType)
            .IsInEnum().WithMessage("RecurrenceType must be a valid enum value");

        // Once - no additional validation needed
        RuleFor(x => x.IntervalValue)
            .GreaterThan(0).WithMessage("IntervalValue must be greater than 0")
            .When(x => x.RecurrenceType == RecurrenceType.Minutely || x.RecurrenceType == RecurrenceType.Hourly)
            .WithMessage("IntervalValue is required for Minutely/Hourly recurrence");

        RuleFor(x => x.IntervalValue)
            .Null().WithMessage("IntervalValue must be null for this recurrence type")
            .When(x => x.RecurrenceType != RecurrenceType.Minutely && x.RecurrenceType != RecurrenceType.Hourly);

        // Weekly validation
        RuleFor(x => x.DaysOfWeek)
            .NotEmpty().WithMessage("DaysOfWeek is required for Weekly recurrence")
            .When(x => x.RecurrenceType == RecurrenceType.Weekly)
            .Must(BeValidDaysOfWeek).WithMessage("DaysOfWeek format is invalid. Use comma-separated values: MON,TUE,WED,THU,FRI,SAT,SUN")
            .When(x => !string.IsNullOrEmpty(x.DaysOfWeek));

        RuleFor(x => x.DaysOfWeek)
            .Null().WithMessage("DaysOfWeek must be null for this recurrence type")
            .When(x => x.RecurrenceType != RecurrenceType.Weekly);

        // Monthly validation
        RuleFor(x => x.DayOfMonth)
            .GreaterThanOrEqualTo(1).WithMessage("DayOfMonth must be between 1 and 31")
            .LessThanOrEqualTo(31).WithMessage("DayOfMonth must be between 1 and 31")
            .When(x => x.RecurrenceType == RecurrenceType.Monthly)
            .WithMessage("DayOfMonth is required for Monthly recurrence");

        RuleFor(x => x.DayOfMonth)
            .Null().WithMessage("DayOfMonth must be null for this recurrence type")
            .When(x => x.RecurrenceType != RecurrenceType.Monthly);

        // TimeOfDay validation for Daily, Weekly, Monthly
        RuleFor(x => x.TimeOfDay)
            .NotEmpty().WithMessage("TimeOfDay is required for this recurrence type")
            .When(x => x.RecurrenceType == RecurrenceType.Daily || 
                       x.RecurrenceType == RecurrenceType.Weekly || 
                       x.RecurrenceType == RecurrenceType.Monthly)
            .Must(BeValidTimeSpan).WithMessage("TimeOfDay must be in format HH:mm:ss")
            .When(x => !string.IsNullOrEmpty(x.TimeOfDay));

        // CronExpression validation
        RuleFor(x => x.CronExpression)
            .NotEmpty().WithMessage("CronExpression is required for CronExpression recurrence type")
            .When(x => x.RecurrenceType == RecurrenceType.CronExpression)
            .Must(BeValidCronExpression).WithMessage("CronExpression is not valid")
            .When(x => !string.IsNullOrEmpty(x.CronExpression));

        RuleFor(x => x.CronExpression)
            .Null().WithMessage("CronExpression must be null for this recurrence type")
            .When(x => x.RecurrenceType != RecurrenceType.CronExpression);

        // Date range validation
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("EndDate must be after StartDate")
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

        // TimeZone validation
        RuleFor(x => x.TimeZone)
            .NotEmpty().WithMessage("TimeZone is required")
            .Must(BeValidTimeZone).WithMessage("TimeZone is not a valid system time zone ID");

        RuleFor(x => x.MisfireInstruction)
            .IsInEnum().WithMessage("MisfireInstruction must be a valid enum value");
    }

    private static bool BeValidDaysOfWeek(string? daysOfWeek)
    {
        if (string.IsNullOrWhiteSpace(daysOfWeek))
            return false;

        var validDays = new[] { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
        var days = daysOfWeek.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return days.Length > 0 && days.All(d => validDays.Contains(d.ToUpperInvariant()));
    }

    private static bool BeValidTimeSpan(string? timeSpan)
    {
        if (string.IsNullOrWhiteSpace(timeSpan))
            return false;
        return System.TimeSpan.TryParse(timeSpan, out _);
    }

    private static bool BeValidCronExpression(string? cronExpression)
    {
        if (string.IsNullOrWhiteSpace(cronExpression))
            return false;

        try
        {
            // Basic validation: Cron expression should have 5 or 6 fields
            var parts = cronExpression!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 5 && parts.Length <= 6;
        }
        catch
        {
            return false;
        }
    }

    private static bool BeValidTimeZone(string? timeZone)
    {
        if (string.IsNullOrEmpty(timeZone))
            return false;

        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
