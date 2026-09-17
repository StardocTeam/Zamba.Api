# Zamba MCP Server

This is a separate, stdio-based MCP server for exploring the Zamba database and preparing Zamba implementation drafts. It is intentionally isolated from the existing `Zamba.Api` project and does not modify files at runtime.

## Run

From this directory:

```powershell
dotnet run --project .\Zamba.McpServer.csproj
```

By default the server looks for `..\Zamba.Api\appsettings.json` and reads only `ConnectionStrings:ZambaConnection` at runtime. To use another configuration location, set `ZAMBA_API_APPSETTINGS_PATH`. To bypass appsettings, set `ZAMBA_CONNECTION_STRING`.

The server implements MCP JSON-RPC over stdin/stdout and exposes:

- `zamba_database_overview`
- `zamba_list_tables`
- `zamba_describe_table`
- `zamba_query` (bounded read-only SQL)
- `zamba_generate_component`
- `zamba_generate_endpoint_client`

It also exposes `zamba://instructions/expert`, which contains the operating instructions for the specialized agent. Manuals and endpoint contracts can be added later as MCP resources without changing the existing API project.

## Client configuration

Example MCP client configuration:

```json
{
  "mcpServers": {
    "zamba-expert": {
      "type": "stdio",
      "command": "dotnet",
      "args": ["run", "--project", "C:\\source\\ST\\Zamba.Api\\Zamba.McpServer\\Zamba.McpServer.csproj"]
    }
  }
}
```

Keep credentials in the existing local configuration or environment, never in client configuration committed to source control.