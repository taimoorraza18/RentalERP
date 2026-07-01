using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseTimelineConfiguration : IEntityTypeConfiguration<WarehouseTimeline>
{
    public void Configure(EntityTypeBuilder<WarehouseTimeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseTimelines)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.WarehouseId, x.TimelineId })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_timeline_warehouse_id_timeline_id");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_timeline_warehouse_id");

        builder.HasIndex(x => x.TimelineId)
            .HasDatabaseName("ix_warehouse_timeline_timeline_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
