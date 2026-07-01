using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemManufacturerConfiguration : IEntityTypeConfiguration<ItemManufacturer>
{
    public void Configure(EntityTypeBuilder<ItemManufacturer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ManufacturerCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ManufacturerName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.ManufacturerCode)
            .IsUnique()
            .HasDatabaseName("uq_item_manufacturer_manufacturer_code");

        builder.HasIndex(x => x.ManufacturerName)
            .HasDatabaseName("ix_item_manufacturer_manufacturer_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_item_manufacturer_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
