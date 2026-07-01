using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CityCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CityName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Country)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.StateProvince)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.StateProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CountryId, x.CityCode })
            .IsUnique()
            .HasDatabaseName("uq_city_country_id_city_code");

        builder.HasIndex(x => new { x.CountryId, x.CityName })
            .HasDatabaseName("ix_city_country_id_city_name");

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("ix_city_country_id");

        builder.HasIndex(x => x.StateProvinceId)
            .HasDatabaseName("ix_city_state_province_id");

        builder.HasIndex(x => new { x.IsActive, x.DisplayOrder })
            .HasDatabaseName("ix_city_is_active_display_order");
    }
}
