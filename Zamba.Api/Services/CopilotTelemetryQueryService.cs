using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface ICopilotTelemetryQueryService
{
    Task<CopilotTelemetrySummaryDto> GetSummaryAsync(
        DateTime? fromUtc, DateTime? toUtc, int? documentTypeCode, CancellationToken cancellationToken);

    Task<CopilotNegativeRunsPageDto> GetNegativeRunsAsync(
        DateTime? fromUtc,
        DateTime? toUtc,
        int? documentTypeCode,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<CopilotRunDetailDto?> GetRunDetailAsync(Guid runId, CancellationToken cancellationToken);
}

public sealed class CopilotTelemetryQueryService(ZambaDbContext dbContext) : ICopilotTelemetryQueryService
{
    private const double ProMonthlyPlanCostUsd = 20d;
    private const int ProMonthlyAiCredits = 1500;

    public async Task<CopilotTelemetrySummaryDto> GetSummaryAsync(
        DateTime? fromUtc, DateTime? toUtc, int? documentTypeCode, CancellationToken cancellationToken)
    {
        var runs = FilterRuns(fromUtc, toUtc, documentTypeCode);
        var feedback = dbContext.CopilotFeedback.AsNoTracking()
            .Where(item => runs.Any(run => run.Id == item.RunId));

        var filteredUsage = await GetUsageTotalsAsync(runs, cancellationToken);
        var nowUtc = DateTime.UtcNow;
        var monthStartUtc = new DateTime(nowUtc.Year, nowUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var monthUsage = await GetUsageTotalsAsync(
            FilterRuns(monthStartUtc, nowUtc, null), cancellationToken);
        var lifetimeUsage = await GetUsageTotalsAsync(
            FilterRuns(null, null, null), cancellationToken);
        var totalRuns = filteredUsage.RequestCount;
        var positiveVotes = await feedback.CountAsync(item => item.Rating > 0, cancellationToken);
        var negativeVotes = await feedback.CountAsync(item => item.Rating < 0, cancellationToken);
        var negativeRuns = await runs.CountAsync(run => run.Feedback.Any(item => item.Rating < 0), cancellationToken);
        var ratedRuns = await runs.CountAsync(run => run.Feedback.Any(), cancellationToken);
        var estimatedPlanCostPerRequestUsd = ProMonthlyPlanCostUsd / ProMonthlyAiCredits;

        return new CopilotTelemetrySummaryDto
        {
            TotalRuns = totalRuns,
            SucceededRuns = await runs.CountAsync(run => run.Status == "Succeeded", cancellationToken),
            FailedRuns = await runs.CountAsync(run => run.Status == "Failed", cancellationToken),
            RatedRuns = ratedRuns,
            NegativeRuns = negativeRuns,
            PositiveVotes = positiveVotes,
            NegativeVotes = negativeVotes,
            NegativeVotePercent = positiveVotes + negativeVotes == 0
                ? 0
                : 100d * negativeVotes / (positiveVotes + negativeVotes),
            NegativeRunPercent = totalRuns == 0 ? 0 : 100d * negativeRuns / totalRuns,
            AverageTotalDurationMs = await runs.Where(run => run.TotalDurationMs != null)
                .AverageAsync(run => (double?)run.TotalDurationMs, cancellationToken),
            AverageInputTokens = await runs.Where(run => run.InputTokens != null)
                .AverageAsync(run => (double?)run.InputTokens, cancellationToken),
            AverageEstimatedCost = await runs.Where(run => run.EstimatedCost != null)
                .AverageAsync(run => (double?)run.EstimatedCost, cancellationToken),
            TotalInputTokens = filteredUsage.InputTokens,
            TotalOutputTokens = filteredUsage.OutputTokens,
            TotalTokens = filteredUsage.TotalTokens,
            AverageTotalTokensPerRequest = totalRuns == 0 ? null : (double)filteredUsage.TotalTokens / totalRuns,
            EstimatedPlanCostPerRequestUsd = estimatedPlanCostPerRequestUsd,
            MonthRequestCount = monthUsage.RequestCount,
            MonthTotalTokens = monthUsage.TotalTokens,
            MonthEstimatedCostUsd = monthUsage.RequestCount * estimatedPlanCostPerRequestUsd,
            LifetimeRequestCount = lifetimeUsage.RequestCount,
            LifetimeTotalTokens = lifetimeUsage.TotalTokens,
            LifetimeEstimatedCostUsd = lifetimeUsage.RequestCount * estimatedPlanCostPerRequestUsd
        };
    }

    public async Task<CopilotNegativeRunsPageDto> GetNegativeRunsAsync(
        DateTime? fromUtc,
        DateTime? toUtc,
        int? documentTypeCode,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = FilterRuns(fromUtc, toUtc, documentTypeCode)
            .Where(run => run.Feedback.Any(item => item.Rating < 0));
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(run => run.Prompt.Contains(term)
                || (run.Response != null && run.Response.Contains(term))
                || (run.FileName != null && run.FileName.Contains(term)));
        }

        var totalRuns = await query.CountAsync(cancellationToken);
        var items = await query
            .AsNoTracking()
            .Include(run => run.Feedback)
            .OrderByDescending(run => run.StartedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new CopilotNegativeRunsPageDto
        {
            Page = page,
            PageSize = pageSize,
            TotalRuns = totalRuns,
            Items = items.Select(run => new CopilotNegativeRunDto
            {
                RunId = run.Id,
                StartedAtUtc = run.StartedAtUtc,
                Status = run.Status,
                DocumentTypeCode = run.DocumentTypeCode,
                FileName = run.FileName,
                PromptPreview = Preview(run.Prompt, 220),
                ResponsePreview = Preview(run.Response, 260),
                ChatModel = run.ChatModel,
                TotalDurationMs = run.TotalDurationMs,
                InputTokens = run.InputTokens,
                OutputTokens = run.OutputTokens,
                NegativeVoteCount = run.Feedback.Count(item => item.Rating < 0),
                LatestFeedbackComment = run.Feedback
                    .Where(item => item.Rating < 0)
                    .OrderByDescending(item => item.CreatedAtUtc)
                    .Select(item => item.Comment)
                    .FirstOrDefault(comment => !string.IsNullOrWhiteSpace(comment))
            }).ToList()
        };
    }

    public async Task<CopilotRunDetailDto?> GetRunDetailAsync(Guid runId, CancellationToken cancellationToken)
    {
        var run = await dbContext.CopilotRuns.AsNoTracking()
            .Include(item => item.Steps)
            .Include(item => item.Feedback)
            .SingleOrDefaultAsync(item => item.Id == runId, cancellationToken);
        if (run is null)
        {
            return null;
        }

        return new CopilotRunDetailDto
        {
            RunId = run.Id,
            StartedAtUtc = run.StartedAtUtc,
            CompletedAtUtc = run.CompletedAtUtc,
            Status = run.Status,
            Prompt = run.Prompt,
            Response = run.Response,
            FileName = run.FileName,
            DocumentId = run.DocumentId,
            DocumentTypeCode = run.DocumentTypeCode,
            SessionId = run.SessionId,
            EmbeddingProvider = run.EmbeddingProvider,
            ChatModel = run.ChatModel,
            DocumentAlreadyIndexed = run.DocumentAlreadyIndexed,
            ChunksIndexed = run.ChunksIndexed,
            ChunksUsed = run.ChunksUsed,
            EmbeddingDurationMs = run.EmbeddingDurationMs,
            RetrievalDurationMs = run.RetrievalDurationMs,
            ChatDurationMs = run.ChatDurationMs,
            TotalDurationMs = run.TotalDurationMs,
            InputTokens = run.InputTokens,
            OutputTokens = run.OutputTokens,
            CacheReadTokens = run.CacheReadTokens,
            CacheWriteTokens = run.CacheWriteTokens,
            EstimatedCost = run.EstimatedCost,
            ErrorMessage = run.ErrorMessage,
            Steps = run.Steps.OrderBy(step => step.StartedAtUtc).Select(step => new CopilotRunStepDto
            {
                Id = step.Id,
                Name = step.Name,
                Status = step.Status,
                StartedAtUtc = step.StartedAtUtc,
                CompletedAtUtc = step.CompletedAtUtc,
                DurationMs = step.DurationMs,
                Detail = step.Detail,
                ErrorMessage = step.ErrorMessage
            }).ToList(),
            Feedback = run.Feedback.OrderByDescending(item => item.CreatedAtUtc).Select(item => new CopilotFeedbackDto
            {
                Rating = item.Rating,
                Comment = item.Comment,
                CreatedAtUtc = item.CreatedAtUtc
            }).ToList()
        };
    }

    private IQueryable<CopilotRun> FilterRuns(DateTime? fromUtc, DateTime? toUtc, int? documentTypeCode)
    {
        var query = dbContext.CopilotRuns.AsNoTracking();
        if (fromUtc.HasValue)
        {
            query = query.Where(run => run.StartedAtUtc >= fromUtc.Value);
        }

        if (toUtc.HasValue)
        {
            query = query.Where(run => run.StartedAtUtc < toUtc.Value);
        }

        if (documentTypeCode.HasValue)
        {
            query = query.Where(run => run.DocumentTypeCode == documentTypeCode.Value);
        }

        return query;
    }

    private static async Task<UsageTotals> GetUsageTotalsAsync(
        IQueryable<CopilotRun> runs, CancellationToken cancellationToken)
    {
        var totals = await runs.GroupBy(_ => 1)
            .Select(group => new
            {
                RequestCount = group.Count(),
                InputTokens = group.Sum(run => run.InputTokens ?? 0L),
                OutputTokens = group.Sum(run => run.OutputTokens ?? 0L)
            })
            .SingleOrDefaultAsync(cancellationToken);

        return totals is null
            ? new UsageTotals(0, 0, 0)
            : new UsageTotals(totals.RequestCount, totals.InputTokens, totals.OutputTokens);
    }

    private static string Preview(string? value, int maxLength) =>
        string.IsNullOrWhiteSpace(value) || value.Length <= maxLength
            ? value ?? string.Empty
            : value[..maxLength] + "...";

    private sealed record UsageTotals(int RequestCount, long InputTokens, long OutputTokens)
    {
        public long TotalTokens => InputTokens + OutputTokens;
    }
}
