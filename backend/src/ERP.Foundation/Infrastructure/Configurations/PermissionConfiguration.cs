using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.PermissionKey).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Module).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Feature).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Action).IsRequired().HasMaxLength(30);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsSystemPermission).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasIndex(x => x.PermissionKey)
            .IsUnique()
            .HasDatabaseName("uq_permission_permission_key");

        builder.HasIndex(x => new { x.Module, x.Feature, x.Action })
            .HasDatabaseName("ix_permission_module_feature_action");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_permission_is_active");
    }
}
