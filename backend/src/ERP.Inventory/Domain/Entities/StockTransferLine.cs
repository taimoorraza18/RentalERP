using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Inventory.Domain.Entities;

public sealed class StockTransferLine : AuditableEntity
{
    public long StockTransferId { get; private set; }
    public long ItemId { get; private set; }
    public long? FromLocationId { get; private set; }
    public long? ToLocationId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public StockTransfer? StockTransfer { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseLocation? FromLocation { get; private set; }
    public WarehouseLocation? ToLocation { get; private set; }
}
