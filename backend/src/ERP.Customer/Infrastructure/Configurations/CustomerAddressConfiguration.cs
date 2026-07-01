using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.AddressType).IsRequired().HasMaxLength(30);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefaultBilling).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsDefaultShipping).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerAddresses)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerId, x.AddressId })
            .IsUnique()
            .HasDatabaseName("uq_customer_address_customer_id_address_id");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_address_customer_id");

        builder.HasIndex(x => x.AddressId)
            .HasDatabaseName("ix_customer_address_address_id");

        builder.HasIndex(x => new { x.CustomerId, x.IsPrimary })
            .HasDatabaseName("ix_customer_address_customer_id_is_primary");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
