using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;

namespace ERP.Purchase.Domain.Entities;

public sealed class RequestForQuotationLine : AuditableEntity
{
    public long RequestForQuotationId { get; private set; }
    public long ItemId { get; private set; }
    public string? Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public DateOnly? DeliveryDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RequestForQuotation? RequestForQuotation { get; private set; }
    public Item? Item { get; private set; }
}
