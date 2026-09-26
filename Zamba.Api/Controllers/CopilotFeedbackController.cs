using Microsoft.AspNetCore.Mvc;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/copilot/feedback")]
public sealed class CopilotFeedbackController(ICopilotTelemetryService telemetryService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Submit(
        [FromBody] CopilotFeedbackRequest request, CancellationToken cancellationToken)
    {
        if (request.Rating is not (-1 or 1))
        {
            return BadRequest("Rating must be 1 (thumbs up) or -1 (thumbs down).");
        }

        var saved = await telemetryService.SaveFeedbackAsync(
            request.RunId, request.VoterId, request.Rating, request.Comment, cancellationToken);
        return saved ? NoContent() : NotFound("The Copilot run was not found or did not succeed.");
    }
}
