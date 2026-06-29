using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class Tenant : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public long LicenseId { get; private set; }
    public string TenantCode { get; private set; } = string.Empty;
    public string TenantName { get; private set; } = string.Empty;
    public string DatabaseName { get; private set; } = string.Empty;
    public string? DomainName { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
    public License? License { get; private set; }
}
