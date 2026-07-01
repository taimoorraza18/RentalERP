using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorContactConfiguration : IEntityTypeConfiguration<VendorContact>
{
    public void Configure(EntityTypeBuilder<VendorContact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ContactRole).IsRequired().HasMaxLength(100).HasDefaultValue("Primary");
        builder.Property(x => x.Department).HasMaxLength(150);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.ReceiveNotifications).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorContacts)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.ContactId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_contact_vendor_id_contact_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_contact_vendor_id");

        builder.HasIndex(x => x.ContactId)
            .HasDatabaseName("ix_vendor_contact_contact_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
