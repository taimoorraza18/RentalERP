# RentalERP v1.0

# AssetDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Asset

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Asset Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. Asset

6. AssetCategory

7. AssetType

---

# Domain Overview

The Asset Domain is the heart of RentalERP.

Unlike traditional accounting systems where inventory is the primary operational entity, RentalERP revolves around Assets. Every rental transaction, maintenance activity, service history, inspection, depreciation record and profitability analysis is ultimately linked to an Asset.

The Asset Domain manages the complete lifecycle of every rentable asset from acquisition to disposal.

The domain supports:

- Asset Registration
- Asset Classification
- Asset Types
- Asset Groups
- Asset Status
- Asset Ownership
- Asset Costing
- Asset Lifecycle
- Asset Availability
- Asset Rental History
- Asset Service History
- Asset Inspection History
- Asset Attachments
- Asset Notes
- Asset Activities
- Asset Timeline

The Asset Domain integrates closely with Inventory, Rental, Service, Purchase, Sales and Accounting while remaining the central operational domain of RentalERP.

---

# Business Objectives

The Asset Domain provides:

- Multi Company Assets
- Multi Branch Assets
- Asset Classification
- Asset Categories
- Asset Types
- Asset Registration
- Asset Cost Tracking
- Asset Lifecycle Management
- Asset Availability Tracking
- Rental History
- Maintenance History
- Inspection History
- Asset Profitability
- Asset Audit Trail
- Shared Kernel Integration

---

# Aggregate Root

## Primary Aggregate Root

- Asset

## Supporting Entities

- AssetCategory
- AssetType
- AssetGroup
- AssetStatus
- AssetInspection
- AssetMaintenance

## Bridge Entities

- AssetAttachment
- AssetNote
- AssetActivity
- AssetTimeline

---

# Implementation Order

001 Asset

002 AssetCategory

003 AssetType

004 AssetGroup

005 AssetStatus

006 AssetInspection

007 AssetMaintenance

008 AssetAttachment

009 AssetNote

010 AssetActivity

011 AssetTimeline

---

# ====================================================
# 001 Asset
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** Asset

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

Asset represents an individual rentable machine, equipment, vehicle or tool owned or managed by the organization.

Every operational module ultimately revolves around an Asset.

Assets can be rented, serviced, inspected, transferred, maintained and eventually disposed.

Each Asset maintains its own operational history throughout its lifecycle.

---

# Dependencies

Depends On

- Company
- Branch
- AssetCategory
- AssetType
- Vendor
- Warehouse

Referenced By

- Rental
- Service
- Inspection
- Maintenance
- Accounting
- AssetAttachment
- AssetNote
- AssetActivity
- AssetTimeline

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| AssetCategoryId | BIGINT | No | | | ✔ | Asset Category |
| AssetTypeId | BIGINT | No | | | ✔ | Asset Type |
| WarehouseId | BIGINT | Yes | NULL | | ✔ | Current Warehouse |
| VendorId | BIGINT | Yes | NULL | | ✔ | Supplier |
| AssetCode | NVARCHAR(30) | No | Number Series | | | Asset Number |
| AssetName | NVARCHAR(200) | No | | | | Asset Name |
| ModelNumber | NVARCHAR(100) | Yes | NULL | | | Model |
| SerialNumber | NVARCHAR(100) | Yes | NULL | | | Serial Number |
| PurchaseDate | DATE | Yes | NULL | | | Purchase Date |
| PurchaseCost | DECIMAL(18,2) | Yes | NULL | | | Acquisition Cost |
| CurrentBookValue | DECIMAL(18,2) | Yes | NULL | | | Book Value |
| CurrentStatus | SMALLINT | No | 1 | | | Asset Status |
| IsAvailable | BIT | No | 1 | | | Rental Availability |
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

PK_Asset

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- AssetCategoryId → AssetCategory
- AssetTypeId → AssetType
- WarehouseId → Warehouse
- VendorId → Vendor

## Unique Keys

- UQ_Asset_Code
- UQ_Asset_SerialNumber

## Check Constraints

- PurchaseCost >= 0
- CurrentBookValue >= 0

---

# Indexes

## Clustered

PK_Asset

## Non Clustered

IX_Asset_Code

IX_Asset_Name

IX_Asset_Category

IX_Asset_Type

IX_Asset_Warehouse

IX_Asset_Status

IX_Asset_Available

---

# Relationships

Company (1) → Asset (Many)

Branch (1) → Asset (Many)

AssetCategory (1) → Asset (Many)

AssetType (1) → Asset (Many)

Warehouse (1) → Asset (Many)

Vendor (1) → Asset (Many)

---

# Business Rules

- Asset Code generated from Number Series.
- Serial Number must be unique when available.
- Asset cannot be rented when unavailable.
- Asset cannot be deleted once involved in transactions.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetCreated
- AssetUpdated
- AssetActivated
- AssetRetired
- AssetDisposed

---

# Developer Notes

- Central Aggregate Root of RentalERP.
- Referenced by nearly every operational module.
- Maintains complete lifecycle history.
- Supports future IoT integration.
- Supports GPS integration.
- Supports telematics integration.

# ====================================================
# 002 AssetCategory
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetCategory

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

AssetCategory classifies Assets into logical business categories for reporting, maintenance planning, depreciation, rental analysis and operational management.

Examples include:

- Heavy Equipment
- Earth Moving Equipment
- Power Equipment
- Generators
- Vehicles
- Tools
- Lifting Equipment
- Construction Machinery

---

# Dependencies

Depends On

- Company

Referenced By

- Asset

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetCategoryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| CategoryCode | NVARCHAR(30) | No | | | | Unique Category Code |
| CategoryName | NVARCHAR(150) | No | | | | Category Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| IsSystemDefined | BIT | No | 0 | | | System Record |
| SortOrder | INT | No | 1 | | | Display Order |
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

---

# Constraints

## Primary Key

PK_AssetCategory

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_AssetCategory_Code
- UQ_AssetCategory_Name

## Check Constraints

- SortOrder > 0

---

# Indexes

## Clustered

PK_AssetCategory

## Non Clustered

IX_AssetCategory_Code

IX_AssetCategory_Name

IX_AssetCategory_Status

IX_AssetCategory_Company

---

# Relationships

Company (1) → AssetCategory (Many)

AssetCategory (1) → Asset (Many)

---

# Business Rules

- Category Code must be unique within a Company.
- Category Name must be unique within a Company.
- System-defined categories cannot be deleted.
- Categories already assigned to Assets cannot be deleted.
- Soft Delete mandatory.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetCategoryCreated
- AssetCategoryUpdated
- AssetCategoryActivated
- AssetCategoryDeactivated
- AssetCategoryDeleted

---

# Developer Notes

- Used extensively for reporting.
- Frequently cached.
- Supports future category hierarchy.

---

# ====================================================
# 003 AssetType
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetType

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

AssetType defines the operational type of an Asset.

Unlike AssetCategory, which groups assets for business reporting, AssetType defines operational behavior.

Examples:

- Excavator
- Bulldozer
- Crane
- Forklift
- Compressor
- Generator
- Concrete Mixer
- Trailer

AssetType may later determine maintenance schedules, inspection frequencies, rental pricing rules and depreciation methods.

---

# Dependencies

Depends On

- Company
- AssetCategory

Referenced By

- Asset

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetTypeId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| AssetCategoryId | BIGINT | No | | | ✔ | Asset Category |
| TypeCode | NVARCHAR(30) | No | | | | Unique Type Code |
| TypeName | NVARCHAR(150) | No | | | | Asset Type |
| StandardLifeMonths | INT | Yes | NULL | | | Useful Life |
| DefaultDepreciationRate | DECIMAL(8,2) | Yes | NULL | | | Percentage |
| RequiresInspection | BIT | No | 1 | | | Inspection Required |
| RequiresMaintenance | BIT | No | 1 | | | Maintenance Required |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
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

---

# Constraints

## Primary Key

PK_AssetType

## Foreign Keys

- CompanyId → Company
- AssetCategoryId → AssetCategory

## Unique Keys

- UQ_AssetType_Code
- UQ_AssetType_Name

## Check Constraints

- DefaultDepreciationRate >= 0
- StandardLifeMonths >= 0

---

# Indexes

## Clustered

PK_AssetType

## Non Clustered

IX_AssetType_Category

IX_AssetType_Code

IX_AssetType_Name

IX_AssetType_Status

---

# Relationships

AssetCategory (1) → AssetType (Many)

AssetType (1) → Asset (Many)

---

# Business Rules

- Asset Type belongs to one Asset Category.
- Type Code must be unique.
- Depreciation rate cannot be negative.
- Asset Type cannot be deleted if referenced by Assets.
- Soft Delete mandatory.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetTypeCreated
- AssetTypeUpdated
- AssetTypeActivated
- AssetTypeDeactivated
- AssetTypeDeleted

---

# Developer Notes

- Determines operational behavior of assets.
- Foundation for maintenance planning.
- Foundation for inspection scheduling.
- Supports future depreciation strategies.

# ====================================================
# 004 AssetGroup
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetGroup

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

AssetGroup provides a higher-level grouping of Assets for operational, financial and reporting purposes.

Unlike AssetCategory and AssetType, AssetGroup allows organizations to create custom business groupings for analysis.

Examples:

- Rental Fleet
- Internal Equipment
- Customer Owned Equipment
- Demo Equipment
- Idle Fleet
- High Value Assets

---

# Dependencies

Depends On

- Company

Referenced By

- Asset

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetGroupId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| GroupCode | NVARCHAR(30) | No | | | | Group Code |
| GroupName | NVARCHAR(150) | No | | | | Group Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| SortOrder | INT | No | 1 | | | Display Order |
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

---

# Constraints

## Primary Key

PK_AssetGroup

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_AssetGroup_Code
- UQ_AssetGroup_Name

---

# Indexes

## Clustered

PK_AssetGroup

## Non Clustered

IX_AssetGroup_Code

IX_AssetGroup_Name

IX_AssetGroup_Status

---

# Relationships

Company (1) → AssetGroup (Many)

AssetGroup (1) → Asset (Many)

---

# Business Rules

- Group Code must be unique within Company.
- Group Name must be unique within Company.
- Groups referenced by Assets cannot be deleted.
- Soft Delete mandatory.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetGroupCreated
- AssetGroupUpdated
- AssetGroupDeleted

---

# Developer Notes

- Used primarily for business reporting.
- Supports flexible operational grouping.

---

# ====================================================
# 005 AssetStatus
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetStatus

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

Defines operational states through which an Asset passes during its lifecycle.

Examples:

- Available
- Reserved
- Rented
- Under Maintenance
- Under Inspection
- Out of Service
- Retired
- Disposed

---

# Dependencies

Depends On

- Company

Referenced By

- Asset

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetStatusId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| StatusCode | NVARCHAR(30) | No | | | | Status Code |
| StatusName | NVARCHAR(100) | No | | | | Status Name |
| IsRentable | BIT | No | 0 | | | Can be Rented |
| IsOperational | BIT | No | 1 | | | Operational |
| IsSystemDefined | BIT | No | 1 | | | System Status |
| SortOrder | INT | No | 1 | | | Display Order |
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

PK_AssetStatus

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_AssetStatus_Code
- UQ_AssetStatus_Name

---

# Indexes

## Clustered

PK_AssetStatus

## Non Clustered

IX_AssetStatus_Code

IX_AssetStatus_Name

IX_AssetStatus_Rentable

IX_AssetStatus_Status

---

# Relationships

AssetStatus (1) → Asset (Many)

---

# Business Rules

- System statuses cannot be deleted.
- Only one active status per Asset.
- Rentable flag determines rental eligibility.
- Soft Delete mandatory.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetStatusCreated
- AssetStatusUpdated
- AssetStatusDeleted

---

# Developer Notes

- Used by Rental and Service Domains.
- Controls operational workflow.

---

# ====================================================
# 006 AssetInspection
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetInspection

**Classification:** Transaction Table

**Aggregate Root:** Asset

---

# Purpose

Records inspections performed on Assets before rental, after return, during scheduled maintenance or for regulatory compliance.

Inspection results determine whether an Asset remains available for rental or requires maintenance.

---

# Dependencies

Depends On

- Asset
- Employee

Referenced By

- Service
- Rental

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetInspectionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
| InspectionDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Inspection Date |
| InspectorId | BIGINT | No | | | ✔ | Employee |
| InspectionType | SMALLINT | No | | | | Pre/Post/Periodic |
| Result | SMALLINT | No | | | | Pass/Fail |
| OdometerReading | DECIMAL(18,2) | Yes | NULL | | | Meter Reading |
| HourMeterReading | DECIMAL(18,2) | Yes | NULL | | | Hour Meter |
| NextInspectionDate | DATE | Yes | NULL | | | Next Inspection |
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

PK_AssetInspection

## Foreign Keys

- AssetId → Asset
- InspectorId → Employee

---

# Indexes

## Clustered

PK_AssetInspection

## Non Clustered

IX_Asset

IX_InspectionDate

IX_Inspector

IX_Result

---

# Relationships

Asset (1) → AssetInspection (Many)

Employee (1) → AssetInspection (Many)

---

# Business Rules

- Inspection cannot exist without an Asset.
- Failed inspections automatically make Asset unavailable for rental.
- Next inspection date must be greater than inspection date.
- Inspection history is immutable after completion.
- Soft Delete prohibited after completion.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetInspectionCreated
- AssetInspectionCompleted
- AssetInspectionFailed

---

# Developer Notes

- Supports pre-rental inspections.
- Supports return inspections.
- Supports regulatory compliance inspections.
- Integrates with Maintenance scheduling.

# ====================================================
# 007 AssetMaintenance
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetMaintenance

**Classification:** Transaction Table

**Aggregate Root:** Asset

---

# Purpose

AssetMaintenance records all preventive, corrective, emergency and scheduled maintenance activities performed on an Asset.

This table provides complete maintenance history for every Asset and serves as the basis for maintenance costing, downtime analysis, lifecycle management and profitability calculations.

---

# Dependencies

Depends On

- Asset
- Vendor
- Employee
- Service

Referenced By

- Accounting
- AssetTimeline
- AssetActivity

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetMaintenanceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
| VendorId | BIGINT | Yes | NULL | | ✔ | Service Vendor |
| EmployeeId | BIGINT | Yes | NULL | | ✔ | Technician |
| ServiceId | BIGINT | Yes | NULL | | ✔ | Related Service |
| MaintenanceNo | NVARCHAR(30) | No | Number Series | | | Maintenance Number |
| MaintenanceDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Maintenance Date |
| MaintenanceType | SMALLINT | No | | | | Preventive / Corrective / Breakdown |
| StartDate | DATETIME2(7) | Yes | NULL | | | Work Start |
| EndDate | DATETIME2(7) | Yes | NULL | | | Work Completion |
| DowntimeHours | DECIMAL(18,2) | Yes | NULL | | | Downtime |
| LaborCost | DECIMAL(18,2) | No | 0 | | | Labor Cost |
| PartsCost | DECIMAL(18,2) | No | 0 | | | Parts Cost |
| ExternalCost | DECIMAL(18,2) | No | 0 | | | Vendor Cost |
| TotalCost | DECIMAL(18,2) | No | 0 | | | Total Maintenance Cost |
| NextMaintenanceDate | DATE | Yes | NULL | | | Next Scheduled Maintenance |
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

PK_AssetMaintenance

## Foreign Keys

- AssetId → Asset
- VendorId → Vendor
- EmployeeId → Employee
- ServiceId → Service

## Unique Keys

- UQ_AssetMaintenance_No

## Check Constraints

- LaborCost >= 0
- PartsCost >= 0
- ExternalCost >= 0
- TotalCost >= 0
- DowntimeHours >= 0

---

# Indexes

## Clustered

PK_AssetMaintenance

## Non Clustered

IX_Asset

IX_MaintenanceNo

IX_MaintenanceDate

IX_MaintenanceType

IX_NextMaintenanceDate

IX_Status

---

# Relationships

Asset (1) → AssetMaintenance (Many)

Vendor (1) → AssetMaintenance (Many)

Employee (1) → AssetMaintenance (Many)

Service (1) → AssetMaintenance (Many)

---

# Business Rules

- Every maintenance record belongs to one Asset.
- Maintenance Number is generated using Number Series.
- Total Cost = Labor Cost + Parts Cost + External Cost.
- Asset becomes unavailable while maintenance is in progress.
- Completion updates Asset availability.
- Next Maintenance Date must be greater than Maintenance Date.
- Completed maintenance records cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetMaintenanceCreated
- AssetMaintenanceStarted
- AssetMaintenanceCompleted
- AssetMaintenanceCancelled

---

# Developer Notes

- Maintains complete maintenance history.
- Supports preventive maintenance planning.
- Used for asset lifecycle costing.
- Integrates with Service Domain.
- Integrates with Accounting.

---

# ====================================================
# 008 AssetAttachment
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Assets with reusable Attachment records maintained within the Shared Kernel.

Typical attachments include:

- Asset Images
- Purchase Invoice
- Warranty Certificate
- Registration Documents
- Insurance Policy
- User Manuals
- Inspection Reports

---

# Dependencies

Depends On

- Asset
- Attachment

Referenced By

- Asset Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
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

PK_AssetAttachment

## Foreign Keys

- AssetId → Asset
- AttachmentId → Attachment

## Unique Keys

- UQ_Asset_Attachment

---

# Indexes

## Clustered

PK_AssetAttachment

## Non Clustered

IX_Asset

IX_Attachment

---

# Relationships

Asset (1) → AssetAttachment (Many)

Attachment (1) → AssetAttachment (Many)

---

# Business Rules

- Attachments remain in Shared Kernel.
- Business ownership belongs to Asset Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetAttachmentAdded
- AssetAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports unlimited attachments.

---

# ====================================================
# 009 AssetNote
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Assets with reusable Note records maintained within the Shared Kernel.

Notes store operational remarks, customer feedback, inspection comments, maintenance observations and internal documentation throughout the Asset lifecycle.

---

# Dependencies

Depends On

- Asset
- Note

Referenced By

- Asset Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
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

PK_AssetNote

## Foreign Keys

- AssetId → Asset
- NoteId → Note

---

# Indexes

## Clustered

PK_AssetNote

## Non Clustered

IX_Asset

IX_Note

---

# Relationships

Asset (1) → AssetNote (Many)

Note (1) → AssetNote (Many)

---

# Business Rules

- Notes remain reusable in Shared Kernel.
- Business ownership belongs to Asset Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetNoteAdded
- AssetNoteUpdated
- AssetNoteRemoved

---

# Developer Notes

- Shared Kernel bridge implementation.
- Maintains complete operational history.

# ====================================================
# 010 AssetActivity
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Assets with reusable Activity records maintained within the Shared Kernel.

Activities represent operational actions performed on an Asset throughout its lifecycle.

Examples include:

- Asset Registered
- Asset Assigned
- Asset Returned
- Maintenance Started
- Maintenance Completed
- Inspection Passed
- Inspection Failed
- Asset Retired
- Asset Disposed

---

# Dependencies

Depends On

- Asset
- Activity

Referenced By

- Asset Dashboard
- Workflow Engine
- Activity Widget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
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

PK_AssetActivity

## Foreign Keys

- AssetId → Asset
- ActivityId → Activity

## Unique Keys

- UQ_Asset_Activity

---

# Indexes

## Clustered

PK_AssetActivity

## Non Clustered

IX_Asset

IX_Activity

IX_Status

---

# Relationships

Asset (1) → AssetActivity (Many)

Activity (1) → AssetActivity (Many)

---

# Business Rules

- Activities remain reusable in Shared Kernel.
- Business ownership belongs to Asset Domain.
- Activities are append-only.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetActivityCreated
- AssetActivityUpdated
- AssetActivityDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Provides complete operational audit history.
- Supports Workflow Engine.
- Supports Notification Engine.

---

# ====================================================
# 011 AssetTimeline
# ====================================================

# Table Classification

**Domain:** Asset Domain

**Table Name:** AssetTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Assets with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological history of every important event occurring throughout the Asset lifecycle.

Examples include:

- Asset Created
- Asset Purchased
- Rental Started
- Rental Returned
- Inspection Completed
- Maintenance Performed
- Spare Parts Replaced
- Asset Transferred
- Asset Retired
- Asset Disposed

---

# Dependencies

Depends On

- Asset
- Timeline

Referenced By

- Asset Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AssetTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
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

PK_AssetTimeline

## Foreign Keys

- AssetId → Asset
- TimelineId → Timeline

## Unique Keys

- UQ_Asset_Timeline

---

# Indexes

## Clustered

PK_AssetTimeline

## Non Clustered

IX_Asset

IX_Timeline

IX_Status

---

# Relationships

Asset (1) → AssetTimeline (Many)

Timeline (1) → AssetTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline events are append-only.
- Business ownership belongs to Asset Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AssetTimelineCreated
- AssetTimelineUpdated
- AssetTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Provides complete asset lifecycle history.
- Optimized for timeline rendering.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Asset Domain is the core operational domain of RentalERP. It manages the complete lifecycle of every rentable asset from acquisition to retirement.

Unlike traditional ERPs where inventory is the primary operational entity, RentalERP revolves around Assets. Every Rental, Service, Inspection, Maintenance, Inventory Movement and Profitability Report ultimately references an Asset.

The Asset Domain provides a complete operational history, enabling lifecycle management, utilization analysis, maintenance planning and profitability reporting.

---

## Aggregate Root

- Asset

---

## Supporting Entities

- AssetCategory
- AssetType
- AssetGroup
- AssetStatus
- AssetInspection
- AssetMaintenance

---

## Bridge Entities

- AssetAttachment
- AssetNote
- AssetActivity
- AssetTimeline

---

## Major Business Capabilities

- Multi Company Assets
- Multi Branch Assets
- Asset Registration
- Asset Classification
- Asset Lifecycle Management
- Asset Availability
- Asset Inspection History
- Asset Maintenance History
- Maintenance Cost Tracking
- Rental History
- Asset Profitability
- Shared Kernel Integration
- Complete Audit Trail

---

## Published Domain Events

The Asset Domain publishes events including:

- AssetCreated
- AssetUpdated
- AssetActivated
- AssetRetired
- AssetDisposed
- AssetInspectionCompleted
- AssetInspectionFailed
- AssetMaintenanceStarted
- AssetMaintenanceCompleted

These events integrate with:

- Rental Domain
- Service Domain
- Inventory Domain
- Purchase Domain
- Sales Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Asset Domain integrates directly with:

- Foundation
- Shared Kernel
- Warehouse Domain
- Inventory Domain
- Rental Domain
- Service Domain
- Purchase Domain
- Sales Domain
- Vendor Domain
- Customer Domain
- Accounting Domain
- Administration

---

# Asset Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. Asset
2. AssetCategory
3. AssetType
4. AssetGroup
5. AssetStatus
6. AssetInspection
7. AssetMaintenance
8. AssetAttachment
9. AssetNote
10. AssetActivity
11. AssetTimeline

---
