using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Zamba.Api.Configuration;

namespace Zamba.Api.Services;

public interface IEmbeddingClient
{
    Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken);
}

/// <summary>Calls the GitHub Models (Copilot) OpenAI-compatible embeddings endpoint.</summary>
public class GitHubModelsEmbeddingClient(HttpClient httpClient, IOptions<GitHubCopilotOptions> options) : IEmbeddingClient
{
    private readonly GitHubCopilotOptions _options = options.Value;

    public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken)
    {
        var request = new EmbeddingRequest(_options.EmbeddingModel, [text]);
        using var response = await httpClient.PostAsJsonAsync("embeddings", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<EmbeddingResponse>(cancellationToken)
            ?? throw new InvalidOperationException("Empty response from embeddings endpoint.");

        var vector = body.Data.FirstOrDefault()?.Embedding
            ?? throw new InvalidOperationException("Embeddings endpoint returned no data.");

        return vector;
    }

    private record EmbeddingRequest(
        [property: JsonPropertyName("model")] string Model,
        [property: JsonPropertyName("input")] string[] Input);

    private record EmbeddingResponse([property: JsonPropertyName("data")] List<EmbeddingData> Data);

    private record EmbeddingData([property: JsonPropertyName("embedding")] float[] Embedding);
}
