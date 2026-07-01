using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class NumberSeriesConfiguration : IEntityTypeConfiguration<NumberSeries>
{
    public void Configure(EntityTypeBuilder<NumberSeries> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ModuleName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DocumentType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SeriesName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Prefix).HasMaxLength(20);
        builder.Property(x => x.Suffix).HasMaxLength(20);
        builder.Property(x => x.Separator).IsRequired().HasMaxLength(5).HasDefaultValue("-");
        builder.Property(x => x.NextNumber).IsRequired().HasDefaultValue(1L);
        builder.Property(x => x.ResetPolicy).IsRequired().HasMaxLength(20).HasDefaultValue("Never");
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.NumberSeries)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.ModuleName, x.DocumentType, x.SeriesName })
            .IsUnique()
            .HasDatabaseName("uq_number_series_company_module_doc_series");

        builder.HasIndex(x => new { x.CompanyId, x.ModuleName, x.DocumentType, x.IsDefault })
            .HasDatabaseName("ix_number_series_company_module_doc_default");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_number_series_company_id");

        builder.HasIndex(x => x.BranchId)
            .HasDatabaseName("ix_number_series_branch_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_number_series_is_active");

        builder.HasCheckConstraint("chk_number_series_next_number_positive", "next_number >= 1");
        builder.HasCheckConstraint("chk_number_series_length_positive", "number_length >= 1");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
