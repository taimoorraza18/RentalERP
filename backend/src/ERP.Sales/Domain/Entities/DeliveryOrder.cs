using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Sales.Domain.Entities;

public sealed class DeliveryOrder : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string DeliveryNo { get; private set; } = string.Empty;
    public DateOnly DeliveryDate { get; private set; }
    public long? SalesOrderId { get; private set; }
    public long CustomerId { get; private set; }
    public long WarehouseId { get; private set; }
    public long? DeliveredBy { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public SalesOrder? SalesOrder { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public ICollection<DeliveryOrderLine> DeliveryOrderLines { get; private set; } = [];
}
