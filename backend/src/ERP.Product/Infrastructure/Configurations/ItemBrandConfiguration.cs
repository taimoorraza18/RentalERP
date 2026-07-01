using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemBrandConfiguration : IEntityTypeConfiguration<ItemBrand>
{
    public void Configure(EntityTypeBuilder<ItemBrand> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.BrandCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.BrandName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.BrandCode)
            .IsUnique()
            .HasDatabaseName("uq_item_brand_brand_code");

        builder.HasIndex(x => x.BrandName)
            .HasDatabaseName("ix_item_brand_brand_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_item_brand_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
