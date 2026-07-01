using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerAttachmentConfiguration : IEntityTypeConfiguration<CustomerAttachment>
{
    public void Configure(EntityTypeBuilder<CustomerAttachment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.AttachmentCategory).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerAttachments)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerId, x.AttachmentId })
            .IsUnique()
            .HasDatabaseName("uq_customer_attachment_customer_id_attachment_id");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_attachment_customer_id");

        builder.HasIndex(x => x.AttachmentId)
            .HasDatabaseName("ix_customer_attachment_attachment_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
