namespace Zamba.Api.Models;

/// <summary>
/// Result of executing a prompt against the memory context built from an uploaded file.
/// </summary>
public class CopilotPromptResponse
{
    public string Response { get; set; } = string.Empty;

    /// <summary>Copilot SDK session identifier for tracking conversation persistence.</summary>
    public string SessionId { get; set; } = string.Empty;

    /// <summary>Hash-based id of the file, used to cache/reuse embeddings across requests.</summary>
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>Number of document chunks fed into the prompt as context.</summary>
    public int ChunksUsed { get; set; }

    /// <summary>Timing/token/cost diagnostics for this request, to track and improve the pipeline over time.</summary>
    public PromptStats Stats { get; set; } = new();
}
