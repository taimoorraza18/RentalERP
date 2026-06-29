using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Purchase.Domain.Entities;

public sealed class GoodsReceipt : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string GrnNo { get; private set; } = string.Empty;
    public DateOnly GrnDate { get; private set; }
    public long? PurchaseOrderId { get; private set; }
    public long VendorId { get; private set; }
    public long WarehouseId { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public long ReceivedBy { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public PurchaseOrder? PurchaseOrder { get; private set; }
    public VendorEntity? Vendor { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public Currency? Currency { get; private set; }
    public ICollection<GoodsReceiptLine> GoodsReceiptLines { get; private set; } = [];
}
