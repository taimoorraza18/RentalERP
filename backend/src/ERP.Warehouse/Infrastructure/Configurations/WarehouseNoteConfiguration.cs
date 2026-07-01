using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseNoteConfiguration : IEntityTypeConfiguration<WarehouseNote>
{
    public void Configure(EntityTypeBuilder<WarehouseNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseNotes)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.WarehouseId, x.NoteId })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_note_warehouse_id_note_id");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_note_warehouse_id");

        builder.HasIndex(x => x.NoteId)
            .HasDatabaseName("ix_warehouse_note_note_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
