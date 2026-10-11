# Trace Monitoring

Zamba.Api stores trace source configuration and trace file metadata in SQL Server. The JSON files remain in their application folders; the API is the only component that reads them for the dashboard.

## Configure the trace root

Development uses `C:\ZambaWeb` as the allowed root. Other deployments must set `TraceMonitoring__AllowedRootPath` to the parent directory containing the application log folders. Every registered folder and trace file must stay beneath this root, and the API process must be able to read it.

Apply the `AddTraceMonitoring` EF migration to each environment's database before starting the dashboard.

## Register a source and import existing files

Register one source for each application, environment, and log folder:

```http
POST /api/traces/sources
Content-Type: application/json

{
  "applicationName": "Zamba.API",
  "environment": "Production",
  "folderPath": "C:\\ZambaWeb\\Zamba.api\\Log"
}
```

The response contains the source `id`. In the dashboard, **Add log source** registers the folder and immediately scans it. Existing trace files are found recursively when their filename starts with `Trace` and ends with `.json`.

Applications can also ask the API to index the current files in a source:

```http
POST /api/traces/sources/{sourceId}/scan
```

## Register a newly created trace

After a trace file has been completely written, call `POST /api/traces`. The relative path is resolved beneath the registered source folder; absolute paths and path traversal are rejected. `traceTimestampUtc`, `userId`, `traceConfig`, and `title` are optional when the API can derive them from the filename or JSON payload.

```http
POST /api/traces
Content-Type: application/json

{
  "traceSourceId": 1,
  "relativePath": "2026-10-11/Trace/Trace zamba -  - 11-10-2026 00-21-55.json",
  "userId": "22242",
  "traceConfig": "{}",
  "title": "GetDocument"
}
```

The unique source and relative-path constraint makes repeat registrations detectable (`409 Conflict`).

## Dashboard API

- `GET /api/traces/sources` lists registered application/environment folders.
- `GET /api/traces` searches trace metadata. Optional query parameters: `applicationName`, `environment`, `userId`, `search` (title or filename), `from`, `to`, `page`, and `pageSize` (maximum 200).
- `GET /api/traces/{id}` returns one trace record.
- `GET /api/traces/{id}/content` returns the parsed JSON payload, with a 10 MB preview limit.

The Angular dashboard is available at `/zamba/traces`.

## Access control

The current Zamba.Api project does not configure authentication or authorization. These trace endpoints follow that existing API behavior, so keep the API on a trusted network or add authentication and authorization before exposing trace contents to untrusted clients.