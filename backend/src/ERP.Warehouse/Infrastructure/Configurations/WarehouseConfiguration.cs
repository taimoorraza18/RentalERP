using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Warehouse.Infrastructure.Configurations
{
    using Warehouse = ERP.Warehouse.Domain.Entities.Warehouse;
    using WarehouseType = ERP.Warehouse.Domain.Entities.WarehouseType;

    public sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.WarehouseCode).IsRequired().HasMaxLength(30);
            builder.Property(x => x.WarehouseName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ManagerName).HasMaxLength(150);
            builder.Property(x => x.Phone).HasMaxLength(30);
            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.Capacity).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CapacityUnit).HasMaxLength(20);
            builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(x => x.WarehouseType)
                .WithMany(x => x.Warehouses)
                .HasForeignKey(x => x.WarehouseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.CompanyId, x.WarehouseCode })
                .IsUnique()
                .HasDatabaseName("uq_warehouse_company_id_warehouse_code");

            builder.HasIndex(x => new { x.CompanyId, x.WarehouseName })
                .HasDatabaseName("ix_warehouse_company_id_warehouse_name");

            builder.HasIndex(x => x.CompanyId).HasDatabaseName("ix_warehouse_company_id");
            builder.HasIndex(x => x.BranchId).HasDatabaseName("ix_warehouse_branch_id");
            builder.HasIndex(x => x.WarehouseTypeId).HasDatabaseName("ix_warehouse_warehouse_type_id");
            builder.HasIndex(x => x.AddressId).HasDatabaseName("ix_warehouse_address_id");
            builder.HasIndex(x => x.ContactId).HasDatabaseName("ix_warehouse_contact_id");
            builder.HasIndex(x => x.IsActive).HasDatabaseName("ix_warehouse_is_active");

            builder.HasCheckConstraint("chk_warehouse_capacity_positive", "capacity IS NULL OR capacity > 0");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
