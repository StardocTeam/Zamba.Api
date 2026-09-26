# Copilot flow telemetry

## Apply the schema

The migration `AddCopilotTelemetry` creates `CopilotRuns`, `CopilotRunSteps`, and `CopilotFeedback`. It is not applied automatically at API startup. If the API reports `Invalid object name 'CopilotRuns'`, the API's configured SQL Server database is missing this migration. Apply the migration to that same database before retrying requests.

The telemetry-only idempotent script is [copilot-telemetry-migrations.sql](copilot-telemetry-migrations.sql). Review and execute it in SQL Server Management Studio against the database configured by `ConnectionStrings:ZambaConnectionDEV`; it checks `__EFMigrationsHistory` and creates only the telemetry tables/indexes. Alternatively, from `c:\source\ST\Zamba.Api`, run EF against the intended configured connection:

```powershell
dotnet ef database update 20260926220710_AddCopilotTelemetry --project .\Zamba.Api\Zamba.Api.csproj --startup-project .\Zamba.Api\Zamba.Api.csproj
```

Verify after applying that `CopilotRuns`, `CopilotRunSteps`, and `CopilotFeedback` exist in the API's active database and that `20260926220710_AddCopilotTelemetry` is recorded in `__EFMigrationsHistory`. Do not apply the script to a different environment than the API is using.

## Stored data

- `CopilotRuns`: one row per accepted ask request, including prompt/answer, document/type identifiers, status, model, token/cost fields, cache-hit state, and total/per-phase timings. The uploaded file bytes/base64 are never stored in telemetry.
- `CopilotRunSteps`: one row per instrumented stage, with start/end timestamps, duration, status, bounded diagnostic detail, and exception text on failure. Stages include file decoding/size validation, cache lookup, OCR/text extraction, chunking, embedding, index persistence, retrieval/reranking, prompt construction, and Copilot completion.
- `CopilotFeedback`: latest thumbs-up (`1`) or thumbs-down (`-1`) per run and browser voter ID, plus optional comment. A unique index on `(RunId, VoterId)` means a changed rating replaces that voter's previous rating for that answer.

Prompts, answers, and exception messages can contain personal or confidential data. Restrict SQL access, define a retention policy, and avoid putting secrets or document text into diagnostic details. `VoterId` is a client-generated stable browser identifier, not an authenticated identity.

## Example tuning queries

Slow or failing stages over the last seven days:

```sql
SELECT Name, Status, COUNT(*) AS Runs,
       AVG(CAST(DurationMs AS float)) AS AverageMs,
       MAX(DurationMs) AS MaxMs
FROM CopilotRunSteps
WHERE StartedAtUtc >= DATEADD(day, -7, SYSUTCDATETIME())
GROUP BY Name, Status
ORDER BY AverageMs DESC;
```

Answer quality by document type and model:

```sql
SELECT r.DocumentTypeCode, r.ChatModel,
       COUNT(*) AS RatedAnswers,
       SUM(CASE WHEN f.Rating = -1 THEN 1 ELSE 0 END) AS ThumbsDown,
       CAST(100.0 * SUM(CASE WHEN f.Rating = -1 THEN 1 ELSE 0 END) / COUNT(*) AS decimal(5,2)) AS ThumbsDownPercent,
       AVG(CAST(r.TotalDurationMs AS float)) AS AverageTotalMs,
       AVG(CAST(r.InputTokens AS float)) AS AverageInputTokens
FROM CopilotFeedback f
JOIN CopilotRuns r ON r.Id = f.RunId
GROUP BY r.DocumentTypeCode, r.ChatModel
ORDER BY ThumbsDownPercent DESC;
```

Recent negative feedback for manual review:

```sql
SELECT TOP (100) r.StartedAtUtc, r.DocumentTypeCode, r.FileName,
       r.Prompt, r.Response, f.Comment, r.ChatModel, r.TotalDurationMs
FROM CopilotFeedback f
JOIN CopilotRuns r ON r.Id = f.RunId
WHERE f.Rating = -1
ORDER BY f.CreatedAtUtc DESC;
```
