namespace Zamba.Api.Configuration;

/// <summary>
/// Settings for calling the GitHub Copilot / GitHub Models inference API.
/// </summary>
public class GitHubCopilotOptions
{
    public const string SectionName = "GitHubCopilot";

    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ChatModel { get; set; } = string.Empty;
    public string EmbeddingModel { get; set; } = string.Empty;

    /// <summary>"Local" (default, offline hashing-trick embeddings) or "GitHubModels" (hosted API).</summary>
    public string EmbeddingProvider { get; set; } = "Local";
}
