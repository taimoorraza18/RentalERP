using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.UserCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.PasswordSalt).HasMaxLength(500);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Mobile).HasMaxLength(30);
        builder.Property(x => x.IsLocked).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsSystemAdmin).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.FailedLoginAttempts).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DefaultBranch)
            .WithMany()
            .HasForeignKey(x => x.DefaultBranchId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.CompanyId, x.UserCode })
            .IsUnique()
            .HasDatabaseName("uq_user_company_id_user_code");

        builder.HasIndex(x => new { x.CompanyId, x.Username })
            .IsUnique()
            .HasDatabaseName("uq_user_company_id_username");

        builder.HasIndex(x => new { x.CompanyId, x.Email })
            .IsUnique()
            .HasDatabaseName("uq_user_company_id_email");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_user_company_id");

        builder.HasIndex(x => x.DefaultBranchId)
            .HasDatabaseName("ix_user_default_branch_id");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_user_is_active");

        builder.HasIndex(x => x.IsLocked)
            .HasDatabaseName("ix_user_is_locked");

        builder.HasCheckConstraint("chk_user_failed_login_attempts", "failed_login_attempts >= 0");
    }
}
