using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using VendorEntity = ERP.Vendor.Domain.Entities.Vendor;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Asset.Domain.Entities;

public sealed class Asset : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long AssetCategoryId { get; private set; }
    public long AssetTypeId { get; private set; }
    public long AssetGroupId { get; private set; }
    public long? WarehouseId { get; private set; }
    public long? VendorId { get; private set; }
    public string AssetCode { get; private set; } = string.Empty;
    public string AssetName { get; private set; } = string.Empty;
    public string? ModelNumber { get; private set; }
    public string? SerialNumber { get; private set; }
    public DateOnly? PurchaseDate { get; private set; }
    public decimal? PurchaseCost { get; private set; }
    public decimal? CurrentBookValue { get; private set; }
    public short CurrentStatus { get; private set; }
    public bool IsAvailable { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public AssetCategory? AssetCategory { get; private set; }
    public AssetType? AssetType { get; private set; }
    public AssetGroup? AssetGroup { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public VendorEntity? Vendor { get; private set; }
    public ICollection<AssetInspection> AssetInspections { get; private set; } = [];
    public ICollection<AssetMaintenance> AssetMaintenances { get; private set; } = [];
    public ICollection<AssetAttachment> AssetAttachments { get; private set; } = [];
    public ICollection<AssetNote> AssetNotes { get; private set; } = [];
    public ICollection<AssetActivity> AssetActivities { get; private set; } = [];
    public ICollection<AssetTimeline> AssetTimelines { get; private set; } = [];
}
