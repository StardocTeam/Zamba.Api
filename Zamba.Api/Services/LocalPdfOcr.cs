using System.Diagnostics;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UglyToad.PdfPig;
using Zamba.Api.Configuration;

namespace Zamba.Api.Services;

public interface IPdfOcr
{
    Task<string?> ExtractTextAsync(byte[] pdfBytes, CancellationToken cancellationToken);
}

public sealed class PdfOcrException(string message, Exception? innerException = null)
    : Exception(message, innerException);

public sealed class LocalPdfOcr(IOptions<PdfOcrOptions> options, ILogger<LocalPdfOcr> logger) : IPdfOcr
{
    private readonly PdfOcrOptions _options = options.Value;

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

        var temporaryDirectory = Path.Combine(Path.GetTempPath(), $"zamba-ocr-{Guid.NewGuid():N}");
        Directory.CreateDirectory(temporaryDirectory);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(Math.Max(1, _options.TimeoutSeconds)));

        try
        {
            var pdfPath = Path.Combine(temporaryDirectory, "input.pdf");
            var imagePrefix = Path.Combine(temporaryDirectory, "page");
            await File.WriteAllBytesAsync(pdfPath, pdfBytes, timeout.Token);

            var renderResult = await RunProcessAsync(
                _options.PdfToPpmPath,
                ["-f", "1", "-l", pageCount.ToString(), "-r", _options.Dpi.ToString(), "-png", pdfPath, imagePrefix],
                timeout.Token);
            if (renderResult.ExitCode != 0)
            {
                throw new PdfOcrException($"PDF page rendering failed: {renderResult.StandardError.Trim()}");
            }

            var pageImages = Directory.GetFiles(temporaryDirectory, "page-*.png")
                .OrderBy(GetPageNumber)
                .ToArray();
            if (pageImages.Length == 0)
            {
                throw new PdfOcrException("PDF page rendering produced no images.");
            }

            var extractedText = new StringBuilder();
            foreach (var imagePath in pageImages)
            {
                timeout.Token.ThrowIfCancellationRequested();
                var ocrResult = await RunProcessAsync(
                    _options.TesseractPath,
                    [imagePath, "stdout", "-l", _options.Language, "--psm", "3"],
                    timeout.Token);
                if (ocrResult.ExitCode != 0)
                {
                    throw new PdfOcrException($"Tesseract OCR failed: {ocrResult.StandardError.Trim()}");
                }

                extractedText.AppendLine(ocrResult.StandardOutput.Trim());
            }

            var result = extractedText.ToString().Trim();
            return result.Length > 0 ? result : null;
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            throw new PdfOcrException($"Local PDF OCR exceeded the {_options.TimeoutSeconds}-second processing limit.", ex);
        }
        catch (Win32Exception ex)
        {
            throw new PdfOcrException(
                $"Local PDF OCR requires Poppler ('{_options.PdfToPpmPath}') and Tesseract ('{_options.TesseractPath}') to be installed and configured.", ex);
        }
        finally
        {
            try
            {
                Directory.Delete(temporaryDirectory, recursive: true);
            }
            catch (IOException ex)
            {
                logger.LogWarning(ex, "Unable to remove temporary OCR directory {DirectoryPath}.", temporaryDirectory);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogWarning(ex, "Unable to remove temporary OCR directory {DirectoryPath} due to access restrictions.", temporaryDirectory);
            }
        }
    }

    private static async Task<(int ExitCode, string StandardOutput, string StandardError)> RunProcessAsync(
        string executable, IReadOnlyList<string> arguments, CancellationToken cancellationToken)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = executable,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = new Process { StartInfo = startInfo };
        if (!process.Start())
        {
            throw new PdfOcrException($"Could not start OCR executable '{executable}'.");
        }

        using var registration = cancellationToken.Register(() =>
        {
            try
            {
                process.Kill(entireProcessTree: true);
            }
            catch (InvalidOperationException)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        });

        var standardOutputTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var standardErrorTask = process.StandardError.ReadToEndAsync(cancellationToken);
        await process.WaitForExitAsync(cancellationToken);
        return (process.ExitCode, await standardOutputTask, await standardErrorTask);
    }

    private static int GetPageNumber(string imagePath)
    {
        var match = Regex.Match(Path.GetFileNameWithoutExtension(imagePath), @"-(\d+)$");
        return match.Success && int.TryParse(match.Groups[1].Value, out var pageNumber)
            ? pageNumber
            : int.MaxValue;
    }
}
