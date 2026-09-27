using Microsoft.Extensions.Options;
using PDFtoImage;
using SkiaSharp;
using UglyToad.PdfPig;
using Zamba.Api.Configuration;
using Zamba.Api.Models;
using ZXing;
using ZXing.SkiaSharp;

namespace Zamba.Api.Services;

public interface IInvoiceQrCodeExtractor
{
    Task<IReadOnlyList<InvoiceQrCodeData>> ExtractAsync(
        byte[] fileBytes, string fileName, CancellationToken cancellationToken);
}

public sealed class InvoiceQrCodeException(string message, Exception? innerException = null)
    : Exception(message, innerException);

public sealed class InvoiceQrCodeExtractor(
    IOptions<FileProcessingOptions> fileOptions,
    IOptions<PdfOcrOptions> pdfOptions) : IInvoiceQrCodeExtractor
{
    private readonly FileProcessingOptions _fileOptions = fileOptions.Value;
    private readonly PdfOcrOptions _pdfOptions = pdfOptions.Value;

    public async Task<IReadOnlyList<InvoiceQrCodeData>> ExtractAsync(
        byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    {
        if (fileBytes.Length == 0)
        {
            throw new ArgumentException("The uploaded file is empty.");
        }

        if (fileBytes.LongLength > _fileOptions.MaxFileSizeBytes)
        {
            throw new ArgumentException(
                $"File exceeds the maximum allowed size of {_fileOptions.MaxFileSizeBytes} bytes.");
        }

        return IsPdf(fileBytes, fileName)
            ? await ExtractFromPdfAsync(fileBytes, cancellationToken)
            : ExtractFromImage(fileBytes);
    }

    private async Task<IReadOnlyList<InvoiceQrCodeData>> ExtractFromPdfAsync(
        byte[] pdfBytes, CancellationToken cancellationToken)
    {
        int pageCount;
        try
        {
            using var stream = new MemoryStream(pdfBytes);
            using var document = PdfDocument.Open(stream);
            pageCount = document.GetPages().Count();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("The uploaded PDF could not be opened.", ex);
        }

        if (pageCount == 0)
        {
            return [];
        }

        if (pageCount > _pdfOptions.MaxPages)
        {
            throw new ArgumentException($"The PDF has {pageCount} pages; the limit is {_pdfOptions.MaxPages} pages.");
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(Math.Max(1, _pdfOptions.TimeoutSeconds)));

        try
        {
            var qrCodes = new List<InvoiceQrCodeData>();
            using var stream = new MemoryStream(pdfBytes);
            var options = new RenderOptions(Dpi: _pdfOptions.Dpi);
            var pageNumber = 0;
            await foreach (var bitmap in Conversion.ToImagesAsync(
                stream,
                Enumerable.Range(0, pageCount),
                options: options,
                cancellationToken: timeout.Token))
            {
                using (bitmap)
                {
                    qrCodes.AddRange(Decode(bitmap, ++pageNumber));
                }
            }

            return qrCodes;
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            throw new InvoiceQrCodeException(
                $"Invoice QR extraction exceeded the {_pdfOptions.TimeoutSeconds}-second processing limit.", ex);
        }
    }

    private static IReadOnlyList<InvoiceQrCodeData> ExtractFromImage(byte[] imageBytes)
    {
        using var bitmap = SKBitmap.Decode(imageBytes);
        if (bitmap is null)
        {
            throw new ArgumentException("The uploaded file must be a supported image or PDF.");
        }

        return Decode(bitmap, 1);
    }

    private static IReadOnlyList<InvoiceQrCodeData> Decode(SKBitmap bitmap, int page)
    {
        var reader = new BarcodeReader();
        reader.Options.PossibleFormats = [BarcodeFormat.QR_CODE];
        reader.Options.TryHarder = true;
        var result = reader.Decode(bitmap);
        return result is null
            ? []
            : [new InvoiceQrCodeData(page, result.BarcodeFormat.ToString(), result.Text)];
    }

    private static bool IsPdf(byte[] bytes, string fileName) =>
        fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
        (bytes.Length >= 4 && bytes[0] == '%' && bytes[1] == 'P' && bytes[2] == 'D' && bytes[3] == 'F');

}