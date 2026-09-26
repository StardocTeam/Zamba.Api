using System.ComponentModel.DataAnnotations;

namespace Zamba.Api.Models;

public sealed class CopilotFeedbackRequest
{
    [Required]
    public Guid RunId { get; set; }

    [Required]
    [MaxLength(128)]
    public string VoterId { get; set; } = string.Empty;

    [Range(-1, 1)]
    public int Rating { get; set; }

    [MaxLength(2000)]
    public string? Comment { get; set; }
}
