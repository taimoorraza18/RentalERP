using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Customer.Domain.Entities;

namespace ERP.Customer.Infrastructure.Configurations;

public sealed class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact>
{
    public void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ContactRole).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsPrimary).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.ReceiveEmail).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.ReceiveSMS).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.ReceiveWhatsApp).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerContacts)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerId, x.ContactId })
            .IsUnique()
            .HasDatabaseName("uq_customer_contact_customer_id_contact_id");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_customer_contact_customer_id");

        builder.HasIndex(x => x.ContactId)
            .HasDatabaseName("ix_customer_contact_contact_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
