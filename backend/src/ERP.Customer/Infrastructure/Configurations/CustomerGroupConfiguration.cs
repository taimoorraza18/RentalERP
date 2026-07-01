using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerGroupConfiguration : IEntityTypeConfiguration<CustomerGroup>
{
    public void Configure(EntityTypeBuilder<CustomerGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.GroupCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.GroupName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => new { x.CompanyId, x.GroupCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_group_company_id_group_code");

        builder.HasIndex(x => new { x.CompanyId, x.GroupName })
            .HasDatabaseName("ix_customer_group_company_id_group_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_group_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_group_status_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
