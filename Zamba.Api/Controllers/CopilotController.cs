using Microsoft.AspNetCore.Mvc;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CopilotController(IMemoryContextService memoryContextService, ILogger<CopilotController> logger) : ControllerBase
{
    /// <summary>
    /// Receives a base64-encoded file and a prompt, builds a memory context from the file
    /// (chunking + embeddings cached in a local vector store) and executes the prompt against
    /// the GitHub Copilot chat API using that context.
    /// </summary>
    [HttpPost("ask")]
    [ProducesResponseType(typeof(CopilotPromptResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<ActionResult<CopilotPromptResponse>> Ask(
        [FromBody] CopilotPromptRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FileBase64) ||
            string.IsNullOrWhiteSpace(request.FileName) ||
            string.IsNullOrWhiteSpace(request.Prompt))
        {
            return BadRequest("FileBase64, FileName and Prompt are all required.");
        }

        try
        {
            var result = await memoryContextService.AskAsync(request, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Failed to reach the GitHub Copilot API.");
            return StatusCode(StatusCodes.Status502BadGateway, "Failed to reach the GitHub Copilot API.");
        }
    }
}
