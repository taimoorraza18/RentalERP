using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.AddressType).IsRequired().HasMaxLength(30);
        builder.Property(x => x.AddressLine1).IsRequired().HasMaxLength(250);
        builder.Property(x => x.AddressLine2).HasMaxLength(250);
        builder.Property(x => x.PostalCode).HasMaxLength(20);
        builder.Property(x => x.Latitude).HasColumnType("decimal(10,8)");
        builder.Property(x => x.Longitude).HasColumnType("decimal(11,8)");
        builder.Property(x => x.GooglePlaceId).HasMaxLength(100);
        builder.Property(x => x.Remarks).HasMaxLength(500);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.StateProvince)
            .WithMany()
            .HasForeignKey(x => x.StateProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("ix_address_country_id");

        builder.HasIndex(x => x.StateProvinceId)
            .HasDatabaseName("ix_address_state_province_id");

        builder.HasIndex(x => x.CityId)
            .HasDatabaseName("ix_address_city_id");

        builder.HasIndex(x => x.AddressType)
            .HasDatabaseName("ix_address_address_type");

        builder.HasCheckConstraint("chk_address_type_not_empty", "address_type <> ''");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
