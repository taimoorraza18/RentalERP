using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class PlatformEnvironment : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string EnvironmentCode { get; private set; } = string.Empty;
    public string EnvironmentName { get; private set; } = string.Empty;
    public short EnvironmentType { get; private set; }
    public string? BaseUrl { get; private set; }
    public string? DatabaseServer { get; private set; }
    public bool IsProduction { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<ApplicationVersion> ApplicationVersions { get; private set; } = [];
    public ICollection<License> Licenses { get; private set; } = [];
    public ICollection<Tenant> Tenants { get; private set; } = [];
    public ICollection<Deployment> Deployments { get; private set; } = [];
    public ICollection<PlatformMetric> PlatformMetrics { get; private set; } = [];
    public ICollection<HealthCheck> HealthChecks { get; private set; } = [];
    public ICollection<PlatformAttachment> PlatformAttachments { get; private set; } = [];
    public ICollection<PlatformNote> PlatformNotes { get; private set; } = [];
    public ICollection<PlatformActivity> PlatformActivities { get; private set; } = [];
    public ICollection<PlatformTimeline> PlatformTimelines { get; private set; } = [];
}
