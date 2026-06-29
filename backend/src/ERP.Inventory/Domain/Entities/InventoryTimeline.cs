using ERP.SharedKernel.Base;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryTimeline : AuditableEntity
{
    public long InventoryTransactionId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public InventoryTransaction? InventoryTransaction { get; private set; }
}
