namespace Zamba.Api.Configuration;

public class FileProcessingOptions
{
    public const string SectionName = "FileProcessing";

    public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;
}
