using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Vendor.Domain.Entities;

namespace ERP.Vendor.Infrastructure.Configurations;

public sealed class VendorRatingConfiguration : IEntityTypeConfiguration<VendorRating>
{
    public void Configure(EntityTypeBuilder<VendorRating> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.RatingCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.RatingName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.MinimumScore).IsRequired().HasColumnType("decimal(5,2)");
        builder.Property(x => x.MaximumScore).IsRequired().HasColumnType("decimal(5,2)");
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasIndex(x => x.RatingCode)
            .IsUnique()
            .HasDatabaseName("uq_vendor_rating_rating_code");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_vendor_rating_is_active");

        builder.HasCheckConstraint("chk_vendor_rating_score_range", "minimum_score <= maximum_score");
        builder.HasCheckConstraint("chk_vendor_rating_minimum_score", "minimum_score >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
