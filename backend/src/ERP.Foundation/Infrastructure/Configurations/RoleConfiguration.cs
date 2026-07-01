using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.RoleCode).IsRequired().HasMaxLength(30);
        builder.Property(x => x.RoleName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsSystemRole).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.RoleCode })
            .IsUnique()
            .HasDatabaseName("uq_role_company_id_role_code");

        builder.HasIndex(x => new { x.CompanyId, x.RoleName })
            .HasDatabaseName("ix_role_company_id_role_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_role_company_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_role_is_active");
    }
}
