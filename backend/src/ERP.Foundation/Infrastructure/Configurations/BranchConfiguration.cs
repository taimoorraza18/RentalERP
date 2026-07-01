using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.BranchCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.BranchName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Phone).HasMaxLength(30);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.PostalCode).HasMaxLength(20);
        builder.Property(x => x.IsHeadOffice).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Branches)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ManagerUser)
            .WithMany()
            .HasForeignKey(x => x.ManagerUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.StateProvince)
            .WithMany()
            .HasForeignKey(x => x.StateProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.BranchCode })
            .IsUnique()
            .HasDatabaseName("uq_branch_company_id_branch_code");

        builder.HasIndex(x => new { x.CompanyId, x.BranchName })
            .HasDatabaseName("ix_branch_company_id_branch_name");

        builder.HasIndex(x => x.ManagerUserId)
            .HasDatabaseName("ix_branch_manager_user_id");

        builder.HasIndex(x => x.CityId)
            .HasDatabaseName("ix_branch_city_id");

        builder.HasIndex(x => x.StateProvinceId)
            .HasDatabaseName("ix_branch_state_province_id");

        builder.HasIndex(x => x.CountryId)
            .HasDatabaseName("ix_branch_country_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_branch_is_active");

        builder.HasCheckConstraint("chk_branch_code_not_empty", "branch_code <> ''");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
