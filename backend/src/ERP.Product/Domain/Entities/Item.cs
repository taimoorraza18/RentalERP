using ERP.SharedKernel.Base;
using Account = ERP.Accounting.Domain.Entities.Account;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Product.Domain.Entities;

public sealed class Item : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string ItemCode { get; private set; } = string.Empty;
    public string ItemName { get; private set; } = string.Empty;
    public long ItemGroupId { get; private set; }
    public long? ItemCategoryId { get; private set; }
    public long? ItemBrandId { get; private set; }
    public long? ItemManufacturerId { get; private set; }
    public long UnitId { get; private set; }
    public long? TaxConfigurationId { get; private set; }
    public long? SalesAccountId { get; private set; }
    public long? PurchaseAccountId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public TaxConfiguration? TaxConfiguration { get; private set; }
    public ItemGroup? ItemGroup { get; private set; }
    public ItemCategory? ItemCategory { get; private set; }
    public ItemBrand? ItemBrand { get; private set; }
    public ItemManufacturer? ItemManufacturer { get; private set; }
    public Unit? Unit { get; private set; }
    public Account? SalesAccount { get; private set; }
    public Account? PurchaseAccount { get; private set; }
    public ICollection<ItemBarcode> ItemBarcodes { get; private set; } = [];
    public ICollection<ItemImage> ItemImages { get; private set; } = [];
    public ICollection<ItemAttachment> ItemAttachments { get; private set; } = [];
    public ICollection<ItemNote> ItemNotes { get; private set; } = [];
    public ICollection<ItemActivity> ItemActivities { get; private set; } = [];
    public ICollection<ItemTimeline> ItemTimelines { get; private set; } = [];
}
