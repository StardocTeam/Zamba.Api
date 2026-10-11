using Microsoft.AspNetCore.Mvc;
using Zamba.Api.DTOs;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/entity-rag-indexing")]
[Produces("application/json")]
public sealed class EntityRagIndexingController(IEntityRagIndexingService indexingService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EntityRagIndexingConfigDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<EntityRagIndexingConfigDto>>> GetConfigurations(CancellationToken cancellationToken) =>
        Ok(await indexingService.GetConfigurationsAsync(cancellationToken));

    [HttpGet("{documentTypeId:decimal}")]
    public async Task<ActionResult<EntityRagIndexingConfigDto>> GetConfiguration(decimal documentTypeId, CancellationToken cancellationToken)
    {
        var config = await indexingService.GetConfigurationAsync(documentTypeId, cancellationToken);
        return config is null ? NotFound() : Ok(config);
    }

    [HttpPut("{documentTypeId:decimal}")]
    [ProducesResponseType(typeof(EntityRagIndexingConfigDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EntityRagIndexingConfigDto>> SaveConfiguration(
        decimal documentTypeId,
        [FromBody] UpdateEntityRagIndexingConfigDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await indexingService.SaveConfigurationAsync(documentTypeId, dto, cancellationToken));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new ProblemDetails { Title = "Document type unavailable", Detail = exception.Message, Status = StatusCodes.Status404NotFound });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ProblemDetails { Title = "Invalid indexing configuration", Detail = exception.Message, Status = StatusCodes.Status400BadRequest });
        }
    }

    [HttpDelete("{documentTypeId:decimal}")]
    public async Task<IActionResult> DeleteConfiguration(decimal documentTypeId, CancellationToken cancellationToken) =>
        await indexingService.DeleteConfigurationAsync(documentTypeId, cancellationToken) ? NoContent() : NotFound();

    [HttpPost("{documentTypeId:decimal}/run")]
    public async Task<ActionResult<EntityRagIndexingRunDto>> RunBatch(decimal documentTypeId, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await indexingService.RunBatchAsync(documentTypeId, cancellationToken));
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Batch indexing unavailable", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpPost("{documentTypeId:decimal}/documents/{documentId:decimal}/index")]
    public async Task<ActionResult<EntityRagIndexingRunDto>> IndexInsertedDocument(
        decimal documentTypeId,
        decimal documentId,
        CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await indexingService.IndexInsertedDocumentAsync(documentTypeId, documentId, cancellationToken));
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new ProblemDetails { Title = "Insert-time indexing unavailable", Detail = exception.Message, Status = StatusCodes.Status409Conflict });
        }
    }
}