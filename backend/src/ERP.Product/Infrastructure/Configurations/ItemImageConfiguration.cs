using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemImageConfiguration : IEntityTypeConfiguration<ItemImage>
{
    public void Configure(EntityTypeBuilder<ItemImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemImages)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.ItemId, x.AttachmentId })
            .IsUnique()
            .HasDatabaseName("uq_item_image_item_id_attachment_id");

        builder.HasIndex(x => x.ItemId)
            .HasDatabaseName("ix_item_image_item_id");

        builder.HasIndex(x => x.AttachmentId)
            .HasDatabaseName("ix_item_image_attachment_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
