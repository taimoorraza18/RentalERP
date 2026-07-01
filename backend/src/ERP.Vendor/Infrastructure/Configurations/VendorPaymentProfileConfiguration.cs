using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorPaymentProfileConfiguration : IEntityTypeConfiguration<VendorPaymentProfile>
{
    public void Configure(EntityTypeBuilder<VendorPaymentProfile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ProfileCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ProfileName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.AdvancePaymentPercent).IsRequired().HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.PaymentTermDays).IsRequired().HasDefaultValue(30);

        builder.HasIndex(x => x.ProfileCode)
            .IsUnique()
            .HasDatabaseName("uq_vendor_payment_profile_profile_code");

        builder.HasIndex(x => x.ProfileName)
            .HasDatabaseName("ix_vendor_payment_profile_profile_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_vendor_payment_profile_is_active");

        builder.HasCheckConstraint("chk_vendor_payment_profile_advance_percent", "advance_payment_percent BETWEEN 0 AND 100");
        builder.HasCheckConstraint("chk_vendor_payment_profile_payment_term_days", "payment_term_days >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
