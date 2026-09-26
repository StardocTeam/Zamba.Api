using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface ICopilotTelemetryService
{
    Task<CopilotRun> StartRunAsync(CopilotPromptRequest request, CancellationToken cancellationToken);
    Task<CopilotRunStep> StartStepAsync(Guid runId, string name, CancellationToken cancellationToken);
    Task CompleteStepAsync(CopilotRunStep step, string? detail, CancellationToken cancellationToken);
    Task FailStepAsync(CopilotRunStep step, Exception exception, CancellationToken cancellationToken);
    Task CompleteRunAsync(CopilotRun run, CopilotPromptResponse response, CancellationToken cancellationToken);
    Task FailRunAsync(CopilotRun run, Exception exception, long durationMs, CancellationToken cancellationToken);
    Task<bool> SaveFeedbackAsync(Guid runId, string voterId, int rating, string? comment, CancellationToken cancellationToken);
}

public sealed class CopilotTelemetryService(ZambaDbContext dbContext) : ICopilotTelemetryService
{
    public async Task<CopilotRun> StartRunAsync(CopilotPromptRequest request, CancellationToken cancellationToken)
    {
        var run = new CopilotRun
        {
            Id = Guid.NewGuid(),
            StartedAtUtc = DateTime.UtcNow,
            Status = "Running",
            Prompt = request.Prompt,
            FileName = request.FileName,
            DocumentId = request.DocumentId,
            DocumentTypeCode = request.DocumentTypeCode
        };
        dbContext.CopilotRuns.Add(run);
        await dbContext.SaveChangesAsync(cancellationToken);
        return run;
    }

    public async Task<CopilotRunStep> StartStepAsync(Guid runId, string name, CancellationToken cancellationToken)
    {
        var step = new CopilotRunStep
        {
            RunId = runId,
            Name = name,
            Status = "Running",
            StartedAtUtc = DateTime.UtcNow
        };
        dbContext.CopilotRunSteps.Add(step);
        await dbContext.SaveChangesAsync(cancellationToken);
        return step;
    }

    public async Task CompleteStepAsync(CopilotRunStep step, string? detail, CancellationToken cancellationToken)
    {
        step.Status = "Succeeded";
        step.CompletedAtUtc = DateTime.UtcNow;
        step.DurationMs = (long)(step.CompletedAtUtc.Value - step.StartedAtUtc).TotalMilliseconds;
        step.Detail = Truncate(detail, 4000);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task FailStepAsync(CopilotRunStep step, Exception exception, CancellationToken cancellationToken)
    {
        step.Status = "Failed";
        step.CompletedAtUtc = DateTime.UtcNow;
        step.DurationMs = (long)(step.CompletedAtUtc.Value - step.StartedAtUtc).TotalMilliseconds;
        step.ErrorMessage = Truncate(exception.ToString(), 4000);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CompleteRunAsync(CopilotRun run, CopilotPromptResponse response, CancellationToken cancellationToken)
    {
        run.Status = "Succeeded";
        run.CompletedAtUtc = DateTime.UtcNow;
        run.Response = response.Response;
        run.DocumentId = response.DocumentId;
        run.SessionId = response.SessionId;
        run.DocumentAlreadyIndexed = response.Stats.DocumentAlreadyIndexed;
        run.EmbeddingProvider = response.Stats.EmbeddingProvider;
        run.ChatModel = response.Stats.ChatModel;
        run.ChunksIndexed = response.Stats.ChunksIndexed;
        run.ChunksUsed = response.ChunksUsed;
        run.EmbeddingDurationMs = response.Stats.EmbeddingDurationMs;
        run.RetrievalDurationMs = response.Stats.RetrievalDurationMs;
        run.ChatDurationMs = response.Stats.ChatDurationMs;
        run.TotalDurationMs = response.Stats.TotalDurationMs;
        run.InputTokens = response.Stats.InputTokens;
        run.OutputTokens = response.Stats.OutputTokens;
        run.CacheReadTokens = response.Stats.CacheReadTokens;
        run.CacheWriteTokens = response.Stats.CacheWriteTokens;
        run.EstimatedCost = response.Stats.EstimatedCost;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task FailRunAsync(CopilotRun run, Exception exception, long durationMs, CancellationToken cancellationToken)
    {
        run.Status = "Failed";
        run.CompletedAtUtc = DateTime.UtcNow;
        run.TotalDurationMs = durationMs;
        run.ErrorMessage = Truncate(exception.ToString(), 4000);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveFeedbackAsync(
        Guid runId, string voterId, int rating, string? comment, CancellationToken cancellationToken)
    {
        if (!await dbContext.CopilotRuns.AnyAsync(run => run.Id == runId && run.Status == "Succeeded", cancellationToken))
        {
            return false;
        }

        var feedback = await dbContext.CopilotFeedback
            .SingleOrDefaultAsync(item => item.RunId == runId && item.VoterId == voterId, cancellationToken);
        if (feedback is null)
        {
            feedback = new CopilotFeedback { RunId = runId, VoterId = voterId };
            dbContext.CopilotFeedback.Add(feedback);
        }

        feedback.Rating = rating;
        feedback.Comment = Truncate(comment, 2000);
        feedback.CreatedAtUtc = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static string? Truncate(string? value, int length) =>
        value is { Length: > 0 } && value.Length > length ? value[..length] : value;
}
