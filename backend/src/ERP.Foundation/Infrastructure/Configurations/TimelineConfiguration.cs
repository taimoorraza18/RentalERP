using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class TimelineConfiguration : IEntityTypeConfiguration<Timeline>
{
    public void Configure(EntityTypeBuilder<Timeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.EventType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ReferenceModule).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReferenceNo).HasMaxLength(50);
        builder.Property(x => x.IsSystemGenerated).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.PerformedByUser)
            .WithMany()
            .HasForeignKey(x => x.PerformedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ReferenceModule, x.ReferenceId })
            .HasDatabaseName("ix_timeline_reference_module_reference_id");

        builder.HasIndex(x => x.PerformedBy)
            .HasDatabaseName("ix_timeline_performed_by");

        builder.HasIndex(x => x.EventDate)
            .HasDatabaseName("ix_timeline_event_date");

        builder.HasIndex(x => x.EventType)
            .HasDatabaseName("ix_timeline_event_type");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
