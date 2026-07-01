using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CategoryCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.CategoryCode)
            .IsUnique()
            .HasDatabaseName("uq_item_category_category_code");

        builder.HasIndex(x => x.CategoryName)
            .HasDatabaseName("ix_item_category_category_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_item_category_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
