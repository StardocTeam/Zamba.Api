namespace Zamba.Api.Services;

public interface ITextChunker
{
    IReadOnlyList<string> Chunk(string text, int chunkSize, int overlap);
}

/// <summary>Splits text into overlapping, sentence/whitespace-aware chunks for embedding.</summary>
public class TextChunker : ITextChunker
{
    public IReadOnlyList<string> Chunk(string text, int chunkSize, int overlap)
    {
        var chunks = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
        {
            return chunks;
        }

        if (chunkSize <= 0)
        {
            chunks.Add(text);
            return chunks;
        }

        overlap = Math.Clamp(overlap, 0, chunkSize - 1);
        var start = 0;
        while (start < text.Length)
        {
            var length = Math.Min(chunkSize, text.Length - start);
            var end = start + length;

            // Try to break on a whitespace boundary instead of mid-word, when not at the end of the text.
            if (end < text.Length)
            {
                var lastWhitespace = text.LastIndexOf(' ', end - 1, length);
                if (lastWhitespace > start)
                {
                    end = lastWhitespace;
                }
            }

            var chunk = text[start..end].Trim();
            if (chunk.Length > 0)
            {
                chunks.Add(chunk);
            }

            if (end >= text.Length)
            {
                break;
            }

            start = Math.Max(end - overlap, start + 1);
        }

        return chunks;
    }
}
