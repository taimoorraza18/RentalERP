# RentalERP v1.0

# RentalDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Rental

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Rental Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. RentalQuotation

6. RentalQuotationLine

7. RentalReservation

---

# Domain Overview

The Rental Domain is the operational heart of RentalERP.

It manages the complete rental lifecycle from quotation to reservation, agreement creation, asset checkout, rental extensions, returns, invoicing and rental completion.

Unlike traditional ERP systems that focus on inventory movement, RentalERP focuses on the utilization and profitability of Assets.

The Rental Domain integrates directly with Asset, Customer, Inventory, Service and Accounting to ensure complete operational control and financial accuracy.

---

# Business Objectives

The Rental Domain provides:

- Rental Quotations
- Rental Reservations
- Rental Agreements
- Asset Availability Checking
- Rental Check-Out
- Rental Check-In
- Rental Extensions
- Partial Returns
- Rental Damage Recording
- Rental Billing
- Rental Profitability
- Asset Utilization
- Customer Rental History
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Roots

- RentalQuotation
- RentalReservation
- RentalAgreement

## Supporting Entities

- RentalExtension
- RentalReturn
- RentalDamage

## Bridge Entities

- RentalAttachment
- RentalNote
- RentalActivity
- RentalTimeline

---

# Implementation Order

001 RentalQuotation

002 RentalQuotationLine

003 RentalReservation

004 RentalReservationLine

005 RentalAgreement

006 RentalAgreementLine

007 RentalCheckOut

008 RentalCheckIn

009 RentalReturn

010 RentalReturnLine

011 RentalExtension

012 RentalDamage

013 RentalInvoice

014 RentalAttachment

015 RentalNote

016 RentalActivity

017 RentalTimeline

---

# ====================================================
# 001 RentalQuotation
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalQuotation

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

RentalQuotation represents a commercial offer provided to a customer before confirming a rental.

It contains rental duration, pricing, terms, requested assets and commercial conditions.

A quotation does not reserve inventory or assets.

Once approved by the customer, it may be converted into a Rental Reservation or directly into a Rental Agreement.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- NumberSeries
- Currency
- PaymentProfile

Referenced By

- RentalQuotationLine
- RentalReservation
- RentalAgreement

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalQuotationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| QuotationNo | NVARCHAR(30) | No | Number Series | | | Quotation Number |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| QuotationDate | DATE | No | GETDATE() | | | Quotation Date |
| ValidUntil | DATE | No | | | | Expiry Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| PaymentProfileId | BIGINT | Yes | NULL | | ✔ | Customer Payment Profile |
| RentalStartDate | DATE | No | | | | Planned Start Date |
| RentalEndDate | DATE | No | | | | Planned End Date |
| TotalAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Draft / Approved / Cancelled |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalQuotation

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- CurrencyId → Currency
- PaymentProfileId → CustomerPaymentProfile

## Unique Keys

- UQ_RentalQuotation_QuotationNo

## Check Constraints

- RentalEndDate >= RentalStartDate
- NetAmount >= 0

---

# Indexes

## Clustered

PK_RentalQuotation

## Non Clustered

IX_QuotationNo

IX_Customer

IX_QuotationDate

IX_Status

IX_RentalPeriod

---

# Relationships

Customer (1) → RentalQuotation (Many)

RentalQuotation (1) → RentalQuotationLine (Many)

RentalQuotation (1) → RentalReservation (One Optional)

RentalQuotation (1) → RentalAgreement (One Optional)

---

# Business Rules

- Quotation Number generated using Number Series.
- Cannot modify after approval.
- Expired quotations cannot be converted.
- Customer is mandatory.
- Rental period must be valid.
- Does not reserve assets.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalQuotationCreated
- RentalQuotationApproved
- RentalQuotationCancelled
- RentalQuotationConverted

---

# Developer Notes

- First document in rental workflow.
- Does not affect Inventory.
- Does not affect Accounting.
- Supports quotation versioning in future.

# ====================================================
# 002 RentalQuotationLine
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalQuotationLine

**Classification:** Transaction Detail

**Aggregate Root:** RentalQuotation

---

# Purpose

RentalQuotationLine stores the individual Assets quoted for rental.

Each line represents one rentable Asset (or Asset Type), rental duration, pricing, discounts, taxes and calculated rental charges.

The quotation line is informational only and does not reserve the Asset.

---

# Dependencies

Depends On

- RentalQuotation
- Asset
- TaxProfile

Referenced By

- RentalReservationLine
- RentalAgreementLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalQuotationLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalQuotationId | BIGINT | No | | | ✔ | Rental Quotation |
| AssetId | BIGINT | No | | | ✔ | Asset |
| RentalDays | INT | No | 1 | | | Rental Duration |
| Quantity | DECIMAL(18,2) | No | 1 | | | Quantity |
| UnitRate | DECIMAL(18,2) | No | 0 | | | Daily Rental Rate |
| DiscountPercentage | DECIMAL(8,2) | No | 0 | | | Discount % |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount Amount |
| TaxProfileId | BIGINT | Yes | NULL | | ✔ | Tax Profile |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax Amount |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total Amount |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalQuotationLine

## Foreign Keys

- RentalQuotationId → RentalQuotation
- AssetId → Asset
- TaxProfileId → CustomerTaxProfile

## Check Constraints

- RentalDays > 0
- Quantity > 0
- UnitRate >= 0
- LineTotal >= 0

---

# Indexes

## Clustered

PK_RentalQuotationLine

## Non Clustered

IX_RentalQuotation

IX_Asset

IX_Status

---

# Relationships

RentalQuotation (1) → RentalQuotationLine (Many)

Asset (1) → RentalQuotationLine (Many)

---

# Business Rules

- Asset cannot appear twice in the same quotation.
- Rental duration must be greater than zero.
- Quantity must be greater than zero.
- Asset availability is checked but not reserved.
- Line Total is system calculated.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalQuotationLineAdded
- RentalQuotationLineUpdated
- RentalQuotationLineRemoved

---

# Developer Notes

- Does not reserve inventory.
- Pricing snapshot stored here.
- Used during quotation-to-agreement conversion.

---

# ====================================================
# 003 RentalReservation
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalReservation

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

RentalReservation reserves Assets for a customer during a specified rental period.

Unlike a quotation, a reservation blocks the selected Assets from being rented to another customer by creating Inventory Reservations.

Reservations may later be converted into Rental Agreements.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- RentalQuotation
- NumberSeries

Referenced By

- RentalReservationLine
- RentalAgreement
- InventoryReservation

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalReservationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| ReservationNo | NVARCHAR(30) | No | Number Series | | | Reservation Number |
| RentalQuotationId | BIGINT | Yes | NULL | | ✔ | Source Quotation |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| ReservationDate | DATE | No | GETDATE() | | | Reservation Date |
| RentalStartDate | DATE | No | | | | Planned Start |
| RentalEndDate | DATE | No | | | | Planned End |
| ExpiryDate | DATE | No | | | | Reservation Expiry |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsConfirmed | BIT | No | 0 | | | Confirmed |
| StatusId | SMALLINT | No | 1 | | | Draft / Reserved / Cancelled / Expired |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalReservation

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- RentalQuotationId → RentalQuotation

## Unique Keys

- UQ_RentalReservation_No

## Check Constraints

- RentalEndDate >= RentalStartDate
- ExpiryDate >= ReservationDate

---

# Indexes

## Clustered

PK_RentalReservation

## Non Clustered

IX_ReservationNo

IX_Customer

IX_Status

IX_RentalPeriod

IX_Quotation

---

# Relationships

Customer (1) → RentalReservation (Many)

RentalQuotation (1) → RentalReservation (Optional One)

RentalReservation (1) → RentalReservationLine (Many)

RentalReservation (1) → RentalAgreement (Optional One)

---

# Business Rules

- Reservation Number generated using Number Series.
- Reservation blocks Assets.
- InventoryReservation records created automatically.
- Expired reservations release reserved Assets.
- Confirmed reservations cannot be edited.
- Reservation may be converted to Rental Agreement.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalReservationCreated
- RentalReservationConfirmed
- RentalReservationExpired
- RentalReservationCancelled
- RentalReservationConverted

---

# Developer Notes

- Creates Inventory Reservations.
- Does not yet start rental.
- Asset remains physically available until checkout.
- Integrates directly with Inventory Domain.

# ====================================================
# 004 RentalReservationLine
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalReservationLine

**Classification:** Transaction Detail

**Aggregate Root:** RentalReservation

---

# Purpose

RentalReservationLine stores the Assets reserved for a customer during the reservation period.

Each reservation line creates a corresponding Inventory Reservation to ensure the Asset cannot be allocated to another rental during the reserved period.

---

# Dependencies

Depends On

- RentalReservation
- Asset
- InventoryReservation

Referenced By

- RentalAgreementLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalReservationLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalReservationId | BIGINT | No | | | ✔ | Rental Reservation |
| AssetId | BIGINT | No | | | ✔ | Reserved Asset |
| InventoryReservationId | BIGINT | Yes | NULL | | ✔ | Inventory Reservation |
| RentalDays | INT | No | 1 | | | Reserved Days |
| DailyRate | DECIMAL(18,2) | No | 0 | | | Daily Rental Rate |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total Amount |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalReservationLine

## Foreign Keys

- RentalReservationId → RentalReservation
- AssetId → Asset
- InventoryReservationId → InventoryReservation

## Check Constraints

- RentalDays > 0
- DailyRate >= 0
- LineTotal >= 0

---

# Indexes

## Clustered

PK_RentalReservationLine

## Non Clustered

IX_RentalReservation

IX_Asset

IX_InventoryReservation

IX_Status

---

# Relationships

RentalReservation (1) → RentalReservationLine (Many)

Asset (1) → RentalReservationLine (Many)

InventoryReservation (1) → RentalReservationLine (One)

---

# Business Rules

- One Asset can only appear once per reservation.
- Inventory Reservation created automatically.
- Reservation release removes Inventory Reservation.
- Reserved Asset cannot be allocated elsewhere.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalReservationLineCreated
- RentalReservationLineUpdated
- RentalReservationLineCancelled

---

# Developer Notes

- Integrates directly with Inventory Reservation.
- Asset remains physically in warehouse until checkout.

---

# ====================================================
# 005 RentalAgreement
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalAgreement

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

RentalAgreement represents the legally binding rental contract between the company and the customer.

Once a Rental Agreement is posted, the rental officially begins.

The agreement initiates Asset checkout, Inventory movement, Accounting entries and future billing.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- RentalReservation
- NumberSeries

Referenced By

- RentalAgreementLine
- RentalCheckOut
- RentalInvoice
- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalAgreementId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| AgreementNo | NVARCHAR(30) | No | Number Series | | | Agreement Number |
| RentalReservationId | BIGINT | Yes | NULL | | ✔ | Source Reservation |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| AgreementDate | DATE | No | GETDATE() | | | Agreement Date |
| RentalStartDate | DATE | No | | | | Rental Start |
| RentalEndDate | DATE | No | | | | Rental End |
| SecurityDeposit | DECIMAL(18,2) | No | 0 | | | Deposit |
| TotalRentalAmount | DECIMAL(18,2) | No | 0 | | | Total Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | | Posted By |
| StatusId | SMALLINT | No | 1 | | | Draft / Active / Completed |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalAgreement

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- RentalReservationId → RentalReservation

## Unique Keys

- UQ_RentalAgreement_No

## Check Constraints

- RentalEndDate >= RentalStartDate
- SecurityDeposit >= 0
- TotalRentalAmount >= 0

---

# Indexes

## Clustered

PK_RentalAgreement

## Non Clustered

IX_AgreementNo

IX_Customer

IX_Status

IX_RentalPeriod

---

# Relationships

RentalAgreement (1) → RentalAgreementLine (Many)

Customer (1) → RentalAgreement (Many)

RentalReservation (1) → RentalAgreement (Optional One)

---

# Business Rules

- Agreement Number generated by Number Series.
- Posting officially starts rental.
- Cannot modify after posting.
- Agreement automatically updates Asset Status.
- Agreement integrates with Accounting.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalAgreementCreated
- RentalAgreementPosted
- RentalAgreementCompleted
- RentalAgreementCancelled

---

# Developer Notes

- Central business document of Rental Domain.
- Starts rental lifecycle.
- Integrates with Inventory and Accounting.

---

# ====================================================
# 006 RentalAgreementLine
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalAgreementLine

**Classification:** Transaction Detail

**Aggregate Root:** RentalAgreement

---

# Purpose

Stores every Asset included within a Rental Agreement.

Each line records rental pricing, rental duration and links directly to the physical Asset being rented.

Posting these lines changes Asset availability and initiates inventory checkout.

---

# Dependencies

Depends On

- RentalAgreement
- Asset

Referenced By

- RentalCheckOut
- RentalReturn
- RentalInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalAgreementLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| AssetId | BIGINT | No | | | ✔ | Asset |
| RentalDays | INT | No | 1 | | | Rental Days |
| DailyRate | DECIMAL(18,2) | No | 0 | | | Daily Rate |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalAgreementLine

## Foreign Keys

- RentalAgreementId → RentalAgreement
- AssetId → Asset

---

# Indexes

## Clustered

PK_RentalAgreementLine

## Non Clustered

IX_RentalAgreement

IX_Asset

IX_Status

---

# Relationships

RentalAgreement (1) → RentalAgreementLine (Many)

Asset (1) → RentalAgreementLine (Many)

---

# Business Rules

- One Asset appears once per Agreement.
- Posting updates Asset status to Rented.
- Posting creates checkout transaction.
- Pricing snapshot stored permanently.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalAgreementLineCreated
- RentalAgreementLinePosted
- RentalAgreementLineCompleted

---

# Developer Notes

- Links Rental Domain with Asset lifecycle.
- Forms the basis for Rental Billing.
```

# ====================================================
# 007 RentalCheckOut
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalCheckOut

**Classification:** Transaction Header

**Aggregate Root:** RentalAgreement

---

# Purpose

RentalCheckOut records the physical handover of Assets to the customer.

Posting a Check-Out officially starts the rental period, changes Asset status to **Rented**, creates inventory movements, updates Asset history and publishes the required domain events.

---

# Dependencies

Depends On

- RentalAgreement
- Customer
- Employee
- NumberSeries

Referenced By

- RentalCheckIn
- RentalReturn
- RentalInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalCheckOutId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| CheckOutNo | NVARCHAR(30) | No | Number Series | | | Check-Out Number |
| CheckOutDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Check-Out Date |
| ReleasedBy | BIGINT | No | | | ✔ | Employee |
| CustomerRepresentative | NVARCHAR(150) | Yes | NULL | | | Customer Representative |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalCheckOut

## Foreign Keys

- RentalAgreementId → RentalAgreement
- ReleasedBy → Employee
- PostedBy → Employee

## Unique Keys

- UQ_RentalCheckOut_No

---

# Indexes

## Clustered

PK_RentalCheckOut

## Non Clustered

IX_CheckOutNo

IX_RentalAgreement

IX_CheckOutDate

IX_Status

---

# Relationships

RentalAgreement (1) → RentalCheckOut (One)

RentalCheckOut (1) → RentalCheckIn (One)

---

# Business Rules

- Check-Out Number generated using Number Series.
- Agreement must be Posted.
- Asset must be Available.
- Posting changes Asset Status to **Rented**.
- Posting creates Inventory Transaction.
- Posting creates Timeline entries.
- Check-Out cannot be modified after posting.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalCheckOutCreated
- RentalCheckOutPosted
- RentalStarted

---

# Developer Notes

- Official start of rental.
- Updates Asset lifecycle.
- Updates Inventory.
- Integrates with Accounting.

---

# ====================================================
# 008 RentalCheckIn
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalCheckIn

**Classification:** Transaction Header

**Aggregate Root:** RentalAgreement

---

# Purpose

RentalCheckIn records the physical return of rented Assets.

It captures the return date, receiving employee and initial observations before detailed inspection.

---

# Dependencies

Depends On

- RentalAgreement
- RentalCheckOut
- Employee

Referenced By

- RentalReturn

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalCheckInId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| RentalCheckOutId | BIGINT | No | | | ✔ | Rental Check-Out |
| CheckInNo | NVARCHAR(30) | No | Number Series | | | Check-In Number |
| CheckInDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Check-In Date |
| ReceivedBy | BIGINT | No | | | ✔ | Employee |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalCheckIn

## Foreign Keys

- RentalAgreementId → RentalAgreement
- RentalCheckOutId → RentalCheckOut
- ReceivedBy → Employee
- PostedBy → Employee

## Unique Keys

- UQ_RentalCheckIn_No

---

# Indexes

## Clustered

PK_RentalCheckIn

## Non Clustered

IX_CheckInNo

IX_RentalAgreement

IX_CheckInDate

IX_Status

---

# Relationships

RentalAgreement (1) → RentalCheckIn (One)

RentalCheckOut (1) → RentalCheckIn (One)

RentalCheckIn (1) → RentalReturn (One)

---

# Business Rules

- Check-In cannot occur before Check-Out.
- Check-In Number generated using Number Series.
- Posting changes Asset status to **Inspection Pending**.
- Posting creates Inventory return transaction.
- Asset not yet available until inspection completes.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalCheckInCreated
- RentalCheckInPosted
- RentalEnded

---

# Developer Notes

- Physical asset return.
- Precedes inspection and damage assessment.

---

# ====================================================
# 009 RentalReturn
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalReturn

**Classification:** Transaction Header

**Aggregate Root:** RentalAgreement

---

# Purpose

RentalReturn represents the completion of the rental process after inspection.

It records returned Assets, damages, shortages, additional charges and closes the Rental Agreement.

Once posted, Assets become available for future rentals if inspection passes.

---

# Dependencies

Depends On

- RentalAgreement
- RentalCheckIn
- NumberSeries

Referenced By

- RentalReturnLine
- RentalDamage
- RentalInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalReturnId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| RentalCheckInId | BIGINT | No | | | ✔ | Rental Check-In |
| ReturnNo | NVARCHAR(30) | No | Number Series | | | Return Number |
| ReturnDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Return Date |
| InspectionResult | SMALLINT | No | | | | Pass / Fail |
| AdditionalCharges | DECIMAL(18,2) | No | 0 | | | Additional Charges |
| DamageCharges | DECIMAL(18,2) | No | 0 | | | Damage Charges |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalReturn

## Foreign Keys

- RentalAgreementId → RentalAgreement
- RentalCheckInId → RentalCheckIn
- PostedBy → Employee

## Unique Keys

- UQ_RentalReturn_No

---

# Indexes

## Clustered

PK_RentalReturn

## Non Clustered

IX_ReturnNo

IX_RentalAgreement

IX_ReturnDate

IX_Status

---

# Relationships

RentalAgreement (1) → RentalReturn (One)

RentalReturn (1) → RentalReturnLine (Many)

RentalReturn (1) → RentalDamage (Many)

---

# Business Rules

- Return Number generated using Number Series.
- Return closes Rental Agreement.
- Passed inspection returns Asset to Available status.
- Failed inspection creates Maintenance request.
- Posting generates final rental billing.
- Posting updates Asset utilization statistics.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalReturnCreated
- RentalReturnPosted
- RentalCompleted

---

# Developer Notes

- Final business document of rental lifecycle.
- Integrates with Billing, Asset and Service Domains.

# ====================================================
# 010 RentalReturnLine
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalReturnLine

**Classification:** Transaction Detail

**Aggregate Root:** RentalReturn

---

# Purpose

RentalReturnLine records each Asset returned under a Rental Return transaction.

It captures the returned Asset, return condition, meter readings, rental duration, late days and charges before completing the rental lifecycle.

---

# Dependencies

Depends On

- RentalReturn
- Asset

Referenced By

- RentalDamage
- AssetInspection
- AssetMaintenance

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalReturnLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalReturnId | BIGINT | No | | | ✔ | Rental Return |
| AssetId | BIGINT | No | | | ✔ | Returned Asset |
| ExpectedReturnDate | DATE | No | | | | Expected Return |
| ActualReturnDate | DATE | No | | | | Actual Return |
| RentalDays | INT | No | 0 | | | Actual Rental Days |
| LateDays | INT | No | 0 | | | Late Days |
| OdometerReading | DECIMAL(18,2) | Yes | NULL | | | Vehicle Meter |
| HourMeterReading | DECIMAL(18,2) | Yes | NULL | | | Equipment Hours |
| ReturnCondition | SMALLINT | No | | | | Good / Damaged / Missing |
| LateCharges | DECIMAL(18,2) | No | 0 | | | Late Charges |
| DamageCharges | DECIMAL(18,2) | No | 0 | | | Damage Charges |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalReturnLine

## Foreign Keys

- RentalReturnId → RentalReturn
- AssetId → Asset

## Check Constraints

- RentalDays >= 0
- LateDays >= 0
- LateCharges >= 0
- DamageCharges >= 0

---

# Indexes

## Clustered

PK_RentalReturnLine

## Non Clustered

IX_RentalReturn

IX_Asset

IX_ReturnCondition

IX_Status

---

# Relationships

RentalReturn (1) → RentalReturnLine (Many)

Asset (1) → RentalReturnLine (Many)

---

# Business Rules

- Every returned Asset has one return line.
- Late Days calculated automatically.
- Charges calculated using rental policy.
- Failed return condition triggers Asset Inspection.
- Damaged assets automatically create Rental Damage records.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalReturnLineCreated
- RentalReturnLineProcessed
- RentalAssetReturned

---

# Developer Notes

- Links Rental and Asset Domains.
- Supports utilization calculations.
- Supports automatic maintenance workflow.

---

# ====================================================
# 011 RentalExtension
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalExtension

**Classification:** Transaction Table

**Aggregate Root:** RentalAgreement

---

# Purpose

RentalExtension records any approved extension to an active rental agreement.

It updates the rental period, recalculates rental charges and maintains complete extension history.

---

# Dependencies

Depends On

- RentalAgreement
- NumberSeries

Referenced By

- RentalInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalExtensionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| ExtensionNo | NVARCHAR(30) | No | Number Series | | | Extension Number |
| ExtensionDate | DATE | No | GETDATE() | | | Extension Date |
| PreviousEndDate | DATE | No | | | | Old End Date |
| NewEndDate | DATE | No | | | | New End Date |
| AdditionalDays | INT | No | 0 | | | Extension Days |
| AdditionalAmount | DECIMAL(18,2) | No | 0 | | | Additional Rental Amount |
| Reason | NVARCHAR(500) | Yes | NULL | | | Extension Reason |
| IsApproved | BIT | No | 0 | | | Approved |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalExtension

## Foreign Keys

- RentalAgreementId → RentalAgreement

## Unique Keys

- UQ_RentalExtension_No

## Check Constraints

- NewEndDate > PreviousEndDate
- AdditionalDays > 0
- AdditionalAmount >= 0

---

# Indexes

## Clustered

PK_RentalExtension

## Non Clustered

IX_RentalAgreement

IX_ExtensionNo

IX_ExtensionDate

IX_Status

---

# Relationships

RentalAgreement (1) → RentalExtension (Many)

---

# Business Rules

- Only active agreements may be extended.
- Extension Number generated using Number Series.
- Rental period updated automatically after approval.
- Extension recalculates future billing.
- Completed rentals cannot be extended.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalExtensionCreated
- RentalExtensionApproved
- RentalPeriodExtended

---

# Developer Notes

- Preserves extension history.
- Supports multiple extensions.
- Updates billing schedule.

---

# ====================================================
# 012 RentalDamage
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalDamage

**Classification:** Transaction Table

**Aggregate Root:** RentalReturn

---

# Purpose

RentalDamage records damages identified during the return inspection.

It tracks repair estimates, customer liability, insurance claims and links to maintenance activities.

---

# Dependencies

Depends On

- RentalReturn
- RentalReturnLine
- Asset

Referenced By

- AssetMaintenance
- RentalInvoice
- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalDamageId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalReturnId | BIGINT | No | | | ✔ | Rental Return |
| RentalReturnLineId | BIGINT | No | | | ✔ | Rental Return Line |
| AssetId | BIGINT | No | | | ✔ | Asset |
| DamageType | SMALLINT | No | | | | Mechanical / Cosmetic / Missing |
| Severity | SMALLINT | No | | | | Minor / Major / Critical |
| EstimatedRepairCost | DECIMAL(18,2) | No | 0 | | | Estimated Cost |
| CustomerCharge | DECIMAL(18,2) | No | 0 | | | Charge to Customer |
| InsuranceClaimAmount | DECIMAL(18,2) | No | 0 | | | Insurance Amount |
| Description | NVARCHAR(1000) | Yes | NULL | | | Damage Description |
| IsRepaired | BIT | No | 0 | | | Repaired |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalDamage

## Foreign Keys

- RentalReturnId → RentalReturn
- RentalReturnLineId → RentalReturnLine
- AssetId → Asset

## Check Constraints

- EstimatedRepairCost >= 0
- CustomerCharge >= 0
- InsuranceClaimAmount >= 0

---

# Indexes

## Clustered

PK_RentalDamage

## Non Clustered

IX_RentalReturn

IX_Asset

IX_DamageType

IX_Status

---

# Relationships

RentalReturn (1) → RentalDamage (Many)

RentalReturnLine (1) → RentalDamage (Many)

Asset (1) → RentalDamage (Many)

---

# Business Rules

- Damage must belong to a returned Asset.
- Repair estimate required before invoicing.
- Customer charges generated according to rental policy.
- Creates Asset Maintenance request automatically.
- Updates Asset availability.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalDamageRecorded
- RentalDamageApproved
- RentalDamageRepaired

---

# Developer Notes

- Integrates with Maintenance.
- Integrates with Accounting.
- Supports insurance processing.
- Maintains complete damage history.

# ====================================================
# 013 RentalInvoice
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalInvoice

**Classification:** Transaction Header

**Aggregate Root:** RentalAgreement

---

# Purpose

RentalInvoice represents the commercial invoice generated for a Rental Agreement.

Invoices may be generated at the beginning of the rental, periodically during the rental, after extensions, or upon completion of the rental depending on the customer's billing policy.

Posting the invoice creates the corresponding Accounting entries.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- RentalAgreement
- Currency
- NumberSeries

Referenced By

- Accounting
- Payment
- Customer Ledger

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalInvoiceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| InvoiceNo | NVARCHAR(30) | No | Number Series | | | Invoice Number |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| InvoiceDate | DATE | No | GETDATE() | | | Invoice Date |
| DueDate | DATE | No | | | | Due Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| OutstandingAmount | DECIMAL(18,2) | No | 0 | | | Balance Due |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Paid / Cancelled |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalInvoice

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- RentalAgreementId → RentalAgreement
- CustomerId → Customer
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_RentalInvoice_No

## Check Constraints

- NetAmount >= 0
- OutstandingAmount >= 0
- DueDate >= InvoiceDate

---

# Indexes

## Clustered

PK_RentalInvoice

## Non Clustered

IX_InvoiceNo

IX_Customer

IX_RentalAgreement

IX_InvoiceDate

IX_Status

---

# Relationships

RentalAgreement (1) → RentalInvoice (Many)

Customer (1) → RentalInvoice (Many)

---

# Business Rules

- Invoice Number generated using Number Series.
- Posting creates Journal Voucher.
- Posting updates Customer Balance.
- Invoice cannot be modified after posting.
- Outstanding Amount updated after each payment.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalInvoiceCreated
- RentalInvoicePosted
- RentalInvoicePaid
- RentalInvoiceCancelled

---

# Developer Notes

- Integrates directly with Accounting.
- Supports periodic billing.
- Supports final settlement billing.

---

# ====================================================
# 014 RentalAttachment
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Rental Agreements with reusable Attachment records maintained within the Shared Kernel.

Typical attachments include:

- Signed Rental Agreement
- Delivery Challan
- Customer ID
- Insurance Documents
- Vehicle Registration
- Inspection Photos

---

# Dependencies

Depends On

- RentalAgreement
- Attachment

Referenced By

- Rental Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| AttachmentId | BIGINT | No | | | ✔ | Shared Attachment |
| DisplayOrder | INT | No | 1 | | | Display Order |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalAttachment

## Foreign Keys

- RentalAgreementId → RentalAgreement
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_RentalAttachment

## Non Clustered

IX_RentalAgreement

IX_Attachment

---

# Relationships

RentalAgreement (1) → RentalAttachment (Many)

Attachment (1) → RentalAttachment (Many)

---

# Business Rules

- Attachment stored in Shared Kernel.
- Business ownership belongs to Rental Domain.
- Unlimited attachments supported.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalAttachmentAdded
- RentalAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.

---

# ====================================================
# 015 RentalNote
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Rental Agreements with reusable Note records maintained within the Shared Kernel.

Notes capture operational remarks, customer instructions, delivery notes, billing comments and internal communications.

---

# Dependencies

Depends On

- RentalAgreement
- Note

Referenced By

- Rental Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| NoteId | BIGINT | No | | | ✔ | Shared Note |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalNote

## Foreign Keys

- RentalAgreementId → RentalAgreement
- NoteId → Note

---

# Indexes

## Clustered

PK_RentalNote

## Non Clustered

IX_RentalAgreement

IX_Note

---

# Relationships

RentalAgreement (1) → RentalNote (Many)

Note (1) → RentalNote (Many)

---

# Business Rules

- Notes remain reusable within Shared Kernel.
- Business ownership belongs to Rental Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalNoteAdded
- RentalNoteUpdated
- RentalNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Maintains operational communication history.

# ====================================================
# 016 RentalActivity
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Rental Agreements with reusable Activity records maintained within the Shared Kernel.

Activities capture every operational event that occurs during the rental lifecycle, providing complete traceability for users and workflow processes.

Examples include:

- Quotation Created
- Reservation Confirmed
- Agreement Posted
- Asset Checked Out
- Rental Extended
- Asset Returned
- Invoice Posted
- Payment Received

---

# Dependencies

Depends On

- RentalAgreement
- Activity

Referenced By

- Rental Dashboard
- Workflow Engine
- Activity Widget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| ActivityId | BIGINT | No | | | ✔ | Shared Activity |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalActivity

## Foreign Keys

- RentalAgreementId → RentalAgreement
- ActivityId → Activity

## Unique Keys

- UQ_Rental_Activity

---

# Indexes

## Clustered

PK_RentalActivity

## Non Clustered

IX_RentalAgreement

IX_Activity

IX_Status

---

# Relationships

RentalAgreement (1) → RentalActivity (Many)

Activity (1) → RentalActivity (Many)

---

# Business Rules

- Activities remain reusable in Shared Kernel.
- Business ownership belongs to Rental Domain.
- Activities are append-only.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalActivityCreated
- RentalActivityUpdated
- RentalActivityDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports Workflow Engine.
- Supports Notification Module.
- Maintains complete operational audit trail.

---

# ====================================================
# 017 RentalTimeline
# ====================================================

# Table Classification

**Domain:** Rental Domain

**Table Name:** RentalTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Rental Agreements with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological history of every significant event during the rental lifecycle, allowing users to trace the complete journey of a rental agreement.

Examples include:

- Quotation Created
- Reservation Created
- Reservation Confirmed
- Agreement Posted
- Check-Out Completed
- Rental Extended
- Invoice Generated
- Check-In Completed
- Asset Returned
- Rental Completed

---

# Dependencies

Depends On

- RentalAgreement
- Timeline

Referenced By

- Rental Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RentalTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RentalAgreementId | BIGINT | No | | | ✔ | Rental Agreement |
| TimelineId | BIGINT | No | | | ✔ | Shared Timeline |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_RentalTimeline

## Foreign Keys

- RentalAgreementId → RentalAgreement
- TimelineId → Timeline

## Unique Keys

- UQ_Rental_Timeline

---

# Indexes

## Clustered

PK_RentalTimeline

## Non Clustered

IX_RentalAgreement

IX_Timeline

IX_Status

---

# Relationships

RentalAgreement (1) → RentalTimeline (Many)

Timeline (1) → RentalTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Rental Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RentalTimelineCreated
- RentalTimelineUpdated
- RentalTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for Timeline rendering.
- Maintains complete rental lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Rental Domain is the operational engine of RentalERP. It manages the complete rental lifecycle from customer quotation through reservation, agreement creation, asset checkout, rental extension, asset return, damage assessment, invoicing and rental completion.

The Rental Domain coordinates Asset utilization, Inventory reservations, Customer billing and Accounting integration while maintaining complete operational traceability.

---

## Aggregate Roots

- RentalQuotation
- RentalReservation
- RentalAgreement

---

## Supporting Entities

- RentalQuotationLine
- RentalReservationLine
- RentalAgreementLine
- RentalCheckOut
- RentalCheckIn
- RentalReturn
- RentalReturnLine
- RentalExtension
- RentalDamage
- RentalInvoice

---

## Bridge Entities

- RentalAttachment
- RentalNote
- RentalActivity
- RentalTimeline

---

## Major Business Capabilities

- Rental Quotations
- Rental Reservations
- Asset Availability Checking
- Rental Agreements
- Asset Check-Out
- Asset Check-In
- Rental Extensions
- Partial Returns
- Damage Assessment
- Rental Billing
- Customer Rental History
- Asset Utilization Tracking
- Rental Profitability Analysis
- Complete Audit Trail
- Shared Kernel Integration

---

## Published Domain Events

The Rental Domain publishes events including:

- RentalQuotationCreated
- RentalReservationCreated
- RentalReservationConfirmed
- RentalAgreementPosted
- RentalStarted
- RentalExtended
- RentalCheckInPosted
- RentalCompleted
- RentalDamageRecorded
- RentalInvoicePosted

These events integrate with:

- Asset Domain
- Inventory Domain
- Service Domain
- Customer Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Rental Domain integrates directly with:

- Foundation
- Shared Kernel
- Customer Domain
- Asset Domain
- Warehouse Domain
- Inventory Domain
- Service Domain
- Sales Domain
- Accounting Domain
- Administration

---

# Rental Domain Status

**Status:** ✅ Complete

**Total Tables:** 17

1. RentalQuotation
2. RentalQuotationLine
3. RentalReservation
4. RentalReservationLine
5. RentalAgreement
6. RentalAgreementLine
7. RentalCheckOut
8. RentalCheckIn
9. RentalReturn
10. RentalReturnLine
11. RentalExtension
12. RentalDamage
13. RentalInvoice
14. RentalAttachment
15. RentalNote
16. RentalActivity
17. RentalTimeline

---
