using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class PasswordHistory : AuditableEntity
{
    public long UserId { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime ChangedDate { get; private set; }
    public long ChangedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
