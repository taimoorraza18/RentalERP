using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class Deployment : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public long ApplicationVersionId { get; private set; }
    public DateTime DeploymentDate { get; private set; }
    public short DeploymentType { get; private set; }
    public short DeploymentStatus { get; private set; }
    public string ExecutedBy { get; private set; } = string.Empty;
    public int DurationSeconds { get; private set; }
    public string? DeploymentNotes { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
    public ApplicationVersion? ApplicationVersion { get; private set; }
}
