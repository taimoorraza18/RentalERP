using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class RestoreHistory : AuditableEntity
{
    public long AuditLogId { get; private set; }
    public string EntityName { get; private set; } = string.Empty;
    public long EntityId { get; private set; }
    public long RestoredBy { get; private set; }
    public DateTime RestoreDate { get; private set; }
    public string? RestoreReason { get; private set; }
    public DateTime? PreviousDeletedDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AuditLog? AuditLog { get; private set; }
}
