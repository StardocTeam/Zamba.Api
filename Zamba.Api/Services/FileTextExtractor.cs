using System.Text;
using UglyToad.PdfPig;

namespace Zamba.Api.Services;

public interface IFileTextExtractor
{
    Task<string?> ExtractTextAsync(byte[] fileBytes, string fileName, CancellationToken cancellationToken);
}

public sealed class FileTextExtractor(IPdfOcr pdfOcr) : IFileTextExtractor
{
    public async Task<string?> ExtractTextAsync(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    {
        if (fileBytes.Length == 0)
        {
            return null;
        }

        if (IsPdf(fileBytes, fileName))
        {
            var text = TryExtractPdfText(fileBytes);
            return string.IsNullOrWhiteSpace(text)
                ? await pdfOcr.ExtractTextAsync(fileBytes, cancellationToken)
                : text;
        }

        try
        {
            // Strict UTF-8 decoding: throws if the content is not valid text (e.g. images, zips).
            var strictUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            var text = strictUtf8.GetString(fileBytes).Trim();
            return text.Length > 0 ? text : null;
        }
        catch (DecoderFallbackException)
        {
            return null;
        }
    }

    private static bool IsPdf(byte[] fileBytes, string fileName)
    {
        return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
               (fileBytes.Length >= 4 && fileBytes[0] == '%' && fileBytes[1] == 'P' && fileBytes[2] == 'D' && fileBytes[3] == 'F');
    }

    private static string? TryExtractPdfText(byte[] fileBytes)
    {
        try
        {
            using var stream = new MemoryStream(fileBytes);
            using var document = PdfDocument.Open(stream);

            var builder = new StringBuilder();
            foreach (var page in document.GetPages())
            {
                builder.AppendLine(page.Text);
            }

            var text = builder.ToString().Trim();
            return text.Length > 0 ? text : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
