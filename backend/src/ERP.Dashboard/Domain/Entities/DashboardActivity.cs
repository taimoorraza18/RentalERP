using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardActivity : AuditableEntity
{
    public long DashboardId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
}
