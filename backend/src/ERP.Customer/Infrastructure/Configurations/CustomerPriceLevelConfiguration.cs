using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerPriceLevelConfiguration : IEntityTypeConfiguration<CustomerPriceLevel>
{
    public void Configure(EntityTypeBuilder<CustomerPriceLevel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.PriceLevelCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.PriceLevelName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.DiscountPercentage).IsRequired().HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(x => x.MarkupPercentage).IsRequired().HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => new { x.CompanyId, x.PriceLevelCode })
            .IsUnique()
            .HasDatabaseName("uq_customer_price_level_company_id_price_level_code");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_customer_price_level_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_customer_price_level_status_id");

        builder.HasCheckConstraint("chk_customer_price_level_discount", "discount_percentage BETWEEN 0 AND 100");
        builder.HasCheckConstraint("chk_customer_price_level_markup", "markup_percentage >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
