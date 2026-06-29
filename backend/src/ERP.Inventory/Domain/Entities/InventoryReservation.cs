using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using ERP.Product.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;
using WarehouseLocationEntity = ERP.Warehouse.Domain.Entities.WarehouseLocation;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryReservation : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? WarehouseLocationId { get; private set; }
    public long ItemId { get; private set; }
    public long? CustomerId { get; private set; }
    public string ReferenceNo { get; private set; } = string.Empty;
    public DateOnly ReservationDate { get; private set; }
    public decimal ReservedQuantity { get; private set; }
    public decimal ReleasedQuantity { get; private set; }
    public DateOnly? ExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public WarehouseLocationEntity? WarehouseLocation { get; private set; }
    public Item? Item { get; private set; }
    public CustomerEntity? Customer { get; private set; }
}
