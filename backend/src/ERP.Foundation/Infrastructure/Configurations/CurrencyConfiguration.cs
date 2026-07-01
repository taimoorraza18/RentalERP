using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(x => x.CurrencyName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Symbol).IsRequired().HasMaxLength(10);
        builder.Property(x => x.IsBaseCurrency).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.CurrencyCode)
            .IsUnique()
            .HasDatabaseName("uq_currency_currency_code");

        builder.HasIndex(x => x.CurrencyName)
            .HasDatabaseName("ix_currency_currency_name");

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("ix_currency_country_id");

        builder.HasIndex(x => new { x.IsActive, x.DisplayOrder })
            .HasDatabaseName("ix_currency_is_active_display_order");

        builder.HasCheckConstraint("chk_currency_decimal_places", "decimal_places BETWEEN 0 AND 6");
    }
}
