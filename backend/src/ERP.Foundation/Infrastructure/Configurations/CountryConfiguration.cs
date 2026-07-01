using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CountryCode).IsRequired().HasMaxLength(2);
        builder.Property(x => x.CountryCode3).IsRequired().HasMaxLength(3);
        builder.Property(x => x.CountryName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Nationality).HasMaxLength(100);
        builder.Property(x => x.PhoneCode).HasMaxLength(10);
        builder.Property(x => x.TimeZone).HasMaxLength(100);
        builder.Property(x => x.DateFormat).IsRequired().HasMaxLength(20).HasDefaultValue("dd/MM/yyyy");
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.CountryCode)
            .IsUnique()
            .HasDatabaseName("uq_country_country_code");

        builder.HasIndex(x => x.CountryCode3)
            .IsUnique()
            .HasDatabaseName("uq_country_country_code3");

        builder.HasIndex(x => x.CountryName)
            .HasDatabaseName("ix_country_country_name");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_country_currency_id");

        builder.HasIndex(x => new { x.IsActive, x.DisplayOrder })
            .HasDatabaseName("ix_country_is_active_display_order");
    }
}
