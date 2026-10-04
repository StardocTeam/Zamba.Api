using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

/// <summary>
/// Utility service for mapping between entities and DTOs.
/// </summary>
public interface IScheduleMapper
{
    /// <summary>
    /// Maps a ScheduleEvent entity to a ScheduleEventDto.
    /// </summary>
    ScheduleEventDto MapToDto(ScheduleEvent entity);

    /// <summary>
    /// Maps a ScheduleConfig entity to a ScheduleConfigDto.
    /// </summary>
    ScheduleConfigDto MapConfigToDto(ScheduleConfig config);

    /// <summary>
    /// Maps a ScheduleExecutionLog entity to a ScheduleExecutionLogDto.
    /// </summary>
    ScheduleExecutionLogDto MapLogToDto(ScheduleExecutionLog log);

    /// <summary>
    /// Maps a CreateOrUpdateScheduleEventDto to a ScheduleEvent entity.
    /// </summary>
    ScheduleEvent MapToEntity(CreateOrUpdateScheduleEventDto dto, int? existingId = null);

    /// <summary>
    /// Maps a ScheduleConfigDto to a ScheduleConfig entity.
    /// </summary>
    ScheduleConfig MapConfigToEntity(ScheduleConfigDto dto, int scheduleEventId);
}

/// <summary>
/// Implementation of IScheduleMapper for mapping between Schedule entities and DTOs.
/// </summary>
public class ScheduleMapper : IScheduleMapper
{
    public ScheduleEventDto MapToDto(ScheduleEvent entity)
    {
        return new ScheduleEventDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            RuleId = entity.RuleId,
            ExecutionType = entity.ExecutionType,
            EndpointOverride = entity.EndpointOverride,
            Query = entity.Query,
            EndpointUrl = entity.EndpointUrl,
            EndpointMethod = entity.EndpointMethod,
            EndpointParameters = entity.EndpointParameters,
            EndpointBody = entity.EndpointBody,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy,
            ScheduleConfig = entity.ScheduleConfig != null ? MapConfigToDto(entity.ScheduleConfig) : null
        };
    }

    public ScheduleConfigDto MapConfigToDto(ScheduleConfig config)
    {
        return new ScheduleConfigDto
        {
            RecurrenceType = config.RecurrenceType,
            IntervalValue = config.IntervalValue,
            DaysOfWeek = config.DaysOfWeek,
            DayOfMonth = config.DayOfMonth,
            TimeOfDay = config.TimeOfDay?.ToString(@"hh\:mm\:ss"),
            StartDate = config.StartDate,
            EndDate = config.EndDate,
            CronExpression = config.CronExpression,
            MisfireInstruction = config.MisfireInstruction,
            TimeZone = config.TimeZone
        };
    }

    public ScheduleExecutionLogDto MapLogToDto(ScheduleExecutionLog log)
    {
        return new ScheduleExecutionLogDto
        {
            Id = log.Id,
            ScheduleEventId = log.ScheduleEventId,
            ScheduleEventName = log.ScheduleEvent?.Name,
            ScheduledAt = log.ScheduledAt,
            StartedAt = log.StartedAt,
            FinishedAt = log.FinishedAt,
            Status = log.Status,
            HttpStatusCode = log.HttpStatusCode,
            ResponseBody = log.ResponseBody,
            ErrorMessage = log.ErrorMessage,
            DurationMs = log.DurationMs,
            RetryCount = log.RetryCount
        };
    }

    public ScheduleEvent MapToEntity(CreateOrUpdateScheduleEventDto dto, int? existingId = null)
    {
        var now = DateTime.UtcNow;
        return new ScheduleEvent
        {
            Id = existingId ?? 0,
            Name = dto.Name,
            Description = dto.Description,
            RuleId = dto.RuleId,
            ExecutionType = dto.ExecutionType,
            EndpointOverride = dto.EndpointOverride,
            Query = dto.Query,
            EndpointUrl = dto.EndpointUrl,
            EndpointMethod = dto.EndpointMethod,
            EndpointParameters = dto.EndpointParameters,
            EndpointBody = dto.EndpointBody,
            IsActive = dto.IsActive,
            CreatedAt = existingId.HasValue ? DateTime.UtcNow : now,
            UpdatedAt = now,
            UpdatedBy = "API" // TODO: Set to actual user when authentication is implemented
        };
    }

    public ScheduleConfig MapConfigToEntity(ScheduleConfigDto dto, int scheduleEventId)
    {
        return new ScheduleConfig
        {
            ScheduleEventId = scheduleEventId,
            RecurrenceType = dto.RecurrenceType,
            IntervalValue = dto.IntervalValue,
            DaysOfWeek = dto.DaysOfWeek,
            DayOfMonth = dto.DayOfMonth,
            TimeOfDay = string.IsNullOrEmpty(dto.TimeOfDay) ? null : TimeSpan.Parse(dto.TimeOfDay),
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CronExpression = dto.CronExpression,
            MisfireInstruction = dto.MisfireInstruction,
            TimeZone = dto.TimeZone
        };
    }
}
