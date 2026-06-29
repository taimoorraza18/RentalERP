using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class SecurityAudit : AuditableEntity
{
    public long? UserId { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string? EventDescription { get; private set; }
    public string? IPAddress { get; private set; }
    public DateTime EventDate { get; private set; }
    public short RiskLevel { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
