using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Purchase.Domain.Entities;

public sealed class GoodsReceiptLine : AuditableEntity
{
    public long GoodsReceiptId { get; private set; }
    public long? PurchaseOrderLineId { get; private set; }
    public long ItemId { get; private set; }
    public long? WarehouseBinId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public string? BatchNo { get; private set; }
    public DateOnly? ExpiryDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public GoodsReceipt? GoodsReceipt { get; private set; }
    public PurchaseOrderLine? PurchaseOrderLine { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseLocation? WarehouseBin { get; private set; }
}
