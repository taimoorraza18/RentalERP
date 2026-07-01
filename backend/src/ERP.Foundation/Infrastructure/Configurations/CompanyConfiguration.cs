using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.CompanyCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LegalName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.NTN).HasMaxLength(30);
        builder.Property(x => x.STRN).HasMaxLength(30);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Phone).HasMaxLength(30);
        builder.Property(x => x.Website).HasMaxLength(200);
        builder.Property(x => x.LogoPath).HasMaxLength(500);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.City).HasMaxLength(100);
        builder.Property(x => x.Country).HasMaxLength(100);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CompanyCode)
            .IsUnique()
            .HasDatabaseName("uq_company_company_code");

        builder.HasIndex(x => x.CompanyName)
            .HasDatabaseName("ix_company_company_name");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_company_is_active");

        builder.HasIndex(x => new { x.IsActive, x.CompanyName })
            .HasDatabaseName("ix_company_is_active_company_name");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_company_currency_id");

        builder.HasCheckConstraint("chk_company_code_not_empty", "company_code <> ''");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
