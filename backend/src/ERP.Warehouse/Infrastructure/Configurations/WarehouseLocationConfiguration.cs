using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseLocationConfiguration : IEntityTypeConfiguration<WarehouseLocation>
{
    public void Configure(EntityTypeBuilder<WarehouseLocation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.LocationCode).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LocationName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Barcode).HasMaxLength(100);
        builder.Property(x => x.QRCode).HasMaxLength(200);
        builder.Property(x => x.MaximumCapacity).HasColumnType("decimal(18,4)");
        builder.Property(x => x.CapacityUnit).HasMaxLength(20);
        builder.Property(x => x.IsPickingLocation).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsReceivingLocation).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDispatchLocation).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.SortOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseLocations)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.WarehouseZone)
            .WithMany(x => x.WarehouseLocations)
            .HasForeignKey(x => x.WarehouseZoneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.ParentLocation)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.WarehouseId, x.LocationCode })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_location_warehouse_id_location_code");

        builder.HasIndex(x => x.Barcode)
            .HasDatabaseName("ix_warehouse_location_barcode");

        builder.HasIndex(x => x.WarehouseId)
            .HasDatabaseName("ix_warehouse_location_warehouse_id");

        builder.HasIndex(x => x.WarehouseZoneId)
            .HasDatabaseName("ix_warehouse_location_warehouse_zone_id");

        builder.HasIndex(x => x.ParentLocationId)
            .HasDatabaseName("ix_warehouse_location_parent_location_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_warehouse_location_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
