using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerIndustryConfiguration : IEntityTypeConfiguration<CustomerIndustry>
{
    public void Configure(EntityTypeBuilder<CustomerIndustry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.IndustryCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.IndustryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => new { x.CompanyId, x.IndustryCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_industry_company_id_industry_code");

        builder.HasIndex(x => new { x.CompanyId, x.IndustryName })
            .HasDatabaseName("ix_customer_industry_company_id_industry_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_industry_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_industry_status_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
