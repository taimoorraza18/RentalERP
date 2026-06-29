using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Inventory.Domain.Entities;

public sealed class StockTransfer : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long FromWarehouseId { get; private set; }
    public long ToWarehouseId { get; private set; }
    public string TransferNo { get; private set; } = string.Empty;
    public DateOnly TransferDate { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? FromWarehouse { get; private set; }
    public WarehouseEntity? ToWarehouse { get; private set; }
    public ICollection<StockTransferLine> StockTransferLines { get; private set; } = [];
}
