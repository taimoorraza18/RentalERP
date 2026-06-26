using ERP.SharedKernel.Base;

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
    public long ItemUnitId { get; private set; }
    public long? ItemTaxProfileId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ItemGroup? ItemGroup { get; private set; }
    public ItemCategory? ItemCategory { get; private set; }
    public ItemBrand? ItemBrand { get; private set; }
    public ItemManufacturer? ItemManufacturer { get; private set; }
    public ItemUnit? ItemUnit { get; private set; }
    public ItemTaxProfile? ItemTaxProfile { get; private set; }
    public ICollection<ItemBarcode> ItemBarcodes { get; private set; } = [];
    public ICollection<ItemImage> ItemImages { get; private set; } = [];
    public ICollection<ItemAttachment> ItemAttachments { get; private set; } = [];
    public ICollection<ItemNote> ItemNotes { get; private set; } = [];
    public ICollection<ItemActivity> ItemActivities { get; private set; } = [];
    public ICollection<ItemTimeline> ItemTimelines { get; private set; } = [];
}
