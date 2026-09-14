using System.Text;
using GitHub.Copilot;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface ICopilotChatClient
{
    Task<ChatCompletionResult> GetChatCompletionAsync(string systemContext, string userPrompt, CancellationToken cancellationToken);
}

/// <summary>Runs prompts through the GitHub Copilot SDK (agentic CLI runtime) instead of a raw REST call.</summary>
public class CopilotSdkChatClient(ICopilotSdkClientProvider clientProvider, IOptions<GitHubCopilotOptions> options) : ICopilotChatClient
{
    private readonly GitHubCopilotOptions _options = options.Value;

    public async Task<ChatCompletionResult> GetChatCompletionAsync(string systemContext, string userPrompt, CancellationToken cancellationToken)
    {
        var client = await clientProvider.GetClientAsync(cancellationToken);

        await using var session = await client.CreateSessionAsync(new SessionConfig
        {
            SessionId = $"zamba-api-{Guid.NewGuid()}",
            GitHubToken = _options.ApiKey,
            Model = _options.ChatModel,
            SystemMessage = string.IsNullOrWhiteSpace(systemContext)
                ? null
                : new SystemMessageConfig { Mode = SystemMessageMode.Append, Content = systemContext },
            Streaming = false,
            // No custom tools are provided for this scenario; keep the model from invoking any.
            AvailableTools = []
        });

        var responseBuilder = new StringBuilder();
        var usage = new ChatUsage();
        var idle = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        Exception? sessionError = null;

        using var subscription = session.On<SessionEvent>(evt =>
        {
            switch (evt)
            {
                case AssistantMessageEvent msg:
                    responseBuilder.Append(msg.Data.Content);
                    break;
                case AssistantUsageEvent usageEvent:
                    // A prompt can involve more than one model call (e.g. retries); accumulate tokens across all of them.
                    usage.Model = usageEvent.Data.Model;
                    usage.InputTokens += usageEvent.Data.InputTokens ?? 0;
                    usage.OutputTokens += usageEvent.Data.OutputTokens ?? 0;
                    usage.CacheReadTokens += usageEvent.Data.CacheReadTokens ?? 0;
                    usage.CacheWriteTokens += usageEvent.Data.CacheWriteTokens ?? 0;
                    usage.ReasoningTokens += usageEvent.Data.ReasoningTokens ?? 0;
                    usage.DurationMs += (long)(usageEvent.Data.Duration?.TotalMilliseconds ?? 0);
#pragma warning disable GHCP001 // Cost is experimental but is exactly the metric we want to surface.
                    if (usageEvent.Data.Cost is { } cost)
                    {
                        usage.Cost = (usage.Cost ?? 0) + cost;
                    }
#pragma warning restore GHCP001
                    break;
                case SessionErrorEvent err:
                    sessionError = new InvalidOperationException(err.Data.Message);
                    idle.TrySetResult();
                    break;
                case SessionIdleEvent:
                    idle.TrySetResult();
                    break;
            }
        });

        await session.SendAsync(new MessageOptions { Prompt = userPrompt });

        await using (cancellationToken.Register(() => idle.TrySetCanceled(cancellationToken)))
        {
            await idle.Task;
        }

        if (sessionError is not null)
        {
            throw sessionError;
        }

        var response = responseBuilder.ToString();
        if (string.IsNullOrEmpty(response))
        {
            throw new InvalidOperationException("Copilot session returned no assistant message.");
        }

        return new ChatCompletionResult(response, string.IsNullOrEmpty(usage.Model) ? null : usage);
    }
}
