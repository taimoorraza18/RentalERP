using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Sales.Domain.Entities;

public sealed class DeliveryOrderLine : AuditableEntity
{
    public long DeliveryOrderId { get; private set; }
    public long? SalesOrderLineId { get; private set; }
    public long ItemId { get; private set; }
    public long? WarehouseBinId { get; private set; }
    public decimal Quantity { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public DeliveryOrder? DeliveryOrder { get; private set; }
    public SalesOrderLine? SalesOrderLine { get; private set; }
    public Item? Item { get; private set; }
    public WarehouseLocation? WarehouseBin { get; private set; }
}
