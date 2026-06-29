using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class Dashboard : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long? UserId { get; private set; }
    public string DashboardCode { get; private set; } = string.Empty;
    public string DashboardName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public short DashboardType { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsSystem { get; private set; }
    public int RefreshIntervalSeconds { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<DashboardWidget> DashboardWidgets { get; private set; } = [];
    public ICollection<DashboardLayout> DashboardLayouts { get; private set; } = [];
    public ICollection<DashboardFilter> DashboardFilters { get; private set; } = [];
    public ICollection<DashboardFavorite> DashboardFavorites { get; private set; } = [];
    public ICollection<DashboardSnapshot> DashboardSnapshots { get; private set; } = [];
    public ICollection<DashboardAttachment> DashboardAttachments { get; private set; } = [];
    public ICollection<DashboardNote> DashboardNotes { get; private set; } = [];
    public ICollection<DashboardActivity> DashboardActivities { get; private set; } = [];
    public ICollection<DashboardTimeline> DashboardTimelines { get; private set; } = [];
}
