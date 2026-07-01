using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseActivityConfiguration : IEntityTypeConfiguration<WarehouseActivity>
{
    public void Configure(EntityTypeBuilder<WarehouseActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseActivities)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.WarehouseId, x.ActivityId })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_activity_warehouse_id_activity_id");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_activity_warehouse_id");

        builder.HasIndex(x => x.ActivityId)
            .HasDatabaseName("ix_warehouse_activity_activity_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
