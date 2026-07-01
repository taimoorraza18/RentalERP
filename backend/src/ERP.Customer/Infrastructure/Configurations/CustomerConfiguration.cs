using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Customer.Infrastructure.Configurations
{
    using Customer = ERP.Customer.Domain.Entities.Customer;
    using CustomerGroup = ERP.Customer.Domain.Entities.CustomerGroup;
    using CustomerCategory = ERP.Customer.Domain.Entities.CustomerCategory;
    using CustomerIndustry = ERP.Customer.Domain.Entities.CustomerIndustry;
    using CustomerTerritory = ERP.Customer.Domain.Entities.CustomerTerritory;
    using CustomerPriceLevel = ERP.Customer.Domain.Entities.CustomerPriceLevel;
    using CustomerPaymentProfile = ERP.Customer.Domain.Entities.CustomerPaymentProfile;
    using CustomerCreditProfile = ERP.Customer.Domain.Entities.CustomerCreditProfile;
    using CustomerAddress = ERP.Customer.Domain.Entities.CustomerAddress;
    using CustomerContact = ERP.Customer.Domain.Entities.CustomerContact;
    using CustomerAttachment = ERP.Customer.Domain.Entities.CustomerAttachment;
    using CustomerNote = ERP.Customer.Domain.Entities.CustomerNote;
    using CustomerActivity = ERP.Customer.Domain.Entities.CustomerActivity;
    using CustomerTimeline = ERP.Customer.Domain.Entities.CustomerTimeline;
    using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.CustomerCode).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.LegalName).HasMaxLength(250);
            builder.Property(x => x.Remarks).HasMaxLength(1000);

            builder.HasOne(x => x.CustomerGroup)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerCategory)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerIndustry)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerIndustryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.CustomerTerritory)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerTerritoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.CustomerPriceLevel)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerPriceLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerPaymentProfile)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerPaymentProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerCreditProfile)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerCreditProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TaxConfiguration)
                .WithMany()
                .HasForeignKey(x => x.TaxConfigurationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.CompanyId, x.CustomerCode })
                .IsUnique()
                .HasDatabaseName("uq_customer_company_id_customer_code");

            builder.HasIndex(x => new { x.CompanyId, x.CustomerName })
                .HasDatabaseName("ix_customer_company_id_customer_name");

            builder.HasIndex(x => x.CompanyId).HasDatabaseName("ix_customer_company_id");
            builder.HasIndex(x => x.CustomerGroupId).HasDatabaseName("ix_customer_customer_group_id");
            builder.HasIndex(x => x.CustomerCategoryId).HasDatabaseName("ix_customer_customer_category_id");
            builder.HasIndex(x => x.CustomerIndustryId).HasDatabaseName("ix_customer_customer_industry_id");
            builder.HasIndex(x => x.CustomerTerritoryId).HasDatabaseName("ix_customer_customer_territory_id");
            builder.HasIndex(x => x.CustomerPriceLevelId).HasDatabaseName("ix_customer_customer_price_level_id");
            builder.HasIndex(x => x.CustomerPaymentProfileId).HasDatabaseName("ix_customer_customer_payment_profile_id");
            builder.HasIndex(x => x.CustomerCreditProfileId).HasDatabaseName("ix_customer_customer_credit_profile_id");
            builder.HasIndex(x => x.TaxConfigurationId).HasDatabaseName("ix_customer_tax_configuration_id");
            builder.HasIndex(x => x.StatusId).HasDatabaseName("ix_customer_status_id");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
