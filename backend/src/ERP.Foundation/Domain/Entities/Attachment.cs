using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Attachment : AuditableEntity
{
    public string FileName { get; private set; } = string.Empty;
    public string StoredFileName { get; private set; } = string.Empty;
    public string FileExtension { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string StorageProvider { get; private set; } = "Local";
    public string StoragePath { get; private set; } = string.Empty;
    public string? FileHash { get; private set; }
    public int VersionNo { get; private set; }
    public bool IsEncrypted { get; private set; }
    public bool IsPublic { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
