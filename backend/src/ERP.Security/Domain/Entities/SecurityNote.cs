using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class SecurityNote : AuditableEntity
{
    public long SecurityPolicyId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SecurityPolicy? SecurityPolicy { get; private set; }
}
