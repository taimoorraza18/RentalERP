using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ActivityType).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Subject).IsRequired().HasMaxLength(250);
        builder.Property(x => x.StartDateTime).IsRequired();

        builder.HasOne(x => x.AssignedToUser)
            .WithMany()
            .HasForeignKey(x => x.AssignedTo)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.CompletedByUser)
            .WithMany()
            .HasForeignKey(x => x.CompletedBy)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.ActivityType)
            .HasDatabaseName("ix_activity_activity_type");

        builder.HasIndex(x => x.StartDateTime)
            .HasDatabaseName("ix_activity_start_date_time");

        builder.HasIndex(x => x.AssignedTo)
            .HasDatabaseName("ix_activity_assigned_to");

        builder.HasIndex(x => x.CompletedBy)
            .HasDatabaseName("ix_activity_completed_by");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_activity_status_id");

        builder.HasCheckConstraint("chk_activity_end_after_start", "end_date_time IS NULL OR end_date_time >= start_date_time");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
