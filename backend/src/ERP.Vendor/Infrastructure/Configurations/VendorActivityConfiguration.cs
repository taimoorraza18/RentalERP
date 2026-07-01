using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorActivityConfiguration : IEntityTypeConfiguration<VendorActivity>
{
    public void Configure(EntityTypeBuilder<VendorActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ActivityCategory).HasMaxLength(100);
        builder.Property(x => x.IsImportant).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorActivities)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.ActivityId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_activity_vendor_id_activity_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_activity_vendor_id");

        builder.HasIndex(x => x.ActivityId)
            .HasDatabaseName("ix_vendor_activity_activity_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
