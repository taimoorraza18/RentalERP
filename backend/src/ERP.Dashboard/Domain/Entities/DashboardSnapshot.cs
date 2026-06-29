using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardSnapshot : AuditableEntity
{
    public long DashboardId { get; private set; }
    public DateTime SnapshotDate { get; private set; }
    public string SnapshotJson { get; private set; } = string.Empty;
    public long? GeneratedBy { get; private set; }
    public short SnapshotType { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
}
