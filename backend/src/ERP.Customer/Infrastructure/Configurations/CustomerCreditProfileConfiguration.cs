using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerCreditProfileConfiguration : IEntityTypeConfiguration<CustomerCreditProfile>
{
    public void Configure(EntityTypeBuilder<CustomerCreditProfile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CreditProfileCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CreditProfileName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.DefaultCreditLimit).IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0m);
        builder.Property(x => x.MaximumOutstanding).IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0m);
        builder.Property(x => x.RequireApprovalOverLimit).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.AllowCreditHoldOverride).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.MaximumOverdueDays).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => new { x.CompanyId, x.CreditProfileCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_credit_profile_company_id_code");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_credit_profile_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_credit_profile_status_id");

        builder.HasCheckConstraint("chk_customer_credit_profile_credit_limit", "default_credit_limit >= 0");
        builder.HasCheckConstraint("chk_customer_credit_profile_outstanding", "maximum_outstanding >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
