using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.KeyName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ApiKeyValue).IsRequired().HasMaxLength(500);
        builder.Property(x => x.SecretKey).IsRequired().HasMaxLength(500);
        builder.Property(x => x.IsRevoked).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => x.ApiKeyValue)
            .IsUnique()
            .HasDatabaseName("uq_api_key_api_key_value");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_api_key_user_id");

        builder.HasIndex(x => x.IsRevoked)
            .HasDatabaseName("ix_api_key_is_revoked");

        builder.HasIndex(x => x.ExpiryDate)
            .HasDatabaseName("ix_api_key_expiry_date");

        builder.HasCheckConstraint("chk_api_key_requests_per_minute", "requests_per_minute >= 0");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
