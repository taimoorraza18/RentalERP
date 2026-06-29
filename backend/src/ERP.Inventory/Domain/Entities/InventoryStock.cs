using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using ERP.Product.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;
using WarehouseLocationEntity = ERP.Warehouse.Domain.Entities.WarehouseLocation;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryStock : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public long ItemId { get; private set; }
    public decimal AvailableQuantity { get; private set; }
    public decimal ReservedQuantity { get; private set; }
    public decimal InTransitQuantity { get; private set; }
    public decimal DamagedQuantity { get; private set; }
    public decimal OnHandQuantity { get; private set; }
    public decimal AverageCost { get; private set; }
    public DateTime? LastTransactionDate { get; private set; }
    public DateTime? LastStockUpdate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public WarehouseLocationEntity? WarehouseLocation { get; private set; }
    public Item? Item { get; private set; }
}
