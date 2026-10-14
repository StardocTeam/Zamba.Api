using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;
using Zamba.Api.Configuration;
using Zamba.Api.Services;

namespace Zamba.Api.Tests;

[TestClass]
public sealed class TextChunkerTests
{
    [TestMethod]
    public void Chunk_PrefersSentenceBoundaries()
    {
        var text = "Primera frase con varias palabras. Segunda frase con varias palabras. Tercera frase.";

        var chunks = new TextChunker().Chunk(text, chunkSize: 45, overlap: 0);

        Assert.IsTrue(chunks[0].EndsWith('.'));
        Assert.AreEqual(text.Replace(" ", ""), string.Concat(chunks).Replace(" ", ""));
    }

    [TestMethod]
    public void Chunk_OverlapDoesNotBeginInsideAWord()
    {
        var chunks = new TextChunker().Chunk("abcdefghij klmnopqrst uvwxyz", chunkSize: 12, overlap: 4);

        Assert.AreEqual("abcdefghij", chunks[0]);
        Assert.AreEqual("klmnopqrst", chunks[1]);
        Assert.AreEqual("uvwxyz", chunks[2]);
    }

    [TestMethod]
    public async Task FindMostRelevantChunks_HybridRankingCanPromoteLexicalMatch()
    {
        var databasePath = Path.Combine(Path.GetTempPath(), $"zamba-vector-{Guid.NewGuid():N}.db");
        try
        {
            var store = new SqliteVectorStore(Options.Create(new VectorStoreOptions { DatabasePath = databasePath }));
            await store.SaveChunksAsync(
                "document-1",
                "contract.txt",
                [
                    new DocumentChunk(0, "A generic passage about unrelated administrative details.", [1f, 0f]),
                    new DocumentChunk(1, "El contrato establece indemnización por despido improcedente.", [0f, 1f])
                ],
                CancellationToken.None);

            var results = await store.FindMostRelevantChunksAsync(
                "document-1", [1f, 0f], "indemnización despido improcedente", 1, CancellationToken.None);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(1, results[0].Index);
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
        }
    }
}