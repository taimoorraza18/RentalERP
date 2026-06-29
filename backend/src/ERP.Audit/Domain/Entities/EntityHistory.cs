using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class EntityHistory : AuditableEntity
{
    public long AuditLogId { get; private set; }
    public string EntityName { get; private set; } = string.Empty;
    public long EntityId { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string? EventDescription { get; private set; }
    public DateTime EventDate { get; private set; }
    public long UserId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AuditLog? AuditLog { get; private set; }
}
