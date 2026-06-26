# RentalERP v1.0 — Product Domain

> **Architecture:** Domain Driven Design (DDD)
> **Database:** Microsoft SQL Server
> **Application:** .NET Core Web API + Angular
> **Status:** In Progress | **Version:** v3

---

## Domain Overview

The Product Domain is the centralized catalog for inventory items, rental assets, consumables, spare parts and sellable products.

## Business Objectives

- Centralized Item Master
- Inventory Ready
- Rental Ready
- Service Ready
- Accounting Integration

## Aggregate Root

**Item**

## Implementation Order

| # | Table |
|---|---|
| 001 | ItemGroup |
| 002 | ItemCategory |
| 003 | ItemBrand |
| 004 | ItemManufacturer |
| 005 | ItemUnit |
| 006 | ItemTaxProfile |
| 007 | ItemPriceLevel |
| 008 | ItemAttribute |
| 009 | ItemAttributeValue |
| 010 | Item |
| 011 | ItemBarcode |
| 012 | ItemImage |
| 013 | ItemAttachment |
| 014 | ItemNote |
| 015 | ItemActivity |
| 016 | ItemTimeline |

---

## Table of Contents

1. [ItemGroup](#001-itemgroup)
2. [ItemCategory](#002-itemcategory)
3. [ItemBrand](#003-itembrand)
4. [ItemManufacturer](#004-itemmanufacturer)
5. [ItemUnit](#005-itemunit)
6. [ItemTaxProfile](#006-itemtaxprofile)
7. [ItemPriceLevel](#007-itempricelevel)
8. [ItemAttribute](#008-itemattribute)
9. [ItemAttributeValue](#009-itemattributevalue)
10. [Item](#010-item)
11. [ItemBarcode](#011-itembarcode)
12. [ItemImage](#012-itemimage)
13. [ItemAttachment](#013-itemattachment)
14. [ItemNote](#014-itemnote)
15. [ItemActivity](#015-itemactivity)
16. [ItemTimeline](#016-itemtimeline)

---

## 001 ItemGroup

**Classification:** Master Table

### Purpose

Defines logical grouping of items.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemGroupId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| GroupCode | NVARCHAR(20) | No | | | | Unique Code |
| GroupName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemGroup`
- `UQ_GroupCode`, `UQ_GroupName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemGroup |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemGroup (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemGroupCreated`
- `ItemGroupUpdated`
- `ItemGroupActivated`
- `ItemGroupDeactivated`
- `ItemGroupDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 002 ItemCategory

**Classification:** Master Table

### Purpose

Defines business categories for items.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemCategoryId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| CategoryCode | NVARCHAR(20) | No | | | | Unique Code |
| CategoryName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemCategory`
- `UQ_CategoryCode`, `UQ_CategoryName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemCategory |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemCategory (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemCategoryCreated`
- `ItemCategoryUpdated`
- `ItemCategoryActivated`
- `ItemCategoryDeactivated`
- `ItemCategoryDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 003 ItemBrand

**Classification:** Master Table

### Purpose

Defines brands for items.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemBrandId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| BrandCode | NVARCHAR(20) | No | | | | Unique Code |
| BrandName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemBrand`
- `UQ_BrandCode`, `UQ_BrandName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemBrand |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemBrand (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemBrandCreated`
- `ItemBrandUpdated`
- `ItemBrandActivated`
- `ItemBrandDeactivated`
- `ItemBrandDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 004 ItemManufacturer

**Classification:** Master Table

### Purpose

Defines manufacturers for items.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemManufacturerId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ManufacturerCode | NVARCHAR(20) | No | | | | Unique Code |
| ManufacturerName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemManufacturer`
- `UQ_ManufacturerCode`, `UQ_ManufacturerName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemManufacturer |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemManufacturer (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemManufacturerCreated`
- `ItemManufacturerUpdated`
- `ItemManufacturerActivated`
- `ItemManufacturerDeactivated`
- `ItemManufacturerDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 005 ItemUnit

**Classification:** Master Table

### Purpose

Defines units of measure (Each, Box, Kg, Hour, Day, etc.).

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemUnitId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| UnitCode | NVARCHAR(20) | No | | | | Unique Code |
| UnitName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemUnit`
- `UQ_UnitCode`, `UQ_UnitName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemUnit |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemUnit (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemUnitCreated`
- `ItemUnitUpdated`
- `ItemUnitActivated`
- `ItemUnitDeactivated`
- `ItemUnitDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 006 ItemTaxProfile

**Classification:** Master Table

### Purpose

Defines default tax configuration for items.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemTaxProfileId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| TaxProfileCode | NVARCHAR(20) | No | | | | Unique Code |
| TaxProfileName | NVARCHAR(150) | No | | | | Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemTaxProfile`
- `UQ_TaxProfileCode`, `UQ_TaxProfileName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemTaxProfile |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemTaxProfile (1) -> (N) Item`

### Business Rules

- Code and name must be unique.
- Inactive records cannot be assigned.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemTaxProfileCreated`
- `ItemTaxProfileUpdated`
- `ItemTaxProfileActivated`
- `ItemTaxProfileDeactivated`
- `ItemTaxProfileDeleted`

### Developer Notes

Lookup/master table. Filter active, non-deleted records. Complies with global standards.

---

## 007 ItemPriceLevel

**Classification:** Master Table

### Purpose

Defines pricing levels and default pricing strategies.

### Dependencies

- **Depends On:** None
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemPriceLevelId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| PriceLevelCode | NVARCHAR(20) | No | | | | Unique Code |
| PriceLevelName | NVARCHAR(150) | No | | | | Name |
| PriceCalculationMethod | NVARCHAR(20) | No | Fixed | | | Calculation Method |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemPriceLevel`
- `UQ_PriceLevelCode`, `UQ_PriceLevelName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemPriceLevel |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemPriceLevel (1) -> (N) Item`

### Business Rules

- Unique codes and names.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemPriceLevelCreated`
- `ItemPriceLevelUpdated`
- `ItemPriceLevelActivated`
- `ItemPriceLevelDeactivated`
- `ItemPriceLevelDeleted`

### Developer Notes

Lookup/master table. Complies with RentalERP standards.

---

## 008 ItemAttribute

**Classification:** Master Table

### Purpose

Defines configurable attributes such as Color, Size, Capacity and Power.

### Dependencies

- **Depends On:** None
- **Referenced By:** ItemAttributeValue

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemAttributeId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| AttributeCode | NVARCHAR(20) | No | | | | Unique Code |
| AttributeName | NVARCHAR(150) | No | | | | Name |
| DataType | NVARCHAR(20) | No | Text | | | Attribute Type |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemAttribute`
- `UQ_AttributeCode`, `UQ_AttributeName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemAttribute |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemAttribute (1) -> (N) ItemAttributeValue`

### Business Rules

- Unique codes and names.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemAttributeCreated`
- `ItemAttributeUpdated`
- `ItemAttributeActivated`
- `ItemAttributeDeactivated`
- `ItemAttributeDeleted`

### Developer Notes

Lookup/master table. Complies with RentalERP standards.

---

## 009 ItemAttributeValue

**Classification:** Master Table

### Purpose

Defines selectable values for each item attribute.

### Dependencies

- **Depends On:** ItemAttribute
- **Referenced By:** Item

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemAttributeValueId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| AttributeValueCode | NVARCHAR(20) | No | | | | Unique Code |
| AttributeValueName | NVARCHAR(150) | No | | | | Name |
| ItemAttributeId | BIGINT | No | | | ItemAttribute | Parent Attribute |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsActive | BIT | No | 1 | | | Active Flag |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemAttributeValue`
- `FK_ItemAttributeValue_ItemAttribute`
- `UQ_AttributeValueCode`, `UQ_AttributeValueName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemAttributeValue |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |
| Non-Clustered | IX_DisplayOrder |

### Relationships

- `ItemAttribute (1) -> (N) ItemAttributeValue`
- `ItemAttributeValue (1) -> (N) Item`

### Business Rules

- Unique codes and names.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemAttributeValueCreated`
- `ItemAttributeValueUpdated`
- `ItemAttributeValueActivated`
- `ItemAttributeValueDeactivated`
- `ItemAttributeValueDeleted`

### Developer Notes

Lookup/master table. Complies with RentalERP standards.

---

## 010 Item

**Classification:** Master (Aggregate Root)

### Purpose

Aggregate root representing every inventory, rental and service item in the ERP.

### Dependencies

- **Depends On:** Company, Branch, ItemGroup, ItemCategory, ItemBrand, ItemManufacturer, ItemUnit, ItemTaxProfile
- **Referenced By:** ItemBarcode, ItemImage, ItemAttachment, ItemNote, ItemActivity, ItemTimeline, Sales, Rental, Service, Inventory

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Company |
| BranchId | BIGINT | No | | | Branch | Branch |
| ItemCode | NVARCHAR(30) | No | NumberSeries | | | Unique Item Code |
| ItemName | NVARCHAR(200) | No | | | | Item Name |
| ItemGroupId | BIGINT | No | | | ItemGroup | Group |
| ItemCategoryId | BIGINT | Yes | NULL | | ItemCategory | Category |
| ItemBrandId | BIGINT | Yes | NULL | | ItemBrand | Brand |
| ItemManufacturerId | BIGINT | Yes | NULL | | ItemManufacturer | Manufacturer |
| ItemUnitId | BIGINT | No | | | ItemUnit | Unit |
| ItemTaxProfileId | BIGINT | Yes | NULL | | ItemTaxProfile | Tax Profile |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_Item`
- `FK_Item_Company`, `FK_Item_Branch`
- `FK_Item_ItemGroup`, `FK_Item_ItemCategory`, `FK_Item_ItemBrand`
- `FK_Item_ItemManufacturer`, `FK_Item_ItemUnit`, `FK_Item_ItemTaxProfile`
- `UQ_Item_ItemCode`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Item |
| Non-Clustered | IX_Code |
| Non-Clustered | IX_Name |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemBarcode`
- `Item (1) -> (N) ItemImage`
- `Item (1) -> (N) ItemAttachment`
- `Item (1) -> (N) ItemNote`
- `Item (1) -> (N) ItemActivity`
- `Item (1) -> (N) ItemTimeline`

### Business Rules

- ItemCode generated through NumberSeries and is immutable.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemCreated`
- `ItemUpdated`

### Developer Notes

Aggregate root of the Product Domain. Implementation-ready table.

---

## 011 ItemBarcode

**Classification:** Master Table

### Purpose

Stores multiple barcodes per item.

### Dependencies

- **Depends On:** Item
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemBarcodeId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| Barcode | NVARCHAR(100) | No | | | | Barcode |
| IsPrimary | BIT | No | 0 | | | Primary Barcode |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2 | No | SYSUTCDATETIME() | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemBarcode`
- `FK_ItemBarcode_Item`
- `UQ_ItemBarcode_Barcode`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemBarcode |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemBarcode`

### Business Rules

- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemBarcodeCreated`
- `ItemBarcodeUpdated`

### Developer Notes

Implementation-ready table.

---

## 012 ItemImage

**Classification:** Master Table

### Purpose

Associates images with items via the Shared Kernel Attachment.

### Dependencies

- **Depends On:** Item, Attachment (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemImageId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| AttachmentId | BIGINT | No | | | Attachment | Image Attachment |
| DisplayOrder | INT | No | 0 | | | Display Order |
| IsPrimary | BIT | No | 0 | | | Primary Image |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2 | No | SYSUTCDATETIME() | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemImage`
- `FK_ItemImage_Item`
- `FK_ItemImage_Attachment`
- `UQ_ItemImage_ItemId_AttachmentId`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemImage |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemImage`
- `Attachment (1) -> (N) ItemImage`

### Business Rules

- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemImageCreated`
- `ItemImageUpdated`

### Developer Notes

Implementation-ready table.

---

## 013 ItemAttachment

**Classification:** Bridge Table

### Purpose

Associates Item records with reusable Attachment records from the Shared Kernel.

### Dependencies

- **Depends On:** Item, Attachment (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| AttachmentId | BIGINT | No | | | Attachment | Shared Kernel Attachment |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemAttachment`
- `FK_ItemAttachment_Item` → Item
- `FK_ItemAttachment_Attachment` → Attachment
- `UQ_ItemAttachment_ItemId_AttachmentId`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemAttachment |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemAttachment`
- `Attachment (1) -> (N) ItemAttachment`

### Business Rules

- Bridge table only.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemAttachmentAdded`
- `ItemAttachmentUpdated`
- `ItemAttachmentRemoved`

### Developer Notes

Uses Shared Kernel Attachment. No business ownership stored in Shared Kernel.

---

## 014 ItemNote

**Classification:** Bridge Table

### Purpose

Associates Item records with reusable Note records from the Shared Kernel.

### Dependencies

- **Depends On:** Item, Note (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemNoteId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| NoteId | BIGINT | No | | | Note | Shared Kernel Note |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemNote`
- `FK_ItemNote_Item` → Item
- `FK_ItemNote_Note` → Note
- `UQ_ItemNote_ItemId_NoteId`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemNote |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemNote`
- `Note (1) -> (N) ItemNote`

### Business Rules

- Bridge table only.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemNoteAdded`
- `ItemNoteUpdated`
- `ItemNoteRemoved`

### Developer Notes

Uses Shared Kernel Note. No business ownership stored in Shared Kernel.

---

## 015 ItemActivity

**Classification:** Bridge Table

### Purpose

Associates Item records with reusable Activity records from the Shared Kernel.

### Dependencies

- **Depends On:** Item, Activity (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemActivityId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| ActivityId | BIGINT | No | | | Activity | Shared Kernel Activity |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemActivity`
- `FK_ItemActivity_Item` → Item
- `FK_ItemActivity_Activity` → Activity
- `UQ_ItemActivity_ItemId_ActivityId`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemActivity |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemActivity`
- `Activity (1) -> (N) ItemActivity`

### Business Rules

- Bridge table only.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemActivityAdded`
- `ItemActivityUpdated`
- `ItemActivityRemoved`

### Developer Notes

Uses Shared Kernel Activity. No business ownership stored in Shared Kernel.

---

## 016 ItemTimeline

**Classification:** Bridge Table

### Purpose

Associates Item records with reusable Timeline records from the Shared Kernel.

### Dependencies

- **Depends On:** Item, Timeline (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | SQL Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ItemTimelineId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ItemId | BIGINT | No | | | Item | Item |
| TimelineId | BIGINT | No | | | Timeline | Shared Kernel Timeline |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_ItemTimeline`
- `FK_ItemTimeline_Item` → Item
- `FK_ItemTimeline_Timeline` → Timeline
- `UQ_ItemTimeline_ItemId_TimelineId`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_ItemTimeline |
| Non-Clustered | IX_Item |
| Non-Clustered | IX_Status |

### Relationships

- `Item (1) -> (N) ItemTimeline`
- `Timeline (1) -> (N) ItemTimeline`

### Business Rules

- Bridge table only.
- Soft delete only.
- Audit fields and RowVersion mandatory.

### Events Published

- `ItemTimelineAdded`
- `ItemTimelineUpdated`
- `ItemTimelineRemoved`

### Developer Notes

Uses Shared Kernel Timeline. No business ownership stored in Shared Kernel.

---

## Domain Summary

Product Domain is complete. All lookup, master and bridge tables have been documented following the RentalERP locked documentation standard. Version 3 adds ItemUnit and ItemTaxProfile while retaining all previously documented Product Domain sections.

---

*RentalERP v1.0 — Product Domain Documentation | Generated June 2026*
