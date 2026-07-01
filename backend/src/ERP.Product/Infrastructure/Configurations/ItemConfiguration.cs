using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Product.Domain.Entities;
using Account = ERP.Accounting.Domain.Entities.Account;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Product.Infrastructure.Configurations;

public sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.ItemCode).IsRequired().HasMaxLength(30);
        builder.Property(x => x.ItemName).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.ItemGroup)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ItemGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ItemCategory)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ItemCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.ItemBrand)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ItemBrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.ItemManufacturer)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ItemManufacturerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Unit)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TaxConfiguration)
            .WithMany()
            .HasForeignKey(x => x.TaxConfigurationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.SalesAccount)
            .WithMany()
            .HasForeignKey(x => x.SalesAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.PurchaseAccount)
            .WithMany()
            .HasForeignKey(x => x.PurchaseAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.CompanyId, x.ItemCode })
            .IsUnique()
            .HasDatabaseName("uq_item_company_id_item_code");

        builder.HasIndex(x => new { x.CompanyId, x.ItemName })
            .HasDatabaseName("ix_item_company_id_item_name");

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_item_company_id");

        builder.HasIndex(x => x.BranchId)
            .HasDatabaseName("ix_item_branch_id");

        builder.HasIndex(x => x.ItemGroupId)
            .HasDatabaseName("ix_item_item_group_id");

        builder.HasIndex(x => x.ItemCategoryId)
            .HasDatabaseName("ix_item_item_category_id");

        builder.HasIndex(x => x.ItemBrandId)
            .HasDatabaseName("ix_item_item_brand_id");

        builder.HasIndex(x => x.ItemManufacturerId)
            .HasDatabaseName("ix_item_item_manufacturer_id");

        builder.HasIndex(x => x.UnitId)
            .HasDatabaseName("ix_item_unit_id");

        builder.HasIndex(x => x.TaxConfigurationId)
            .HasDatabaseName("ix_item_tax_configuration_id");

        builder.HasIndex(x => x.SalesAccountId)
            .HasDatabaseName("ix_item_sales_account_id");

        builder.HasIndex(x => x.PurchaseAccountId)
            .HasDatabaseName("ix_item_purchase_account_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_item_status_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
