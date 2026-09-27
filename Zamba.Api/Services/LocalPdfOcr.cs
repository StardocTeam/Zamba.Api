using System.Text;
using Microsoft.Extensions.Options;
using OpenCvSharp;
using PDFtoImage;
using Sdcb.PaddleOCR;
using SkiaSharp;
using UglyToad.PdfPig;
using Zamba.Api.Configuration;

namespace Zamba.Api.Services;

public interface IPdfOcr
{
    Task<string?> ExtractTextAsync(byte[] pdfBytes, CancellationToken cancellationToken);
}

public sealed class PdfOcrException(string message, Exception? innerException = null)
    : Exception(message, innerException);

public sealed class LocalPdfOcr(IOptions<PdfOcrOptions> options, PaddleOcrAll ocr) : IPdfOcr
{
    private readonly PdfOcrOptions _options = options.Value;
    private readonly SemaphoreSlim _ocrGate = new(1, 1);

    public async Task<string?> ExtractTextAsync(byte[] pdfBytes, CancellationToken cancellationToken)
    {
        using var pdfStream = new MemoryStream(pdfBytes);
        int pageCount;
        try
        {
            using var pdf = PdfDocument.Open(pdfStream);
            pageCount = pdf.GetPages().Count();
        }
        catch (Exception ex)
        {
            throw new PdfOcrException("The PDF could not be opened for OCR.", ex);
        }

        if (pageCount == 0)
        {
            return null;
        }

        if (pageCount > _options.MaxPages)
        {
            throw new PdfOcrException($"The PDF has {pageCount} pages; the OCR limit is {_options.MaxPages} pages.");
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(Math.Max(1, _options.TimeoutSeconds)));

        try
        {
            var extractedText = new StringBuilder();
            using var stream = new MemoryStream(pdfBytes);
            var renderOptions = new RenderOptions(Dpi: _options.Dpi);
            await foreach (var bitmap in Conversion.ToImagesAsync(
                stream,
                Enumerable.Range(0, pageCount),
                options: renderOptions,
                cancellationToken: timeout.Token))
            {
                timeout.Token.ThrowIfCancellationRequested();
                using (bitmap)
                {
                    using var encodedImage = bitmap.Encode(SKEncodedImageFormat.Png, 100);
                    var image = Cv2.ImDecode(encodedImage.ToArray(), ImreadModes.Color);
                    if (image.Empty())
                    {
                        image.Dispose();
                        continue;
                    }

                    try
                    {
                        await _ocrGate.WaitAsync(timeout.Token);
                    }
                    catch
                    {
                        image.Dispose();
                        throw;
                    }

                    var inferenceTask = Task.Run(() => ocr.Run(image));
                    _ = inferenceTask.ContinueWith(completedTask =>
                    {
                        try
                        {
                            _ = completedTask.Exception;
                            image.Dispose();
                        }
                        finally
                        {
                            _ocrGate.Release();
                        }
                    }, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

                    var pageOcrResult = await inferenceTask.WaitAsync(timeout.Token);
                    extractedText.AppendLine(pageOcrResult.Text.Trim());
                }
            }

            var result = extractedText.ToString().Trim();
            return result.Length > 0 ? result : null;
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            throw new PdfOcrException($"Local PDF OCR exceeded the {_options.TimeoutSeconds}-second processing limit.", ex);
        }
    }
}
