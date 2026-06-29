using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesOrder : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string OrderNo { get; private set; } = string.Empty;
    public DateOnly OrderDate { get; private set; }
    public long? SalesQuotationId { get; private set; }
    public long CustomerId { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public DateOnly? DeliveryDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal NetAmount { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public SalesQuotation? SalesQuotation { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public Currency? Currency { get; private set; }
    public ICollection<SalesOrderLine> SalesOrderLines { get; private set; } = [];
    public ICollection<SalesAttachment> SalesAttachments { get; private set; } = [];
    public ICollection<SalesNote> SalesNotes { get; private set; } = [];
    public ICollection<SalesActivity> SalesActivities { get; private set; } = [];
    public ICollection<SalesTimeline> SalesTimelines { get; private set; } = [];
}
