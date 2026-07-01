using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class MfaVerificationConfiguration : IEntityTypeConfiguration<MfaVerification>
{
    public void Configure(EntityTypeBuilder<MfaVerification> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.VerificationCodeHash).HasMaxLength(500);
        builder.Property(x => x.IsSuccessful).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.AttemptCount).IsRequired().HasDefaultValue((short)0);

        builder.HasOne(x => x.UserSession)
            .WithMany(x => x.MfaVerifications)
            .HasForeignKey(x => x.UserSessionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_mfa_verification_user_id");

        builder.HasIndex(x => x.UserSessionId)
            .HasDatabaseName("ix_mfa_verification_user_session_id");

        builder.HasIndex(x => x.ExpiryDate)
            .HasDatabaseName("ix_mfa_verification_expiry_date");

        builder.HasIndex(x => new { x.UserId, x.IsSuccessful })
            .HasDatabaseName("ix_mfa_verification_user_id_is_successful");

        builder.HasCheckConstraint("chk_mfa_verification_expiry_after_generated", "expiry_date > generated_date");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
