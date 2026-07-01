using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorTimelineConfiguration : IEntityTypeConfiguration<VendorTimeline>
{
    public void Configure(EntityTypeBuilder<VendorTimeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.TimelineCategory).HasMaxLength(100);
        builder.Property(x => x.IsSystemGenerated).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorTimelines)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.TimelineId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_timeline_vendor_id_timeline_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_timeline_vendor_id");

        builder.HasIndex(x => x.TimelineId)
            .HasDatabaseName("ix_vendor_timeline_timeline_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
