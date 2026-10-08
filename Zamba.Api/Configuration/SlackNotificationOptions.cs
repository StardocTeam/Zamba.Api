namespace Zamba.Api.Configuration;

public sealed class SlackNotificationOptions
{
    public const string SectionName = "Slack";

    public string BotToken { get; set; } = "";

    public string ChannelId { get; set; } = "";
}