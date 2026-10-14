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
            var end = start + Math.Min(chunkSize, text.Length - start);
            if (end < text.Length)
            {
                end = FindBoundary(text, start, end);
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

            var nextStart = Math.Max(end - overlap, start + 1);
            while (nextStart < end && !char.IsWhiteSpace(text[nextStart]))
            {
                nextStart++;
            }

            while (nextStart < text.Length && char.IsWhiteSpace(text[nextStart]))
            {
                nextStart++;
            }

            start = Math.Max(nextStart, end);
        }

        return chunks;
    }

    private static int FindBoundary(string text, int start, int hardEnd)
    {
        var minimumBoundary = start + Math.Max(1, (hardEnd - start) * 2 / 3);
        var lastLineBreak = FindLastLineBreak(text, minimumBoundary, hardEnd);
        var lastSentenceEnd = FindLastSentenceEnd(text, minimumBoundary, hardEnd);
        var lastWhitespace = FindLastWhitespace(text, minimumBoundary, hardEnd);

        if (lastLineBreak > start)
        {
            return lastLineBreak;
        }

        if (lastSentenceEnd > start)
        {
            return lastSentenceEnd;
        }

        return lastWhitespace > start ? lastWhitespace : hardEnd;
    }

    private static int FindLastLineBreak(string text, int minimumBoundary, int hardEnd)
    {
        for (var index = hardEnd - 1; index >= minimumBoundary; index--)
        {
            if (text[index] is '\r' or '\n')
            {
                return index;
            }
        }

        return -1;
    }

    private static int FindLastSentenceEnd(string text, int minimumBoundary, int hardEnd)
    {
        for (var index = hardEnd - 1; index >= minimumBoundary; index--)
        {
            if (index + 1 < text.Length && char.IsWhiteSpace(text[index + 1]) && text[index] is '.' or '!' or '?')
            {
                return index + 1;
            }
        }

        return -1;
    }

    private static int FindLastWhitespace(string text, int minimumBoundary, int hardEnd)
    {
        for (var index = hardEnd - 1; index >= minimumBoundary; index--)
        {
            if (char.IsWhiteSpace(text[index]))
            {
                return index;
            }
        }

        return -1;
    }
}
