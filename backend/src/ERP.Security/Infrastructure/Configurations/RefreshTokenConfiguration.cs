using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.TokenHash).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ReplacedByTokenHash).HasMaxLength(500);
        builder.Property(x => x.IsRevoked).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.UserSession)
            .WithMany()
            .HasForeignKey(x => x.UserSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TokenHash)
            .IsUnique()
            .HasDatabaseName("uq_refresh_token_token_hash");

        builder.HasIndex(x => x.UserSessionId)
            .HasDatabaseName("ix_refresh_token_user_session_id");

        builder.HasIndex(x => x.ExpiryDate)
            .HasDatabaseName("ix_refresh_token_expiry_date");

        builder.HasIndex(x => x.IsRevoked)
            .HasDatabaseName("ix_refresh_token_is_revoked");

        builder.HasCheckConstraint("chk_refresh_token_expiry_after_issued", "expiry_date > issued_date");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
