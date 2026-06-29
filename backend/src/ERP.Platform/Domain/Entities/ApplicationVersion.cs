using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class ApplicationVersion : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public string VersionNumber { get; private set; } = string.Empty;
    public string? BuildNumber { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? ReleaseNotes { get; private set; }
    public bool IsCurrent { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
    public ICollection<Deployment> Deployments { get; private set; } = [];
}
