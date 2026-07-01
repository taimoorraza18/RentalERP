using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemBarcodeConfiguration : IEntityTypeConfiguration<ItemBarcode>
{
    public void Configure(EntityTypeBuilder<ItemBarcode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Barcode).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemBarcodes)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Barcode)
            .IsUnique()
            .HasDatabaseName("uq_item_barcode_barcode");

        builder.HasIndex(x => x.ItemId)
            .HasDatabaseName("ix_item_barcode_item_id");

        builder.HasIndex(x => new { x.ItemId, x.IsPrimary })
            .HasDatabaseName("ix_item_barcode_item_id_is_primary");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
