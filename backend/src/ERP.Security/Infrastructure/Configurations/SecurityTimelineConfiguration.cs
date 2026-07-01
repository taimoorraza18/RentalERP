using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class SecurityTimelineConfiguration : IEntityTypeConfiguration<SecurityTimeline>
{
    public void Configure(EntityTypeBuilder<SecurityTimeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.SecurityPolicy)
            .WithMany(x => x.SecurityTimelines)
            .HasForeignKey(x => x.SecurityPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.SecurityPolicyId, x.TimelineId })
            .IsUnique()
            .HasDatabaseName("uq_security_timeline_policy_timeline");

        builder.HasIndex(x => x.SecurityPolicyId)
            .HasDatabaseName("ix_security_timeline_security_policy_id");

        builder.HasIndex(x => x.TimelineId)
            .HasDatabaseName("ix_security_timeline_timeline_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
