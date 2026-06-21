# RentalERP v1.0

# InventoryDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Inventory

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Inventory Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. WarehouseLocation

6. InventoryOpeningBalance

7. InventoryStock

---

# Domain Overview

The Inventory Domain is responsible for managing the physical movement, storage, valuation, reservation, and availability of inventory across the organization.

Unlike a traditional accounting-focused ERP, RentalERP places inventory at the center of operational workflows supporting Rental, Service, Sales, Purchase, and Asset Management.

The Inventory Domain maintains accurate stock balances while recording every inventory movement through a complete audit trail. It integrates closely with the Product Domain for item definitions and the Accounting Domain for inventory valuation and financial postings.

The Inventory Domain supports:

- Multi Company
- Multi Branch
- Multi Warehouse
- Warehouse Locations (Bins / Racks / Shelves)
- Opening Balances
- Inventory Reservations
- Inventory Adjustments
- Stock Transfers
- Stock Counting
- Inventory Transactions
- Complete Stock Ledger
- Future Batch Tracking
- Future Serial Number Tracking
- Future Lot Management

All inventory transactions are event-driven and publish domain events for downstream processing.

---

# Business Objectives

The Inventory Domain has the following business objectives:

- Maintain real-time inventory balances.
- Track inventory movement history.
- Support multiple warehouses.
- Support warehouse locations.
- Support stock reservations.
- Support inventory adjustments.
- Support warehouse transfers.
- Support stock counting and reconciliation.
- Maintain inventory valuation.
- Integrate with Accounting.
- Integrate with Rental operations.
- Integrate with Service operations.
- Integrate with Purchase receiving.
- Integrate with Sales fulfillment.
- Maintain complete audit history.

---

# Aggregate Root

The Inventory Domain contains multiple aggregate roots.

Primary Aggregate Roots:

- InventoryTransaction
- StockTransfer
- InventoryAdjustment
- StockCount

Supporting Entities:

- WarehouseLocation
- InventoryOpeningBalance
- InventoryStock
- InventoryStockLedger
- InventoryReservation

Bridge Entities:

- InventoryAttachment
- InventoryNote
- InventoryActivity
- InventoryTimeline

---

# Implementation Order

001 WarehouseLocation

002 InventoryOpeningBalance

003 InventoryStock

004 InventoryStockLedger

005 InventoryReservation

006 InventoryAdjustment

007 InventoryAdjustmentLine

008 StockTransfer

009 StockTransferLine

010 StockCount

011 StockCountLine

012 InventoryTransaction

013 InventoryTransactionLine

014 InventoryAttachment

015 InventoryNote

016 InventoryActivity

017 InventoryTimeline

---

# ====================================================
# 001 WarehouseLocation
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** WarehouseLocation

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

WarehouseLocation defines the physical storage locations within a warehouse.

Locations may represent:

- Warehouse
- Zone
- Rack
- Shelf
- Bin
- Floor Position

This enables precise inventory tracking and improves warehouse operations by identifying the exact storage location of every inventory item.

Warehouse locations are optional and can be enabled based on business requirements.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse

Referenced By

- InventoryStock
- InventoryStockLedger
- InventoryReservation
- StockTransferLine
- InventoryTransactionLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WarehouseLocationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| LocationCode | NVARCHAR(30) | No | | | | Unique Location Code |
| LocationName | NVARCHAR(150) | No | | | | Display Name |
| ParentLocationId | BIGINT | Yes | NULL | | ✔ | Parent Location |
| LocationType | SMALLINT | No | 1 | | | Zone/Rack/Bin/Shelf |
| SequenceNo | INT | No | 1 | | | Display Sequence |
| IsActive | BIT | No | 1 | | | Active Status |
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

- CompanyId → Company
- BranchId → Branch
- WarehouseId → Warehouse
- ParentLocationId → WarehouseLocation

## Unique Keys

- UQ_Warehouse_LocationCode
- UQ_Warehouse_LocationName

## Check Constraints

- SequenceNo > 0
- StatusId > 0

---

# Indexes

## Clustered

PK_WarehouseLocation

## Non Clustered

IX_WarehouseLocation_Code

IX_WarehouseLocation_Name

IX_WarehouseLocation_Warehouse

IX_WarehouseLocation_Parent

IX_WarehouseLocation_Status

---

# Relationships

Warehouse (1) → WarehouseLocation (Many)

WarehouseLocation (1) → WarehouseLocation (Many)

WarehouseLocation (1) → InventoryStock (Many)

WarehouseLocation (1) → InventoryStockLedger (Many)

WarehouseLocation (1) → InventoryReservation (Many)

---

# Business Rules

- Location Code must be unique within a Warehouse.
- Parent Location is optional.
- Circular parent-child relationships are not allowed.
- Inactive locations cannot receive inventory.
- Locations with inventory cannot be deleted.
- Soft Delete is mandatory.
- Audit fields are mandatory.
- RowVersion is required.

---

# Published Domain Events

- WarehouseLocationCreated
- WarehouseLocationUpdated
- WarehouseLocationActivated
- WarehouseLocationDeactivated
- WarehouseLocationDeleted

---

# Developer Notes

- Supports unlimited warehouse hierarchy.
- Designed for future barcode-based warehouse navigation.
- Supports warehouse optimization algorithms.
- Compatible with handheld scanning devices.
- Inventory transactions always reference the lowest-level storage location.
- Fully compliant with RentalERP global standards.

# ====================================================
# 002 InventoryOpeningBalance
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryOpeningBalance

**Classification:** Transaction Table

**Aggregate Root:** Yes

---

# Purpose

InventoryOpeningBalance stores the initial inventory quantities and values imported into the ERP during implementation.

This table is used only during system initialization or opening balance migration. Once posted, it creates the initial Inventory Stock and Inventory Stock Ledger entries and should become read-only.

Opening balances establish the baseline inventory position before live business transactions begin.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- WarehouseLocation
- Item
- FiscalYear

Referenced By

- InventoryStock
- InventoryStockLedger
- Accounting Journal
- InventoryTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryOpeningBalanceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| FiscalYearId | BIGINT | No | | | ✔ | Fiscal Year |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| ItemId | BIGINT | No | | | ✔ | Item |
| OpeningDate | DATE | No | | | | Opening Date |
| Quantity | DECIMAL(18,4) | No | 0 | | | Opening Quantity |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Total Cost |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted Status |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posting Date |
| PostedBy | BIGINT | Yes | NULL | | | Posted By |
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

PK_InventoryOpeningBalance

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- FiscalYearId → FiscalYear
- WarehouseId → Warehouse
- WarehouseLocationId → WarehouseLocation
- ItemId → Item
- CurrencyId → Currency

## Unique Keys

- UQ_OpeningBalance_Item_Warehouse

## Check Constraints

- Quantity >= 0
- UnitCost >= 0
- TotalCost >= 0

---

# Indexes

## Clustered

PK_InventoryOpeningBalance

## Non Clustered

IX_IOB_Item

IX_IOB_Warehouse

IX_IOB_Location

IX_IOB_Posted

IX_IOB_Status

IX_IOB_FiscalYear

---

# Relationships

Company (1) → InventoryOpeningBalance (Many)

Branch (1) → InventoryOpeningBalance (Many)

Warehouse (1) → InventoryOpeningBalance (Many)

WarehouseLocation (1) → InventoryOpeningBalance (Many)

Item (1) → InventoryOpeningBalance (Many)

FiscalYear (1) → InventoryOpeningBalance (Many)

---

# Business Rules

- Opening Balance can only be entered once per Item/Warehouse combination.
- Opening balances cannot be modified after posting.
- Posting automatically creates Inventory Stock records.
- Posting automatically creates Inventory Stock Ledger entries.
- Posting automatically creates Accounting Journal Entries.
- Negative quantities are not allowed.
- Soft Delete only before posting.
- Audit fields are mandatory.
- RowVersion is required.

---

# Published Domain Events

- InventoryOpeningBalanceCreated
- InventoryOpeningBalanceUpdated
- InventoryOpeningBalancePosted
- InventoryOpeningBalanceDeleted

---

# Developer Notes

- Used only during ERP implementation.
- Posting is irreversible.
- Posting initializes current inventory balances.
- Supports multi-currency implementations.
- Integrates with Accounting valuation.

---

# ====================================================
# 003 InventoryStock
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryStock

**Classification:** Aggregate Root

**Aggregate Root:** Yes

---

# Purpose

InventoryStock maintains the current available inventory balance for every Item stored within a Warehouse and Warehouse Location.

Unlike InventoryStockLedger, this table stores only the latest inventory balance and is continuously updated after every inventory transaction.

InventoryStock is the primary source for inventory availability throughout the ERP.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- WarehouseLocation
- Item

Referenced By

- Rental
- Sales
- Purchase
- Service
- InventoryReservation
- InventoryTransaction
- StockTransfer
- StockCount

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryStockId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| ItemId | BIGINT | No | | | ✔ | Item |
| AvailableQuantity | DECIMAL(18,4) | No | 0 | | | Available Stock |
| ReservedQuantity | DECIMAL(18,4) | No | 0 | | | Reserved Stock |
| InTransitQuantity | DECIMAL(18,4) | No | 0 | | | In Transit |
| DamagedQuantity | DECIMAL(18,4) | No | 0 | | | Damaged Stock |
| OnHandQuantity | DECIMAL(18,4) | No | 0 | | | Physical Stock |
| AverageCost | DECIMAL(18,4) | No | 0 | | | Average Cost |
| LastTransactionDate | DATETIME2(7) | Yes | NULL | | | Last Movement |
| LastStockUpdate | DATETIME2(7) | Yes | NULL | | | Last Update |
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

PK_InventoryStock

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- WarehouseId → Warehouse
- WarehouseLocationId → WarehouseLocation
- ItemId → Item

## Unique Keys

- UQ_InventoryStock_Item_Warehouse_Location

## Check Constraints

- AvailableQuantity >= 0
- ReservedQuantity >= 0
- AverageCost >= 0

---

# Indexes

## Clustered

PK_InventoryStock

## Non Clustered

IX_InventoryStock_Item

IX_InventoryStock_Warehouse

IX_InventoryStock_Location

IX_InventoryStock_Status

IX_InventoryStock_Available

---

# Relationships

Warehouse (1) → InventoryStock (Many)

WarehouseLocation (1) → InventoryStock (Many)

Item (1) → InventoryStock (Many)

---

# Business Rules

- InventoryStock is maintained automatically.
- Manual updates are prohibited.
- Every inventory transaction updates this table.
- Reserved Quantity cannot exceed Available Quantity.
- Available Quantity cannot become negative unless negative inventory is enabled.
- Average Cost is maintained automatically.
- Soft Delete is prohibited after stock exists.
- Audit fields are mandatory.
- RowVersion is required.

---

# Published Domain Events

- InventoryStockCreated
- InventoryStockUpdated
- InventoryStockAdjusted
- InventoryStockReserved
- InventoryStockReleased

---

# Developer Notes

- This is the current stock balance table.
- Never calculate stock directly from InventoryStockLedger during normal operations.
- Optimized for fast stock availability queries.
- Updated by InventoryTransaction service only.
- Supports future Batch and Serial Number extensions.
```

# ====================================================
# 004 InventoryStockLedger
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryStockLedger

**Classification:** Transaction Table

**Aggregate Root:** No

---

# Purpose

InventoryStockLedger maintains the complete historical audit trail of every inventory movement occurring within the system.

Unlike InventoryStock, this table is append-only and records every inventory transaction including receipts, issues, transfers, reservations, adjustments, rental movements, sales, purchases, and service consumption.

It serves as the authoritative source for inventory history, valuation, and reconciliation.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- WarehouseLocation
- Item
- InventoryTransaction

Referenced By

- Inventory Reports
- Stock Movement Reports
- Inventory Valuation
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryStockLedgerId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| ItemId | BIGINT | No | | | ✔ | Item |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
| TransactionDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Transaction Date |
| TransactionType | SMALLINT | No | | | | Movement Type |
| ReferenceNo | NVARCHAR(50) | No | | | | Source Document |
| QuantityIn | DECIMAL(18,4) | No | 0 | | | Incoming Qty |
| QuantityOut | DECIMAL(18,4) | No | 0 | | | Outgoing Qty |
| RunningBalance | DECIMAL(18,4) | No | 0 | | | Balance |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Total Cost |
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

PK_InventoryStockLedger

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- WarehouseId → Warehouse
- WarehouseLocationId → WarehouseLocation
- ItemId → Item
- InventoryTransactionId → InventoryTransaction

## Check Constraints

- QuantityIn >= 0
- QuantityOut >= 0
- RunningBalance >= 0

---

# Indexes

## Clustered

PK_InventoryStockLedger

## Non Clustered

IX_InventoryStockLedger_Item

IX_InventoryStockLedger_TransactionDate

IX_InventoryStockLedger_Warehouse

IX_InventoryStockLedger_ReferenceNo

IX_InventoryStockLedger_Transaction

---

# Relationships

InventoryTransaction (1) → InventoryStockLedger (Many)

Item (1) → InventoryStockLedger (Many)

Warehouse (1) → InventoryStockLedger (Many)

---

# Business Rules

- Ledger records are append-only.
- Ledger entries cannot be modified after posting.
- Running Balance is calculated automatically.
- Every inventory movement creates one or more ledger records.
- Soft Delete is prohibited.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryStockLedgerCreated
- InventoryStockLedgerPosted

---

# Developer Notes

- Source of all stock movement reports.
- Used for inventory valuation.
- Never manually edited.
- Supports FIFO, Average Cost and future costing methods.

---

# ====================================================
# 005 InventoryReservation
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryReservation

**Classification:** Transaction Table

**Aggregate Root:** No

---

# Purpose

Stores reserved inventory quantities for pending Rental, Sales, Service and internal operational transactions.

Reservations reduce available inventory without affecting physical stock until the transaction is completed.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- WarehouseLocation
- Item
- Customer

Referenced By

- Rental Agreement
- Sales Order
- Service Order

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryReservationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| ItemId | BIGINT | No | | | ✔ | Item |
| CustomerId | BIGINT | Yes | NULL | | ✔ | Customer |
| ReferenceNo | NVARCHAR(50) | No | | | | Document Number |
| ReservationDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Reservation Date |
| ReservedQuantity | DECIMAL(18,4) | No | 0 | | | Reserved Qty |
| ReleasedQuantity | DECIMAL(18,4) | No | 0 | | | Released Qty |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiry |
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

PK_InventoryReservation

## Foreign Keys

- CompanyId
- BranchId
- WarehouseId
- WarehouseLocationId
- ItemId
- CustomerId

## Check Constraints

- ReservedQuantity > 0
- ReleasedQuantity >= 0

---

# Indexes

- PK_InventoryReservation
- IX_Item
- IX_Customer
- IX_ReferenceNo
- IX_ExpiryDate
- IX_Status

---

# Relationships

Item (1) → InventoryReservation (Many)

Customer (1) → InventoryReservation (Many)

Warehouse (1) → InventoryReservation (Many)

---

# Business Rules

- Reserved Quantity cannot exceed Available Quantity.
- Reservation automatically expires if configured.
- Reservation release updates InventoryStock.
- Reservation cannot be modified after fulfillment.
- Audit mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryReserved
- InventoryReservationUpdated
- InventoryReservationReleased
- InventoryReservationExpired

---

# Developer Notes

- Used by Rental, Sales and Service modules.
- Supports partial reservation release.
- Reservation does not change On-Hand Quantity.

---

# ====================================================
# 006 InventoryAdjustment
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryAdjustment

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

Represents manual inventory corrections resulting from damaged stock, stock variance, stock write-off, inventory gains, inventory losses or operational corrections.

InventoryAdjustment is always accompanied by one or more InventoryAdjustmentLine records.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- Employee
- NumberSeries

Referenced By

- InventoryAdjustmentLine
- InventoryTransaction
- InventoryStockLedger
- Accounting Journal

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryAdjustmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| AdjustmentNo | NVARCHAR(30) | No | Number Series | | | Adjustment Number |
| AdjustmentDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Adjustment Date |
| AdjustmentType | SMALLINT | No | | | | Gain / Loss / Damage |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
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

PK_InventoryAdjustment

## Foreign Keys

- CompanyId
- BranchId
- WarehouseId
- PostedBy

## Unique Keys

- UQ_InventoryAdjustment_AdjustmentNo

---

# Indexes

- PK_InventoryAdjustment
- IX_AdjustmentNo
- IX_Warehouse
- IX_AdjustmentDate
- IX_Posted
- IX_Status

---

# Relationships

InventoryAdjustment (1) → InventoryAdjustmentLine (Many)

---

# Business Rules

- Adjustment Number generated from Number Series.
- Cannot post without detail lines.
- Posting updates InventoryStock.
- Posting creates InventoryStockLedger entries.
- Posting creates Accounting Journal Entries.
- Posted documents cannot be edited.
- Audit mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryAdjustmentCreated
- InventoryAdjustmentPosted
- InventoryAdjustmentCancelled

---

# Developer Notes

- Supports inventory gains and losses.
- Fully integrated with Accounting.
- Designed for audit compliance.
- Supports future approval workflows.
```

# ====================================================
# 007 InventoryAdjustmentLine
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryAdjustmentLine

**Classification:** Transaction Detail

**Aggregate Root:** InventoryAdjustment

---

# Purpose

InventoryAdjustmentLine stores the individual inventory items being adjusted within an Inventory Adjustment transaction.

Each line represents the quantity and valuation impact of a single inventory item.

---

# Dependencies

Depends On

- InventoryAdjustment
- Item
- WarehouseLocation

Referenced By

- InventoryTransaction
- InventoryStock
- InventoryStockLedger

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryAdjustmentLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryAdjustmentId | BIGINT | No | | | ✔ | Inventory Adjustment |
| ItemId | BIGINT | No | | | ✔ | Item |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| Quantity | DECIMAL(18,4) | No | 0 | | | Adjustment Quantity |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Total Cost |
| ReasonCode | NVARCHAR(30) | Yes | NULL | | | Adjustment Reason |
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

PK_InventoryAdjustmentLine

## Foreign Keys

- InventoryAdjustmentId → InventoryAdjustment
- ItemId → Item
- WarehouseLocationId → WarehouseLocation

## Check Constraints

- Quantity <> 0
- UnitCost >= 0
- TotalCost >= 0

---

# Indexes

## Clustered

PK_InventoryAdjustmentLine

## Non Clustered

IX_Adjustment

IX_Item

IX_Location

IX_Status

---

# Relationships

InventoryAdjustment (1) → InventoryAdjustmentLine (Many)

Item (1) → InventoryAdjustmentLine (Many)

WarehouseLocation (1) → InventoryAdjustmentLine (Many)

---

# Business Rules

- At least one line is required.
- Quantity cannot be zero.
- Positive quantity increases stock.
- Negative quantity decreases stock.
- Posting updates InventoryStock automatically.
- Posting creates InventoryStockLedger entries.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryAdjustmentLineCreated
- InventoryAdjustmentLineUpdated
- InventoryAdjustmentLineDeleted

---

# Developer Notes

- Supports gain, loss, damaged and expired inventory.
- Costing follows configured valuation method.

---

# ====================================================
# 008 StockTransfer
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** StockTransfer

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

StockTransfer records inventory movement between warehouses or warehouse locations.

Transfers preserve inventory valuation while changing physical storage locations.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- Employee
- NumberSeries

Referenced By

- StockTransferLine
- InventoryTransaction
- InventoryStockLedger

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| StockTransferId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| FromWarehouseId | BIGINT | No | | | ✔ | Source Warehouse |
| ToWarehouseId | BIGINT | No | | | ✔ | Destination Warehouse |
| TransferNo | NVARCHAR(30) | No | Number Series | | | Transfer Number |
| TransferDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Transfer Date |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
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

PK_StockTransfer

## Foreign Keys

- CompanyId
- BranchId
- FromWarehouseId
- ToWarehouseId
- PostedBy

## Unique Keys

- UQ_StockTransfer_TransferNo

## Check Constraints

- FromWarehouseId <> ToWarehouseId

---

# Indexes

- PK_StockTransfer
- IX_TransferNo
- IX_FromWarehouse
- IX_ToWarehouse
- IX_Posted
- IX_Status

---

# Relationships

StockTransfer (1) → StockTransferLine (Many)

---

# Business Rules

- Source and Destination Warehouse cannot be identical.
- Posting transfers inventory between warehouses.
- Posting creates InventoryTransaction records.
- Posting creates InventoryStockLedger entries.
- Posted documents are read-only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- StockTransferCreated
- StockTransferPosted
- StockTransferCancelled

---

# Developer Notes

- Supports inter-warehouse and intra-warehouse transfers.
- Inventory valuation remains unchanged.

---

# ====================================================
# 009 StockTransferLine
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** StockTransferLine

**Classification:** Transaction Detail

**Aggregate Root:** StockTransfer

---

# Purpose

StockTransferLine stores individual inventory items transferred between warehouses or warehouse locations.

---

# Dependencies

Depends On

- StockTransfer
- Item
- WarehouseLocation

Referenced By

- InventoryTransaction
- InventoryStockLedger

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| StockTransferLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| StockTransferId | BIGINT | No | | | ✔ | Stock Transfer |
| ItemId | BIGINT | No | | | ✔ | Item |
| FromLocationId | BIGINT | Yes | NULL | | ✔ | Source Location |
| ToLocationId | BIGINT | Yes | NULL | | ✔ | Destination Location |
| Quantity | DECIMAL(18,4) | No | 0 | | | Transfer Quantity |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Total Cost |
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

PK_StockTransferLine

## Foreign Keys

- StockTransferId
- ItemId
- FromLocationId
- ToLocationId

## Check Constraints

- Quantity > 0

---

# Indexes

- PK_StockTransferLine
- IX_Transfer
- IX_Item
- IX_FromLocation
- IX_ToLocation

---

# Relationships

StockTransfer (1) → StockTransferLine (Many)

Item (1) → StockTransferLine (Many)

WarehouseLocation (1) → StockTransferLine (Many)

---

# Business Rules

- Quantity must be greater than zero.
- Transfer quantity cannot exceed available stock.
- Posting updates InventoryStock.
- Posting creates InventoryStockLedger entries.
- Audit mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- StockTransferLineCreated
- StockTransferLineUpdated
- StockTransferLineDeleted

---

# Developer Notes

- Supports location-to-location movement.
- Preserves inventory valuation.
- Supports future barcode-based warehouse transfers.
```

# ====================================================
# 010 StockCount
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** StockCount

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

StockCount represents a physical inventory counting session for a warehouse.

It is used to verify actual inventory against system inventory and identify shortages, overages, damaged items, and reconciliation differences.

A StockCount consists of one or more StockCountLine records.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- Employee
- NumberSeries

Referenced By

- StockCountLine
- InventoryAdjustment
- InventoryTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| StockCountId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| CountNo | NVARCHAR(30) | No | Number Series | | | Count Number |
| CountDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Count Date |
| CountType | SMALLINT | No | 1 | | | Full / Cycle Count |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posting Date |
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

PK_StockCount

## Foreign Keys

- CompanyId
- BranchId
- WarehouseId
- PostedBy

## Unique Keys

- UQ_StockCount_CountNo

## Check Constraints

- CountDate <= SYSUTCDATETIME()

---

# Indexes

## Clustered

PK_StockCount

## Non Clustered

IX_CountNo

IX_Warehouse

IX_CountDate

IX_Posted

IX_Status

---

# Relationships

StockCount (1) → StockCountLine (Many)

Warehouse (1) → StockCount (Many)

---

# Business Rules

- Count Number generated using Number Series.
- Warehouse must be selected.
- Cannot post without detail lines.
- Posting automatically generates Inventory Adjustment.
- Posted Count cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- StockCountCreated
- StockCountPosted
- StockCountCancelled

---

# Developer Notes

- Supports full inventory count.
- Supports cycle counting.
- Supports barcode scanning.
- Supports mobile inventory counting.

---

# ====================================================
# 011 StockCountLine
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** StockCountLine

**Classification:** Transaction Detail

**Aggregate Root:** StockCount

---

# Purpose

Stores physical inventory quantities counted during a Stock Count session.

Differences between counted quantity and system quantity are converted into Inventory Adjustments after posting.

---

# Dependencies

Depends On

- StockCount
- Item
- WarehouseLocation

Referenced By

- InventoryAdjustment

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| StockCountLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| StockCountId | BIGINT | No | | | ✔ | Stock Count |
| ItemId | BIGINT | No | | | ✔ | Item |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Location |
| SystemQuantity | DECIMAL(18,4) | No | 0 | | | ERP Quantity |
| CountedQuantity | DECIMAL(18,4) | No | 0 | | | Physical Quantity |
| DifferenceQuantity | DECIMAL(18,4) | No | 0 | | | Variance |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Total Cost |
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

PK_StockCountLine

## Foreign Keys

- StockCountId
- ItemId
- WarehouseLocationId

## Check Constraints

- CountedQuantity >= 0

---

# Indexes

- PK_StockCountLine
- IX_StockCount
- IX_Item
- IX_Location

---

# Relationships

StockCount (1) → StockCountLine (Many)

Item (1) → StockCountLine (Many)

WarehouseLocation (1) → StockCountLine (Many)

---

# Business Rules

- Difference Quantity calculated automatically.
- Posting creates Inventory Adjustment.
- Posting updates Inventory Stock.
- Posting creates Inventory Stock Ledger.
- Audit mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- StockCountLineCreated
- StockCountLineUpdated
- StockCountCompleted

---

# Developer Notes

- Supports handheld barcode devices.
- Supports cycle counting.
- Supports partial warehouse counting.

---

# ====================================================
# 012 InventoryTransaction
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryTransaction

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

InventoryTransaction is the central inventory movement document within RentalERP.

Every inventory movement—whether originating from Purchase, Sales, Rental, Service, Stock Transfer, Inventory Adjustment, or Stock Count—is normalized into an InventoryTransaction.

It serves as the single source of truth for inventory movement processing and is responsible for updating InventoryStock and InventoryStockLedger.

---

# Dependencies

Depends On

- Company
- Branch
- Warehouse
- Employee
- NumberSeries

Referenced By

- InventoryTransactionLine
- InventoryStock
- InventoryStockLedger
- Accounting Journal

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryTransactionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| MovementNo | NVARCHAR(30) | No | Number Series | | | Movement Number |
| MovementDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Posting Date |
| MovementType | SMALLINT | No | | | | Purchase, Sale, Rental Issue, Rental Return, Transfer, Adjustment, Stock Count, Opening Balance, etc. |
| SourceDocumentType | SMALLINT | Yes | NULL | | | Purchase Order, Sales Invoice, Rental Contract, Stock Transfer, Adjustment, etc. |
| SourceDocumentId | BIGINT | Yes | NULL | | | Source Document Id |
| ReferenceNo | NVARCHAR(50) | Yes | NULL | | | Source Document Number |
| IsPosted | BIT | No | 0 | | | Posted Flag |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | ✔ | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | ✔ | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | ✔ | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_InventoryTransaction

## Foreign Keys

- CompanyId
- BranchId
- WarehouseId
- PostedBy

## Unique Keys

- UQ_InventoryTransaction_TransactionNo

---

# Indexes

- PK_InventoryTransaction
- IX_TransactionNo
- IX_TransactionDate
- IX_TransactionType
- IX_Warehouse
- IX_Status

---

# Relationships

InventoryTransaction (1) → InventoryTransactionLine (Many)

---

# Business Rules

- All inventory movements must create an InventoryTransaction.
- Transaction Number generated from Number Series.
- Cannot post without detail lines.
- Posting updates InventoryStock.
- Posting creates InventoryStockLedger.
- Posting creates Accounting Journal entries when applicable.
- Posted transactions are immutable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryTransactionCreated
- InventoryTransactionPosted
- InventoryTransactionCancelled

---

# Developer Notes

- Core transaction engine of Inventory Domain.
- Supports all inventory movement scenarios.
- Designed for future event-driven processing.
- Foundation for inventory audit and valuation.
```

# ====================================================
# 013 InventoryTransactionLine
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryTransactionLine

**Classification:** Transaction Detail

**Aggregate Root:** InventoryTransaction

---

# Purpose

InventoryTransactionLine stores the individual inventory movement lines belonging to an Inventory Transaction.

Each line represents the movement of a single inventory item and serves as the source for updating Inventory Stock balances and Inventory Stock Ledger records.

---

# Dependencies

Depends On

- InventoryTransaction
- Item
- WarehouseLocation

Referenced By

- InventoryStock
- InventoryStockLedger

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryTransactionLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
| ItemId | BIGINT | No | | | ✔ | Item |
| WarehouseLocationId | BIGINT | Yes | NULL | | ✔ | Warehouse Bin / Rack |
| TransactionDirection | SMALLINT | No | | | | 1 = In, 2 = Out |
| Quantity | DECIMAL(18,4) | No | 0 | | | Absolute Quantity |
| UnitId | BIGINT | No | | | ✔ | Inventory Unit |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Cost Per Unit |
| TotalCost | DECIMAL(18,4) | No | 0 | | | Quantity × Unit Cost |
| RunningQuantity | DECIMAL(18,4) | No | 0 | | | Stock Balance After Posting |
| AverageCost | DECIMAL(18,4) | No | 0 | | | Moving Average Cost After Posting |
| InventoryValue | DECIMAL(18,4) | No | 0 | | | Running Inventory Value |
| BatchNumber | NVARCHAR(50) | Yes | NULL | | | Future Batch Support |
| SerialNumber | NVARCHAR(100) | Yes | NULL | | | Future Serial Support |
| ExpiryDate | DATE | Yes | NULL | | | Future Expiry Support |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Line Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | ✔ | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | ✔ | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | ✔ | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_InventoryTransactionLine

## Foreign Keys

- InventoryTransactionId → InventoryTransaction
- ItemId → Item
- WarehouseLocationId → WarehouseLocation

## Check Constraints

- Quantity > 0
- UnitCost >= 0
- TotalCost >= 0

---

# Indexes

## Clustered

PK_InventoryTransactionLine

## Non Clustered

IX_InventoryTransaction

IX_Item

IX_WarehouseLocation

IX_Status

---

# Relationships

InventoryTransaction (1) → InventoryTransactionLine (Many)

Item (1) → InventoryTransactionLine (Many)

WarehouseLocation (1) → InventoryTransactionLine (Many)

---

# Business Rules

- Transaction Direction determines stock increase or decrease.
- Quantity must always be greater than zero.
- Posting updates InventoryStock.
- Posting creates InventoryStockLedger.
- Costing follows configured inventory valuation method.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryTransactionLineCreated
- InventoryTransactionLineUpdated
- InventoryTransactionLinePosted

---

# Developer Notes

- Central inventory movement detail table.
- Supports future Batch and Serial Number tracking.
- Used by all inventory-related modules.

---

# ====================================================
# 014 InventoryAttachment
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Inventory Transactions with reusable Attachment records stored within the Shared Kernel.

Business ownership remains inside Inventory Domain while physical file information remains inside Shared Kernel.

---

# Dependencies

Depends On

- InventoryTransaction
- Attachment

Referenced By

- Inventory UI
- Document Viewer

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
| AttachmentId | BIGINT | No | | | ✔ | Shared Attachment |
| DisplayOrder | INT | No | 1 | | | Display Sequence |
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

PK_InventoryAttachment

## Foreign Keys

- InventoryTransactionId → InventoryTransaction
- AttachmentId → Attachment

## Unique Keys

- UQ_InventoryAttachment

---

# Indexes

- PK_InventoryAttachment
- IX_InventoryTransaction
- IX_Attachment

---

# Relationships

InventoryTransaction (1) → InventoryAttachment (Many)

Attachment (1) → InventoryAttachment (Many)

---

# Business Rules

- One attachment may be linked to multiple transactions.
- Physical file remains inside Shared Kernel.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryAttachmentAdded
- InventoryAttachmentRemoved

---

# Developer Notes

- Follows Shared Kernel bridge pattern.
- Business ownership never exists in Shared Kernel.

---

# ====================================================
# 015 InventoryNote
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Inventory Transactions with reusable Note records stored within the Shared Kernel.

Allows users to maintain operational notes, audit remarks and internal comments without duplicating note structures.

---

# Dependencies

Depends On

- InventoryTransaction
- Note

Referenced By

- Inventory UI

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
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

PK_InventoryNote

## Foreign Keys

- InventoryTransactionId → InventoryTransaction
- NoteId → Note

---

# Indexes

- PK_InventoryNote
- IX_InventoryTransaction
- IX_Note

---

# Relationships

InventoryTransaction (1) → InventoryNote (Many)

Note (1) → InventoryNote (Many)

---

# Business Rules

- Uses Shared Kernel Note entity.
- Notes are reusable.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryNoteAdded
- InventoryNoteUpdated
- InventoryNoteRemoved

---

# Developer Notes

- Shared Kernel bridge implementation.
- Prevents duplication of Note entity.

# ====================================================
# 016 InventoryActivity
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Inventory Transactions with reusable Activity records maintained in the Shared Kernel.

Activities represent operational actions such as approvals, postings, reversals, workflow steps, notifications, and user actions.

This table enables complete operational tracking without storing activity ownership inside the Shared Kernel.

---

# Dependencies

Depends On

- InventoryTransaction
- Activity

Referenced By

- Inventory Dashboard
- Activity Timeline
- Workflow Engine
- Notification Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
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

PK_InventoryActivity

## Foreign Keys

- InventoryTransactionId → InventoryTransaction
- ActivityId → Activity

## Unique Keys

- UQ_InventoryActivity

---

# Indexes

## Clustered

PK_InventoryActivity

## Non Clustered

IX_InventoryTransaction

IX_Activity

IX_Status

---

# Relationships

InventoryTransaction (1) → InventoryActivity (Many)

Activity (1) → InventoryActivity (Many)

---

# Business Rules

- Activities are maintained inside Shared Kernel.
- Inventory module owns only the relationship.
- Activities cannot be duplicated.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryActivityCreated
- InventoryActivityUpdated
- InventoryActivityDeleted

---

# Developer Notes

- Implements Shared Kernel bridge pattern.
- Enables reusable activity tracking.
- Supports Workflow Engine.
- Supports Notification Engine.
- Supports ERP Audit Trail.

---

# ====================================================
# 017 InventoryTimeline
# ====================================================

# Table Classification

**Domain:** Inventory Domain

**Table Name:** InventoryTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Inventory Transactions with Timeline records maintained inside the Shared Kernel.

Timeline provides a chronological history of all important events related to an inventory transaction, including creation, approval, posting, adjustment, cancellation, reservation, release, and other operational milestones.

---

# Dependencies

Depends On

- InventoryTransaction
- Timeline

Referenced By

- Inventory Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| InventoryTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| InventoryTransactionId | BIGINT | No | | | ✔ | Inventory Transaction |
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

PK_InventoryTimeline

## Foreign Keys

- InventoryTransactionId → InventoryTransaction
- TimelineId → Timeline

## Unique Keys

- UQ_InventoryTimeline

---

# Indexes

## Clustered

PK_InventoryTimeline

## Non Clustered

IX_InventoryTransaction

IX_Timeline

IX_Status

---

# Relationships

InventoryTransaction (1) → InventoryTimeline (Many)

Timeline (1) → InventoryTimeline (Many)

---

# Business Rules

- Timeline records are immutable.
- Timeline events are append-only.
- Shared Timeline entity remains reusable.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- InventoryTimelineCreated
- InventoryTimelineUpdated
- InventoryTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel bridge pattern.
- Used by Timeline UI.
- Supports ERP event history.
- Provides chronological audit history.
- Optimized for timeline rendering.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Inventory Domain is responsible for maintaining accurate inventory balances, recording every inventory movement, supporting warehouse operations, and providing complete inventory traceability across the ERP.

It acts as the operational bridge between the Product, Purchase, Sales, Rental, Service, Warehouse, Asset, and Accounting domains.

---

## Aggregate Roots

- InventoryTransaction
- InventoryAdjustment
- StockTransfer
- StockCount
- InventoryOpeningBalance

---

## Supporting Entities

- WarehouseLocation
- InventoryStock
- InventoryStockLedger
- InventoryReservation

---

## Bridge Entities

- InventoryAttachment
- InventoryNote
- InventoryActivity
- InventoryTimeline

---

## Major Business Capabilities

- Multi Company
- Multi Branch
- Multi Warehouse
- Warehouse Locations
- Inventory Reservations
- Inventory Adjustments
- Stock Transfers
- Physical Stock Counting
- Inventory Transactions
- Complete Stock Ledger
- Inventory Valuation
- Audit Trail
- Future Batch Tracking
- Future Serial Number Tracking
- Future Lot Tracking

---

## Domain Events

The Inventory Domain publishes events including (but not limited to):

- InventoryTransactionCreated
- InventoryTransactionPosted
- InventoryAdjusted
- InventoryReserved
- InventoryReleased
- StockTransferPosted
- StockCountPosted
- InventoryOpeningBalancePosted

These events enable event-driven integration with:

- Rental Domain
- Sales Domain
- Purchase Domain
- Service Domain
- Asset Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Inventory Domain integrates directly with:

- Foundation
- Shared Kernel
- Product Domain
- Warehouse Domain
- Rental Domain
- Service Domain
- Sales Domain
- Purchase Domain
- Asset Domain
- Accounting Domain
- Administration
- Security

---

# Inventory Domain Status

**Status:** ✅ Complete