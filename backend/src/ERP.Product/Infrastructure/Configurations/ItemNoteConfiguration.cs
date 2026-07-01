using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemNoteConfiguration : IEntityTypeConfiguration<ItemNote>
{
    public void Configure(EntityTypeBuilder<ItemNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemNotes)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.ItemId, x.NoteId })
            .IsUnique()
            .HasDatabaseName("uq_item_note_item_id_note_id");

        builder.HasIndex(x => x.ItemId)
            .HasDatabaseName("ix_item_note_item_id");

        builder.HasIndex(x => x.NoteId)
            .HasDatabaseName("ix_item_note_note_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
