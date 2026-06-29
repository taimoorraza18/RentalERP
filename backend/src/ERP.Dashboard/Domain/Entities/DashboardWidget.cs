using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardWidget : AuditableEntity
{
    public long DashboardId { get; private set; }
    public string WidgetName { get; private set; } = string.Empty;
    public short WidgetType { get; private set; }
    public int DisplayOrder { get; private set; }
    public short Width { get; private set; }
    public short Height { get; private set; }
    public short PositionX { get; private set; }
    public short PositionY { get; private set; }
    public int RefreshIntervalSeconds { get; private set; }
    public bool IsVisible { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
    public ICollection<WidgetDataSource> WidgetDataSources { get; private set; } = [];
}
