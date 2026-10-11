namespace Zamba.Api.Models;

public enum EntityRagIndexMode
{
    OnInsert = 0,
    Batch = 1
}

public sealed class EntityRagIndexingConfig
{
    public decimal DocumentTypeId { get; set; }
    public bool IsEnabled { get; set; }
    public DateTime? IndexFromDate { get; set; }
    public EntityRagIndexMode IndexMode { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}

public enum EntityRagDocumentIndexStatus
{
    Indexed = 1,
    Skipped = 2,
    Failed = 3
}

public sealed class EntityRagDocumentState
{
    public decimal DocumentTypeId { get; set; }
    public decimal DocumentId { get; set; }
    public EntityRagDocumentIndexStatus Status { get; set; }
    public string? VectorDocumentId { get; set; }
    public string? FileName { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime? IndexedAtUtc { get; set; }
    public string? LastError { get; set; }
}