using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorAttachmentConfiguration : IEntityTypeConfiguration<VendorAttachment>
{
    public void Configure(EntityTypeBuilder<VendorAttachment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.AttachmentType).IsRequired().HasMaxLength(50).HasDefaultValue("General");
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorAttachments)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.AttachmentId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_attachment_vendor_id_attachment_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_attachment_vendor_id");

        builder.HasIndex(x => x.AttachmentId)
            .HasDatabaseName("ix_vendor_attachment_attachment_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
