using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class SecurityAttachmentConfiguration : IEntityTypeConfiguration<SecurityAttachment>
{
    public void Configure(EntityTypeBuilder<SecurityAttachment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.SecurityPolicy)
            .WithMany(x => x.SecurityAttachments)
            .HasForeignKey(x => x.SecurityPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.SecurityPolicyId, x.AttachmentId })
            .IsUnique()
            .HasDatabaseName("uq_security_attachment_policy_attachment");

        builder.HasIndex(x => x.SecurityPolicyId)
            .HasDatabaseName("ix_security_attachment_security_policy_id");

        builder.HasIndex(x => x.AttachmentId)
            .HasDatabaseName("ix_security_attachment_attachment_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
