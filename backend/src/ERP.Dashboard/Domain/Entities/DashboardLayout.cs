using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardLayout : AuditableEntity
{
    public long DashboardId { get; private set; }
    public string LayoutJson { get; private set; } = string.Empty;
    public short ScreenType { get; private set; }
    public int VersionNo { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
}
