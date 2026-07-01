using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
{
    public void Configure(EntityTypeBuilder<StateProvince> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.StateCode).IsRequired().HasMaxLength(10);
        builder.Property(x => x.StateName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.ISOCode).HasMaxLength(20);
        builder.Property(x => x.TaxRegionCode).HasMaxLength(20);
        builder.Property(x => x.CapitalCity).HasMaxLength(100);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Country)
            .WithMany(x => x.StateProvinces)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CountryId, x.StateCode })
            .IsUnique()
            .HasDatabaseName("uq_state_province_country_id_state_code");

        builder.HasIndex(x => new { x.CountryId, x.StateName })
            .HasDatabaseName("ix_state_province_country_id_state_name");

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("ix_state_province_country_id");

        builder.HasIndex(x => new { x.IsActive, x.DisplayOrder })
            .HasDatabaseName("ix_state_province_is_active_display_order");
    }
}
