using System.ComponentModel.DataAnnotations;

namespace Zamba.Api.Models;

/// <summary>
/// Request to build a memory context from a file and execute a prompt against it.
/// </summary>
public class CopilotPromptRequest
{
    [Required]
    public string FileBase64 { get; set; } = string.Empty;

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string Prompt { get; set; } = string.Empty;
}
