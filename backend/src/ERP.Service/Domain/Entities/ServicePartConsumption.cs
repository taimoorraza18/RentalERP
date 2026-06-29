using ERP.SharedKernel.Base;
using ERP.Inventory.Domain.Entities;
using ERP.Product.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Service.Domain.Entities;

public sealed class ServicePartConsumption : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public long ItemId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? InventoryTransactionId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public InventoryTransaction? InventoryTransaction { get; private set; }
}
