using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesOrderLine : AuditableEntity
{
    public long SalesOrderId { get; private set; }
    public long ItemId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public decimal DeliveredQuantity { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SalesOrder? SalesOrder { get; private set; }
    public Item? Item { get; private set; }
}
