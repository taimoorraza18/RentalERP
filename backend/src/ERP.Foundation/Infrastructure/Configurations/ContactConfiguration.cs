using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Title).HasMaxLength(20);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.MiddleName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Designation).HasMaxLength(150);
        builder.Property(x => x.Department).HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.MobileNo).HasMaxLength(30);
        builder.Property(x => x.PhoneNo).HasMaxLength(30);
        builder.Property(x => x.Extension).HasMaxLength(10);
        builder.Property(x => x.WhatsAppNo).HasMaxLength(30);
        builder.Property(x => x.PreferredContactMethod).IsRequired().HasMaxLength(20).HasDefaultValue("Email");
        builder.Property(x => x.Remarks).HasMaxLength(500);

        builder.HasIndex(x => x.Email)
            .HasDatabaseName("ix_contact_email");

        builder.HasIndex(x => x.MobileNo)
            .HasDatabaseName("ix_contact_mobile_no");

        builder.HasIndex(x => x.DisplayName)
            .HasDatabaseName("ix_contact_display_name");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
