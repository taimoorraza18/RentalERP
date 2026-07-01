using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.StoredFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.FileExtension).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.StorageProvider).IsRequired().HasMaxLength(50).HasDefaultValue("Local");
        builder.Property(x => x.StoragePath).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.FileHash).HasMaxLength(128);
        builder.Property(x => x.VersionNo).IsRequired().HasDefaultValue(1);
        builder.Property(x => x.IsEncrypted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsPublic).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Remarks).HasMaxLength(500);

        builder.HasIndex(x => x.FileHash)
            .HasDatabaseName("ix_attachment_file_hash");

        builder.HasIndex(x => x.StorageProvider)
            .HasDatabaseName("ix_attachment_storage_provider");

        builder.HasIndex(x => x.ContentType)
            .HasDatabaseName("ix_attachment_content_type");

        builder.HasCheckConstraint("chk_attachment_file_size_positive", "file_size > 0");
        builder.HasCheckConstraint("chk_attachment_version_no_positive", "version_no >= 1");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
