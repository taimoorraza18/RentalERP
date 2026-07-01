using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemGroupConfiguration : IEntityTypeConfiguration<ItemGroup>
{
    public void Configure(EntityTypeBuilder<ItemGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.GroupCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.GroupName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.GroupCode)
            .IsUnique()
            .HasDatabaseName("uq_item_group_group_code");

        builder.HasIndex(x => x.GroupName)
            .HasDatabaseName("ix_item_group_group_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_item_group_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
