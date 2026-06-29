using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class License : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public string LicenseKey { get; private set; } = string.Empty;
    public short LicenseType { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public int MaxUsers { get; private set; }
    public int MaxCompanies { get; private set; }
    public DateTime ActivationDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
    public ICollection<Tenant> Tenants { get; private set; } = [];
}
