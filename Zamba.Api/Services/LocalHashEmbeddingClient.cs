using System.Security.Cryptography;
using System.Text;

namespace Zamba.Api.Services;

/// <summary>
/// Fully offline <see cref="IEmbeddingClient"/> using the hashing trick over word tokens, so no
/// external API call (and no API key) is required. Vectors are deterministic and L2-normalized,
/// but have lower semantic quality than a hosted embedding model - use only as a fallback/dev option.
/// </summary>
public class LocalHashEmbeddingClient : IEmbeddingClient
{
    private const int Dimensions = 384;

    public Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken)
    {
        var vector = new float[Dimensions];

        foreach (var token in Tokenize(text))
        {
            var hash = Hash(token);
            var bucket = (int)(hash % Dimensions);
            var sign = (hash & 1) == 0 ? 1f : -1f;
            vector[bucket] += sign;
        }

        Normalize(vector);
        return Task.FromResult(vector);
    }

    private static IEnumerable<string> Tokenize(string text) =>
        text.ToLowerInvariant().Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);

    private static uint Hash(string token) =>
        BitConverter.ToUInt32(SHA256.HashData(Encoding.UTF8.GetBytes(token)), 0);

    private static void Normalize(float[] vector)
    {
        var magnitude = MathF.Sqrt(vector.Sum(v => v * v));
        if (magnitude <= 0f)
        {
            return;
        }

        for (var i = 0; i < vector.Length; i++)
        {
            vector[i] /= magnitude;
        }
    }
}
