using System.Text;
using UglyToad.PdfPig;

namespace Zamba.Api.Services;

public interface IFileTextExtractor
{
    /// <summary>Attempts to extract text from the file bytes. Returns false for unsupported/empty binary files.</summary>
    bool TryExtractText(byte[] fileBytes, string fileName, out string text);
}

public class FileTextExtractor : IFileTextExtractor
{
    public bool TryExtractText(byte[] fileBytes, string fileName, out string text)
    {
        text = string.Empty;
        if (fileBytes.Length == 0)
        {
            return false;
        }

        if (IsPdf(fileBytes, fileName))
        {
            return TryExtractPdfText(fileBytes, out text);
        }

        try
        {
            // Strict UTF-8 decoding: throws if the content is not valid text (e.g. images, zips).
            var strictUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            text = strictUtf8.GetString(fileBytes).Trim();
            return text.Length > 0;
        }
        catch (DecoderFallbackException)
        {
            return false;
        }
    }

    private static bool IsPdf(byte[] fileBytes, string fileName)
    {
        return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
               (fileBytes.Length >= 4 && fileBytes[0] == '%' && fileBytes[1] == 'P' && fileBytes[2] == 'D' && fileBytes[3] == 'F');
    }

    private static bool TryExtractPdfText(byte[] fileBytes, out string text)
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

            text = builder.ToString().Trim();
            return text.Length > 0;
        }
        catch (Exception)
        {
            text = string.Empty;
            return false;
        }
    }
}
