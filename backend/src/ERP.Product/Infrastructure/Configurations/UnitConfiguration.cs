using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.UnitCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.UnitName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.UnitCode)
            .IsUnique()
            .HasDatabaseName("uq_unit_unit_code");

        builder.HasIndex(x => x.UnitName)
            .HasDatabaseName("ix_unit_unit_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_unit_is_active");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
