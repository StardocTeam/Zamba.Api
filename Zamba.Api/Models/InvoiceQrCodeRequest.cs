using System.ComponentModel.DataAnnotations;

namespace Zamba.Api.Models;

public sealed class InvoiceQrCodeRequest
{
    [Required]
    public string FileBase64 { get; init; } = string.Empty;

    [Required]
    public string FileName { get; init; } = string.Empty;
}

public sealed record InvoiceQrCodeData(int Page, string Format, string Content);

public sealed record InvoiceQrCodeResponse(string FileName, IReadOnlyList<InvoiceQrCodeData> QrCodes);