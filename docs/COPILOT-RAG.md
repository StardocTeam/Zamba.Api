# Copilot document RAG

## Request flow

`POST /api/copilot/ask` accepts `documentTypeCode` together with either `fileBase64`/`fileName` or a previously returned `documentId`. The API extracts text, splits it into overlapping chunks, creates local deterministic embeddings, and stores the chunks in SQLite using the uploaded file's SHA-256 as its document ID. A repeated upload reuses the index. Each prompt embeds the query, retrieves candidates for that document, applies a lexical/vector reranking pass, and sends only the selected excerpts plus the user's request and document-type instructions to the Copilot SDK. The original PDF is never attached to the SDK.

The chat SDK creates an isolated session per prompt with tools disabled. This prevents another request's session history from crossing document or document-type boundaries. `VectorStore:ChunkSize`, `ChunkOverlap`, and `TopK` bound the retrieved context and help control chat token usage. The default local hash embedding implementation makes no embedding-provider network calls.

## Scanned PDFs

Text PDFs continue to use PdfPig. If a PDF has no extractable text, the API renders its pages in memory with PDFium and runs PaddleOCR's Latin V5 model. The OCR engine, model, image-processing runtime, and Windows native dependencies are NuGet dependencies of `Zamba.Api`; no Poppler/Tesseract executables or server-side installers are required. OCR limits are configured under `PdfOcr` in `appsettings.json`:

The invoice QR endpoint (`POST /api/invoices/qr`) also renders PDF pages in memory with PDFium through the `PDFtoImage` NuGet package.

- `Dpi`: rasterization resolution.
- `MaxPages`: maximum PDF pages accepted for OCR.
- `TimeoutSeconds`: overall OCR processing deadline.

The Latin V5 recognizer covers Latin-script text including Spanish and English. The API rejects PDFs above the configured page limit.

## Document skills

Add a UTF-8 Markdown file named `DocumentSkills/<documentTypeCode>.md`. These files are embedded into the API assembly and loaded for each prompt, so deployment does not depend on the current working directory. Existing examples:

- `26.md`: invoice instructions.
- `2524.md`: legal-pleading instructions.

Unknown or omitted document codes use the general RAG instructions without a type-specific skill. The Angular custom element exposes `documentTypeCode`; the demo page has a selector for the two example codes.

## Scale considerations

The current SQLite store is local and simple to operate, and it avoids sending documents or embeddings to a hosted vector service. Retrieval computes exact cosine similarity over every chunk belonging to the requested document, then reranks candidates lexically. This is appropriate for moderate document sizes and benefits from the per-document scope; it is not an approximate-nearest-neighbor index for very large documents or a shared high-volume corpus. Before scaling to that workload, benchmark representative documents and move vector retrieval to a local vector engine with indexed ANN search (for example, a self-hosted Qdrant deployment), retaining SQLite or object storage for document metadata and lifecycle. Keep OCR and embedding work asynchronous/backgrounded if indexing latency or concurrent upload volume becomes material.

## Entity RAG indexing

Apply the `AddEntityRagIndexing` EF migration before starting the API. The Angular page at `/zamba/entity-rag-indexing` lists `DOC_TYPE` and stores one enable/mode/start-date configuration per document type.

- **Batch** creates a managed `EmbeddingRag` schedule event that runs every hour. Each run handles at most 100 unindexed documents; remaining documents are picked up by later hourly runs.
- **When inserted** is polled by the API every 15 seconds and handles up to 25 new documents per entity type per pass. Callers that need a direct single-document run can use `POST /api/entity-rag-indexing/{documentTypeId}/documents/{documentId}/index` after the source row is committed.
- Both modes filter on `DOC_I{documentTypeId}.crdate` when a start date is configured. The API joins `DOC_T{documentTypeId}.DOC_ID` to `DOC_I{documentTypeId}.DOC_ID` and resolves files as `DISK_VOL_PATH/{documentTypeId}/{OFFSET}/{DOC_FILE}`. A `DOC_FILE` value without a dot is recorded as skipped.
- `EntityRagDocumentStates` stores status per `(DocumentTypeId, DocumentId)` and the vector document ID. The current vector store is the API's local SQLite store; document IDs are the SHA-256 content hash used by the existing RAG ingestion path.

The scheduled event is created, updated, or removed with its entity configuration and should not be manually edited, toggled, or deleted from the generic schedule list. On-insert mode is eventual (within the 15-second polling interval), not a synchronous SQL insert trigger.
