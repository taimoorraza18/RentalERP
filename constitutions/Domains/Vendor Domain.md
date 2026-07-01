# RentalERP v1.0 — Vendor Domain

> **Architecture:** Domain Driven Design (DDD)
> **Database:** Microsoft SQL Server
> **Application:** .NET Core Web API + Angular
> **Status:** In Progress | **Version:** 1.0

---

## Table of Contents

1. [VendorGroup](#001-vendorgroup)
2. [VendorCategory](#002-vendorcategory)
3. [VendorIndustry](#003-vendorindustry)
4. [VendorTerritory](#004-vendorterritory)
5. [VendorPaymentProfile](#005-vendorpaymentprofile)
6. [VendorRating](#006-vendorrating)
7. [VendorTaxProfile](#007-vendortaxprofile)
8. [Vendor](#008-vendor)
9. [VendorAddress](#009-vendoraddress)
10. [VendorContact](#010-vendorcontact)
11. [VendorAttachment](#011-vendorattachment)
12. [VendorNote](#012-vendornote)
13. [VendorActivity](#013-vendoractivity)
14. [VendorTimeline](#014-vendortimeline)

---

## 001 VendorGroup

**Classification:** Configuration (Master) Table

### Purpose

Stores vendor groups used for procurement classification, reporting, payment policies and analytics.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorGroupId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| GroupCode | NVARCHAR(20) | No | | | | Business Code |
| GroupName | NVARCHAR(150) | No | | | | Display Name |
| Description | NVARCHAR(500) | Yes | | | | Description |
| DisplayOrder | INT | No | 0 | | | Sort Order |
| IsDefault | BIT | No | 0 | | | Default Group |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_VendorGroup`
- `FK_VendorGroup_Company`
- `FK_VendorGroup_CreatedBy`
- `FK_VendorGroup_ModifiedBy`
- `FK_VendorGroup_DeletedBy`
- `UQ (CompanyId, GroupCode)`
- `UQ (CompanyId, GroupName)`
- `CHECK(DisplayOrder >= 0)`
- Only one default group per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorGroup |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, GroupName) |

### Relationships

- `Company (1) -> (N) VendorGroup`
- `VendorGroup (1) -> (N) Vendor`

### Business Rules

- GroupCode generated via NumberSeries.
- Cannot delete while referenced by Vendor.
- Soft delete only.

### Events Published

- `VendorGroupCreated`
- `VendorGroupUpdated`
- `VendorGroupActivated`
- `VendorGroupInactivated`

### Developer Notes

VendorGroup categorizes suppliers for procurement workflows and reporting.

---

## 002 VendorCategory

**Classification:** Configuration (Master) Table

### Purpose

Defines vendor categories used for procurement segmentation, reporting, sourcing strategies and analytics.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorCategoryId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| CategoryCode | NVARCHAR(20) | No | | | | Business Code |
| CategoryName | NVARCHAR(150) | No | | | | Category Name |
| Description | NVARCHAR(500) | Yes | | | | Description |
| DisplayOrder | INT | No | 0 | | | Sort Order |
| IsDefault | BIT | No | 0 | | | Default Category |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_VendorCategory`
- `FK_VendorCategory_Company`
- `FK_VendorCategory_CreatedBy`
- `FK_VendorCategory_ModifiedBy`
- `FK_VendorCategory_DeletedBy`
- `UQ (CompanyId, CategoryCode)`
- `UQ (CompanyId, CategoryName)`
- `CHECK(DisplayOrder >= 0)`
- Only one default category per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorCategory |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, CategoryName) |

### Relationships

- `Company (1) -> (N) VendorCategory`
- `VendorCategory (1) -> (N) Vendor`

### Business Rules

- CategoryCode generated through NumberSeries.
- CategoryName unique within Company.
- Cannot delete while referenced by Vendor.
- Soft delete only.

### Events Published

- `VendorCategoryCreated`
- `VendorCategoryUpdated`
- `VendorCategoryActivated`
- `VendorCategoryInactivated`

### Developer Notes

VendorCategory classifies suppliers for purchasing policies, reporting and analytics.

---

## 003 VendorIndustry

**Classification:** Configuration (Master) Table

### Purpose

Defines the industries or business sectors in which vendors operate. Enables classification, reporting, filtering, procurement analysis and vendor segmentation. Each Vendor belongs to one VendorIndustry.

Examples include: Construction, Manufacturing, Heavy Equipment, Logistics, Electrical, Mechanical, IT Services, Oil & Gas, Mining, Government Supplier, Medical Equipment, Transportation.

### Dependencies

- **Depends On:** None
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorIndustryId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| IndustryCode | NVARCHAR(20) | No | | | | Unique Industry Code |
| IndustryName | NVARCHAR(150) | No | | | | Industry Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| IsActive | BIT | No | 1 | | | Active Flag |
| DisplayOrder | INT | No | 0 | | | Display Sequence |
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

- `PK_VendorIndustry (VendorIndustryId)`
- `UQ_VendorIndustry_IndustryCode`
- `UQ_VendorIndustry_IndustryName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorIndustry |
| Non-Clustered | IX_VendorIndustry_Name (IndustryName) |
| Non-Clustered | IX_VendorIndustry_Status (StatusId, IsDeleted, IsActive) |
| Non-Clustered | IX_VendorIndustry_DisplayOrder (DisplayOrder) |
| Non-Clustered | IX_VendorIndustry_Code (IndustryCode) |

### Relationships

- `VendorIndustry (1) -> (N) Vendor`

### Business Rules

- Industry names must be unique.
- Industry codes must be unique.
- Soft deleted industries cannot be assigned to new vendors.
- Inactive industries cannot be selected during vendor creation.
- Industry cannot be physically deleted.
- Audit fields and RowVersion are mandatory.

### Events Published

- `VendorIndustryCreated`
- `VendorIndustryUpdated`
- `VendorIndustryActivated`
- `VendorIndustryDeactivated`
- `VendorIndustryDeleted`

### Developer Notes

Lookup/master table only. Reusable across procurement, reporting, analytics and dashboards. Always filter active, non-deleted records in lookup screens.

---

## 004 VendorTerritory

**Classification:** Configuration (Master) Table

### Purpose

Defines the geographical or business territories assigned to vendors for procurement, reporting, performance analysis and operational segmentation.

Examples include: North Region, South Region, East Region, West Region, Karachi, Lahore, Islamabad, International.

### Dependencies

- **Depends On:** None
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorTerritoryId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| TerritoryCode | NVARCHAR(20) | No | | | | Unique Territory Code |
| TerritoryName | NVARCHAR(150) | No | | | | Territory Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| IsActive | BIT | No | 1 | | | Active Flag |
| DisplayOrder | INT | No | 0 | | | Display Order |
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

- `PK_VendorTerritory (VendorTerritoryId)`
- `UQ_VendorTerritory_TerritoryCode`
- `UQ_VendorTerritory_TerritoryName`
- `CHECK (DisplayOrder >= 0)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorTerritory |
| Non-Clustered | IX_VendorTerritory_Name |
| Non-Clustered | IX_VendorTerritory_Code |
| Non-Clustered | IX_VendorTerritory_Status (StatusId, IsDeleted, IsActive) |
| Non-Clustered | IX_VendorTerritory_DisplayOrder |

### Relationships

- `VendorTerritory (1) -> (N) Vendor`

### Business Rules

- Territory names and codes must be unique.
- Inactive territories cannot be assigned to new vendors.
- Soft-deleted territories remain available for historical records only.
- Physical deletion is not allowed.
- Referenced territories cannot be deleted while active vendors exist.
- Audit fields and RowVersion are mandatory.

### Events Published

- `VendorTerritoryCreated`
- `VendorTerritoryUpdated`
- `VendorTerritoryActivated`
- `VendorTerritoryDeactivated`
- `VendorTerritoryDeleted`

### Developer Notes

Master lookup table used for reporting, procurement and analytics. Filter active, non-deleted records in lookups.

---

## 005 VendorPaymentProfile

**Classification:** Configuration (Master) Table

### Purpose

Defines standard payment terms and payment behavior templates assigned to vendors.

Examples include: Cash, Net 15, Net 30, Net 45, Advance Payment, Partial Advance, Credit Supplier.

### Dependencies

- **Depends On:** None
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorPaymentProfileId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| ProfileCode | NVARCHAR(20) | No | | | | Unique Profile Code |
| ProfileName | NVARCHAR(150) | No | | | | Payment Profile Name |
| PaymentTermDays | INT | No | 0 | | | Default Payment Term (Days) |
| AdvancePaymentPercent | DECIMAL(5,2) | No | 0 | | | Advance Payment % |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
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

- `PK_VendorPaymentProfile`
- `UQ_VendorPaymentProfile_ProfileCode`
- `UQ_VendorPaymentProfile_ProfileName`
- `CHECK (PaymentTermDays >= 0)`
- `CHECK (AdvancePaymentPercent BETWEEN 0 AND 100)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorPaymentProfile |
| Non-Clustered | IX_ProfileName |
| Non-Clustered | IX_ProfileCode |
| Non-Clustered | IX_Status (StatusId, IsDeleted, IsActive) |

### Relationships

- `VendorPaymentProfile (1) -> (N) Vendor`

### Business Rules

- Profile name and code must be unique.
- Payment terms cannot be negative.
- Advance payment percentage must be between 0 and 100.
- Inactive profiles cannot be assigned to new vendors.
- Profiles referenced by vendors cannot be deleted.
- Soft delete only; physical deletion is prohibited.
- Audit fields and RowVersion are mandatory.

### Events Published

- `VendorPaymentProfileCreated`
- `VendorPaymentProfileUpdated`
- `VendorPaymentProfileActivated`
- `VendorPaymentProfileDeactivated`
- `VendorPaymentProfileDeleted`

### Developer Notes

Master lookup table. Provides default payment behavior during vendor creation. Can be referenced by procurement and accounts payable modules. Always filter active, non-deleted records.

---

## 006 VendorRating

**Classification:** Configuration (Master) Table

### Purpose

Defines the rating classifications used to evaluate vendor performance. Ratings are used for procurement decisions, reporting, scorecards and vendor qualification.

Examples include: Platinum, Gold, Silver, Bronze, Preferred, Approved, Conditional, Blacklisted.

### Dependencies

- **Depends On:** None
- **Referenced By:** Vendor

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorRatingId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| RatingCode | NVARCHAR(20) | No | | | | Unique Rating Code |
| RatingName | NVARCHAR(100) | No | | | | Rating Name |
| MinimumScore | DECIMAL(5,2) | No | 0 | | | Minimum Score |
| MaximumScore | DECIMAL(5,2) | No | 100 | | | Maximum Score |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
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

- `PK_VendorRating`
- `UQ_VendorRating_RatingCode`
- `UQ_VendorRating_RatingName`
- `CHECK (MinimumScore >= 0)`
- `CHECK (MaximumScore <= 100)`
- `CHECK (MinimumScore <= MaximumScore)`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorRating |
| Non-Clustered | IX_VendorRating_Name |
| Non-Clustered | IX_VendorRating_Code |
| Non-Clustered | IX_VendorRating_Status (StatusId, IsDeleted, IsActive) |

### Relationships

- `VendorRating (1) -> (N) Vendor`

### Business Rules

- Rating code and name must be unique.
- Score range must be between 0 and 100.
- Minimum score cannot exceed maximum score.
- Score ranges should not overlap between ratings.
- Inactive ratings cannot be assigned to new vendors.
- Ratings referenced by vendors cannot be physically deleted.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorRatingCreated`
- `VendorRatingUpdated`
- `VendorRatingActivated`
- `VendorRatingDeactivated`
- `VendorRatingDeleted`

### Developer Notes

Lookup/master table only. Used by procurement, reporting and analytics. Always filter active, non-deleted ratings in lookup screens.

---

## 008 Vendor

**Classification:** Master (Aggregate Root)

### Purpose

Stores the master information of suppliers providing goods, services, rental assets or maintenance support. Central aggregate of the Vendor Domain — references all vendor lookup tables while integrating with purchasing, inventory, rental, service and accounting modules.

### Dependencies

- **Depends On:** Company, Branch, VendorGroup, VendorCategory, VendorIndustry, VendorTerritory, VendorPaymentProfile, VendorRating
- **Referenced By:** VendorAddress, VendorContact, VendorAttachment, VendorNote, VendorActivity, VendorTimeline, Purchase modules

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Company |
| BranchId | BIGINT | No | | | Branch | Branch |
| VendorCode | NVARCHAR(30) | No | NumberSeries | | | Unique Vendor Code |
| VendorName | NVARCHAR(200) | No | | | | Legal/Vendor Name |
| DisplayName | NVARCHAR(200) | Yes | NULL | | | Display Name |
| VendorGroupId | BIGINT | No | | | VendorGroup | Vendor Group |
| VendorCategoryId | BIGINT | No | | | VendorCategory | Vendor Category |
| VendorIndustryId | BIGINT | Yes | NULL | | VendorIndustry | Vendor Industry |
| VendorTerritoryId | BIGINT | Yes | NULL | | VendorTerritory | Vendor Territory |
| VendorPaymentProfileId | BIGINT | Yes | NULL | | VendorPaymentProfile | Payment Profile |
| VendorRatingId | BIGINT | Yes | NULL | | VendorRating | Rating |
| TaxConfigurationId | BIGINT | Yes | NULL | | TaxConfiguration | Tax Configuration |
| Email | NVARCHAR(255) | Yes | NULL | | | Primary Email |
| Phone | NVARCHAR(50) | Yes | NULL | | | Primary Phone |
| Mobile | NVARCHAR(50) | Yes | NULL | | | Primary Mobile |
| Website | NVARCHAR(255) | Yes | NULL | | | Website |
| NTN | NVARCHAR(50) | Yes | NULL | | | Tax Number |
| STRN | NVARCHAR(50) | Yes | NULL | | | Sales Tax Registration |
| IsActive | BIT | No | 1 | | | Active |
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

- `PK_Vendor`
- `FK_Vendor_Company` → Company
- `FK_Vendor_Branch` → Branch
- `FK_Vendor_VendorGroup` → VendorGroup
- `FK_Vendor_VendorCategory` → VendorCategory
- `FK_Vendor_VendorIndustry` → VendorIndustry
- `FK_Vendor_VendorTerritory` → VendorTerritory
- `FK_Vendor_VendorPaymentProfile` → VendorPaymentProfile
- `FK_Vendor_VendorRating` → VendorRating
- `FK_Vendor_TaxConfiguration` → TaxConfiguration
- `UQ_Vendor_VendorCode`
- `UQ_Vendor_CompanyId_VendorName`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Vendor |
| Non-Clustered | IX_Vendor_Code |
| Non-Clustered | IX_Vendor_Name |
| Non-Clustered | IX_Vendor_Group |
| Non-Clustered | IX_Vendor_Category |
| Non-Clustered | IX_Vendor_Status (CompanyId, BranchId, StatusId, IsDeleted, IsActive) |

### Relationships

- `Vendor (1) -> (N) VendorAddress`
- `Vendor (1) -> (N) VendorContact`
- `Vendor (1) -> (N) VendorAttachment`
- `Vendor (1) -> (N) VendorNote`
- `Vendor (1) -> (N) VendorActivity`
- `Vendor (1) -> (N) VendorTimeline`
- Vendor references Company, Branch and all Vendor lookup tables (N:1)

### Business Rules

- VendorCode must be generated using NumberSeries and is immutable after creation.
- Vendor name must be unique within a Company.
- Inactive vendors cannot be used in new transactions.
- Referenced vendors cannot be permanently deleted.
- All updates require RowVersion validation.
- Soft delete only; audit fields are mandatory.

### Events Published

- `VendorCreated`
- `VendorUpdated`
- `VendorActivated`
- `VendorDeactivated`
- `VendorDeleted`

### Developer Notes

Aggregate root of the Vendor Domain. Business document number comes from NumberSeries. Accounting ledger is created independently; Vendor is not a ledger. Child entities are managed through separate tables.

---

## 009 VendorAddress

**Classification:** Bridge Table

### Purpose

Links Vendor with the reusable Shared Kernel Address entity. A vendor may have multiple addresses such as Billing, Shipping, Head Office and Warehouse while Address remains business-agnostic.

### Dependencies

- **Depends On:** Vendor, Address (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorAddressId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| AddressId | BIGINT | No | | | Address | Shared Address |
| AddressType | NVARCHAR(30) | No | Billing | | | Billing/Shipping/Office/etc. |
| IsPrimary | BIT | No | 0 | | | Primary Address Flag |
| IsDefaultBilling | BIT | No | 0 | | | Default Billing Address |
| IsDefaultShipping | BIT | No | 0 | | | Default Shipping Address |
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

- `PK_VendorAddress`
- `FK_VendorAddress_Vendor` → Vendor
- `FK_VendorAddress_Address` → Address
- `UQ_VendorAddress_VendorId_AddressId`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorAddress |
| Non-Clustered | IX_VendorAddress_Vendor |
| Non-Clustered | IX_VendorAddress_Address |
| Non-Clustered | IX_VendorAddress_Defaults (VendorId, IsDefaultBilling, IsDefaultShipping) |
| Non-Clustered | IX_VendorAddress_Status (StatusId, IsDeleted) |

### Relationships

- `Vendor (1) -> (N) VendorAddress`
- `Address (1) -> (N) VendorAddress`

### Business Rules

- Each record links one Vendor to one Address.
- Only one default billing address per vendor.
- Only one default shipping address per vendor.
- Address records are stored only in Shared Kernel Address.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorAddressAdded`
- `VendorAddressUpdated`
- `VendorAddressRemoved`
- `VendorDefaultBillingAddressChanged`
- `VendorDefaultShippingAddressChanged`

### Developer Notes

Bridge table only; address details remain in Shared Kernel Address. Supports unlimited addresses per vendor. Prevent multiple active default billing/shipping addresses.

---

## 010 VendorContact

**Classification:** Bridge Table

### Purpose

Associates a Vendor with reusable Contact records from the Shared Kernel. A vendor can have multiple contacts such as Accounts, Procurement, Sales, Technical Support and Management while Contact information remains centralized in the Shared Kernel.

### Dependencies

- **Depends On:** Vendor, Contact (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorContactId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| ContactId | BIGINT | No | | | Contact | Shared Kernel Contact |
| ContactRole | NVARCHAR(50) | No | Primary | | | Role of Contact |
| Department | NVARCHAR(100) | Yes | NULL | | | Department Name |
| IsPrimary | BIT | No | 0 | | | Primary Contact Flag |
| ReceiveNotifications | BIT | No | 1 | | | Receives System Notifications |
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

- `PK_VendorContact`
- `FK_VendorContact_Vendor` → Vendor
- `FK_VendorContact_Contact` → Contact
- `UQ_VendorContact_VendorId_ContactId`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorContact |
| Non-Clustered | IX_VendorContact_Vendor |
| Non-Clustered | IX_VendorContact_Contact |
| Non-Clustered | IX_VendorContact_Primary (VendorId, IsPrimary) |
| Non-Clustered | IX_VendorContact_Status (StatusId, IsDeleted) |

### Relationships

- `Vendor (1) -> (N) VendorContact`
- `Contact (1) -> (N) VendorContact`

### Business Rules

- Each Vendor may have multiple contacts.
- Only one active primary contact is allowed per vendor.
- Contact details are maintained in the Shared Kernel Contact table.
- Referenced Contact records must not be physically deleted.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorContactAdded`
- `VendorContactUpdated`
- `VendorContactRemoved`
- `VendorPrimaryContactChanged`

### Developer Notes

Bridge table only; contact details remain in Shared Kernel Contact. Supports unlimited contacts per vendor. Primary contact should be enforced at the application/business layer.

---

## 011 VendorAttachment

**Classification:** Bridge Table

### Purpose

Associates Vendor records with reusable Attachment records stored in the Shared Kernel. Allows multiple documents such as contracts, tax certificates, agreements, quotations and compliance documents to be linked to a vendor without storing business ownership inside the Shared Kernel.

### Dependencies

- **Depends On:** Vendor, Attachment (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| AttachmentId | BIGINT | No | | | Attachment | Shared Kernel Attachment |
| AttachmentType | NVARCHAR(50) | No | General | | | Business Classification |
| IsPrimary | BIT | No | 0 | | | Primary Attachment |
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

- `PK_VendorAttachment`
- `FK_VendorAttachment_Vendor` → Vendor
- `FK_VendorAttachment_Attachment` → Attachment
- `UQ_VendorAttachment_VendorId_AttachmentId`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorAttachment |
| Non-Clustered | IX_VendorAttachment_Vendor |
| Non-Clustered | IX_VendorAttachment_Attachment |
| Non-Clustered | IX_VendorAttachment_Status (StatusId, IsDeleted) |

### Relationships

- `Vendor (1) -> (N) VendorAttachment`
- `Attachment (1) -> (N) VendorAttachment`

### Business Rules

- Each attachment links one Vendor to one Shared Kernel Attachment.
- Unlimited attachments are supported.
- Attachment content is maintained only in the Shared Kernel.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorAttachmentAdded`
- `VendorAttachmentUpdated`
- `VendorAttachmentRemoved`

### Developer Notes

Bridge table only. No file metadata should be duplicated from Shared Kernel Attachment. Filter active, non-deleted records in lookups.

---

## 012 VendorNote

**Classification:** Bridge Table

### Purpose

Associates Vendor records with reusable Note entities maintained within the Shared Kernel. Allows unlimited business notes to be attached to a Vendor without duplicating note storage.

Typical notes include: procurement discussions, vendor evaluation comments, payment follow-up, quality observations, compliance remarks, internal purchasing comments, contract negotiation history, blacklisting reasons, audit observations.

The actual note content is stored in the Shared Kernel Note table; VendorNote establishes only the business relationship.

### Dependencies

- **Depends On:** Vendor, Note (Shared Kernel)
- **Referenced By:** Vendor Detail Screen, Purchase Module, Vendor Profile, Audit Reports

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorNoteId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| NoteId | BIGINT | No | | | Note | Shared Kernel Note |
| StatusId | SMALLINT | No | 1 | | | Active / Inactive |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

### Constraints

- `PK_VendorNote`
- `FK_VendorNote_Vendor` → Vendor
- `FK_VendorNote_Note` → Note
- `UQ_Vendor_Note (VendorId, NoteId)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorNote |
| Non-Clustered | IX_Vendor |
| Non-Clustered | IX_Note |
| Non-Clustered | IX_Status |

### Relationships

- `Vendor (1) -> (N) VendorNote`
- `Note (1) -> (N) VendorNote`

### Business Rules

- Unlimited notes may be attached to a Vendor.
- A Vendor cannot reference the same Note more than once.
- Notes remain reusable within the Shared Kernel.
- Deleting VendorNote does not delete the Note.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorNoteAdded`
- `VendorNoteUpdated`
- `VendorNoteRemoved`

### Developer Notes

Implements the Shared Kernel Bridge Pattern. No business text is stored in this table. Note content remains centralized inside the Shared Kernel. Fully consistent with VendorAttachment, VendorActivity and VendorTimeline.

---

## 013 VendorActivity

**Classification:** Bridge Table

### Purpose

Associates Vendor records with reusable Activity records stored in the Shared Kernel. Provides a complete history of operational activities related to a vendor such as meetings, phone calls, emails, follow-ups, procurement discussions, audits and other business interactions while keeping Activity ownership outside the Shared Kernel.

### Dependencies

- **Depends On:** Vendor, Activity (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorActivityId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| ActivityId | BIGINT | No | | | Activity | Shared Kernel Activity |
| ActivityCategory | NVARCHAR(50) | Yes | NULL | | | Business Activity Category |
| IsImportant | BIT | No | 0 | | | Important Activity Indicator |
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

- `PK_VendorActivity`
- `FK_VendorActivity_Vendor` → Vendor
- `FK_VendorActivity_Activity` → Activity
- `UQ_VendorActivity_VendorId_ActivityId`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorActivity |
| Non-Clustered | IX_VendorActivity_Vendor |
| Non-Clustered | IX_VendorActivity_Activity |
| Non-Clustered | IX_VendorActivity_Status (StatusId, IsDeleted) |
| Non-Clustered | IX_VendorActivity_Important (VendorId, IsImportant) |

### Relationships

- `Vendor (1) -> (N) VendorActivity`
- `Activity (1) -> (N) VendorActivity`

### Business Rules

- Each record links one Vendor to one Shared Kernel Activity.
- Unlimited activities may be associated with a vendor.
- Important activities should be highlighted in the user interface.
- Activity details are maintained only in the Shared Kernel Activity table.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorActivityAdded`
- `VendorActivityUpdated`
- `VendorActivityMarkedImportant`
- `VendorActivityRemoved`

### Developer Notes

Bridge table only. Do not duplicate activity details from Shared Kernel Activity. Supports unlimited activity records per vendor.

---

## 014 VendorTimeline

**Classification:** Bridge Table

### Purpose

Associates Vendor records with reusable Timeline entries stored in the Shared Kernel. Provides a chronological business history for each vendor including lifecycle events, operational milestones, approvals, procurement activities and system-generated events while keeping Timeline ownership outside the Shared Kernel.

### Dependencies

- **Depends On:** Vendor, Timeline (Shared Kernel)
- **Referenced By:** None

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| VendorTimelineId | BIGINT IDENTITY(1,1) | No | Identity | Yes | | Primary Key |
| VendorId | BIGINT | No | | | Vendor | Vendor |
| TimelineId | BIGINT | No | | | Timeline | Shared Kernel Timeline |
| TimelineCategory | NVARCHAR(50) | Yes | NULL | | | Business Timeline Category |
| IsSystemGenerated | BIT | No | 0 | | | System-Generated Event Flag |
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

- `PK_VendorTimeline`
- `FK_VendorTimeline_Vendor` → Vendor
- `FK_VendorTimeline_Timeline` → Timeline
- `UQ_VendorTimeline_VendorId_TimelineId`
- `CHECK (StatusId > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_VendorTimeline |
| Non-Clustered | IX_VendorTimeline_Vendor |
| Non-Clustered | IX_VendorTimeline_Timeline |
| Non-Clustered | IX_VendorTimeline_Status (StatusId, IsDeleted) |
| Non-Clustered | IX_VendorTimeline_SystemGenerated (VendorId, IsSystemGenerated) |

### Relationships

- `Vendor (1) -> (N) VendorTimeline`
- `Timeline (1) -> (N) VendorTimeline`

### Business Rules

- Each record links one Vendor to one Shared Kernel Timeline entry.
- Unlimited timeline entries are supported.
- Timeline details are maintained only in the Shared Kernel Timeline table.
- System-generated events are read-only.
- Soft delete only; audit fields and RowVersion are mandatory.

### Events Published

- `VendorTimelineEntryAdded`
- `VendorTimelineEntryUpdated`
- `VendorTimelineEntryRemoved`

### Developer Notes

Bridge table only. No timeline data should be duplicated from Shared Kernel Timeline. Supports complete vendor lifecycle history.

---

*RentalERP v1.0 — Vendor Domain Documentation | Generated June 2026*
