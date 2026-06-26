using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class WarehouseTimeline : AuditableEntity
{
    public long WarehouseId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Warehouse? Warehouse { get; private set; }
}
