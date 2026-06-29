using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class HealthCheck : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public string ComponentName { get; private set; } = string.Empty;
    public DateTime CheckDate { get; private set; }
    public bool IsHealthy { get; private set; }
    public int ResponseTimeMs { get; private set; }
    public string? FailureReason { get; private set; }
    public short Severity { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
}
