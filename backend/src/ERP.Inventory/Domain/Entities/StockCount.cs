using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Inventory.Domain.Entities;

public sealed class StockCount : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseId { get; private set; }
    public string CountNo { get; private set; } = string.Empty;
    public DateOnly CountDate { get; private set; }
    public short CountType { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public ICollection<StockCountLine> StockCountLines { get; private set; } = [];
}
