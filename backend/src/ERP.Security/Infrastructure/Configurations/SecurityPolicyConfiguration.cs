using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class SecurityPolicyConfiguration : IEntityTypeConfiguration<SecurityPolicy>
{
    public void Configure(EntityTypeBuilder<SecurityPolicy> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.PolicyName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.RequireUpperCase).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.RequireLowerCase).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.RequireNumeric).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.RequireSpecialCharacter).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.RequireMfa).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => new { x.CompanyId, x.PolicyName })
            .IsUnique()
            .HasDatabaseName("uq_security_policy_company_id_policy_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_security_policy_company_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_security_policy_status_id");

        builder.HasCheckConstraint("chk_security_policy_min_password_length", "minimum_password_length >= 6");
        builder.HasCheckConstraint("chk_security_policy_max_login_attempts", "max_login_attempts >= 1");
        builder.HasCheckConstraint("chk_security_policy_session_timeout", "session_timeout_minutes >= 1");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
