using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;

namespace ERP.Purchase.Domain.Entities;

public sealed class RequestForQuotation : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string RfqNo { get; private set; } = string.Empty;
    public DateOnly RfqDate { get; private set; }
    public long? PurchaseRequisitionId { get; private set; }
    public long VendorId { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public DateOnly? RequiredDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public PurchaseRequisition? PurchaseRequisition { get; private set; }
    public VendorEntity? Vendor { get; private set; }
    public Currency? Currency { get; private set; }
    public ICollection<RequestForQuotationLine> RequestForQuotationLines { get; private set; } = [];
}
