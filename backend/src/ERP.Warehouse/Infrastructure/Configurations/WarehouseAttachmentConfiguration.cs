using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseAttachmentConfiguration : IEntityTypeConfiguration<WarehouseAttachment>
{
    public void Configure(EntityTypeBuilder<WarehouseAttachment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseAttachments)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.WarehouseId, x.AttachmentId })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_attachment_warehouse_id_attachment_id");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_attachment_warehouse_id");

        builder.HasIndex(x => x.AttachmentId)
            .HasDatabaseName("ix_warehouse_attachment_attachment_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
