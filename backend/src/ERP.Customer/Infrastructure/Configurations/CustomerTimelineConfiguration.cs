using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerTimelineConfiguration : IEntityTypeConfiguration<CustomerTimeline>
{
    public void Configure(EntityTypeBuilder<CustomerTimeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.EventType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReferenceModule).HasMaxLength(50);
        builder.Property(x => x.ReferenceNo).HasMaxLength(50);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerTimelines)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_timeline_customer_id");

        builder.HasIndex(x => x.EventDate)
            .HasDatabaseName("ix_customer_timeline_event_date");

        builder.HasIndex(x => x.PerformedBy)
            .HasDatabaseName("ix_customer_timeline_performed_by");

        builder.HasIndex(x => x.EventType)
            .HasDatabaseName("ix_customer_timeline_event_type");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
