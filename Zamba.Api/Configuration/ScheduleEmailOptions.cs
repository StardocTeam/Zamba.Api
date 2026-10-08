namespace Zamba.Api.Configuration;

public sealed class ScheduleEmailOptions
{
    public const string SectionName = "ScheduleEmail";

    public string Provider { get; set; } = "Microsoft365";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FromAddress { get; set; } = "";
    public string FromName { get; set; } = "Zamba Schedule";
    public int Port { get; set; } = 587;
}