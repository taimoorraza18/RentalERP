using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Inventory.Domain.Entities;

public sealed class StockCountLine : AuditableEntity
{
    public long StockCountId { get; private set; }
    public long ItemId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public decimal SystemQuantity { get; private set; }
    public decimal CountedQuantity { get; private set; }
    public decimal DifferenceQuantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public StockCount? StockCount { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseLocation? WarehouseLocation { get; private set; }
}
