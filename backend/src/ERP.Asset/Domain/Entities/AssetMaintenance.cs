using ERP.SharedKernel.Base;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetMaintenance : AuditableEntity
{
    public long AssetId { get; private set; }
    public long? VendorId { get; private set; }
    public long? EmployeeId { get; private set; }
    public long? ServiceId { get; private set; }
    public string MaintenanceNo { get; private set; } = string.Empty;
    public DateOnly MaintenanceDate { get; private set; }
    public short MaintenanceType { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public decimal? DowntimeHours { get; private set; }
    public decimal LaborCost { get; private set; }
    public decimal PartsCost { get; private set; }
    public decimal ExternalCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateOnly? NextMaintenanceDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Asset? Asset { get; private set; }
    public VendorEntity? Vendor { get; private set; }
}
