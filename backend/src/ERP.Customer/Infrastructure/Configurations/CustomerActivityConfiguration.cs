using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerActivityConfiguration : IEntityTypeConfiguration<CustomerActivity>
{
    public void Configure(EntityTypeBuilder<CustomerActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ActivityRole).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerActivities)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerId, x.ActivityId })
            .IsUnique()
            .HasDatabaseName("uq_customer_activity_customer_id_activity_id");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_activity_customer_id");

        builder.HasIndex(x => x.ActivityId)
            .HasDatabaseName("ix_customer_activity_activity_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
