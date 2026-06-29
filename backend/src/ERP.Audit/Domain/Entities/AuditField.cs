using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class AuditField : AuditableEntity
{
    public long AuditLogId { get; private set; }
    public string FieldName { get; private set; } = string.Empty;
    public string? DisplayName { get; private set; }
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }
    public string? DataType { get; private set; }
    public short ChangeType { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AuditLog? AuditLog { get; private set; }
}
