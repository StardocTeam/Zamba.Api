using Zamba.Api.Models;

namespace Zamba.Api.DTOs;

public sealed class EntityRagIndexingConfigDto
{
    public decimal DocumentTypeId { get; init; }
    public string? DocumentTypeName { get; init; }
    public bool IsSupported { get; init; }
    public bool IsEnabled { get; init; }
    public DateTime? IndexFromDate { get; init; }
    public EntityRagIndexMode IndexMode { get; init; }
    public DateTime? CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
}

public sealed class UpdateEntityRagIndexingConfigDto
{
    public bool IsEnabled { get; init; }
    public DateTime? IndexFromDate { get; init; }
    public EntityRagIndexMode IndexMode { get; init; }
}

public sealed record EntityRagIndexingRunDto
{
    public decimal DocumentTypeId { get; init; }
    public int Attempted { get; init; }
    public int Indexed { get; init; }
    public int Skipped { get; init; }
    public int Failed { get; init; }
    public bool HasMore { get; init; }
}