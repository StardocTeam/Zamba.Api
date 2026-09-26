using System.ComponentModel.DataAnnotations;

namespace Zamba.Api.Models;

/// <summary>
/// Request to build a memory context from a file and execute a prompt against it. On the first call,
/// send FileBase64 and FileName; the response's DocumentId can then be passed as DocumentId on
/// subsequent calls to reuse the already indexed document without re-uploading the file.
/// </summary>
public class CopilotPromptRequest
{
    public string? FileBase64 { get; set; }

    public string? FileName { get; set; }

    /// <summary>Id of a previously indexed document, as returned in CopilotPromptResponse.DocumentId.</summary>
    public string? DocumentId { get; set; }

    public int? DocumentTypeCode { get; set; }

    [Required]
    public string Prompt { get; set; } = string.Empty;
}
