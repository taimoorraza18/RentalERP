# RentalERP v1.0 — Customer Domain

> **Architecture:** Domain Driven Design (DDD)
> **Database:** Microsoft SQL Server
> **Application:** .NET Core Web API + Angular
> **Status:** In Progress | **Version:** 1.0

---

## Table of Contents

1. [CustomerGroup](#001-customergroup)
2. [CustomerCategory](#002-customercategory)
3. [CustomerIndustry](#003-customerindustry)
4. [CustomerTerritory](#004-customerterritory)
5. [CustomerPriceLevel](#005-customerpricelevel)
6. [CustomerPaymentProfile](#006-customerpaymentprofile)
7. [CustomerCreditProfile](#007-customercreditprofile)
9. [Customer](#009-customer)
10. [CustomerAddress](#010-customeraddress)
11. [CustomerContact](#011-customercontact)
12. [CustomerAttachment](#012-customerattachment)
13. [CustomerNote](#013-customernote)
14. [CustomerActivity](#014-customeractivity)
15. [CustomerTimeline](#015-customertimeline)

---

## 001 CustomerGroup

**Classification:** Configuration (Master) Table

### Purpose

Stores customer groups used for segmentation, pricing, reporting, credit policy, sales analysis and rental analysis.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerGroupId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
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

- `PK_CustomerGroup`
- `FK_CustomerGroup_Company`
- `FK_CustomerGroup_CreatedBy`
- `FK_CustomerGroup_ModifiedBy`
- `FK_CustomerGroup_DeletedBy`
- `UQ (CompanyId, GroupCode)`
- `UQ (CompanyId, GroupName)`
- `CHECK DisplayOrder >= 0`
- `CHECK StatusId > 0`
- One default group per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerGroup |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, GroupName) |

### Relationships

- `Company (1) -> (N) CustomerGroup`
- `CustomerGroup (1) -> (N) Customer`

### Business Rules

- GroupCode generated through NumberSeries.
- GroupName unique within Company.
- Cannot delete if referenced by Customer.
- Soft delete only.
- One default group per Company.

### Events Published

- `CustomerGroupCreated`
- `CustomerGroupUpdated`
- `CustomerGroupActivated`
- `CustomerGroupInactivated`
- `CustomerGroupMerged`

### Developer Notes

This specification is locked under RentalERP v1.0 Architecture Baseline.

---

## 002 CustomerCategory

**Classification:** Configuration (Master) Table

### Purpose

Defines customer categories used for business classification, reporting, sales strategy and rental analytics.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerCategoryId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
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

- `PK_CustomerCategory`
- `FK_CustomerCategory_Company`
- `FK_CustomerCategory_CreatedBy`
- `FK_CustomerCategory_ModifiedBy`
- `FK_CustomerCategory_DeletedBy`
- `UQ (CompanyId, CategoryCode)`
- `UQ (CompanyId, CategoryName)`
- `CHECK(DisplayOrder >= 0)`
- `CHECK(StatusId > 0)`
- Only one default category per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerCategory |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, CategoryName) |

### Relationships

- `Company (1) -> (N) CustomerCategory`
- `CustomerCategory (1) -> (N) Customer`

### Business Rules

- CategoryCode generated using NumberSeries.
- CategoryName must be unique within a Company.
- Cannot delete if referenced by Customer.
- Soft delete only.
- Each Company must have one default category.

### Sample Seed Data

| Code | Category |
|---|---|
| CC-001 | Corporate |
| CC-002 | SME |
| CC-003 | Government |
| CC-004 | Educational |
| CC-005 | Healthcare |
| CC-006 | Retail |
| CC-007 | Industrial |
| CC-008 | NGO |

### Events Published

- `CustomerCategoryCreated`
- `CustomerCategoryUpdated`
- `CustomerCategoryActivated`
- `CustomerCategoryInactivated`

### Developer Notes

Conforms to RentalERP v1.0 Architecture Baseline.

---

## 003 CustomerIndustry

**Classification:** Configuration (Master) Table

### Purpose

Stores customer industries for classification, reporting, analytics, sales strategy and rental portfolio segmentation.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerIndustryId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| IndustryCode | NVARCHAR(20) | No | | | | Business Code |
| IndustryName | NVARCHAR(150) | No | | | | Industry Name |
| Description | NVARCHAR(500) | Yes | | | | Description |
| DisplayOrder | INT | No | 0 | | | Sort Order |
| IsDefault | BIT | No | 0 | | | Default Industry |
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

- `PK_CustomerIndustry`
- `FK_CustomerIndustry_Company`
- `FK_CustomerIndustry_CreatedBy`
- `FK_CustomerIndustry_ModifiedBy`
- `FK_CustomerIndustry_DeletedBy`
- `UQ (CompanyId, IndustryCode)`
- `UQ (CompanyId, IndustryName)`
- `CHECK(DisplayOrder >= 0)`
- `CHECK(StatusId > 0)`
- Only one default industry per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerIndustry |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, IndustryName) |

### Relationships

- `Company (1) -> (N) CustomerIndustry`
- `CustomerIndustry (1) -> (N) Customer`

### Business Rules

- IndustryCode generated through NumberSeries.
- IndustryName unique within Company.
- Cannot delete when referenced by Customer.
- Soft delete only.
- One default industry per Company.

### Sample Seed Data

| Code | Industry |
|---|---|
| CI-001 | Manufacturing |
| CI-002 | Healthcare |
| CI-003 | Education |
| CI-004 | Banking |
| CI-005 | Retail |
| CI-006 | Government |
| CI-007 | Textile |
| CI-008 | Logistics |

### Events Published

- `CustomerIndustryCreated`
- `CustomerIndustryUpdated`
- `CustomerIndustryActivated`
- `CustomerIndustryInactivated`

### Developer Notes

Conforms to RentalERP v1.0 Architecture Baseline.

---

## 004 CustomerTerritory

**Classification:** Configuration (Master) Table

### Purpose

Stores sales and service territories used for assigning customers, sales representatives, technicians, reporting and regional management.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer, CustomerSalesPerson (Future)

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerTerritoryId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| TerritoryCode | NVARCHAR(20) | No | | | | Business Code |
| TerritoryName | NVARCHAR(150) | No | | | | Territory Name |
| ParentTerritoryId | BIGINT | Yes | | | CustomerTerritory | Hierarchy Support |
| ManagerUserId | BIGINT | Yes | | | User | Territory Manager |
| Description | NVARCHAR(500) | Yes | | | | Description |
| DisplayOrder | INT | No | 0 | | | Sort Order |
| IsDefault | BIT | No | 0 | | | Default Territory |
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

- `PK_CustomerTerritory`
- `FK_CustomerTerritory_Company`
- `FK_CustomerTerritory_ParentTerritory`
- `FK_CustomerTerritory_ManagerUser`
- `FK_CustomerTerritory_CreatedBy`
- `FK_CustomerTerritory_ModifiedBy`
- `FK_CustomerTerritory_DeletedBy`
- `UQ (CompanyId, TerritoryCode)`
- `UQ (CompanyId, TerritoryName)`
- `CHECK(DisplayOrder >= 0)`
- `CHECK(StatusId > 0)`
- Only one default territory per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerTerritory |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_ParentTerritory (ParentTerritoryId) |
| Non-Clustered | IX_Manager (ManagerUserId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Name (CompanyId, TerritoryName) |

### Relationships

- `Company (1) -> (N) CustomerTerritory`
- `CustomerTerritory (1) -> (N) Customer`
- `CustomerTerritory (1) -> (N) CustomerTerritory` *(Self Hierarchy)*

### Business Rules

- TerritoryCode generated through NumberSeries.
- TerritoryName must be unique within Company.
- Territories support unlimited hierarchy.
- Parent territory cannot reference itself.
- Cannot delete territory assigned to customers.
- Soft delete only.

### Sample Seed Data

| Code | Territory |
|---|---|
| TR-001 | North |
| TR-002 | South |
| TR-003 | East |
| TR-004 | West |
| TR-005 | Central |
| TR-006 | Karachi |
| TR-007 | Lahore |
| TR-008 | Islamabad |

### Events Published

- `CustomerTerritoryCreated`
- `CustomerTerritoryUpdated`
- `CustomerTerritoryActivated`
- `CustomerTerritoryInactivated`

### Developer Notes

Supports hierarchical territories for future sales, service and rental expansion. Conforms to RentalERP v1.0 Architecture Baseline.

---

## 005 CustomerPriceLevel

**Classification:** Configuration (Master) Table

### Purpose

Defines customer price levels used to determine default selling prices, rental pricing strategies and discount policies.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer, Sales Domain, Rental Domain

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerPriceLevelId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| PriceLevelCode | NVARCHAR(20) | No | | | | Business Code |
| PriceLevelName | NVARCHAR(150) | No | | | | Price Level Name |
| Description | NVARCHAR(500) | Yes | | | | Description |
| DiscountPercentage | DECIMAL(5,2) | No | 0 | | | Default Discount % |
| MarkupPercentage | DECIMAL(5,2) | No | 0 | | | Markup % |
| Priority | INT | No | 1 | | | Selection Priority |
| IsDefault | BIT | No | 0 | | | Default Price Level |
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

- `PK_CustomerPriceLevel`
- `FK_CustomerPriceLevel_Company`
- `FK_CustomerPriceLevel_CreatedBy`
- `FK_CustomerPriceLevel_ModifiedBy`
- `FK_CustomerPriceLevel_DeletedBy`
- `UQ (CompanyId, PriceLevelCode)`
- `UQ (CompanyId, PriceLevelName)`
- `CHECK (DiscountPercentage BETWEEN 0 AND 100)`
- `CHECK (MarkupPercentage BETWEEN 0 AND 100)`
- `CHECK (Priority > 0)`
- Only one default price level per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerPriceLevel |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Priority (CompanyId, Priority) |
| Non-Clustered | IX_Name (CompanyId, PriceLevelName) |

### Relationships

- `Company (1) -> (N) CustomerPriceLevel`
- `CustomerPriceLevel (1) -> (N) Customer`

### Business Rules

- PriceLevelCode generated through NumberSeries.
- PriceLevelName must be unique within Company.
- Only one default price level is allowed.
- Cannot delete if assigned to any Customer.
- Soft delete only.

### Sample Seed Data

| Code | Name | Discount % | Markup % |
|---|---|---|---|
| PL-001 | Standard | 0 | 0 |
| PL-002 | Silver | 5 | 0 |
| PL-003 | Gold | 10 | 0 |
| PL-004 | Platinum | 15 | 0 |
| PL-005 | Dealer | 20 | 0 |
| PL-006 | Distributor | 25 | 0 |

### Events Published

- `CustomerPriceLevelCreated`
- `CustomerPriceLevelUpdated`
- `CustomerPriceLevelActivated`
- `CustomerPriceLevelInactivated`

### Developer Notes

Price Level provides the default pricing strategy. Actual item pricing will be resolved in the Sales and Rental domains based on Item Price Lists.

---

## 006 CustomerPaymentProfile

**Classification:** Configuration (Master) Table

### Purpose

Defines default payment behavior and terms for customers used by Sales, Rental and Accounting domains.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer, SalesInvoice, RentalContract

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerPaymentProfileId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| PaymentProfileCode | NVARCHAR(20) | No | | | | Business Code |
| PaymentProfileName | NVARCHAR(150) | No | | | | Profile Name |
| PaymentTermDays | INT | No | 0 | | | Credit Days |
| CreditLimit | DECIMAL(19,4) | No | 0 | | | Default Credit Limit |
| AllowPartialPayment | BIT | No | 1 | | | Allow Partial Payments |
| AllowAdvancePayment | BIT | No | 1 | | | Allow Advance Payments |
| GracePeriodDays | INT | No | 0 | | | Grace Period |
| Description | NVARCHAR(500) | Yes | | | | Description |
| IsDefault | BIT | No | 0 | | | Default Profile |
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

- `PK_CustomerPaymentProfile`
- `FK_CustomerPaymentProfile_Company`
- `FK_CustomerPaymentProfile_CreatedBy`
- `FK_CustomerPaymentProfile_ModifiedBy`
- `FK_CustomerPaymentProfile_DeletedBy`
- `UQ (CompanyId, PaymentProfileCode)`
- `UQ (CompanyId, PaymentProfileName)`
- `CHECK (PaymentTermDays >= 0)`
- `CHECK (CreditLimit >= 0)`
- `CHECK (GracePeriodDays >= 0)`
- Only one default profile per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerPaymentProfile |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, PaymentProfileName) |

### Relationships

- `Company (1) -> (N) CustomerPaymentProfile`
- `CustomerPaymentProfile (1) -> (N) Customer`

### Business Rules

- Profile code generated through NumberSeries.
- Cannot delete while referenced by Customer.
- Only one default profile per Company.
- Soft delete only.

### Sample Seed Data

| Code | Name | Credit Days |
|---|---|---|
| PP-001 | Cash | 0 |
| PP-002 | Net 15 | 15 |
| PP-003 | Net 30 | 30 |
| PP-004 | Net 45 | 45 |
| PP-005 | Net 60 | 60 |

### Events Published

- `CustomerPaymentProfileCreated`
- `CustomerPaymentProfileUpdated`
- `CustomerPaymentProfileActivated`
- `CustomerPaymentProfileInactivated`

### Developer Notes

This table stores default payment behavior only. Actual payment transactions belong to the Accounting Domain.

---

## 007 CustomerCreditProfile

**Classification:** Configuration (Master) Table

### Purpose

Defines customer credit policies including default credit limits, credit hold rules, overdue handling and approval requirements.

### Dependencies

- **Depends On:** Company, User
- **Referenced By:** Customer, Sales Domain, Rental Domain, Accounting Domain

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerCreditProfileId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| CreditProfileCode | NVARCHAR(20) | No | | | | Business Code |
| CreditProfileName | NVARCHAR(150) | No | | | | Profile Name |
| DefaultCreditLimit | DECIMAL(19,4) | No | 0 | | | Credit Limit |
| MaximumOutstanding | DECIMAL(19,4) | No | 0 | | | Maximum Receivable |
| MaximumOverdueDays | INT | No | 0 | | | Allowed Overdue Days |
| RequireApprovalOverLimit | BIT | No | 1 | | | Approval Required |
| AllowCreditHoldOverride | BIT | No | 0 | | | Management Override |
| Description | NVARCHAR(500) | Yes | | | | Description |
| IsDefault | BIT | No | 0 | | | Default Profile |
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

- `PK_CustomerCreditProfile`
- `FK_CustomerCreditProfile_Company`
- `FK_CustomerCreditProfile_CreatedBy`
- `FK_CustomerCreditProfile_ModifiedBy`
- `FK_CustomerCreditProfile_DeletedBy`
- `UQ (CompanyId, CreditProfileCode)`
- `UQ (CompanyId, CreditProfileName)`
- `CHECK (DefaultCreditLimit >= 0)`
- `CHECK (MaximumOutstanding >= 0)`
- `CHECK (MaximumOverdueDays >= 0)`
- Only one default profile per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerCreditProfile |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Status (CompanyId, StatusId) |
| Non-Clustered | IX_Default (CompanyId, IsDefault) |
| Non-Clustered | IX_Name (CompanyId, CreditProfileName) |

### Relationships

- `Company (1) -> (N) CustomerCreditProfile`
- `CustomerCreditProfile (1) -> (N) Customer`

### Business Rules

- CreditProfileCode generated through NumberSeries.
- CreditProfileName must be unique within Company.
- Cannot delete while assigned to Customers.
- Only one default profile allowed.
- Soft delete only.

### Sample Seed Data

| Code | Profile | Credit Limit | Overdue Days |
|---|---|---|---|
| CP-001 | Cash Customer | 0 | 0 |
| CP-002 | Standard | 100000 | 30 |
| CP-003 | Corporate | 500000 | 45 |
| CP-004 | Government | 1000000 | 60 |
| CP-005 | VIP | 2000000 | 90 |

### Events Published

- `CustomerCreditProfileCreated`
- `CustomerCreditProfileUpdated`
- `CustomerCreditProfileActivated`
- `CustomerCreditProfileInactivated`

### Developer Notes

CustomerCreditProfile defines default credit policies only. Actual customer balances are calculated from accounting ledger entries.

---

## 009 Customer

**Classification:** Master (Aggregate Root)

### Purpose

Represents the primary customer master used across Sales, Rental, Service and Accounting domains.

### Dependencies

- **Depends On:** Company, CustomerGroup, CustomerCategory, CustomerIndustry, CustomerTerritory, CustomerPriceLevel, CustomerPaymentProfile, CustomerCreditProfile, User
- **Referenced By:** Sales, Rental, Service, Accounting, Asset

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| CustomerCode | NVARCHAR(30) | No | | | | Business Code |
| CustomerName | NVARCHAR(250) | No | | | | Display Name |
| LegalName | NVARCHAR(250) | Yes | | | | Legal Name |
| CustomerGroupId | BIGINT | No | | | CustomerGroup | Group |
| CustomerCategoryId | BIGINT | No | | | CustomerCategory | Category |
| CustomerIndustryId | BIGINT | Yes | | | CustomerIndustry | Industry |
| CustomerTerritoryId | BIGINT | Yes | | | CustomerTerritory | Territory |
| CustomerPriceLevelId | BIGINT | No | | | CustomerPriceLevel | Price Level |
| CustomerPaymentProfileId | BIGINT | No | | | CustomerPaymentProfile | Payment Profile |
| CustomerCreditProfileId | BIGINT | No | | | CustomerCreditProfile | Credit Profile |
| TaxConfigurationId | BIGINT | No | | | TaxConfiguration | Tax Configuration |
| RegistrationDate | DATE | No | GETDATE() | | | Customer Since |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| Remarks | NVARCHAR(1000) | Yes | | | | Remarks |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Customer`
- `FK_Customer_Company`
- `FK_Customer_CustomerGroup`
- `FK_Customer_CustomerCategory`
- `FK_Customer_CustomerIndustry`
- `FK_Customer_CustomerTerritory`
- `FK_Customer_CustomerPriceLevel`
- `FK_Customer_CustomerPaymentProfile`
- `FK_Customer_CustomerCreditProfile`
- `FK_Customer_TaxConfiguration`
- `FK_Customer_CreatedBy`
- `FK_Customer_ModifiedBy`
- `FK_Customer_DeletedBy`
- `UQ (CompanyId, CustomerCode)`
- `UQ (CompanyId, CustomerName)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Customer |
| Non-Clustered | IX_Customer_Code |
| Non-Clustered | IX_Customer_Name |
| Non-Clustered | IX_Customer_Group |
| Non-Clustered | IX_Customer_Category |
| Non-Clustered | IX_Customer_Territory |
| Non-Clustered | IX_Customer_Status |

### Relationships

- `CustomerGroup (1) -> (N) Customer`
- `CustomerCategory (1) -> (N) Customer`
- `CustomerIndustry (1) -> (N) Customer`
- `CustomerTerritory (1) -> (N) Customer`
- `CustomerPriceLevel (1) -> (N) Customer`
- `CustomerPaymentProfile (1) -> (N) Customer`
- `CustomerCreditProfile (1) -> (N) Customer`
- `TaxConfiguration (1) -> (N) Customer`
- `Customer (1) -> (N) CustomerAddress`
- `Customer (1) -> (N) CustomerContact`
- `Customer (1) -> (N) CustomerAttachment`
- `Customer (1) -> (N) CustomerNote`
- `Customer (1) -> (N) CustomerActivity`
- `Customer (1) -> (N) CustomerTimeline`

### Business Rules

- CustomerCode generated through NumberSeries.
- CustomerName unique within Company.
- Customer cannot be deleted when referenced by transactions.
- At least one address is required before first Sales/Rental transaction.
- Soft delete only.

### Events Published

- `CustomerCreated`
- `CustomerUpdated`
- `CustomerActivated`
- `CustomerDeactivated`

### Developer Notes

Customer is the Aggregate Root of the Customer Domain. Shared objects such as Address, Contact, Attachment, Note and Activity are linked through dedicated bridge tables.

---

## 010 CustomerAddress

**Classification:** Link Table

### Purpose

Links Customers with shared Address records. A customer can have multiple addresses such as Billing, Shipping, Head Office and Installation locations.

### Dependencies

- **Depends On:** Customer, Address (Shared Kernel), User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerAddressId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| AddressId | BIGINT | No | | | Address | Shared Address |
| AddressType | NVARCHAR(30) | No | | | | Billing/Shipping/HeadOffice/Site |
| IsPrimary | BIT | No | 0 | | | Primary Address |
| IsDefaultBilling | BIT | No | 0 | | | Billing Default |
| IsDefaultShipping | BIT | No | 0 | | | Shipping Default |
| EffectiveFrom | DATE | Yes | | | | Start Date |
| EffectiveTo | DATE | Yes | | | | End Date |
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

- `PK_CustomerAddress`
- `FK_CustomerAddress_Customer`
- `FK_CustomerAddress_Address`
- `FK_CustomerAddress_CreatedBy`
- `FK_CustomerAddress_ModifiedBy`
- `FK_CustomerAddress_DeletedBy`
- `CHECK (EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom)`
- Only one primary address per Customer and AddressType

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerAddress |
| Non-Clustered | IX_Customer (CustomerId) |
| Non-Clustered | IX_Address (AddressId) |
| Non-Clustered | IX_AddressType (CustomerId, AddressType) |
| Non-Clustered | IX_Status (CustomerId, StatusId) |

### Relationships

- `Customer (1) -> (N) CustomerAddress`
- `Address (1) -> (N) CustomerAddress`

### Business Rules

- A customer may have multiple addresses.
- One primary address per AddressType.
- Billing and Shipping defaults are independent.
- Historical addresses are retained using Effective dates.
- Soft delete only.

### Events Published

- `CustomerAddressAdded`
- `CustomerAddressUpdated`
- `CustomerAddressRemoved`

### Developer Notes

Address data resides in the Shared Kernel Address table. CustomerAddress only maintains the relationship and address usage metadata.

---

## 011 CustomerContact

**Classification:** Link Table

### Purpose

Links Customers with shared Contact records. Supports multiple contacts per customer such as Accounts, Sales, Technical and Primary contacts.

### Dependencies

- **Depends On:** Customer, Contact (Shared Kernel), User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerContactId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| ContactId | BIGINT | No | | | Contact | Shared Contact |
| ContactRole | NVARCHAR(30) | No | | | | Primary/Accounts/Sales/Technical |
| IsPrimary | BIT | No | 0 | | | Primary Contact |
| ReceiveEmail | BIT | No | 1 | | | Email Notifications |
| ReceiveSMS | BIT | No | 0 | | | SMS Notifications |
| ReceiveWhatsApp | BIT | No | 0 | | | WhatsApp Notifications |
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

- `PK_CustomerContact`
- `FK_CustomerContact_Customer`
- `FK_CustomerContact_Contact`
- `FK_CustomerContact_CreatedBy`
- `FK_CustomerContact_ModifiedBy`
- `FK_CustomerContact_DeletedBy`
- Only one Primary Contact per Customer

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerContact |
| Non-Clustered | IX_Customer (CustomerId) |
| Non-Clustered | IX_Contact (ContactId) |
| Non-Clustered | IX_ContactRole (CustomerId, ContactRole) |
| Non-Clustered | IX_Status (CustomerId, StatusId) |

### Relationships

- `Customer (1) -> (N) CustomerContact`
- `Contact (1) -> (N) CustomerContact`

### Business Rules

- A customer may have multiple contacts.
- Only one primary contact is allowed.
- Contact can be reused across customer branches if required.
- Soft delete only.

### Events Published

- `CustomerContactAdded`
- `CustomerContactUpdated`
- `CustomerContactRemoved`

### Developer Notes

Contact details are stored in the Shared Kernel Contact table. CustomerContact stores relationship metadata and notification preferences.

---

## 012 CustomerAttachment

**Classification:** Link Table

### Purpose

Associates customers with documents stored in the Shared Kernel Attachment table, including contracts, certificates, agreements and supporting files.

### Dependencies

- **Depends On:** Customer, Attachment (Shared Kernel), User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerAttachmentId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| AttachmentId | BIGINT | No | | | Attachment | Shared Attachment |
| AttachmentCategory | NVARCHAR(50) | No | | | | Contract/NTN/GST/Other |
| Description | NVARCHAR(500) | Yes | | | | Description |
| IsPrimary | BIT | No | 0 | | | Primary Document |
| ExpiryDate | DATE | Yes | | | | Optional Expiry |
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

- `PK_CustomerAttachment`
- `FK_CustomerAttachment_Customer`
- `FK_CustomerAttachment_Attachment`
- `FK_CustomerAttachment_CreatedBy`
- `FK_CustomerAttachment_ModifiedBy`
- `FK_CustomerAttachment_DeletedBy`
- `CHECK (ExpiryDate IS NULL OR ExpiryDate >= CAST(CreatedDate AS DATE))`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerAttachment |
| Non-Clustered | IX_Customer (CustomerId) |
| Non-Clustered | IX_Attachment (AttachmentId) |
| Non-Clustered | IX_Category (CustomerId, AttachmentCategory) |
| Non-Clustered | IX_Status (CustomerId, StatusId) |

### Relationships

- `Customer (1) -> (N) CustomerAttachment`
- `Attachment (1) -> (N) CustomerAttachment`

### Business Rules

- Multiple attachments allowed per customer.
- One document may be marked as primary per category.
- Documents are never physically deleted.
- Expired documents remain for audit.

### Events Published

- `CustomerAttachmentAdded`
- `CustomerAttachmentUpdated`
- `CustomerAttachmentRemoved`

### Developer Notes

Binary files and metadata are stored in the Shared Kernel Attachment table. This table stores only the relationship and customer-specific metadata.

---

## 013 CustomerNote

**Classification:** Link Table

### Purpose

Associates customers with notes stored in the Shared Kernel Note table. Used for internal remarks, follow-ups and operational comments.

### Dependencies

- **Depends On:** Customer, Note (Shared Kernel), User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerNoteId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| NoteId | BIGINT | No | | | Note | Shared Note |
| NoteCategory | NVARCHAR(50) | No | | | | General/Credit/Service/Sales |
| IsPinned | BIT | No | 0 | | | Pinned Note |
| IsConfidential | BIT | No | 0 | | | Restricted Visibility |
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

- `PK_CustomerNote`
- `FK_CustomerNote_Customer`
- `FK_CustomerNote_Note`
- `FK_CustomerNote_CreatedBy`
- `FK_CustomerNote_ModifiedBy`
- `FK_CustomerNote_DeletedBy`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerNote |
| Non-Clustered | IX_Customer (CustomerId) |
| Non-Clustered | IX_Note (NoteId) |
| Non-Clustered | IX_Category (CustomerId, NoteCategory) |
| Non-Clustered | IX_Status (CustomerId, StatusId) |

### Relationships

- `Customer (1) -> (N) CustomerNote`
- `Note (1) -> (N) CustomerNote`

### Business Rules

- Multiple notes are allowed per customer.
- Pinned notes appear first in the UI.
- Confidential notes are visible only to authorized users.
- Soft delete only.

### Events Published

- `CustomerNoteAdded`
- `CustomerNoteUpdated`
- `CustomerNoteRemoved`

### Developer Notes

The Shared Kernel Note table stores the note content. CustomerNote stores only the association and customer-specific metadata.

---

## 014 CustomerActivity

**Classification:** Link Table

### Purpose

Associates customers with activities stored in the Shared Kernel Activity table. Activities include calls, meetings, visits, emails, tasks and follow-ups.

### Dependencies

- **Depends On:** Customer, Activity (Shared Kernel), User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerActivityId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| ActivityId | BIGINT | No | | | Activity | Shared Activity |
| ActivityRole | NVARCHAR(30) | No | | | | Sales/Support/Service/Collection |
| IsPrimary | BIT | No | 0 | | | Primary Activity |
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

- `PK_CustomerActivity`
- `FK_CustomerActivity_Customer`
- `FK_CustomerActivity_Activity`
- `FK_CustomerActivity_CreatedBy`
- `FK_CustomerActivity_ModifiedBy`
- `FK_CustomerActivity_DeletedBy`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerActivity |
| Non-Clustered | IX_Customer (CustomerId) |
| Non-Clustered | IX_Activity (ActivityId) |
| Non-Clustered | IX_Role (CustomerId, ActivityRole) |
| Non-Clustered | IX_Status (CustomerId, StatusId) |

### Relationships

- `Customer (1) -> (N) CustomerActivity`
- `Activity (1) -> (N) CustomerActivity`

### Business Rules

- Customers may have unlimited activities.
- Activities remain immutable after completion except by authorized users.
- Completed activities cannot be physically deleted.
- Soft delete only.

### Events Published

- `CustomerActivityAdded`
- `CustomerActivityUpdated`
- `CustomerActivityCompleted`
- `CustomerActivityRemoved`

### Developer Notes

Activity details are maintained in the Shared Kernel Activity table. CustomerActivity stores only the association and customer-specific metadata.

---

## 015 CustomerTimeline

**Classification:** History Table

### Purpose

Maintains a chronological audit trail of significant customer events for operational visibility and reporting.

### Dependencies

- **Depends On:** Customer, User

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| CustomerTimelineId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CustomerId | BIGINT | No | | | Customer | Customer |
| EventType | NVARCHAR(50) | No | | | | Created/Updated/Rental/Sales/Service/etc. |
| ReferenceModule | NVARCHAR(50) | Yes | | | | Origin Module |
| ReferenceId | BIGINT | Yes | | | | Origin Record Id |
| ReferenceNo | NVARCHAR(50) | Yes | | | | Document Number |
| Title | NVARCHAR(200) | No | | | | Timeline Title |
| Description | NVARCHAR(MAX) | Yes | | | | Details |
| EventDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Event Timestamp |
| PerformedBy | BIGINT | No | | | User | Performed By |
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

- `PK_CustomerTimeline`
- `FK_CustomerTimeline_Customer`
- `FK_CustomerTimeline_PerformedBy`
- `FK_CustomerTimeline_CreatedBy`
- `FK_CustomerTimeline_ModifiedBy`
- `FK_CustomerTimeline_DeletedBy`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_CustomerTimeline |
| Non-Clustered | IX_Customer_EventDate (CustomerId, EventDate DESC) |
| Non-Clustered | IX_EventType (EventType) |
| Non-Clustered | IX_Reference (ReferenceModule, ReferenceId) |

### Relationships

- `Customer (1) -> (N) CustomerTimeline`
- `User (1) -> (N) CustomerTimeline`

### Business Rules

- Timeline records are append-only.
- Business events from Sales, Rental, Service and Accounting automatically create timeline entries.
- Timeline records must never be physically deleted.
- Only system administrators may correct timeline entries.

### Events Published

- `CustomerTimelineRecorded`

### Developer Notes

This table provides the unified customer history visible from the Customer profile. Records are generated automatically by domain events.

---

*RentalERP v1.0 — Customer Domain Documentation | Generated June 2026*
