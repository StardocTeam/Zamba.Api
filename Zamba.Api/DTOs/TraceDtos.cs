using System.ComponentModel.DataAnnotations;

namespace Zamba.Api.DTOs;

/// <summary>Request to register a folder containing an application's trace files.</summary>
public sealed record CreateTraceSourceRequest
{
    [Required, MaxLength(128)]
    public required string ApplicationName { get; init; }

    [Required, MaxLength(64)]
    public required string Environment { get; init; }

    [Required, MaxLength(2048)]
    public required string FolderPath { get; init; }
}

/// <summary>Request to register a trace file created by an application.</summary>
public sealed record RegisterTraceRequest
{
    [Range(1, int.MaxValue)]
    public required int TraceSourceId { get; init; }

    [Required, MaxLength(2048)]
    public required string RelativePath { get; init; }

    public DateTimeOffset? TraceTimestampUtc { get; init; }

    [MaxLength(128)]
    public string? UserId { get; init; }

    public string? TraceConfig { get; init; }

    [MaxLength(512)]
    public string? Title { get; init; }
}

/// <summary>Filters and pagination settings for searching trace records.</summary>
public sealed record TraceSearchRequest
{
    public int? TraceSourceId { get; init; }
    public string? ApplicationName { get; init; }
    public string? Environment { get; init; }
    public string? UserId { get; init; }
    public string? Search { get; init; }
    public DateTimeOffset? From { get; init; }
    public DateTimeOffset? To { get; init; }

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, 200)]
    public int PageSize { get; init; } = 50;
}

/// <summary>Trace source configuration available to the dashboard.</summary>
public sealed record TraceSourceResponse(
    int Id,
    string ApplicationName,
    string Environment,
    string FolderPath,
    bool IsEnabled);

/// <summary>Trace file metadata available to the dashboard.</summary>
public sealed record TraceRecordResponse(
    long Id,
    int TraceSourceId,
    string ApplicationName,
    string Environment,
    string FolderPath,
    string RelativePath,
    string FileName,
    DateTimeOffset TraceTimestampUtc,
    string? UserId,
    string? TraceConfig,
    string Title,
    DateTimeOffset RegisteredAtUtc);

/// <summary>Result of scanning a source folder for trace files.</summary>
public sealed record TraceScanResponse(int Discovered, int Registered);

/// <summary>A paged trace search result.</summary>
public sealed record TracePageResponse(
    IReadOnlyList<TraceRecordResponse> Items,
    int TotalCount,
    int Page,
    int PageSize);