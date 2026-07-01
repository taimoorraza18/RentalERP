using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
{
    public void Configure(EntityTypeBuilder<PasswordHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.PasswordHash).IsRequired();

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_password_history_user_id");

        builder.HasIndex(x => new { x.UserId, x.ChangedDate })
            .HasDatabaseName("ix_password_history_user_id_changed_date");

        builder.HasIndex(x => x.ChangedBy)
            .HasDatabaseName("ix_password_history_changed_by");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
