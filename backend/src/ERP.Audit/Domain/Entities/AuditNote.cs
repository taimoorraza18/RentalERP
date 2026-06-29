using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class AuditNote : AuditableEntity
{
    public long AuditLogId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AuditLog? AuditLog { get; private set; }
}
