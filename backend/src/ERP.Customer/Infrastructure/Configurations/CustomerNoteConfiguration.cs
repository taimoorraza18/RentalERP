using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerNoteConfiguration : IEntityTypeConfiguration<CustomerNote>
{
    public void Configure(EntityTypeBuilder<CustomerNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.NoteCategory).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsPinned).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsConfidential).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerNotes)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerId, x.NoteId })
            .IsUnique()
            .HasDatabaseName("uq_customer_note_customer_id_note_id");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_note_customer_id");

        builder.HasIndex(x => x.NoteId)
            .HasDatabaseName("ix_customer_note_note_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
