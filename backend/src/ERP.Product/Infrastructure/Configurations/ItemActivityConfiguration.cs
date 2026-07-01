using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemActivityConfiguration : IEntityTypeConfiguration<ItemActivity>
{
    public void Configure(EntityTypeBuilder<ItemActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemActivities)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.ItemId, x.ActivityId })
            .IsUnique()
            .HasDatabaseName("uq_item_activity_item_id_activity_id");

        builder.HasIndex(x => x.ItemId)
            .HasDatabaseName("ix_item_activity_item_id");

        builder.HasIndex(x => x.ActivityId)
            .HasDatabaseName("ix_item_activity_activity_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
