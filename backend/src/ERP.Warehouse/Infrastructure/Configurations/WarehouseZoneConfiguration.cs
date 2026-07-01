using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseZoneConfiguration : IEntityTypeConfiguration<WarehouseZone>
{
    public void Configure(EntityTypeBuilder<WarehouseZone> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ZoneCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ZoneName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.TemperatureControlled).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsRestricted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.SortOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseZones)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.WarehouseId, x.ZoneCode })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_zone_warehouse_id_zone_code");

        builder.HasIndex(x => new { x.WarehouseId, x.ZoneName })
            .HasDatabaseName("ix_warehouse_zone_warehouse_id_zone_name");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_zone_warehouse_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_warehouse_zone_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
