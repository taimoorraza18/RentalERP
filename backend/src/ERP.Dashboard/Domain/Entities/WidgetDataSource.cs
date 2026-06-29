using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class WidgetDataSource : AuditableEntity
{
    public long DashboardWidgetId { get; private set; }
    public short DataSourceType { get; private set; }
    public string? ConnectionName { get; private set; }
    public string DataSourceName { get; private set; } = string.Empty;
    public string? ParameterJson { get; private set; }
    public int RefreshIntervalSeconds { get; private set; }
    public int CacheDurationSeconds { get; private set; }
    public bool IsRealTime { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public DashboardWidget? DashboardWidget { get; private set; }
}
