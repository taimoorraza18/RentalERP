using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerTerritoryConfiguration : IEntityTypeConfiguration<CustomerTerritory>
{
    public void Configure(EntityTypeBuilder<CustomerTerritory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.TerritoryCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.TerritoryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.ParentTerritory)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentTerritoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.TerritoryCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_territory_company_id_territory_code");

        builder.HasIndex(x => new { x.CompanyId, x.TerritoryName })
            .HasDatabaseName("ix_customer_territory_company_id_territory_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_territory_company_id");

        builder.HasIndex(x => x.ParentTerritoryId)
            .HasDatabaseName("ix_customer_territory_parent_territory_id");

        builder.HasIndex(x => x.ManagerUserId)
            .HasDatabaseName("ix_customer_territory_manager_user_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_territory_status_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
