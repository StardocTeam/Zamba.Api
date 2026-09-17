using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

var server = new ZambaMcpServer(new ZambaConfiguration());
await server.RunAsync(Console.OpenStandardInput(), Console.OpenStandardOutput());

sealed class ZambaConfiguration
{
    public string? GetConnectionString()
    {
        var configuredPath = Environment.GetEnvironmentVariable("ZAMBA_API_APPSETTINGS_PATH");
        var candidates = new[]
        {
            configuredPath,
            Path.Combine(Environment.CurrentDirectory, "..", "Zamba.Api", "appsettings.json"),
            Path.Combine(Environment.CurrentDirectory, "Zamba.Api", "appsettings.json"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Zamba.Api", "appsettings.json")
        }.Where(path => !string.IsNullOrWhiteSpace(path));

        foreach (var candidate in candidates)
        {
            var path = Path.GetFullPath(candidate!);
            if (!File.Exists(path))
            {
                continue;
            }

            using var document = JsonDocument.Parse(File.ReadAllText(path));
            if (document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings) &&
                connectionStrings.TryGetProperty("ZambaConnection", out var connection))
            {
                return connection.GetString();
            }
        }

        return Environment.GetEnvironmentVariable("ZAMBA_CONNECTION_STRING");
    }
}

sealed class ZambaMcpServer
{
    private static readonly Regex UnsafeSql = new(
        @"\b(INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|TRUNCATE|CREATE|EXEC|EXECUTE|GRANT|REVOKE|DENY|DBCC|BACKUP|RESTORE)\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private readonly ZambaConfiguration configuration;

    public ZambaMcpServer(ZambaConfiguration configuration) => this.configuration = configuration;

    public async Task RunAsync(Stream input, Stream output)
    {
        using var reader = new StreamReader(input);
        await using var writer = new StreamWriter(output) { AutoFlush = true };

        while (await reader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            JsonDocument? request = null;
            try
            {
                request = JsonDocument.Parse(line);
                var response = await HandleAsync(request.RootElement);
                if (response is not null)
                {
                    await writer.WriteLineAsync(response);
                }
            }
            catch (Exception exception)
            {
                await writer.WriteLineAsync(JsonSerializer.Serialize(new
                {
                    jsonrpc = "2.0",
                    id = request?.RootElement.TryGetProperty("id", out var id) == true ? id : (JsonElement?)null,
                    error = new { code = -32603, message = exception.Message }
                }));
            }
            finally
            {
                request?.Dispose();
            }
        }
    }

    private async Task<string?> HandleAsync(JsonElement request)
    {
        var method = request.GetProperty("method").GetString();
        var id = request.TryGetProperty("id", out var requestId) ? requestId : (JsonElement?)null;

        if (method?.StartsWith("notifications/", StringComparison.Ordinal) == true)
        {
            return null;
        }

        object result = method switch
        {
            "initialize" => new
            {
                protocolVersion = "2025-06-18",
                capabilities = new { tools = new { }, resources = new { } },
                serverInfo = new { name = "zamba-expert", version = "0.1.0" },
                instructions = ExpertInstructions
            },
            "ping" => new { },
            "tools/list" => new { tools = Tools },
            "resources/list" => new { resources = new[] { new { uri = "zamba://instructions/expert", name = "Zamba expert operating instructions", mimeType = "text/plain" } } },
            "resources/read" => ReadResource(request),
            "tools/call" => await CallToolAsync(request),
            _ => throw new InvalidOperationException($"Unsupported MCP method '{method}'.")
        };

        return JsonSerializer.Serialize(new { jsonrpc = "2.0", id = id ?? default, result });
    }

    private static object ReadResource(JsonElement request)
    {
        var uri = request.GetProperty("params").GetProperty("uri").GetString();
        if (uri != "zamba://instructions/expert")
        {
            throw new InvalidOperationException($"Unknown resource '{uri}'.");
        }

        return new { contents = new[] { new { uri, mimeType = "text/plain", text = ExpertInstructions } } };
    }

    private async Task<object> CallToolAsync(JsonElement request)
    {
        var parameters = request.GetProperty("params");
        var name = parameters.GetProperty("name").GetString();
        var arguments = parameters.TryGetProperty("arguments", out var suppliedArguments)
            ? suppliedArguments
            : JsonDocument.Parse("{}").RootElement;

        var text = name switch
        {
            "zamba_database_overview" => await DatabaseOverviewAsync(),
            "zamba_list_tables" => await QueryAsync("SELECT TABLE_SCHEMA AS [schema], TABLE_NAME AS [table], TABLE_TYPE AS [type] FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_SCHEMA, TABLE_NAME", 500),
            "zamba_describe_table" => await DescribeTableAsync(arguments),
            "zamba_query" => await ReadOnlyQueryAsync(arguments),
            "zamba_generate_component" => GenerateComponent(arguments),
            "zamba_generate_endpoint_client" => GenerateEndpointClient(arguments),
            _ => throw new InvalidOperationException($"Unknown tool '{name}'.")
        };

        return new { content = new[] { new { type = "text", text } } };
    }

    private async Task<string> DatabaseOverviewAsync()
    {
        var connection = configuration.GetConnectionString();
        if (string.IsNullOrWhiteSpace(connection))
        {
            return "No Zamba connection string was found. Set ZAMBA_API_APPSETTINGS_PATH or ZAMBA_CONNECTION_STRING.";
        }

        await using var sqlConnection = new SqlConnection(connection);
        await sqlConnection.OpenAsync();
        return JsonSerializer.Serialize(new
        {
            database = sqlConnection.Database,
            dataSource = sqlConnection.DataSource,
            serverVersion = sqlConnection.ServerVersion,
            tables = await QueryAsync("SELECT TABLE_SCHEMA AS [schema], TABLE_NAME AS [table], TABLE_TYPE AS [type] FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_SCHEMA, TABLE_NAME", 500)
        });
    }

    private async Task<string> DescribeTableAsync(JsonElement arguments)
    {
        var table = arguments.GetProperty("table").GetString();
        if (string.IsNullOrWhiteSpace(table) || !Regex.IsMatch(table, "^[A-Za-z0-9_]+$"))
        {
            throw new ArgumentException("table must contain only letters, numbers, and underscores.");
        }

        var schema = arguments.TryGetProperty("schema", out var schemaElement) ? schemaElement.GetString() : "dbo";
        if (string.IsNullOrWhiteSpace(schema) || !Regex.IsMatch(schema, "^[A-Za-z0-9_]+$"))
        {
            throw new ArgumentException("schema must contain only letters, numbers, and underscores.");
        }

        return await QueryAsync($"SELECT COLUMN_NAME AS [column], DATA_TYPE AS [dataType], IS_NULLABLE AS [nullable], CHARACTER_MAXIMUM_LENGTH AS [maxLength], ORDINAL_POSITION AS [position] FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{schema}' AND TABLE_NAME = '{table}' ORDER BY ORDINAL_POSITION", 500);
    }

    private async Task<string> ReadOnlyQueryAsync(JsonElement arguments)
    {
        var sql = arguments.GetProperty("sql").GetString();
        if (string.IsNullOrWhiteSpace(sql) || UnsafeSql.IsMatch(sql) || !Regex.IsMatch(sql.TrimStart(), "^(SELECT|WITH)\\b", RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("Only read-only SELECT or WITH queries are allowed.");
        }

        var maxRows = arguments.TryGetProperty("maxRows", out var maxRowsElement) && maxRowsElement.TryGetInt32(out var suppliedMaxRows)
            ? Math.Clamp(suppliedMaxRows, 1, 500)
            : 100;
        return await QueryAsync(sql, maxRows);
    }

    private async Task<string> QueryAsync(string sql, int maxRows)
    {
        var connection = configuration.GetConnectionString();
        if (string.IsNullOrWhiteSpace(connection))
        {
            return "No Zamba connection string was found. Set ZAMBA_API_APPSETTINGS_PATH or ZAMBA_CONNECTION_STRING.";
        }

        await using var sqlConnection = new SqlConnection(connection);
        await sqlConnection.OpenAsync();
        await using var command = new SqlCommand(sql, sqlConnection) { CommandTimeout = 30 };
        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult);
        var rows = new List<Dictionary<string, object?>>();
        while (await reader.ReadAsync() && rows.Count < maxRows)
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (var index = 0; index < reader.FieldCount; index++)
            {
                row[reader.GetName(index)] = reader.IsDBNull(index) ? null : reader.GetValue(index);
            }

            rows.Add(row);
        }

        return JsonSerializer.Serialize(new { rowCount = rows.Count, truncated = rows.Count == maxRows, rows });
    }

    private static string GenerateComponent(JsonElement arguments)
    {
        var name = arguments.GetProperty("name").GetString() ?? "zamba-component";
        var purpose = arguments.GetProperty("purpose").GetString() ?? "read Zamba data";
        var query = arguments.TryGetProperty("sql", out var sql) ? sql.GetString() : "SELECT ...";
        return $"Component draft: {name}\nPurpose: {purpose}\nSQL contract:\n{query}\n\nThe draft is intentionally not written to disk. Confirm framework and target module before materializing it.";
    }

    private static string GenerateEndpointClient(JsonElement arguments)
    {
        var method = arguments.TryGetProperty("method", out var methodElement) ? methodElement.GetString() : "GET";
        var url = arguments.GetProperty("url").GetString() ?? "/";
        var purpose = arguments.TryGetProperty("purpose", out var purposeElement) ? purposeElement.GetString() : "Zamba operation";
        return $"Endpoint client draft:\nMethod: {method}\nURL: {url}\nPurpose: {purpose}\n\nNo request is sent and no source file is changed. Supply the endpoint manual and authentication contract before implementation.";
    }

    private const string ExpertInstructions = """
You are the Zamba domain expert. Treat the Zamba database and application manuals as authoritative only after inspecting them through the available tools or supplied resources.

Rules:
- Prefer zamba_database_overview, zamba_list_tables, and zamba_describe_table before proposing SQL.
- Use zamba_query only for bounded, read-only SELECT or WITH statements.
- Never invent table names, columns, business rules, endpoint contracts, or authentication behavior.
- Explain assumptions and distinguish observed database facts from inferred domain knowledge.
- Generated components and endpoint clients are drafts until the user supplies the relevant manual and confirms the target application/module.
- Do not expose connection strings, passwords, tokens, or other secrets in responses.
""";

    private static readonly object[] Tools =
    [
        new { name = "zamba_database_overview", description = "Inspect the configured Zamba SQL Server and list its tables.", inputSchema = new { type = "object", properties = new { }, additionalProperties = false } },
        new { name = "zamba_list_tables", description = "List Zamba database tables from INFORMATION_SCHEMA.", inputSchema = new { type = "object", properties = new { }, additionalProperties = false } },
        new { name = "zamba_describe_table", description = "Describe the columns of a Zamba table.", inputSchema = new { type = "object", properties = new { schema = new { type = "string", description = "SQL schema, defaults to dbo." }, table = new { type = "string" } }, required = new[] { "table" }, additionalProperties = false } },
        new { name = "zamba_query", description = "Run a bounded, read-only SELECT or WITH query against Zamba.", inputSchema = new { type = "object", properties = new { sql = new { type = "string" }, maxRows = new { type = "integer", minimum = 1, maximum = 500 } }, required = new[] { "sql" }, additionalProperties = false } },
        new { name = "zamba_generate_component", description = "Create a non-destructive draft for a Zamba component backed by a query.", inputSchema = new { type = "object", properties = new { name = new { type = "string" }, purpose = new { type = "string" }, sql = new { type = "string" } }, required = new[] { "name", "purpose" }, additionalProperties = false } },
        new { name = "zamba_generate_endpoint_client", description = "Create a non-destructive draft for consuming a Zamba endpoint.", inputSchema = new { type = "object", properties = new { method = new { type = "string" }, url = new { type = "string" }, purpose = new { type = "string" } }, required = new[] { "url" }, additionalProperties = false } }
    ];
}