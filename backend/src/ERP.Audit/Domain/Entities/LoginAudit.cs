using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class LoginAudit : AuditableEntity
{
    public long? UserId { get; private set; }
    public DateTime LoginDate { get; private set; }
    public short LoginStatus { get; private set; }
    public string? IPAddress { get; private set; }
    public string? Browser { get; private set; }
    public string? Device { get; private set; }
    public string? OperatingSystem { get; private set; }
    public Guid? SessionId { get; private set; }
    public string? FailureReason { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
