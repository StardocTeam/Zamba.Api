using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface IScheduleExecutionService
{
    Task<ScheduleExecutionResultDto> ExecuteAsync(
        CreateOrUpdateScheduleEventDto schedule,
        Uri apiBaseUri,
        CancellationToken cancellationToken = default);

    Task<ScheduleExecutionResultDto> ExecuteQueryAsync(
        string query,
        CancellationToken cancellationToken = default);
}

public sealed class ScheduleExecutionService : IScheduleExecutionService
{
    private const int MaxQueryRows = 500;
    private const string ResultSuccess = "success";
    private const string ResultFail = "fail";
    private static readonly Regex ForbiddenSql = new(
        @"\b(INTO|EXEC|EXECUTE|OPENROWSET|OPENQUERY|OPENDATASOURCE|BULK|WAITFOR|INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|CREATE|TRUNCATE|GRANT|REVOKE|DENY|DBCC|BACKUP|RESTORE)\b",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private readonly ZambaDbContext _dbContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IEntityRagIndexingService _entityRagIndexingService;
    private readonly ILogger<ScheduleExecutionService> _logger;

    public ScheduleExecutionService(
        ZambaDbContext dbContext,
        IHttpClientFactory httpClientFactory,
        IEntityRagIndexingService entityRagIndexingService,
        ILogger<ScheduleExecutionService> logger)
    {
        _dbContext = dbContext;
        _httpClientFactory = httpClientFactory;
        _entityRagIndexingService = entityRagIndexingService;
        _logger = logger;
    }

    public async Task<ScheduleExecutionResultDto> ExecuteAsync(
        CreateOrUpdateScheduleEventDto schedule,
        Uri apiBaseUri,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = schedule.ExecutionType switch
            {
                ScheduleExecutionType.ExecuteRule => await ExecuteRuleAsync(schedule, apiBaseUri, cancellationToken),
                ScheduleExecutionType.ExecuteQuery => await ExecuteQueryAsync(schedule.Query ?? "", cancellationToken),
                ScheduleExecutionType.ExecuteEndPoint => await ExecuteEndpointAsync(schedule, cancellationToken),
                ScheduleExecutionType.EmbeddingRag => await ExecuteEmbeddingRagAsync(schedule, cancellationToken),
                _ => new ScheduleExecutionResultDto { StatusCode = 400, ErrorMessage = "Unknown schedule execution type." }
            };

            if (string.IsNullOrWhiteSpace(schedule.ExpectedResult))
            {
                result.Result = ResultSuccess;
            }
            else if (ResponseMatches(schedule.ExpectedResult, result.ResponseBody))
            {
                result.Result = ResultSuccess;
            }
            else
            {
                result.Result = ResultFail;
                result.ErrorMessage ??= "The response did not match the configured expected result.";
            }

            return result;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return new ScheduleExecutionResultDto
            {
                StatusCode = 408,
                ErrorMessage = "Execution timed out or was cancelled.",
                Result = string.IsNullOrWhiteSpace(schedule.ExpectedResult) ? ResultSuccess : ResultFail
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Schedule action execution failed for type {ExecutionType}", schedule.ExecutionType);
            return new ScheduleExecutionResultDto
            {
                StatusCode = 500,
                ErrorMessage = ex.Message,
                Result = string.IsNullOrWhiteSpace(schedule.ExpectedResult) ? ResultSuccess : ResultFail
            };
        }
    }

    private async Task<ScheduleExecutionResultDto> ExecuteEmbeddingRagAsync(
        CreateOrUpdateScheduleEventDto schedule,
        CancellationToken cancellationToken)
    {
        if (!schedule.RagDocumentTypeId.HasValue)
        {
            return new ScheduleExecutionResultDto { StatusCode = 400, ErrorMessage = "RagDocumentTypeId is required for EmbeddingRag schedules." };
        }

        var stopwatch = Stopwatch.StartNew();
        var result = await _entityRagIndexingService.RunBatchAsync(schedule.RagDocumentTypeId.Value, cancellationToken);
        stopwatch.Stop();
        return new ScheduleExecutionResultDto
        {
            StatusCode = result.Failed > 0 ? 500 : 200,
            ResponseBody = JsonSerializer.Serialize(result),
            ErrorMessage = result.Failed > 0 ? $"{result.Failed} document(s) failed to index." : null,
            DurationMs = stopwatch.ElapsedMilliseconds
        };
    }

    public async Task<ScheduleExecutionResultDto> ExecuteQueryAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        if (!IsReadOnlySelect(query))
        {
            return new ScheduleExecutionResultDto
            {
                StatusCode = 400,
                ErrorMessage = "Only a single read-only SELECT statement is allowed."
            };
        }

        var stopwatch = Stopwatch.StartNew();
        var connection = _dbContext.Database.GetDbConnection();
        var openedHere = connection.State != ConnectionState.Open;
        try
        {
            if (openedHere)
            {
                await connection.OpenAsync(cancellationToken);
            }

            await using var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandTimeout = 30;
            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult, cancellationToken);

            var rows = new List<Dictionary<string, object?>>();
            while (rows.Count < MaxQueryRows && await reader.ReadAsync(cancellationToken))
            {
                var row = new Dictionary<string, object?>();
                for (var index = 0; index < reader.FieldCount; index++)
                {
                    var columnName = reader.GetName(index);
                    if (row.ContainsKey(columnName))
                    {
                        columnName = $"{columnName}_{index}";
                    }

                    row[columnName] = await reader.IsDBNullAsync(index, cancellationToken)
                        ? null
                        : reader.GetValue(index);
                }

                rows.Add(row);
            }

            var truncated = rows.Count == MaxQueryRows && await reader.ReadAsync(cancellationToken);
            stopwatch.Stop();
            return new ScheduleExecutionResultDto
            {
                StatusCode = 200,
                ResponseBody = JsonSerializer.Serialize(new { rows, truncated }),
                DurationMs = stopwatch.ElapsedMilliseconds
            };
        }
        catch (Exception ex) when (ex is DbException or InvalidOperationException)
        {
            _logger.LogWarning(ex, "Read-only schedule query failed");
            return new ScheduleExecutionResultDto
            {
                StatusCode = 400,
                ErrorMessage = ex.Message,
                DurationMs = stopwatch.ElapsedMilliseconds
            };
        }
        finally
        {
            if (openedHere && connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }

    private async Task<ScheduleExecutionResultDto> ExecuteRuleAsync(
        CreateOrUpdateScheduleEventDto schedule,
        Uri apiBaseUri,
        CancellationToken cancellationToken)
    {
        var endpoint = string.IsNullOrWhiteSpace(schedule.EndpointOverride)
            ? new Uri(apiBaseUri, $"api/rules/{Uri.EscapeDataString(schedule.RuleId)}/execute")
            : new Uri(schedule.EndpointOverride);

        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(string.Empty)
        };
        return await SendHttpRequestAsync(request, cancellationToken);
    }

    private async Task<ScheduleExecutionResultDto> ExecuteEndpointAsync(
        CreateOrUpdateScheduleEventDto schedule,
        CancellationToken cancellationToken)
    {
        if (!Uri.TryCreate(schedule.EndpointUrl, UriKind.Absolute, out var endpoint)
            || (endpoint.Scheme != Uri.UriSchemeHttp && endpoint.Scheme != Uri.UriSchemeHttps))
        {
            return new ScheduleExecutionResultDto { StatusCode = 400, ErrorMessage = "A valid HTTP or HTTPS endpoint URL is required." };
        }

        if (!string.IsNullOrWhiteSpace(schedule.EndpointParameters))
        {
            using var parameters = JsonDocument.Parse(schedule.EndpointParameters);
            var query = parameters.RootElement.EnumerateObject()
                .Select(parameter => $"{Uri.EscapeDataString(parameter.Name)}={Uri.EscapeDataString(parameter.Value.ToString())}");
            var builder = new UriBuilder(endpoint);
            var existingQuery = builder.Query.TrimStart('?');
            builder.Query = string.Join("&", new[] { existingQuery, string.Join("&", query) }.Where(part => part.Length > 0));
            endpoint = builder.Uri;
        }

        var method = string.Equals(schedule.EndpointMethod, "GET", StringComparison.OrdinalIgnoreCase)
            ? HttpMethod.Get
            : HttpMethod.Post;
        using var request = new HttpRequestMessage(method, endpoint);
        if (method == HttpMethod.Post && schedule.EndpointBody != null)
        {
            request.Content = new StringContent(schedule.EndpointBody, Encoding.UTF8, "application/json");
        }

        return await SendHttpRequestAsync(request, cancellationToken);
    }

    private async Task<ScheduleExecutionResultDto> SendHttpRequestAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        using var response = await _httpClientFactory.CreateClient("ScheduleExecution")
            .SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        if (body.Length > 100000)
        {
            body = body[..100000];
        }

        stopwatch.Stop();
        return new ScheduleExecutionResultDto
        {
            StatusCode = (int)response.StatusCode,
            ResponseBody = body,
            ErrorMessage = response.IsSuccessStatusCode ? null : $"Endpoint returned {(int)response.StatusCode} ({response.StatusCode}).",
            DurationMs = stopwatch.ElapsedMilliseconds
        };
    }

    private static bool IsReadOnlySelect(string query)
    {
        var normalizedQuery = query.Trim();
        if (normalizedQuery.EndsWith(';'))
        {
            normalizedQuery = normalizedQuery[..^1].TrimEnd();
        }

        if (string.IsNullOrWhiteSpace(normalizedQuery)
            || !Regex.IsMatch(normalizedQuery, @"^SELECT\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
            || normalizedQuery.Contains(';')
            || normalizedQuery.Contains("--", StringComparison.Ordinal)
            || normalizedQuery.Contains("/*", StringComparison.Ordinal)
            || normalizedQuery.Contains("*/", StringComparison.Ordinal))
        {
            return false;
        }

        return !ForbiddenSql.IsMatch(normalizedQuery);
    }

    private static bool ResponseMatches(string expected, string? actual)
    {
        if (actual == null)
        {
            return false;
        }

        var expectedText = expected.Trim();
        var actualText = actual.Trim();
        try
        {
            using var expectedJson = JsonDocument.Parse(expectedText);
            using var actualJson = JsonDocument.Parse(actualText);
            return JsonValuesMatch(expectedJson.RootElement, actualJson.RootElement);
        }
        catch (JsonException)
        {
            return string.Equals(expectedText, actualText, StringComparison.Ordinal);
        }
    }

    private static bool JsonValuesMatch(JsonElement expected, JsonElement actual)
    {
        if (expected.ValueKind != actual.ValueKind)
        {
            return false;
        }

        return expected.ValueKind switch
        {
            JsonValueKind.Object => JsonObjectsMatch(expected, actual),
            JsonValueKind.Array => JsonArraysMatch(expected, actual),
            JsonValueKind.String => expected.GetString() == actual.GetString(),
            JsonValueKind.Number => JsonNumbersMatch(expected, actual),
            JsonValueKind.True or JsonValueKind.False or JsonValueKind.Null => true,
            _ => false
        };
    }

    private static bool JsonObjectsMatch(JsonElement expected, JsonElement actual)
    {
        var expectedProperties = expected.EnumerateObject().ToArray();
        var actualProperties = actual.EnumerateObject().ToArray();
        return expectedProperties.Length == actualProperties.Length
            && expectedProperties.All(property =>
                actual.TryGetProperty(property.Name, out var actualValue)
                && JsonValuesMatch(property.Value, actualValue));
    }

    private static bool JsonArraysMatch(JsonElement expected, JsonElement actual)
    {
        var expectedItems = expected.EnumerateArray().ToArray();
        var actualItems = actual.EnumerateArray().ToArray();
        return expectedItems.Length == actualItems.Length
            && expectedItems.Zip(actualItems).All(pair => JsonValuesMatch(pair.First, pair.Second));
    }

    private static bool JsonNumbersMatch(JsonElement expected, JsonElement actual)
    {
        if (expected.TryGetDecimal(out var expectedNumber) && actual.TryGetDecimal(out var actualNumber))
        {
            return expectedNumber == actualNumber;
        }

        var expectedDouble = expected.GetDouble();
        var actualDouble = actual.GetDouble();
        var tolerance = Math.Max(1d, Math.Abs(expectedDouble)) * 1e-12;
        return Math.Abs(expectedDouble - actualDouble) <= tolerance;
    }
}