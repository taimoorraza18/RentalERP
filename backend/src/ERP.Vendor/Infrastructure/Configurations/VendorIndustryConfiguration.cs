using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorIndustryConfiguration : IEntityTypeConfiguration<VendorIndustry>
{
    public void Configure(EntityTypeBuilder<VendorIndustry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.IndustryCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.IndustryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.IndustryCode)
            .IsUnique()
            .HasDatabaseName("uq_vendor_industry_industry_code");

        builder.HasIndex(x => x.IndustryName)
            .HasDatabaseName("ix_vendor_industry_industry_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_vendor_industry_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
