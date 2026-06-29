using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using ERP.Product.Domain.Entities;
using FiscalYear = ERP.Accounting.Domain.Entities.FiscalYear;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;
using WarehouseLocationEntity = ERP.Warehouse.Domain.Entities.WarehouseLocation;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryOpeningBalance : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long FiscalYearId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public long ItemId { get; private set; }
    public DateOnly OpeningDate { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public FiscalYear? FiscalYear { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public WarehouseLocationEntity? WarehouseLocation { get; private set; }
    public Item? Item { get; private set; }
    public Currency? Currency { get; private set; }
}
