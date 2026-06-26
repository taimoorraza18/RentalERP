using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class RefreshToken : AuditableEntity
{
    public long UserSessionId { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTime IssuedDate { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public DateTime? RevokedDate { get; private set; }
    public string? ReplacedByTokenHash { get; private set; }
    public bool IsRevoked { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public UserSession? UserSession { get; private set; }
}
