using Microsoft.AspNetCore.Mvc;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/copilot/telemetry")]
public sealed class CopilotTelemetryController(ICopilotTelemetryQueryService queryService) : ControllerBase
{
    [HttpGet("summary")]
    [ProducesResponseType(typeof(CopilotTelemetrySummaryDto), StatusCodes.Status200OK)]
    public Task<CopilotTelemetrySummaryDto> GetSummary(
        [FromQuery] DateTime? fromUtc,
        [FromQuery] DateTime? toUtc,
        [FromQuery] int? documentTypeCode,
        CancellationToken cancellationToken) =>
        queryService.GetSummaryAsync(fromUtc, toUtc, documentTypeCode, cancellationToken);

    [HttpGet("negative-runs")]
    [ProducesResponseType(typeof(CopilotNegativeRunsPageDto), StatusCodes.Status200OK)]
    public Task<CopilotNegativeRunsPageDto> GetNegativeRuns(
        [FromQuery] DateTime? fromUtc,
        [FromQuery] DateTime? toUtc,
        [FromQuery] int? documentTypeCode,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        return queryService.GetNegativeRunsAsync(
            fromUtc, toUtc, documentTypeCode, search, page, pageSize, cancellationToken);
    }

    [HttpGet("runs/{runId:guid}")]
    [ProducesResponseType(typeof(CopilotRunDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CopilotRunDetailDto>> GetRunDetail(
        Guid runId, CancellationToken cancellationToken)
    {
        var run = await queryService.GetRunDetailAsync(runId, cancellationToken);
        return run is null ? NotFound() : Ok(run);
    }
}
