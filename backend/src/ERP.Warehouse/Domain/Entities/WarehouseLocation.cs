using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class WarehouseLocation : AuditableEntity
{
    public long WarehouseId { get; private set; }
    public long? WarehouseZoneId { get; private set; }
    public long? ParentLocationId { get; private set; }
    public string LocationCode { get; private set; } = string.Empty;
    public string LocationName { get; private set; } = string.Empty;
    public short LocationType { get; private set; }
    public string? Barcode { get; private set; }
    public string? QRCode { get; private set; }
    public decimal? MaximumCapacity { get; private set; }
    public string? CapacityUnit { get; private set; }
    public bool IsPickingLocation { get; private set; }
    public bool IsReceivingLocation { get; private set; }
    public bool IsDispatchLocation { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Warehouse? Warehouse { get; private set; }
    public WarehouseZone? WarehouseZone { get; private set; }
    public WarehouseLocation? ParentLocation { get; private set; }
    public ICollection<WarehouseLocation> Children { get; private set; } = [];
}
