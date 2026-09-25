using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.DTOs;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

/// <summary>
/// API controller for managing schedule events and their executions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScheduleEventsController : ControllerBase
{
    private readonly ZambaDbContext _dbContext;
    private readonly IScheduleMapper _mapper;
    private readonly IValidator<CreateOrUpdateScheduleEventDto> _eventValidator;
    private readonly IValidator<ScheduleConfigDto> _configValidator;
    private readonly ILogger<ScheduleEventsController> _logger;

    public ScheduleEventsController(
        ZambaDbContext dbContext,
        IScheduleMapper mapper,
        IValidator<CreateOrUpdateScheduleEventDto> eventValidator,
        IValidator<ScheduleConfigDto> configValidator,
        ILogger<ScheduleEventsController> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _eventValidator = eventValidator;
        _configValidator = configValidator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a paginated list of schedule events with optional filtering.
    /// </summary>
    /// <param name="pageNumber">Page number (1-indexed). Default: 1</param>
    /// <param name="pageSize">Items per page. Default: 10, Max: 100</param>
    /// <param name="isActive">Optional filter by active status.</param>
    /// <param name="name">Optional filter by name (contains).</param>
    /// <param name="ruleId">Optional filter by rule ID.</param>
    /// <returns>Paginated list of schedule events.</returns>
    /// <response code="200">List of schedule events retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ScheduleEventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResultDto<ScheduleEventDto>>> GetScheduleEvents(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool? isActive = null,
        [FromQuery] string? name = null,
        [FromQuery] string? ruleId = null)
    {
        _logger.LogInformation("Getting schedule events - Page: {PageNumber}, Size: {PageSize}, Active: {IsActive}, Name: {Name}, RuleId: {RuleId}",
            pageNumber, pageSize, isActive, name, ruleId);

        // Validate pagination
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Min(100, Math.Max(1, pageSize));

        var query = _dbContext.ScheduleEvents
            .Include(e => e.ScheduleConfig)
            .AsQueryable();

        // Apply filters
        if (isActive.HasValue)
        {
            query = query.Where(e => e.IsActive == isActive.Value);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(e => e.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(ruleId))
        {
            query = query.Where(e => e.RuleId == ruleId);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResultDto<ScheduleEventDto>
        {
            Items = items.Select(_mapper.MapToDto).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Ok(result);
    }

    /// <summary>
    /// Gets a specific schedule event by ID.
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <returns>The schedule event details.</returns>
    /// <response code="200">Schedule event retrieved successfully.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ScheduleEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleEventDto>> GetScheduleEvent(int id)
    {
        _logger.LogInformation("Getting schedule event with ID: {ScheduleEventId}", id);

        var scheduleEvent = await _dbContext.ScheduleEvents
            .Include(e => e.ScheduleConfig)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (scheduleEvent == null)
        {
            _logger.LogWarning("Schedule event not found with ID: {ScheduleEventId}", id);
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        return Ok(_mapper.MapToDto(scheduleEvent));
    }

    /// <summary>
    /// Creates a new schedule event with its configuration.
    /// </summary>
    /// <param name="dto">The schedule event creation data.</param>
    /// <returns>The created schedule event.</returns>
    /// <response code="201">Schedule event created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ScheduleEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleEventDto>> CreateScheduleEvent([FromBody] CreateOrUpdateScheduleEventDto dto)
    {
        _logger.LogInformation("Creating schedule event: {Name} (RuleId: {RuleId})", dto.Name, dto.RuleId);

        // Validate the request
        var validationResult = await _eventValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for schedule event creation: {Errors}", string.Join(", ", validationResult.Errors));
            return BadRequest(new ErrorResponseDto
            {
                Code = "VALIDATION_ERROR",
                Message = "The request contains invalid data.",
                ValidationErrors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList())
            });
        }

        // Map to entity
        var scheduleEvent = _mapper.MapToEntity(dto);
        var scheduleConfig = _mapper.MapConfigToEntity(dto.ScheduleConfig, 0);

        // Save the event first to get the ID
        _dbContext.ScheduleEvents.Add(scheduleEvent);
        await _dbContext.SaveChangesAsync();

        // Update config with the event ID and save
        scheduleConfig.ScheduleEventId = scheduleEvent.Id;
        scheduleEvent.ScheduleConfig = scheduleConfig;
        _dbContext.ScheduleConfigs.Add(scheduleConfig);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Schedule event created successfully with ID: {ScheduleEventId}", scheduleEvent.Id);

        return CreatedAtAction(nameof(GetScheduleEvent), new { id = scheduleEvent.Id }, _mapper.MapToDto(scheduleEvent));
    }

    /// <summary>
    /// Updates an existing schedule event and its configuration.
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <param name="dto">The updated schedule event data.</param>
    /// <returns>The updated schedule event.</returns>
    /// <response code="200">Schedule event updated successfully.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ScheduleEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleEventDto>> UpdateScheduleEvent(int id, [FromBody] CreateOrUpdateScheduleEventDto dto)
    {
        _logger.LogInformation("Updating schedule event with ID: {ScheduleEventId}", id);

        // Validate the request
        var validationResult = await _eventValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for schedule event update: {Errors}", string.Join(", ", validationResult.Errors));
            return BadRequest(new ErrorResponseDto
            {
                Code = "VALIDATION_ERROR",
                Message = "The request contains invalid data.",
                ValidationErrors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList())
            });
        }

        // Find the existing event
        var scheduleEvent = await _dbContext.ScheduleEvents
            .Include(e => e.ScheduleConfig)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (scheduleEvent == null)
        {
            _logger.LogWarning("Schedule event not found for update with ID: {ScheduleEventId}", id);
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        // Update event properties
        scheduleEvent.Name = dto.Name;
        scheduleEvent.Description = dto.Description;
        scheduleEvent.RuleId = dto.RuleId;
        scheduleEvent.EndpointOverride = dto.EndpointOverride;
        scheduleEvent.IsActive = dto.IsActive;
        scheduleEvent.UpdatedAt = DateTime.UtcNow;
        scheduleEvent.UpdatedBy = "API"; // TODO: Set to actual user when authentication is implemented

        // Update config
        if (scheduleEvent.ScheduleConfig != null)
        {
            var updatedConfig = _mapper.MapConfigToEntity(dto.ScheduleConfig, id);
            scheduleEvent.ScheduleConfig.RecurrenceType = updatedConfig.RecurrenceType;
            scheduleEvent.ScheduleConfig.IntervalValue = updatedConfig.IntervalValue;
            scheduleEvent.ScheduleConfig.DaysOfWeek = updatedConfig.DaysOfWeek;
            scheduleEvent.ScheduleConfig.DayOfMonth = updatedConfig.DayOfMonth;
            scheduleEvent.ScheduleConfig.TimeOfDay = updatedConfig.TimeOfDay;
            scheduleEvent.ScheduleConfig.StartDate = updatedConfig.StartDate;
            scheduleEvent.ScheduleConfig.EndDate = updatedConfig.EndDate;
            scheduleEvent.ScheduleConfig.CronExpression = updatedConfig.CronExpression;
            scheduleEvent.ScheduleConfig.MisfireInstruction = updatedConfig.MisfireInstruction;
            scheduleEvent.ScheduleConfig.TimeZone = updatedConfig.TimeZone;
        }

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Schedule event updated successfully with ID: {ScheduleEventId}", id);

        return Ok(_mapper.MapToDto(scheduleEvent));
    }

    /// <summary>
    /// Toggles the active status of a schedule event (quick enable/disable).
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <returns>The updated schedule event.</returns>
    /// <response code="200">Schedule event status toggled successfully.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpPatch("{id}/toggle")]
    [ProducesResponseType(typeof(ScheduleEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleEventDto>> ToggleScheduleEvent(int id)
    {
        _logger.LogInformation("Toggling schedule event status for ID: {ScheduleEventId}", id);

        var scheduleEvent = await _dbContext.ScheduleEvents
            .Include(e => e.ScheduleConfig)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (scheduleEvent == null)
        {
            _logger.LogWarning("Schedule event not found for toggle with ID: {ScheduleEventId}", id);
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        scheduleEvent.IsActive = !scheduleEvent.IsActive;
        scheduleEvent.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Schedule event status toggled for ID: {ScheduleEventId}, New Status: {IsActive}", id, scheduleEvent.IsActive);

        return Ok(_mapper.MapToDto(scheduleEvent));
    }

    /// <summary>
    /// Deletes a schedule event (hard delete).
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Schedule event deleted successfully.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteScheduleEvent(int id)
    {
        _logger.LogInformation("Deleting schedule event with ID: {ScheduleEventId}", id);

        var scheduleEvent = await _dbContext.ScheduleEvents.FindAsync(id);

        if (scheduleEvent == null)
        {
            _logger.LogWarning("Schedule event not found for deletion with ID: {ScheduleEventId}", id);
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        _dbContext.ScheduleEvents.Remove(scheduleEvent);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Schedule event deleted successfully with ID: {ScheduleEventId}", id);

        return NoContent();
    }

    /// <summary>
    /// Gets paginated execution logs for a specific schedule event.
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <param name="pageNumber">Page number (1-indexed). Default: 1</param>
    /// <param name="pageSize">Items per page. Default: 10, Max: 100</param>
    /// <returns>Paginated execution logs.</returns>
    /// <response code="200">Execution logs retrieved successfully.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpGet("{id}/logs")]
    [ProducesResponseType(typeof(PagedResultDto<ScheduleExecutionLogDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagedResultDto<ScheduleExecutionLogDto>>> GetExecutionLogs(
        int id,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting execution logs for schedule event ID: {ScheduleEventId}, Page: {PageNumber}", id, pageNumber);

        // Verify event exists
        var eventExists = await _dbContext.ScheduleEvents.AnyAsync(e => e.Id == id);
        if (!eventExists)
        {
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        // Validate pagination
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Min(100, Math.Max(1, pageSize));

        var query = _dbContext.ScheduleExecutionLogs
            .Where(l => l.ScheduleEventId == id);

        var totalCount = await query.CountAsync();
        var logs = await query
            .OrderByDescending(l => l.ScheduledAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResultDto<ScheduleExecutionLogDto>
        {
            Items = logs.Select(_mapper.MapLogToDto).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Ok(result);
    }

    /// <summary>
    /// Gets paginated execution logs for all schedule events with optional filtering.
    /// </summary>
    /// <param name="pageNumber">Page number (1-indexed). Default: 1</param>
    /// <param name="pageSize">Items per page. Default: 10, Max: 100</param>
    /// <param name="scheduleEventId">Optional filter by schedule event ID.</param>
    /// <param name="status">Optional filter by execution status.</param>
    /// <param name="fromDate">Optional filter logs from this date onwards.</param>
    /// <param name="toDate">Optional filter logs up to this date.</param>
    /// <returns>Paginated execution logs.</returns>
    /// <response code="200">Execution logs retrieved successfully.</response>
    [HttpGet("logs")]
    [ProducesResponseType(typeof(PagedResultDto<ScheduleExecutionLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResultDto<ScheduleExecutionLogDto>>> GetAllExecutionLogs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? scheduleEventId = null,
        [FromQuery] ExecutionStatus? status = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        _logger.LogInformation("Getting all execution logs - Page: {PageNumber}, EventId: {EventId}, Status: {Status}", pageNumber, scheduleEventId, status);

        // Validate pagination
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Min(100, Math.Max(1, pageSize));

        var query = _dbContext.ScheduleExecutionLogs.AsQueryable();

        // Apply filters
        if (scheduleEventId.HasValue)
        {
            query = query.Where(l => l.ScheduleEventId == scheduleEventId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(l => l.Status == status.Value);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(l => l.ScheduledAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(l => l.ScheduledAt <= toDate.Value);
        }

        var totalCount = await query.CountAsync();
        var logs = await query
            .OrderByDescending(l => l.ScheduledAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResultDto<ScheduleExecutionLogDto>
        {
            Items = logs.Select(_mapper.MapLogToDto).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Ok(result);
    }

    /// <summary>
    /// Records the start of a schedule execution. Called by Zamba.Schedule worker.
    /// </summary>
    /// <param name="log">The execution log data.</param>
    /// <returns>The created execution log.</returns>
    /// <response code="201">Execution log created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost("logs")]
    [ProducesResponseType(typeof(ScheduleExecutionLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleExecutionLogDto>> CreateExecutionLog([FromBody] ScheduleExecutionLogDto log)
    {
        _logger.LogInformation("Creating execution log for schedule event ID: {ScheduleEventId}, Status: {Status}", log.ScheduleEventId, log.Status);

        // Verify the event exists
        var eventExists = await _dbContext.ScheduleEvents.AnyAsync(e => e.Id == log.ScheduleEventId);
        if (!eventExists)
        {
            _logger.LogWarning("Cannot create log: Schedule event not found with ID: {ScheduleEventId}", log.ScheduleEventId);
            return BadRequest(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {log.ScheduleEventId} not found."
            });
        }

        var executionLog = new ScheduleExecutionLog
        {
            ScheduleEventId = log.ScheduleEventId,
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

        _dbContext.ScheduleExecutionLogs.Add(executionLog);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Execution log created successfully with ID: {LogId}", executionLog.Id);

        return CreatedAtAction(nameof(GetScheduleEvent), new { id = executionLog.ScheduleEventId }, _mapper.MapLogToDto(executionLog));
    }

    /// <summary>
    /// Calculates and returns the next N scheduled executions for a specific schedule event.
    /// Useful for preview in the UI.
    /// </summary>
    /// <param name="id">The schedule event ID.</param>
    /// <param name="count">Number of future executions to calculate (default: 5, max: 100).</param>
    /// <returns>List of calculated next execution times.</returns>
    /// <response code="200">Next executions calculated successfully.</response>
    /// <response code="404">Schedule event not found.</response>
    [HttpGet("{id}/next-runs")]
    [ProducesResponseType(typeof(NextRunsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NextRunsDto>> GetNextRuns(int id, [FromQuery] int count = 5)
    {
        _logger.LogInformation("Calculating next runs for schedule event ID: {ScheduleEventId}, Count: {Count}", id, count);

        var scheduleEvent = await _dbContext.ScheduleEvents
            .Include(e => e.ScheduleConfig)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (scheduleEvent == null)
        {
            return NotFound(new ErrorResponseDto
            {
                Code = "SCHEDULE_EVENT_NOT_FOUND",
                Message = $"Schedule event with ID {id} not found."
            });
        }

        // Validate count
        count = Math.Min(100, Math.Max(1, count));

        var nextRuns = new NextRunsDto
        {
            ScheduleEventId = id,
            CalculatedAt = DateTime.UtcNow,
            NextRuns = new List<DateTime>()
        };

        if (scheduleEvent.ScheduleConfig != null)
        {
            nextRuns.NextRuns = CalculateNextRuns(scheduleEvent.ScheduleConfig, count);
        }

        return Ok(nextRuns);
    }

    /// <summary>
    /// Helper method to calculate next execution times based on schedule configuration.
    /// This is a simplified implementation. For production, use a Cron library like Cronos.
    /// </summary>
    private List<DateTime> CalculateNextRuns(ScheduleConfig config, int count)
    {
        var nextRuns = new List<DateTime>();
        var now = DateTime.UtcNow;
        var startDate = config.StartDate ?? now;
        var endDate = config.EndDate ?? now.AddYears(1); // Default to 1 year in future

        if (startDate > endDate)
            return nextRuns;

        var current = startDate > now ? startDate : now;

        try
        {
            switch (config.RecurrenceType)
            {
                case RecurrenceType.Once:
                    if (startDate >= now && startDate <= endDate)
                        nextRuns.Add(startDate);
                    break;

                case RecurrenceType.Minutely:
                    if (config.IntervalValue.HasValue)
                    {
                        for (int i = 0; i < count && current <= endDate; i++)
                        {
                            nextRuns.Add(current);
                            current = current.AddMinutes(config.IntervalValue.Value);
                        }
                    }
                    break;

                case RecurrenceType.Hourly:
                    if (config.IntervalValue.HasValue)
                    {
                        for (int i = 0; i < count && current <= endDate; i++)
                        {
                            nextRuns.Add(current);
                            current = current.AddHours(config.IntervalValue.Value);
                        }
                    }
                    break;

                case RecurrenceType.Daily:
                    if (config.TimeOfDay.HasValue)
                    {
                        var timeOfDay = config.TimeOfDay.Value;
                        current = current.Date.Add(timeOfDay);
                        if (current <= now)
                            current = current.AddDays(1);

                        for (int i = 0; i < count && current <= endDate; i++)
                        {
                            nextRuns.Add(current);
                            current = current.AddDays(1);
                        }
                    }
                    break;

                case RecurrenceType.Weekly:
                    if (config.TimeOfDay.HasValue && !string.IsNullOrEmpty(config.DaysOfWeek))
                    {
                        var allowedDays = config.DaysOfWeek.Split(',')
                            .Select(d => d.Trim().ToUpperInvariant())
                            .Select(d => d switch
                            {
                                "MON" => DayOfWeek.Monday,
                                "TUE" => DayOfWeek.Tuesday,
                                "WED" => DayOfWeek.Wednesday,
                                "THU" => DayOfWeek.Thursday,
                                "FRI" => DayOfWeek.Friday,
                                "SAT" => DayOfWeek.Saturday,
                                "SUN" => DayOfWeek.Sunday,
                                _ => (DayOfWeek?)null
                            })
                            .Where(d => d.HasValue)
                            .Select(d => d!.Value)
                            .ToList();

                        var timeOfDay = config.TimeOfDay.Value;
                        current = current.Date.Add(timeOfDay);
                        if (current <= now)
                            current = current.AddDays(1);

                        while (nextRuns.Count < count && current <= endDate)
                        {
                            if (allowedDays.Contains(current.DayOfWeek))
                            {
                                nextRuns.Add(current);
                            }
                            current = current.AddDays(1);
                        }
                    }
                    break;

                case RecurrenceType.Monthly:
                    if (config.TimeOfDay.HasValue && config.DayOfMonth.HasValue)
                    {
                        var timeOfDay = config.TimeOfDay.Value;
                        var day = config.DayOfMonth.Value;

                        current = new DateTime(current.Year, current.Month, Math.Min(day, DateTime.DaysInMonth(current.Year, current.Month)))
                            .Add(timeOfDay);

                        if (current <= now)
                        {
                            current = current.AddMonths(1);
                            current = new DateTime(current.Year, current.Month, Math.Min(day, DateTime.DaysInMonth(current.Year, current.Month)))
                                .Add(timeOfDay);
                        }

                        for (int i = 0; i < count && current <= endDate; i++)
                        {
                            nextRuns.Add(current);
                            current = current.AddMonths(1);
                            current = new DateTime(current.Year, current.Month, Math.Min(day, DateTime.DaysInMonth(current.Year, current.Month)))
                                .Add(timeOfDay);
                        }
                    }
                    break;

                case RecurrenceType.CronExpression:
                    // For Cron expressions, simplified handling
                    _logger.LogWarning("Cron expression calculation is simplified. Install Cronos library for accurate calculation.");
                    // Placeholder: return empty list for now
                    // TODO: Integrate Cronos library (CronExpression.GetNextOccurrence)
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating next runs for schedule event");
        }

        return nextRuns;
    }
}

/// <summary>
/// DTO for next runs response.
/// </summary>
public class NextRunsDto
{
    /// <summary>
    /// The schedule event ID.
    /// </summary>
    public int ScheduleEventId { get; set; }

    /// <summary>
    /// When the calculation was performed.
    /// </summary>
    public DateTime CalculatedAt { get; set; }

    /// <summary>
    /// List of calculated next execution times.
    /// </summary>
    public List<DateTime> NextRuns { get; set; } = new();
}
