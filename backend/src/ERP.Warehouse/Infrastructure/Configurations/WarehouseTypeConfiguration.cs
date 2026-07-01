using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Warehouse.Domain.Entities;

namespace ERP.Warehouse.Infrastructure.Configurations;

public sealed class WarehouseTypeConfiguration : IEntityTypeConfiguration<WarehouseType>
{
    public void Configure(EntityTypeBuilder<WarehouseType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.TypeCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.TypeName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsSystemDefined).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.SortOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => new { x.CompanyId, x.TypeCode })
            .IsUnique()
            .HasDatabaseName("uq_warehouse_type_company_id_type_code");

        builder.HasIndex(x => new { x.CompanyId, x.TypeName })
            .HasDatabaseName("ix_warehouse_type_company_id_type_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_warehouse_type_company_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_warehouse_type_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
