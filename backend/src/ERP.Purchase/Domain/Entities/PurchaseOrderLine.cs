using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using Account = ERP.Accounting.Domain.Entities.Account;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseOrderLine : AuditableEntity
{
    public long PurchaseOrderId { get; private set; }
    public long ItemId { get; private set; }
    public string? Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public long? TaxConfigurationId { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public long? ExpenseAccountId { get; private set; }
    public DateOnly? DeliveryDate { get; private set; }
    public decimal ReceivedQuantity { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PurchaseOrder? PurchaseOrder { get; private set; }
    public TaxConfiguration? TaxConfiguration { get; private set; }
    public Item? Item { get; private set; }
    public Account? ExpenseAccount { get; private set; }
}
