using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseOrder : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string PoNo { get; private set; } = string.Empty;
    public DateOnly PoDate { get; private set; }
    public long VendorId { get; private set; }
    public long? RfqId { get; private set; }
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
    public VendorEntity? Vendor { get; private set; }
    public RequestForQuotation? RequestForQuotation { get; private set; }
    public Currency? Currency { get; private set; }
    public ICollection<PurchaseOrderLine> PurchaseOrderLines { get; private set; } = [];
    public ICollection<PurchaseAttachment> PurchaseAttachments { get; private set; } = [];
    public ICollection<PurchaseNote> PurchaseNotes { get; private set; } = [];
    public ICollection<PurchaseActivity> PurchaseActivities { get; private set; } = [];
    public ICollection<PurchaseTimeline> PurchaseTimelines { get; private set; } = [];
}
