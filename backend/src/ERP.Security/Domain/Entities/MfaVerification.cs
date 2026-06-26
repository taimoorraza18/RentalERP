using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class MfaVerification : AuditableEntity
{
    public long UserId { get; private set; }
    public long? UserSessionId { get; private set; }
    public short VerificationMethod { get; private set; }
    public string? VerificationCodeHash { get; private set; }
    public DateTime GeneratedDate { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public DateTime? VerifiedDate { get; private set; }
    public short AttemptCount { get; private set; }
    public bool IsSuccessful { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public UserSession? UserSession { get; private set; }
}
