using System.Text.Json;
using Microsoft.Extensions.Options;
using SlackNet;
using SlackNet.WebApi;
using Zamba.Api.Configuration;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface ISlackScheduleNotificationService
{
    Task SendAsync(ScheduleEvent scheduleEvent, ScheduleExecutionLogDto execution, CancellationToken cancellationToken);
}

public sealed class SlackScheduleNotificationService : ISlackScheduleNotificationService
{
    private readonly SlackNotificationOptions _options;
    private readonly Lazy<ISlackApiClient> _slackClient;

    public SlackScheduleNotificationService(IOptions<SlackNotificationOptions> options)
    {
        _options = options.Value;
        Console.WriteLine("Initializing SlackScheduleNotificationService with BotToken: " + _options.BotToken);
        Console.WriteLine("Initializing SlackScheduleNotificationService with ChannelId: " + _options.ChannelId);
        _slackClient = new Lazy<ISlackApiClient>(() => new SlackServiceBuilder()
            .UseApiToken(_options.BotToken)
            .GetApiClient());
    }

    public async Task SendAsync(
        ScheduleEvent scheduleEvent,
        ScheduleExecutionLogDto execution,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.BotToken)
            || string.IsNullOrWhiteSpace(_options.ChannelId))
        {
            throw new InvalidOperationException("Slack bot token and channel ID must be configured.");
        }

        execution.ScheduleEventName = scheduleEvent.Name;
        var message = new
        {
            Event = new
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
                scheduleEvent.NotifySlackOnSuccess,
                scheduleEvent.NotifySlackOnFailure,
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
            },
            Execution = execution
        };
        var details = JsonSerializer.Serialize(message, new JsonSerializerOptions { WriteIndented = true });
        var text = $"Zamba schedule execution: {execution.Status}\n{details}";
        if (text.Length > 39000)
        {
            text = $"{text[..39000]}\n[Message truncated to fit Slack limits]";
        }

        await _slackClient.Value.Chat.PostMessage(
            new Message
            {
                Channel = _options.ChannelId,
                Text = text
            },
            cancellationToken);
    }
}