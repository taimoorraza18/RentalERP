using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class Warehouse : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseTypeId { get; private set; }
    public string WarehouseCode { get; private set; } = string.Empty;
    public string WarehouseName { get; private set; } = string.Empty;
    public long? AddressId { get; private set; }
    public long? ContactId { get; private set; }
    public string? ManagerName { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public decimal? Capacity { get; private set; }
    public string? CapacityUnit { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WarehouseType? WarehouseType { get; private set; }
    public ICollection<WarehouseZone> WarehouseZones { get; private set; } = [];
    public ICollection<WarehouseLocation> WarehouseLocations { get; private set; } = [];
    public ICollection<WarehouseAttachment> WarehouseAttachments { get; private set; } = [];
    public ICollection<WarehouseNote> WarehouseNotes { get; private set; } = [];
    public ICollection<WarehouseActivity> WarehouseActivities { get; private set; } = [];
    public ICollection<WarehouseTimeline> WarehouseTimelines { get; private set; } = [];
}
