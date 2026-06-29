using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardFavorite : AuditableEntity
{
    public long DashboardId { get; private set; }
    public long UserId { get; private set; }
    public bool IsDefault { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
}
