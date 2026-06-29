using ERP.SharedKernel.Base;
using ERP.Product.Domain.Entities;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesQuotationLine : AuditableEntity
{
    public long SalesQuotationId { get; private set; }
    public long ItemId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public long? TaxConfigurationId { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SalesQuotation? SalesQuotation { get; private set; }
    public TaxConfiguration? TaxConfiguration { get; private set; }
    public Item? Item { get; private set; }
}
