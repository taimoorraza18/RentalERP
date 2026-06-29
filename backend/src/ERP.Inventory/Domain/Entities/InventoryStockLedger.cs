using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using ERP.Product.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;
using WarehouseLocationEntity = ERP.Warehouse.Domain.Entities.WarehouseLocation;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryStockLedger : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public long ItemId { get; private set; }
    public long InventoryTransactionId { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public short TransactionType { get; private set; }
    public string ReferenceNo { get; private set; } = string.Empty;
    public decimal QuantityIn { get; private set; }
    public decimal QuantityOut { get; private set; }
    public decimal RunningBalance { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public WarehouseLocationEntity? WarehouseLocation { get; private set; }
    public Item? Item { get; private set; }
    public InventoryTransaction? InventoryTransaction { get; private set; }
}
