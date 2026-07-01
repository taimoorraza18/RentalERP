using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.SessionToken).IsRequired();
        builder.Property(x => x.IPAddress).HasMaxLength(50);
        builder.Property(x => x.DeviceName).HasMaxLength(200);
        builder.Property(x => x.Browser).HasMaxLength(200);
        builder.Property(x => x.IsRevoked).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.SecurityPolicy)
            .WithMany()
            .HasForeignKey(x => x.SecurityPolicyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SessionToken)
            .IsUnique()
            .HasDatabaseName("uq_user_session_session_token");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_user_session_user_id");

        builder.HasIndex(x => x.SecurityPolicyId)
            .HasDatabaseName("ix_user_session_security_policy_id");

        builder.HasIndex(x => x.ExpiryTime)
            .HasDatabaseName("ix_user_session_expiry_time");

        builder.HasIndex(x => new { x.UserId, x.IsRevoked })
            .HasDatabaseName("ix_user_session_user_id_is_revoked");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
