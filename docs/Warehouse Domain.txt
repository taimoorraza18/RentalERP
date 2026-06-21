# RentalERP v1.0

# WarehouseDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Warehouse

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Warehouse Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. Warehouse

6. WarehouseType

7. WarehouseZone

---

# Domain Overview

The Warehouse Domain is responsible for defining and managing the physical storage infrastructure used throughout RentalERP.

Unlike the Inventory Domain, which maintains stock balances and inventory movements, the Warehouse Domain manages where inventory is stored.

The Warehouse Domain provides the physical hierarchy required by Inventory, Rental, Service, Purchase and Sales modules.

The domain supports:

- Multi Company Warehouses
- Multi Branch Warehouses
- Warehouse Classification
- Warehouse Zones
- Warehouse Locations
- Capacity Management
- Operational Status
- Warehouse Attachments
- Warehouse Notes
- Warehouse Activities
- Warehouse Timeline

The Warehouse Domain never stores inventory quantities. It only defines the storage structure used by the Inventory Domain.

---

# Business Objectives

The Warehouse Domain provides:

- Multi Company support
- Multi Branch support
- Unlimited Warehouses
- Warehouse Classification
- Warehouse Zones
- Warehouse Locations
- Warehouse Capacity
- Warehouse Activation / Deactivation
- Warehouse Audit Trail
- Shared Attachment Integration
- Shared Note Integration
- Shared Activity Integration
- Shared Timeline Integration

---

# Aggregate Root

## Primary Aggregate Root

- Warehouse

## Supporting Entities

- WarehouseType
- WarehouseZone
- WarehouseLocation

## Bridge Entities

- WarehouseAttachment
- WarehouseNote
- WarehouseActivity
- WarehouseTimeline

---

# Implementation Order

001 Warehouse

002 WarehouseType

003 WarehouseZone

004 WarehouseLocation

005 WarehouseAttachment

006 WarehouseNote

007 WarehouseActivity

008 WarehouseTimeline

---

# ====================================================
# 001 Warehouse
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** Warehouse

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

Warehouse represents a physical storage facility used to store inventory, rental assets, spare parts, consumables and other stock items.

A warehouse belongs to a Company and Branch and acts as the root entity for all warehouse-related information.

Each Warehouse can contain multiple Zones and Locations.

---

# Dependencies

Depends On

- Company
- Branch
- Address
- Contact

Referenced By

- WarehouseZone
- WarehouseLocation
- InventoryStock
- InventoryStockLedger
- InventoryReservation
- StockTransfer
- StockCount
- Purchase
- Sales
- Rental
- Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseTypeId | BIGINT | No | | | ✔ | Warehouse Type |
| WarehouseCode | NVARCHAR(30) | No | | | | Unique Warehouse Code |
| WarehouseName | NVARCHAR(150) | No | | | | Warehouse Name |
| AddressId | BIGINT | Yes | NULL | | ✔ | Shared Address |
| ContactId | BIGINT | Yes | NULL | | ✔ | Shared Contact |
| ManagerName | NVARCHAR(150) | Yes | NULL | | | Warehouse Manager |
| Phone | NVARCHAR(30) | Yes | NULL | | | Contact Number |
| Email | NVARCHAR(150) | Yes | NULL | | | Email Address |
| Capacity | DECIMAL(18,2) | Yes | NULL | | | Storage Capacity |
| CapacityUnit | NVARCHAR(20) | Yes | NULL | | | SqFt / Cubic Meter |
| IsDefault | BIT | No | 0 | | | Default Warehouse |
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

PK_Warehouse

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- WarehouseTypeId → WarehouseType
- AddressId → Address
- ContactId → Contact

## Unique Keys

- UQ_Warehouse_Code
- UQ_Warehouse_Name

## Check Constraints

- Capacity >= 0
- StatusId > 0

---

# Indexes

## Clustered

PK_Warehouse

## Non Clustered

IX_Warehouse_Code

IX_Warehouse_Name

IX_Warehouse_Company

IX_Warehouse_Branch

IX_Warehouse_Type

IX_Warehouse_Status

---

# Relationships

Company (1) → Warehouse (Many)

Branch (1) → Warehouse (Many)

WarehouseType (1) → Warehouse (Many)

Warehouse (1) → WarehouseZone (Many)

Warehouse (1) → WarehouseLocation (Many)

---

# Business Rules

- Warehouse Code must be unique within a Company.
- Warehouse Name must be unique within a Company.
- Only one Default Warehouse is allowed per Branch.
- Inactive Warehouses cannot receive inventory.
- Warehouses containing inventory cannot be deleted.
- Soft Delete is mandatory.
- Audit fields are mandatory.
- RowVersion is mandatory.

---

# Published Domain Events

- WarehouseCreated
- WarehouseUpdated
- WarehouseActivated
- WarehouseDeactivated
- WarehouseDeleted

---

# Developer Notes

- Aggregate Root of Warehouse Domain.
- Referenced by Inventory, Purchase, Sales, Rental and Service Domains.
- Inventory quantities are not stored here.
- Supports future warehouse hierarchy and expansion.

# ====================================================
# 002 WarehouseType
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseType

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

WarehouseType defines the classification of warehouses within the organization.

It provides logical categorization for reporting, operational rules, and warehouse behavior.

Examples:

- Main Warehouse
- Regional Warehouse
- Distribution Center
- Transit Warehouse
- Service Warehouse
- Rental Warehouse
- Spare Parts Warehouse
- Scrap Warehouse

---

# Dependencies

Depends On

- Company

Referenced By

- Warehouse

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseTypeId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| TypeCode | NVARCHAR(20) | No | | | | Unique Type Code |
| TypeName | NVARCHAR(100) | No | | | | Warehouse Type Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| IsSystemDefined | BIT | No | 0 | | | System Record |
| IsActive | BIT | No | 1 | | | Active |
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

PK_WarehouseType

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_WarehouseType_Code
- UQ_WarehouseType_Name

## Check Constraints

- SortOrder > 0
- StatusId > 0

---

# Indexes

## Clustered

PK_WarehouseType

## Non Clustered

IX_WarehouseType_Code

IX_WarehouseType_Name

IX_WarehouseType_Status

IX_WarehouseType_Company

---

# Relationships

Company (1) → WarehouseType (Many)

WarehouseType (1) → Warehouse (Many)

---

# Business Rules

- Warehouse Type Code must be unique within a Company.
- System-defined records cannot be deleted.
- Inactive Warehouse Types cannot be assigned to new Warehouses.
- Soft Delete is mandatory.
- Audit fields are mandatory.
- RowVersion is mandatory.

---

# Published Domain Events

- WarehouseTypeCreated
- WarehouseTypeUpdated
- WarehouseTypeActivated
- WarehouseTypeDeactivated
- WarehouseTypeDeleted

---

# Developer Notes

- Lookup table for Warehouse classification.
- Frequently cached by the application.
- Referenced only by Warehouse aggregate.

---

# ====================================================
# 003 WarehouseZone
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseZone

**Classification:** Master Table

**Aggregate Root:** Warehouse

---

# Purpose

WarehouseZone represents a logical subdivision within a Warehouse.

Zones simplify warehouse operations by grouping storage locations into operational areas.

Examples:

- Receiving
- Dispatch
- Rental Equipment
- Spare Parts
- Heavy Machinery
- Returns
- Damaged Goods
- Quality Inspection

Each Zone contains one or more Warehouse Locations.

---

# Dependencies

Depends On

- Warehouse

Referenced By

- WarehouseLocation

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseZoneId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| ZoneCode | NVARCHAR(30) | No | | | | Zone Code |
| ZoneName | NVARCHAR(150) | No | | | | Zone Name |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| TemperatureControlled | BIT | No | 0 | | | Temperature Controlled |
| IsRestricted | BIT | No | 0 | | | Restricted Access |
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

PK_WarehouseZone

## Foreign Keys

- WarehouseId → Warehouse

## Unique Keys

- UQ_WarehouseZone_Code
- UQ_WarehouseZone_Name

## Check Constraints

- SortOrder > 0

---

# Indexes

## Clustered

PK_WarehouseZone

## Non Clustered

IX_WarehouseZone_Warehouse

IX_WarehouseZone_Code

IX_WarehouseZone_Name

IX_WarehouseZone_Status

---

# Relationships

Warehouse (1) → WarehouseZone (Many)

WarehouseZone (1) → WarehouseLocation (Many)

---

# Business Rules

- Zone Code must be unique within a Warehouse.
- Zone Name must be unique within a Warehouse.
- Restricted Zones require authorized access.
- Inactive Zones cannot receive new locations.
- Zones containing active locations cannot be deleted.
- Soft Delete is mandatory.
- Audit fields are mandatory.
- RowVersion is mandatory.

---

# Published Domain Events

- WarehouseZoneCreated
- WarehouseZoneUpdated
- WarehouseZoneActivated
- WarehouseZoneDeactivated
- WarehouseZoneDeleted

---

# Developer Notes

- Logical grouping of Warehouse Locations.
- Improves warehouse organization and reporting.
- Supports future automation and routing.
- Supports environmental storage requirements.

# ====================================================
# 004 WarehouseLocation
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseLocation

**Classification:** Master Table

**Aggregate Root:** Warehouse

---

# Purpose

WarehouseLocation defines the smallest physical storage unit within a warehouse.

A location represents the exact place where inventory is stored, such as a Rack, Shelf, Bin, Floor Position or Pallet Location.

WarehouseLocation enables accurate inventory tracking and supports future barcode and warehouse automation.

---

# Dependencies

Depends On

- Warehouse
- WarehouseZone

Referenced By

- InventoryStock
- InventoryStockLedger
- InventoryReservation
- InventoryTransactionLine
- StockTransferLine
- StockCountLine
- InventoryAdjustmentLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseLocationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| WarehouseZoneId | BIGINT | Yes | NULL | | ✔ | Warehouse Zone |
| ParentLocationId | BIGINT | Yes | NULL | | ✔ | Parent Location |
| LocationCode | NVARCHAR(30) | No | | | | Unique Location Code |
| LocationName | NVARCHAR(150) | No | | | | Location Name |
| LocationType | SMALLINT | No | 1 | | | Rack / Shelf / Bin / Floor |
| Barcode | NVARCHAR(100) | Yes | NULL | | | Barcode |
| QRCode | NVARCHAR(200) | Yes | NULL | | | QR Code |
| MaximumCapacity | DECIMAL(18,4) | Yes | NULL | | | Maximum Capacity |
| CapacityUnit | NVARCHAR(20) | Yes | NULL | | | Unit |
| IsPickingLocation | BIT | No | 0 | | | Picking Location |
| IsReceivingLocation | BIT | No | 0 | | | Receiving Location |
| IsDispatchLocation | BIT | No | 0 | | | Dispatch Location |
| IsActive | BIT | No | 1 | | | Active |
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

PK_WarehouseLocation

## Foreign Keys

- WarehouseId → Warehouse
- WarehouseZoneId → WarehouseZone
- ParentLocationId → WarehouseLocation

## Unique Keys

- UQ_WarehouseLocation_Code
- UQ_WarehouseLocation_Name

## Check Constraints

- MaximumCapacity >= 0
- SortOrder > 0

---

# Indexes

## Clustered

PK_WarehouseLocation

## Non Clustered

IX_WarehouseLocation_Warehouse

IX_WarehouseLocation_Zone

IX_WarehouseLocation_Code

IX_WarehouseLocation_Parent

IX_WarehouseLocation_Status

---

# Relationships

Warehouse (1) → WarehouseLocation (Many)

WarehouseZone (1) → WarehouseLocation (Many)

WarehouseLocation (1) → WarehouseLocation (Many)

WarehouseLocation (1) → InventoryStock (Many)

WarehouseLocation (1) → InventoryStockLedger (Many)

WarehouseLocation (1) → InventoryTransactionLine (Many)

---

# Business Rules

- Location Code must be unique within a Warehouse.
- Parent Location is optional.
- Circular parent-child hierarchy is prohibited.
- Inactive Locations cannot receive inventory.
- Locations containing inventory cannot be deleted.
- Picking, Receiving and Dispatch flags may coexist.
- Soft Delete is mandatory.
- Audit fields are mandatory.
- RowVersion is mandatory.

---

# Published Domain Events

- WarehouseLocationCreated
- WarehouseLocationUpdated
- WarehouseLocationActivated
- WarehouseLocationDeactivated
- WarehouseLocationDeleted

---

# Developer Notes

- Smallest physical storage unit.
- Supports unlimited hierarchy.
- Supports barcode scanning.
- Supports QR code scanning.
- Future compatible with automated warehouses (ASRS).
- Referenced heavily by Inventory Domain.

---

# ====================================================
# 005 WarehouseAttachment
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Warehouse records with reusable Attachment entities stored in the Shared Kernel.

Examples:

- Warehouse Layout
- Fire Safety Certificate
- Building Lease
- Insurance Documents
- Inspection Reports

---

# Dependencies

Depends On

- Warehouse
- Attachment

Referenced By

- Warehouse Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
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

PK_WarehouseAttachment

## Foreign Keys

- WarehouseId → Warehouse
- AttachmentId → Attachment

## Unique Keys

- UQ_Warehouse_Attachment

---

# Indexes

- PK_WarehouseAttachment
- IX_Warehouse
- IX_Attachment

---

# Relationships

Warehouse (1) → WarehouseAttachment (Many)

Attachment (1) → WarehouseAttachment (Many)

---

# Business Rules

- One attachment can be associated with multiple warehouses.
- Physical files remain in Shared Kernel.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WarehouseAttachmentAdded
- WarehouseAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Business ownership remains within Warehouse Domain.

# ====================================================
# 006 WarehouseNote
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Warehouse records with reusable Note entities maintained within the Shared Kernel.

Warehouse Notes store operational information, internal remarks, maintenance comments, inspection observations and administrative notes related to a warehouse.

---

# Dependencies

Depends On

- Warehouse
- Note

Referenced By

- Warehouse Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
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

PK_WarehouseNote

## Foreign Keys

- WarehouseId → Warehouse
- NoteId → Note

## Unique Keys

- UQ_Warehouse_Note

---

# Indexes

## Clustered

PK_WarehouseNote

## Non Clustered

IX_Warehouse

IX_Note

---

# Relationships

Warehouse (1) → WarehouseNote (Many)

Note (1) → WarehouseNote (Many)

---

# Business Rules

- Notes remain reusable in Shared Kernel.
- Business ownership belongs to Warehouse Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WarehouseNoteAdded
- WarehouseNoteUpdated
- WarehouseNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Used for operational and administrative notes.

---

# ====================================================
# 007 WarehouseActivity
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Warehouse records with reusable Activity entities maintained within the Shared Kernel.

Activities provide a chronological record of operational events performed against a warehouse.

Examples include:

- Warehouse Created
- Warehouse Activated
- Warehouse Deactivated
- Inspection Completed
- Capacity Updated
- Safety Audit Completed

---

# Dependencies

Depends On

- Warehouse
- Activity

Referenced By

- Warehouse Dashboard
- Workflow Engine
- Activity Timeline

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
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

PK_WarehouseActivity

## Foreign Keys

- WarehouseId → Warehouse
- ActivityId → Activity

## Unique Keys

- UQ_Warehouse_Activity

---

# Indexes

## Clustered

PK_WarehouseActivity

## Non Clustered

IX_Warehouse

IX_Activity

IX_Status

---

# Relationships

Warehouse (1) → WarehouseActivity (Many)

Activity (1) → WarehouseActivity (Many)

---

# Business Rules

- Activities remain reusable in Shared Kernel.
- Warehouse owns only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WarehouseActivityCreated
- WarehouseActivityUpdated
- WarehouseActivityDeleted

---

# Developer Notes

- Supports Workflow Engine.
- Supports Notification Module.
- Supports complete operational audit trail.

---

# ====================================================
# 008 WarehouseTimeline
# ====================================================

# Table Classification

**Domain:** Warehouse Domain

**Table Name:** WarehouseTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Warehouse records with reusable Timeline entities maintained within the Shared Kernel.

Timeline provides a chronological history of significant warehouse events, ensuring complete traceability of warehouse operations.

Examples include:

- Warehouse Created
- Warehouse Updated
- Warehouse Activated
- Warehouse Deactivated
- Zone Added
- Location Added
- Safety Inspection Completed
- Capacity Changed

---

# Dependencies

Depends On

- Warehouse
- Timeline

Referenced By

- Warehouse Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
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

PK_WarehouseTimeline

## Foreign Keys

- WarehouseId → Warehouse
- TimelineId → Timeline

## Unique Keys

- UQ_Warehouse_Timeline

---

# Indexes

## Clustered

PK_WarehouseTimeline

## Non Clustered

IX_Warehouse

IX_Timeline

IX_Status

---

# Relationships

Warehouse (1) → WarehouseTimeline (Many)

Timeline (1) → WarehouseTimeline (Many)

---

# Business Rules

- Timeline entries are append-only.
- Timeline records remain immutable.
- Business ownership belongs to Warehouse Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WarehouseTimelineCreated
- WarehouseTimelineUpdated
- WarehouseTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Provides complete warehouse event history.
- Optimized for Timeline UI rendering.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Warehouse Domain defines the physical storage infrastructure used throughout RentalERP. It manages warehouses, warehouse classifications, storage zones, and physical storage locations while integrating with the Shared Kernel for attachments, notes, activities, and timelines.

The Warehouse Domain **does not store inventory quantities**. Inventory balances and movements are handled exclusively by the Inventory Domain.

---

## Aggregate Root

- Warehouse

---

## Supporting Entities

- WarehouseType
- WarehouseZone
- WarehouseLocation

---

## Bridge Entities

- WarehouseAttachment
- WarehouseNote
- WarehouseActivity
- WarehouseTimeline

---

## Major Business Capabilities

- Multi Company Warehouses
- Multi Branch Warehouses
- Warehouse Classification
- Warehouse Zones
- Warehouse Locations
- Storage Hierarchy
- Barcode Ready Locations
- QR Code Ready Locations
- Warehouse Capacity Management
- Warehouse Audit Trail
- Shared Kernel Integration

---

## Published Domain Events

- WarehouseCreated
- WarehouseUpdated
- WarehouseActivated
- WarehouseDeactivated
- WarehouseZoneCreated
- WarehouseLocationCreated

---

## Integration Points

The Warehouse Domain integrates directly with:

- Foundation
- Shared Kernel
- Inventory Domain
- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Asset Domain
- Administration

---

# Warehouse Domain Status

**Status:** ✅ Complete

**Total Tables:** 8

1. Warehouse
2. WarehouseType
3. WarehouseZone
4. WarehouseLocation
5. WarehouseAttachment
6. WarehouseNote
7. WarehouseActivity
8. WarehouseTimeline

---