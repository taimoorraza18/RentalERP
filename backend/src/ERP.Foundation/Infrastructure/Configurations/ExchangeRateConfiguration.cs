using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Rate).IsRequired().HasColumnType("decimal(18,8)");
        builder.Property(x => x.BuyingRate).HasColumnType("decimal(18,8)");
        builder.Property(x => x.SellingRate).HasColumnType("decimal(18,8)");
        builder.Property(x => x.Source).HasMaxLength(100);
        builder.Property(x => x.Remarks).HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.FromCurrency)
            .WithMany()
            .HasForeignKey(x => x.FromCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToCurrency)
            .WithMany()
            .HasForeignKey(x => x.ToCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.FromCurrencyId, x.ToCurrencyId, x.RateDate })
            .IsUnique()
            .HasDatabaseName("uq_exchange_rate_company_from_to_date");

        builder.HasIndex(x => new { x.RateDate, x.FromCurrencyId, x.ToCurrencyId })
            .HasDatabaseName("ix_exchange_rate_rate_date_from_to");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_exchange_rate_is_active");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_exchange_rate_company_id");

        builder.HasIndex(x => x.FromCurrencyId)
            .HasDatabaseName("ix_exchange_rate_from_currency_id");

        builder.HasIndex(x => x.ToCurrencyId)
            .HasDatabaseName("ix_exchange_rate_to_currency_id");

        builder.HasCheckConstraint("chk_exchange_rate_positive", "rate > 0");
        builder.HasCheckConstraint("chk_exchange_rate_different_currencies", "from_currency_id <> to_currency_id");
    }
}
