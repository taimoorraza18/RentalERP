using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorNoteConfiguration : IEntityTypeConfiguration<VendorNote>
{
    public void Configure(EntityTypeBuilder<VendorNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.VendorNotes)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.VendorId, x.NoteId })
            .IsUnique()
            .HasDatabaseName("uq_vendor_note_vendor_id_note_id");

        builder.HasIndex(x => x.VendorId)
            .HasDatabaseName("ix_vendor_note_vendor_id");

        builder.HasIndex(x => x.NoteId)
            .HasDatabaseName("ix_vendor_note_note_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
