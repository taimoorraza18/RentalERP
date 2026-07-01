using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorAddressConfiguration : IEntityTypeConfiguration<VendorAddress>
{
    public void Configure(EntityTypeBuilder<VendorAddress> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.AddressType).IsRequired().HasMaxLength(30).HasDefaultValue("Billing");
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefaultBilling).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefaultShipping).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorAddresses)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.AddressId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_address_vendor_id_address_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_address_vendor_id");

        builder.HasIndex(x => x.AddressId)
            .HasDatabaseName("ix_vendor_address_address_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
