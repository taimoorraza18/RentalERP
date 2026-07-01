using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemTimelineConfiguration : IEntityTypeConfiguration<ItemTimeline>
{
    public void Configure(EntityTypeBuilder<ItemTimeline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemTimelines)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.ItemId, x.TimelineId })
            .IsUnique()
            .HasDatabaseName("uq_item_timeline_item_id_timeline_id");

        builder.HasIndex(x => x.ItemId)
            .HasDatabaseName("ix_item_timeline_item_id");

        builder.HasIndex(x => x.TimelineId)
            .HasDatabaseName("ix_item_timeline_timeline_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
