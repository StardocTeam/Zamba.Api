using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface IScheduleEventNotificationService
{
    Task SendAsync(ScheduleEvent scheduleEvent, ScheduleExecutionLogDto execution, CancellationToken cancellationToken);
}

public sealed class ScheduleEventNotificationService : IScheduleEventNotificationService
{
    private readonly ScheduleEmailOptions _options;

    public ScheduleEventNotificationService(IOptions<ScheduleEmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(
        ScheduleEvent scheduleEvent,
        ScheduleExecutionLogDto execution,
        CancellationToken cancellationToken)
    {
        var useGoogleWorkspaceRelay = string.Equals(
            _options.Provider,
            "GoogleWorkspaceRelay",
            StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrWhiteSpace(_options.FromAddress))
        {
            throw new InvalidOperationException("ScheduleEmail from address must be configured.");
        }

        if (!useGoogleWorkspaceRelay
            && (string.IsNullOrWhiteSpace(_options.Username) || string.IsNullOrWhiteSpace(_options.Password)))
        {
            throw new InvalidOperationException("ScheduleEmail username and password must be configured for this provider.");
        }

        execution.ScheduleEventName = scheduleEvent.Name;

        var (host, defaultPort) = _options.Provider.ToLowerInvariant() switch
        {
            "microsoft365" => ("smtp.office365.com", 587),
            "gmail" => ("smtp.gmail.com", 587),
            "googleworkspacerelay" => ("smtp-relay.gmail.com", 587),
            _ => throw new InvalidOperationException("ScheduleEmail provider must be Microsoft365, Gmail, or GoogleWorkspaceRelay.")
        };

        var eventDetails = new
        {
            scheduleEvent.Id,
            scheduleEvent.Name,
            scheduleEvent.Description,
            scheduleEvent.ExpectedResult,
            scheduleEvent.RuleId,
            scheduleEvent.ExecutionType,
            scheduleEvent.EndpointOverride,
            scheduleEvent.Query,
            scheduleEvent.EndpointUrl,
            scheduleEvent.EndpointMethod,
            scheduleEvent.EndpointParameters,
            scheduleEvent.EndpointBody,
            scheduleEvent.IsActive,
            scheduleEvent.NotificationEmail,
            scheduleEvent.NotifyOnSuccess,
            scheduleEvent.NotifyOnFailure,
            scheduleEvent.CreatedAt,
            scheduleEvent.UpdatedAt,
            scheduleEvent.CreatedBy,
            scheduleEvent.UpdatedBy,
            ScheduleConfig = scheduleEvent.ScheduleConfig == null ? null : new
            {
                scheduleEvent.ScheduleConfig.Id,
                scheduleEvent.ScheduleConfig.ScheduleEventId,
                scheduleEvent.ScheduleConfig.RecurrenceType,
                scheduleEvent.ScheduleConfig.IntervalValue,
                scheduleEvent.ScheduleConfig.DaysOfWeek,
                scheduleEvent.ScheduleConfig.DayOfMonth,
                scheduleEvent.ScheduleConfig.TimeOfDay,
                scheduleEvent.ScheduleConfig.StartDate,
                scheduleEvent.ScheduleConfig.EndDate,
                scheduleEvent.ScheduleConfig.CronExpression,
                scheduleEvent.ScheduleConfig.MisfireInstruction,
                scheduleEvent.ScheduleConfig.TimeZone
            }
        };
        var body = JsonSerializer.Serialize(new { Event = eventDetails, Execution = execution }, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        using var message = new MailMessage
        {
            From = new MailAddress(_options.FromAddress, _options.FromName),
            Subject = $"Schedule {execution.Status}: {scheduleEvent.Name}",
            SubjectEncoding = Encoding.UTF8,
            Body = body,
            BodyEncoding = Encoding.UTF8,
            IsBodyHtml = false
        };
        message.To.Add(new MailAddress(scheduleEvent.NotificationEmail!));

        using var client = new SmtpClient(host, _options.Port > 0 ? _options.Port : defaultPort)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = useGoogleWorkspaceRelay
                ? null
                : new NetworkCredential(_options.Username, _options.Password)
        };

        await client.SendMailAsync(message, cancellationToken);
    }
}