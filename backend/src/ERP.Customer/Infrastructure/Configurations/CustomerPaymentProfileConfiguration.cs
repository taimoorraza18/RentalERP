using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerPaymentProfileConfiguration : IEntityTypeConfiguration<CustomerPaymentProfile>
{
    public void Configure(EntityTypeBuilder<CustomerPaymentProfile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.PaymentProfileCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.PaymentProfileName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.CreditLimit).IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0m);
        builder.Property(x => x.AllowPartialPayment).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.AllowAdvancePayment).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.PaymentTermDays).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.GracePeriodDays).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => new { x.CompanyId, x.PaymentProfileCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_payment_profile_company_id_code");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_payment_profile_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_payment_profile_status_id");

        builder.HasCheckConstraint("chk_customer_payment_profile_credit_limit", "credit_limit >= 0");
        builder.HasCheckConstraint("chk_customer_payment_profile_payment_term_days", "payment_term_days >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
