using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseReturn : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string ReturnNo { get; private set; } = string.Empty;
    public DateOnly ReturnDate { get; private set; }
    public long GoodsReceiptId { get; private set; }
    public long VendorId { get; private set; }
    public string? ReturnReason { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public GoodsReceipt? GoodsReceipt { get; private set; }
    public VendorEntity? Vendor { get; private set; }
}
