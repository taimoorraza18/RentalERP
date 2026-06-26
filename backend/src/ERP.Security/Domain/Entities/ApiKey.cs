using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class ApiKey : AuditableEntity
{
    public long UserId { get; private set; }
    public string KeyName { get; private set; } = string.Empty;
    public string ApiKeyValue { get; private set; } = string.Empty; // DB column: api_key (renamed to avoid CS0542)
    public string SecretKey { get; private set; } = string.Empty;
    public DateTime? ExpiryDate { get; private set; }
    public string? AllowedIPs { get; private set; }
    public int RequestsPerMinute { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? LastUsedDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
