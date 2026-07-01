using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorTerritoryConfiguration : IEntityTypeConfiguration<VendorTerritory>
{
    public void Configure(EntityTypeBuilder<VendorTerritory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.TerritoryCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.TerritoryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.TerritoryCode)
            .IsUnique()
            .HasDatabaseName("uq_vendor_territory_territory_code");

        builder.HasIndex(x => x.TerritoryName)
            .HasDatabaseName("ix_vendor_territory_territory_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_vendor_territory_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
