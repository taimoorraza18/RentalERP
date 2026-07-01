using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class SecurityActivityConfiguration : IEntityTypeConfiguration<SecurityActivity>
{
    public void Configure(EntityTypeBuilder<SecurityActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.SecurityPolicy)
            .WithMany(x => x.SecurityActivities)
            .HasForeignKey(x => x.SecurityPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.SecurityPolicyId, x.ActivityId })
            .IsUnique()
            .HasDatabaseName("uq_security_activity_policy_activity");

        builder.HasIndex(x => x.SecurityPolicyId)
            .HasDatabaseName("ix_security_activity_security_policy_id");

        builder.HasIndex(x => x.ActivityId)
            .HasDatabaseName("ix_security_activity_activity_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
