using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Vendor.Infrastructure.Configurations
{
    using Vendor = ERP.Vendor.Domain.Entities.Vendor;
    using VendorGroup = ERP.Vendor.Domain.Entities.VendorGroup;
    using VendorCategory = ERP.Vendor.Domain.Entities.VendorCategory;
    using VendorIndustry = ERP.Vendor.Domain.Entities.VendorIndustry;
    using VendorTerritory = ERP.Vendor.Domain.Entities.VendorTerritory;
    using VendorPaymentProfile = ERP.Vendor.Domain.Entities.VendorPaymentProfile;
    using VendorRating = ERP.Vendor.Domain.Entities.VendorRating;
    using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

    public sealed class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.VendorCode).IsRequired().HasMaxLength(30);
            builder.Property(x => x.VendorName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DisplayName).HasMaxLength(200);
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Phone).HasMaxLength(50);
            builder.Property(x => x.Mobile).HasMaxLength(50);
            builder.Property(x => x.Website).HasMaxLength(255);
            builder.Property(x => x.NTN).HasMaxLength(50);
            builder.Property(x => x.STRN).HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(x => x.VendorGroup)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VendorCategory)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VendorIndustry)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorIndustryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.VendorTerritory)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorTerritoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.VendorPaymentProfile)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorPaymentProfileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.VendorRating)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.VendorRatingId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.TaxConfiguration)
                .WithMany()
                .HasForeignKey(x => x.TaxConfigurationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.CompanyId, x.VendorCode })
                .IsUnique()
                .HasDatabaseName("uq_vendor_company_id_vendor_code");

            builder.HasIndex(x => new { x.CompanyId, x.VendorName })
                .HasDatabaseName("ix_vendor_company_id_vendor_name");

            builder.HasIndex(x => x.CompanyId).HasDatabaseName("ix_vendor_company_id");
            builder.HasIndex(x => x.BranchId).HasDatabaseName("ix_vendor_branch_id");
            builder.HasIndex(x => x.VendorGroupId).HasDatabaseName("ix_vendor_vendor_group_id");
            builder.HasIndex(x => x.VendorCategoryId).HasDatabaseName("ix_vendor_vendor_category_id");
            builder.HasIndex(x => x.VendorIndustryId).HasDatabaseName("ix_vendor_vendor_industry_id");
            builder.HasIndex(x => x.VendorTerritoryId).HasDatabaseName("ix_vendor_vendor_territory_id");
            builder.HasIndex(x => x.VendorPaymentProfileId).HasDatabaseName("ix_vendor_vendor_payment_profile_id");
            builder.HasIndex(x => x.VendorRatingId).HasDatabaseName("ix_vendor_vendor_rating_id");
            builder.HasIndex(x => x.TaxConfigurationId).HasDatabaseName("ix_vendor_tax_configuration_id");
            builder.HasIndex(x => x.StatusId).HasDatabaseName("ix_vendor_status_id");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
