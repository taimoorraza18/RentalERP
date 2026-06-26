using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class WarehouseType : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string TypeCode { get; private set; } = string.Empty;
    public string TypeName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsSystemDefined { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Warehouse> Warehouses { get; private set; } = [];
}
