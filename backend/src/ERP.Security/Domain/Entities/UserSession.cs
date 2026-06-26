using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class UserSession : AuditableEntity
{
    public long UserId { get; private set; }
    public long SecurityPolicyId { get; private set; }
    public Guid SessionToken { get; private set; }
    public DateTime LoginTime { get; private set; }
    public DateTime LastActivity { get; private set; }
    public DateTime ExpiryTime { get; private set; }
    public string? IPAddress { get; private set; }
    public string? DeviceName { get; private set; }
    public string? Browser { get; private set; }
    public bool IsRevoked { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SecurityPolicy? SecurityPolicy { get; private set; }
    public ICollection<MfaVerification> MfaVerifications { get; private set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = [];
}
