using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryTransactionLine : AuditableEntity
{
    public long InventoryTransactionId { get; private set; }
    public long ItemId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public long UnitId { get; private set; }
    public short TransactionDirection { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal RunningQuantity { get; private set; }
    public decimal AverageCost { get; private set; }
    public decimal InventoryValue { get; private set; }
    public string? BatchNumber { get; private set; }
    public string? SerialNumber { get; private set; }
    public DateOnly? ExpiryDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public InventoryTransaction? InventoryTransaction { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseLocation? WarehouseLocation { get; private set; }
    public Unit? Unit { get; private set; }
}
