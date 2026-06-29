using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class PlatformMetric : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public string MetricName { get; private set; } = string.Empty;
    public string MetricCategory { get; private set; } = string.Empty;
    public decimal MetricValue { get; private set; }
    public string? Unit { get; private set; }
    public DateTime RecordedDate { get; private set; }
    public decimal? WarningThreshold { get; private set; }
    public decimal? CriticalThreshold { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
}
