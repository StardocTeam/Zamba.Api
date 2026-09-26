namespace Zamba.Api.Configuration;

public sealed class PdfOcrOptions
{
    public const string SectionName = "PdfOcr";

    public string PdfToPpmPath { get; set; } = "pdftoppm";
    public string TesseractPath { get; set; } = "tesseract";
    public string Language { get; set; } = "spa+eng";
    public int Dpi { get; set; } = 200;
    public int MaxPages { get; set; } = 100;
    public int TimeoutSeconds { get; set; } = 120;
}
