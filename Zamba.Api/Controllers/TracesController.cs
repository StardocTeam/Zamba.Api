using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.DTOs;
using Zamba.Api.Models;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/traces")]
public sealed class TracesController(
    ZambaDbContext dbContext,
    IConfiguration configuration,
    ILogger<TracesController> logger) : ControllerBase
{
    private static readonly Regex TimestampInFileName = new(
        @"(?<timestamp>\d{2}-\d{2}-\d{4} \d{2}-\d{2}-\d{2})\.json$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    [HttpGet("sources")]
    public async Task<ActionResult<IReadOnlyList<TraceSourceResponse>>> GetSources(CancellationToken cancellationToken)
    {
        var sources = await dbContext.TraceSources.AsNoTracking()
            .OrderBy(source => source.ApplicationName)
            .ThenBy(source => source.Environment)
            .Select(source => new TraceSourceResponse(
                source.Id, source.ApplicationName, source.Environment, source.FolderPath, source.IsEnabled))
            .ToListAsync(cancellationToken);

        return Ok(sources);
    }

    [HttpGet("sources/{id:int}")]
    public async Task<ActionResult<TraceSourceResponse>> GetSource(int id, CancellationToken cancellationToken)
    {
        var source = await dbContext.TraceSources.AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return source is null ? NotFound() : Ok(ToResponse(source));
    }

    [HttpPost("sources")]
    public async Task<ActionResult<TraceSourceResponse>> CreateSource(
        CreateTraceSourceRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetAllowedRoot(out var allowedRoot))
        {
            return Problem("TraceMonitoring:AllowedRootPath is not configured.", statusCode: StatusCodes.Status503ServiceUnavailable);
        }

        string folderPath;
        try
        {
            folderPath = Path.GetFullPath(request.FolderPath);
        }
        catch (Exception exception) when (exception is ArgumentException or NotSupportedException or PathTooLongException)
        {
            return BadRequest("FolderPath is not a valid path.");
        }

        if (!IsPathWithinRoot(allowedRoot, folderPath) || !Directory.Exists(folderPath))
        {
            return BadRequest("FolderPath must be an existing folder beneath the configured trace root.");
        }

        var normalizedApplication = request.ApplicationName.Trim();
        var normalizedEnvironment = request.Environment.Trim();
        var duplicate = await dbContext.TraceSources.AnyAsync(source =>
            source.ApplicationName == normalizedApplication &&
            source.Environment == normalizedEnvironment &&
            source.FolderPath == folderPath, cancellationToken);
        if (duplicate)
        {
            return Conflict("This application, environment, and folder are already registered.");
        }

        var source = new TraceSource
        {
            ApplicationName = normalizedApplication,
            Environment = normalizedEnvironment,
            FolderPath = folderPath
        };
        dbContext.TraceSources.Add(source);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = ToResponse(source);
        return CreatedAtAction(nameof(GetSource), new { id = source.Id }, response);
    }

    [HttpGet]
    public async Task<ActionResult<TracePageResponse>> GetTraces(
        [FromQuery] TraceSearchRequest filters,
        CancellationToken cancellationToken = default)
    {
        var page = Math.Max(filters.Page, 1);
        var pageSize = Math.Clamp(filters.PageSize, 1, 200);

        var query = dbContext.TraceRecords.AsNoTracking()
            .Where(trace => trace.TraceSource.IsEnabled);

        if (filters.TraceSourceId.HasValue)
            query = query.Where(trace => trace.TraceSourceId == filters.TraceSourceId.Value);
        if (!string.IsNullOrWhiteSpace(filters.ApplicationName))
            query = query.Where(trace => trace.TraceSource.ApplicationName == filters.ApplicationName);
        if (!string.IsNullOrWhiteSpace(filters.Environment))
            query = query.Where(trace => trace.TraceSource.Environment == filters.Environment);
        if (!string.IsNullOrWhiteSpace(filters.UserId))
            query = query.Where(trace => trace.UserId != null && trace.UserId.Contains(filters.UserId));
        if (!string.IsNullOrWhiteSpace(filters.Search))
            query = query.Where(trace => trace.Title.Contains(filters.Search) || trace.FileName.Contains(filters.Search));
        if (filters.From.HasValue)
            query = query.Where(trace => trace.TraceTimestampUtc >= filters.From.Value);
        if (filters.To.HasValue)
            query = query.Where(trace => trace.TraceTimestampUtc <= filters.To.Value);

        var totalCount = await query.CountAsync(cancellationToken);
        var traces = await query
            .OrderByDescending(trace => trace.TraceTimestampUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(trace => new TraceRecordResponse(
                trace.Id,
                trace.TraceSourceId,
                trace.TraceSource.ApplicationName,
                trace.TraceSource.Environment,
                trace.TraceSource.FolderPath,
                trace.RelativePath,
                trace.FileName,
                trace.TraceTimestampUtc,
                trace.UserId,
                trace.TraceConfig,
                trace.Title,
                trace.RegisteredAtUtc))
            .ToListAsync(cancellationToken);

        return Ok(new TracePageResponse(traces, totalCount, page, pageSize));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TraceRecordResponse>> GetTrace(long id, CancellationToken cancellationToken)
    {
        var trace = await dbContext.TraceRecords.AsNoTracking()
            .Include(item => item.TraceSource)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return trace is null ? NotFound() : Ok(ToResponse(trace));
    }

    [HttpGet("{id:long}/content")]
    public async Task<IActionResult> GetTraceContent(long id, CancellationToken cancellationToken)
    {
        var trace = await dbContext.TraceRecords.AsNoTracking()
            .Include(item => item.TraceSource)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (trace is null || !trace.TraceSource.IsEnabled)
            return NotFound();

        if (!TryResolveTracePath(trace.TraceSource, trace.RelativePath, out var filePath) || !System.IO.File.Exists(filePath))
            return NotFound("The registered trace file is no longer available.");

        const long maxPreviewBytes = 10 * 1024 * 1024;
        if (new FileInfo(filePath).Length > maxPreviewBytes)
            return StatusCode(StatusCodes.Status413PayloadTooLarge, "Trace files larger than 10 MB cannot be previewed.");

        try
        {
            await using var stream = System.IO.File.OpenRead(filePath);
            using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            return Ok(document.RootElement.Clone());
        }
        catch (JsonException exception)
        {
            logger.LogWarning(exception, "Trace file {TraceId} contains invalid JSON", id);
            return UnprocessableEntity("The trace file does not contain valid JSON.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TraceRecordResponse>> RegisterTrace(
        RegisterTraceRequest request,
        CancellationToken cancellationToken)
    {
        var source = await dbContext.TraceSources.FirstOrDefaultAsync(
            item => item.Id == request.TraceSourceId && item.IsEnabled, cancellationToken);
        if (source is null)
            return NotFound("Trace source was not found or is disabled.");

        if (!TryResolveTracePath(source, request.RelativePath, out var filePath))
            return BadRequest("RelativePath must refer to a file beneath the registered source folder.");
        if (!Path.GetFileName(filePath).StartsWith("Trace", StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(Path.GetExtension(filePath), ".json", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Only JSON files whose names start with 'Trace' can be registered.");
        if (!System.IO.File.Exists(filePath))
            return NotFound("The trace file does not exist beneath the registered source folder.");

        var relativePath = Path.GetRelativePath(source.FolderPath, filePath);
        var duplicate = await dbContext.TraceRecords.AnyAsync(
            trace => trace.TraceSourceId == source.Id && trace.RelativePath == relativePath, cancellationToken);
        if (duplicate)
            return Conflict("This trace file is already registered.");

        var metadata = await ReadTraceMetadataAsync(filePath, cancellationToken);
        var trace = new TraceRecord
        {
            TraceSourceId = source.Id,
            RelativePath = relativePath,
            FileName = Path.GetFileName(filePath),
            TraceTimestampUtc = request.TraceTimestampUtc ?? ParseTimestamp(filePath) ??
                new DateTimeOffset(System.IO.File.GetLastWriteTimeUtc(filePath), TimeSpan.Zero),
            UserId = request.UserId ?? metadata.UserId,
            TraceConfig = request.TraceConfig ?? metadata.TraceConfig,
            Title = string.IsNullOrWhiteSpace(request.Title) ? metadata.Title : request.Title.Trim()
        };

        dbContext.TraceRecords.Add(trace);
        await dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetTrace), new { id = trace.Id }, ToResponse(trace, source));
    }

    [HttpPost("sources/{sourceId:int}/scan")]
    public async Task<ActionResult<TraceScanResponse>> ScanSource(int sourceId, CancellationToken cancellationToken)
    {
        var source = await dbContext.TraceSources.FirstOrDefaultAsync(
            item => item.Id == sourceId && item.IsEnabled, cancellationToken);
        if (source is null)
            return NotFound("Trace source was not found or is disabled.");
        if (!Directory.Exists(source.FolderPath))
            return NotFound("The registered source folder is unavailable to this API instance.");

        var files = Directory.EnumerateFiles(source.FolderPath, "Trace*.json", SearchOption.AllDirectories).ToArray();
        var existingPaths = await dbContext.TraceRecords.AsNoTracking()
            .Where(trace => trace.TraceSourceId == source.Id)
            .Select(trace => trace.RelativePath)
            .ToListAsync(cancellationToken);
        var existing = existingPaths.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var discovered = 0;

        foreach (var filePath in files)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var relativePath = Path.GetRelativePath(source.FolderPath, Path.GetFullPath(filePath));
            if (!existing.Add(relativePath))
                continue;

            var metadata = await ReadTraceMetadataAsync(filePath, cancellationToken);
            dbContext.TraceRecords.Add(new TraceRecord
            {
                TraceSourceId = source.Id,
                RelativePath = relativePath,
                FileName = Path.GetFileName(filePath),
                TraceTimestampUtc = ParseTimestamp(filePath) ??
                    new DateTimeOffset(System.IO.File.GetLastWriteTimeUtc(filePath), TimeSpan.Zero),
                UserId = metadata.UserId,
                TraceConfig = metadata.TraceConfig,
                Title = metadata.Title
            });
            discovered++;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Ok(new TraceScanResponse(files.Length, discovered));
    }

    private bool TryGetAllowedRoot(out string allowedRoot)
    {
        var configuredRoot = configuration["TraceMonitoring:AllowedRootPath"];
        if (string.IsNullOrWhiteSpace(configuredRoot))
        {
            allowedRoot = string.Empty;
            return false;
        }

        allowedRoot = Path.GetFullPath(configuredRoot);
        return true;
    }

    private bool TryResolveTracePath(TraceSource source, string relativePath, out string filePath)
    {
        filePath = string.Empty;
        if (Path.IsPathRooted(relativePath) || !TryGetAllowedRoot(out var allowedRoot) ||
            !IsPathWithinRoot(allowedRoot, source.FolderPath))
            return false;

        try
        {
            var sourcePath = Path.GetFullPath(source.FolderPath);
            filePath = Path.GetFullPath(Path.Combine(sourcePath, relativePath));
            return IsPathWithinRoot(sourcePath, filePath) && IsPathWithinRoot(allowedRoot, filePath);
        }
        catch (Exception exception) when (exception is ArgumentException or NotSupportedException or PathTooLongException)
        {
            filePath = string.Empty;
            return false;
        }
    }

    private static bool IsPathWithinRoot(string rootPath, string candidatePath)
    {
        var normalizedRoot = Path.GetFullPath(rootPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var normalizedCandidate = Path.GetFullPath(candidatePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return string.Equals(normalizedRoot, normalizedCandidate, StringComparison.OrdinalIgnoreCase) ||
            normalizedCandidate.StartsWith(normalizedRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
            normalizedCandidate.StartsWith(normalizedRoot + Path.AltDirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
    }

    private static async Task<(string? UserId, string? TraceConfig, string Title)> ReadTraceMetadataAsync(
        string filePath,
        CancellationToken cancellationToken)
    {
        try
        {
            await using var stream = System.IO.File.OpenRead(filePath);
            using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            var root = document.RootElement;
            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                root = root[0];

            var data = root.ValueKind == JsonValueKind.Object && root.TryGetProperty("data", out var dataElement)
                ? dataElement
                : root;
            var userId = TryGetText(data, "userId");
            var title = TryGetText(data, "method") ?? TryGetText(data, "message") ?? Path.GetFileNameWithoutExtension(filePath);
            string? traceConfig = null;
            if (data.ValueKind == JsonValueKind.Object && data.TryGetProperty("traceConfig", out var configElement))
                traceConfig = configElement.ValueKind == JsonValueKind.String
                    ? configElement.GetString()
                    : configElement.GetRawText();
            return (userId, traceConfig, title);
        }
        catch (JsonException)
        {
            return (null, null, Path.GetFileNameWithoutExtension(filePath));
        }
    }

    private static string? TryGetText(JsonElement element, string propertyName)
    {
        if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty(propertyName, out var value))
            return null;
        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }

    private static DateTimeOffset? ParseTimestamp(string filePath)
    {
        var match = TimestampInFileName.Match(Path.GetFileName(filePath));
        if (!match.Success || !DateTimeOffset.TryParseExact(
                match.Groups["timestamp"].Value,
                "dd-MM-yyyy HH-mm-ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out var timestamp))
            return null;

        return timestamp.ToUniversalTime();
    }

    private static TraceSourceResponse ToResponse(TraceSource source) =>
        new(source.Id, source.ApplicationName, source.Environment, source.FolderPath, source.IsEnabled);

    private static TraceRecordResponse ToResponse(TraceRecord trace, TraceSource? source = null)
    {
        source ??= trace.TraceSource;
        return new TraceRecordResponse(
            trace.Id,
            source.Id,
            source.ApplicationName,
            source.Environment,
            source.FolderPath,
            trace.RelativePath,
            trace.FileName,
            trace.TraceTimestampUtc,
            trace.UserId,
            trace.TraceConfig,
            trace.Title,
            trace.RegisteredAtUtc);
    }
}