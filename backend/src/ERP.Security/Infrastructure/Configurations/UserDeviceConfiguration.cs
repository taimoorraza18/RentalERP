using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.DeviceIdentifier).IsRequired().HasMaxLength(500);
        builder.Property(x => x.DeviceName).HasMaxLength(200);
        builder.Property(x => x.OperatingSystem).HasMaxLength(100);
        builder.Property(x => x.Browser).HasMaxLength(200);
        builder.Property(x => x.IsTrusted).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => new { x.UserId, x.DeviceIdentifier })
            .IsUnique()
            .HasDatabaseName("uq_user_device_user_id_device_identifier");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_user_device_user_id");

        builder.HasIndex(x => x.IsTrusted)
            .HasDatabaseName("ix_user_device_is_trusted");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
