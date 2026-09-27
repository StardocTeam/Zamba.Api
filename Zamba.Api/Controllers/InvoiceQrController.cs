using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/invoices")]
public sealed class InvoiceQrController(
    IInvoiceQrCodeExtractor qrCodeExtractor,
    IOptions<FileProcessingOptions> fileProcessingOptions) : ControllerBase
{
    private readonly long _maxFileSizeBytes = fileProcessingOptions.Value.MaxFileSizeBytes;

    /// <summary>Decodes QR codes found in an invoice image or PDF.</summary>
    [HttpPost("qr")]
    [ProducesResponseType(typeof(InvoiceQrCodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<InvoiceQrCodeResponse>> ExtractQr(
        [FromBody] InvoiceQrCodeRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FileBase64) || string.IsNullOrWhiteSpace(request.FileName))
        {
            return BadRequest("FileBase64 and FileName are required.");
        }

        var maxBase64Length = ((_maxFileSizeBytes + 2) / 3) * 4;
        if (request.FileBase64.Length > maxBase64Length)
        {
            return BadRequest($"File exceeds the maximum allowed size of {_maxFileSizeBytes} bytes.");
        }

        byte[] fileBytes;
        try
        {
            fileBytes = Convert.FromBase64String(request.FileBase64);
        }
        catch (FormatException)
        {
            return BadRequest("FileBase64 is not a valid base64 string.");
        }

        try
        {
            var qrCodes = await qrCodeExtractor.ExtractAsync(fileBytes, request.FileName, cancellationToken);
            return Ok(new InvoiceQrCodeResponse(request.FileName, qrCodes));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvoiceQrCodeException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
        }
    }
}