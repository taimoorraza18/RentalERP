using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class WarehouseZone : AuditableEntity
{
    public long WarehouseId { get; private set; }
    public string ZoneCode { get; private set; } = string.Empty;
    public string ZoneName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool TemperatureControlled { get; private set; }
    public bool IsRestricted { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Warehouse? Warehouse { get; private set; }
    public ICollection<WarehouseLocation> WarehouseLocations { get; private set; } = [];
}
