# `/api/copilot/ask` Flow

End-to-end flow of the memory-context pipeline: an uploaded file is chunked, embedded, cached,
and used to ground a prompt sent through the GitHub Copilot SDK.

## C4 - Context

```mermaid
C4Context
    title Zamba.Api - System Context

    Person(user, "Zamba user", "Uploads a file and asks questions about it")
    System(zambaApi, "Zamba.Api", "ASP.NET Core API that builds a memory context from a file and answers prompts")
    System_Ext(zambaAngular, "zamba-angular", "Frontend consuming Zamba.Api")
    System_Ext(copilotRuntime, "GitHub Copilot runtime", "Agentic CLI runtime (Copilot SDK) providing chat completions")

    Rel(user, zambaAngular, "Uses")
    Rel(zambaAngular, zambaApi, "POST /api/copilot/ask", "HTTPS/JSON")
    Rel(zambaApi, copilotRuntime, "Chat completion requests", "Copilot SDK (stdio)")
```

## C4 - Container

```mermaid
C4Container
    title Zamba.Api - Containers

    Person(user, "Zamba user")
    System_Boundary(zambaApi, "Zamba.Api") {
        Container(controller, "CopilotController", "ASP.NET Core Controller", "Validates requests, maps errors to HTTP status codes")
        Container(mcs, "MemoryContextService", ".NET Service", "Orchestrates extraction, chunking, embedding, retrieval and chat")
        Container(embedding, "IEmbeddingClient", ".NET Service", "LocalHashEmbeddingClient (offline) or GitHubModelsEmbeddingClient (retired)")
        Container(vectorStore, "SqliteVectorStore", ".NET Service", "Caches document chunk embeddings")
        Container(chatClient, "CopilotSdkChatClient", ".NET Service", "Sends prompts through the Copilot SDK session")
        Container(clientProvider, "CopilotSdkClientProvider", ".NET Service", "Owns the shared CopilotClient runtime process")
        ContainerDb(sqlite, "vectorstore.db", "SQLite file", "Persisted chunk embeddings, keyed by document hash")
    }
    System_Ext(copilotRuntime, "GitHub Copilot runtime", "Agentic CLI runtime (Copilot SDK)")
    System_Ext(zambaAngular, "zamba-angular", "Frontend")

    Rel(user, zambaAngular, "Uses")
    Rel(zambaAngular, controller, "POST /api/copilot/ask", "HTTPS/JSON")
    Rel(controller, mcs, "AskAsync()")
    Rel(mcs, embedding, "GetEmbeddingAsync()")
    Rel(mcs, vectorStore, "Save/FindMostRelevantChunksAsync()")
    Rel(vectorStore, sqlite, "Reads/writes")
    Rel(mcs, chatClient, "GetChatCompletionAsync()")
    Rel(chatClient, clientProvider, "GetClientAsync()")
    Rel(clientProvider, copilotRuntime, "Spawns/talks to", "stdio")
```

## Request flow

```mermaid
flowchart TD
    Client["Client<br/>(e.g. zamba-angular)"] -->|"POST /api/copilot/ask<br/>{fileBase64, fileName, prompt}"| Controller["CopilotController.Ask()"]

    Controller --> MCS["MemoryContextService.AskAsync()"]

    subgraph Pipeline["Memory-context pipeline"]
        MCS --> Hash["ComputeDocumentId()<br/>SHA-256 of file bytes"]
        Hash --> CacheCheck{"vectorStore.<br/>DocumentExistsAsync()?"}

        CacheCheck -- "No (not indexed yet)" --> Extract["FileTextExtractor<br/>.TryExtractText()"]
        Extract --> Chunk["TextChunker.Chunk()<br/>ChunkSize / ChunkOverlap"]
        Chunk --> Embed["IEmbeddingClient.GetEmbeddingAsync()<br/>per chunk"]
        Embed --> Save["SqliteVectorStore<br/>.SaveChunksAsync()"]
        Save --> Retrieve

        CacheCheck -- "Yes (cache hit)" --> Retrieve["RetrieveRelevantChunksAsync():<br/>embed prompt + FindMostRelevantChunksAsync()<br/>(cosine similarity, TopK)"]

        Retrieve --> SysCtx["BuildSystemContext()<br/>excerpts inserted as system message"]
    end

    SysCtx --> Chat["ICopilotChatClient<br/>.GetChatCompletionAsync()"]

    subgraph SDK["Copilot SDK runtime"]
        Chat --> Provider["CopilotSdkClientProvider<br/>(shared CopilotClient, BaseDirectory=.copilot-home)"]
        Provider --> Session["CopilotClient.CreateSessionAsync()<br/>Model, SystemMessage, GitHubToken"]
        Session --> Send["session.SendAsync(prompt)"]
        Send --> Events["Session events:<br/>AssistantMessageEvent (text)<br/>AssistantUsageEvent (tokens/cost/duration)<br/>SessionIdleEvent / SessionErrorEvent"]
    end

    Events --> Result["ChatCompletionResult<br/>{Response, ChatUsage}"]
    Result --> Stats["MemoryContextService assembles<br/>PromptStats (timings, tokens, cache hit)"]
    Stats --> Response["CopilotPromptResponse<br/>{Response, DocumentId, ChunksUsed, Stats}"]
    Response --> Client

    subgraph Storage["Local persistence"]
        VS[("SQLite vector store<br/>Data/vectorstore.db")]
    end
    Save -.-> VS
    Retrieve -.-> VS

    subgraph EmbedChoice["IEmbeddingClient (config: GitHubCopilot:EmbeddingProvider)"]
        Local["LocalHashEmbeddingClient<br/>(default, offline hashing trick)"]
        GH["GitHubModelsEmbeddingClient<br/>(retired, dead code path)"]
    end
    Embed -.-> EmbedChoice
    Retrieve -.-> EmbedChoice
```

## Key components

| Component | Role |
|---|---|
| `Controllers/CopilotController.cs` | HTTP entry point, validates request, maps errors to 400/502 |
| `Services/MemoryContextService.cs` | Orchestrates hashing → extraction → chunking → embedding → retrieval → chat, and times each phase |
| `Services/FileTextExtractor.cs` | Extracts raw text from the uploaded file (e.g. PDF) |
| `Services/TextChunker.cs` | Splits text into overlapping chunks (`VectorStoreOptions`) |
| `Services/LocalHashEmbeddingClient.cs` | Default offline embedding (hashing trick), since GitHub Models is retired |
| `Services/GitHubModelsEmbeddingClient.cs` | Legacy hosted embedding client (only used if `EmbeddingProvider: GitHubModels`) |
| `Services/SqliteVectorStore.cs` | Persists/looks up chunk embeddings per document hash, caches across requests |
| `Services/CopilotSdkClientProvider.cs` | Lazily starts/shares one `CopilotClient` runtime process for the app's lifetime |
| `Services/GitHubModelsChatClient.cs` (`CopilotSdkChatClient`) | Creates a session per request, sends the prompt, collects the assistant's answer + usage stats |
| `Models/PromptStats.cs` / `Models/ChatUsage.cs` | Carry timing, token, cache-hit, and cost diagnostics back in the response |
