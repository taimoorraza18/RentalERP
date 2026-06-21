# RentalERP v1.0

# PurchaseDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Purchase

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Purchase Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. PurchaseRequisition

6. PurchaseRequisitionLine

7. RequestForQuotation (RFQ)

---

# Domain Overview

The Purchase Domain manages the complete procurement lifecycle of RentalERP, beginning with internal purchase requests and ending with vendor invoicing and inventory updates.

It ensures that Assets, spare parts, consumables and operational supplies are procured efficiently while maintaining complete financial and inventory control.

The Purchase Domain integrates directly with Vendor, Product, Warehouse, Inventory, Asset and Accounting domains.

---

# Business Objectives

The Purchase Domain provides:

- Purchase Requisitions
- Request for Quotations (RFQ)
- Vendor Quotations
- Purchase Orders
- Goods Receipt Notes (GRN)
- Purchase Returns
- Vendor Invoices
- Landed Cost Allocation
- Inventory Updates
- Vendor Performance Tracking
- Procurement Cost Analysis
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Roots

- PurchaseRequisition
- PurchaseOrder
- GoodsReceipt

## Supporting Entities

- PurchaseRequisitionLine
- RFQ
- RFQLine
- PurchaseOrderLine
- GoodsReceiptLine
- PurchaseReturn
- PurchaseInvoice

## Bridge Entities

- PurchaseAttachment
- PurchaseNote
- PurchaseActivity
- PurchaseTimeline

---

# Implementation Order

001 PurchaseRequisition

002 PurchaseRequisitionLine

003 RequestForQuotation (RFQ)

004 RequestForQuotationLine

005 PurchaseOrder

006 PurchaseOrderLine

007 GoodsReceipt

008 GoodsReceiptLine

009 PurchaseReturn

010 PurchaseInvoice

011 PurchaseAttachment

012 PurchaseNote

013 PurchaseActivity

014 PurchaseTimeline

---

# ====================================================
# 001 PurchaseRequisition
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseRequisition

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

PurchaseRequisition represents an internal request to procure goods or services.

Requisitions are created by departments or automated processes when inventory levels fall below reorder thresholds or when new Assets are required.

Approved requisitions are converted into RFQs or directly into Purchase Orders.

---

# Dependencies

Depends On

- Company
- Branch
- Department
- Employee
- NumberSeries

Referenced By

- PurchaseRequisitionLine
- RequestForQuotation
- PurchaseOrder

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseRequisitionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| DepartmentId | BIGINT | No | | | ✔ | Department |
| RequestedBy | BIGINT | No | | | ✔ | Employee |
| RequisitionNo | NVARCHAR(30) | No | Number Series | | | Requisition Number |
| RequisitionDate | DATE | No | GETDATE() | | | Request Date |
| RequiredDate | DATE | Yes | NULL | | | Required By |
| Priority | SMALLINT | No | 2 | | | Low / Medium / High / Critical |
| Purpose | NVARCHAR(500) | Yes | NULL | | | Purpose |
| IsApproved | BIT | No | 0 | | | Approved |
| ApprovedDate | DATETIME2(7) | Yes | NULL | | | Approval Date |
| ApprovedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Draft / Approved / Closed |
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

PK_PurchaseRequisition

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- DepartmentId → Department
- RequestedBy → Employee
- ApprovedBy → Employee

## Unique Keys

- UQ_PurchaseRequisition_No

## Check Constraints

- RequiredDate >= RequisitionDate

---

# Indexes

## Clustered

PK_PurchaseRequisition

## Non Clustered

IX_RequisitionNo

IX_Department

IX_RequestedBy

IX_RequisitionDate

IX_Priority

IX_Status

---

# Relationships

Department (1) → PurchaseRequisition (Many)

PurchaseRequisition (1) → PurchaseRequisitionLine (Many)

PurchaseRequisition (1) → RequestForQuotation (Many)

PurchaseRequisition (1) → PurchaseOrder (Optional)

---

# Business Rules

- Requisition Number generated using Number Series.
- Approval required before RFQ or Purchase Order creation.
- Closed requisitions cannot be modified.
- Multiple requisitions may be consolidated into one Purchase Order.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseRequisitionCreated
- PurchaseRequisitionApproved
- PurchaseRequisitionRejected
- PurchaseRequisitionClosed

---

# Developer Notes

- Entry point of procurement workflow.
- May be generated manually or automatically from Inventory reorder rules.
- Does not affect Inventory or Accounting.

---

# ====================================================
# 002 PurchaseRequisitionLine
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseRequisitionLine

**Classification:** Transaction Detail

**Aggregate Root:** PurchaseRequisition

---

# Purpose

PurchaseRequisitionLine stores the individual products, spare parts, consumables or Assets requested within a Purchase Requisition.

Each line defines the requested quantity, expected delivery date and business justification.

---

# Dependencies

Depends On

- PurchaseRequisition
- Item

Referenced By

- RequestForQuotationLine
- PurchaseOrderLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseRequisitionLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseRequisitionId | BIGINT | No | | | ✔ | Purchase Requisition |
| ItemId | BIGINT | No | | | ✔ | Product / Item |
| RequestedQuantity | DECIMAL(18,4) | No | 0 | | | Requested Quantity |
| ApprovedQuantity | DECIMAL(18,4) | No | 0 | | | Approved Quantity |
| ExpectedDeliveryDate | DATE | Yes | NULL | | | Delivery Date |
| EstimatedUnitCost | DECIMAL(18,2) | Yes | NULL | | | Estimated Cost |
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

PK_PurchaseRequisitionLine

## Foreign Keys

- PurchaseRequisitionId → PurchaseRequisition
- ItemId → Item

## Check Constraints

- RequestedQuantity > 0
- ApprovedQuantity >= 0

---

# Indexes

## Clustered

PK_PurchaseRequisitionLine

## Non Clustered

IX_Requisition

IX_Item

IX_Status

---

# Relationships

PurchaseRequisition (1) → PurchaseRequisitionLine (Many)

Item (1) → PurchaseRequisitionLine (Many)

---

# Business Rules

- Requested Quantity must be greater than zero.
- Approved Quantity cannot exceed requested quantity.
- Lines become read-only after Purchase Order generation.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseRequisitionLineAdded
- PurchaseRequisitionLineApproved
- PurchaseRequisitionLineCancelled

---

# Developer Notes

- Supports partial approval.
- Supports consolidation into Purchase Orders.

---

# ====================================================
# 003 RequestForQuotation (RFQ)
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** RequestForQuotation

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

RequestForQuotation (RFQ) is issued to one or more Vendors to obtain pricing, delivery commitments and commercial terms before placing a Purchase Order.

RFQs support competitive bidding and vendor evaluation.

Approved quotations may be converted into Purchase Orders.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- PurchaseRequisition
- NumberSeries

Referenced By

- RequestForQuotationLine
- PurchaseOrder

...

# ====================================================
# 003 RequestForQuotation (RFQ)
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** RequestForQuotation

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

RequestForQuotation (RFQ) is issued to one or more Vendors to obtain pricing, delivery commitments and commercial terms before placing a Purchase Order.

An RFQ allows multiple vendors to submit quotations for the same requisition, enabling procurement teams to compare pricing, delivery schedules, warranty terms and vendor performance before making a purchasing decision.

Approved quotations are converted into Purchase Orders.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- PurchaseRequisition
- NumberSeries

Referenced By

- RequestForQuotationLine
- PurchaseOrder

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RequestForQuotationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| VendorId | BIGINT | No | | | ✔ | Vendor |
| PurchaseRequisitionId | BIGINT | No | | | ✔ | Purchase Requisition |
| RFQNo | NVARCHAR(30) | No | Number Series | | | RFQ Number |
| RFQDate | DATE | No | GETDATE() | | | RFQ Date |
| ValidUntil | DATE | No | | | | Quote Expiry |
| ExpectedDeliveryDate | DATE | Yes | NULL | | | Expected Delivery |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| TotalAmount | DECIMAL(18,2) | No | 0 | | | Estimated Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsApproved | BIT | No | 0 | | | Selected Vendor |
| StatusId | SMALLINT | No | 1 | | | Draft / Sent / Closed |
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

PK_RequestForQuotation

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- VendorId → Vendor
- PurchaseRequisitionId → PurchaseRequisition
- CurrencyId → Currency

## Unique Keys

- UQ_RFQ_No

## Check Constraints

- ValidUntil >= RFQDate
- TotalAmount >= 0

---

# Indexes

## Clustered

PK_RequestForQuotation

## Non Clustered

IX_RFQNo

IX_Vendor

IX_Requisition

IX_RFQDate

IX_Status

---

# Relationships

PurchaseRequisition (1) → RequestForQuotation (Many)

Vendor (1) → RequestForQuotation (Many)

RequestForQuotation (1) → RequestForQuotationLine (Many)

RequestForQuotation (1) → PurchaseOrder (Optional)

---

# Business Rules

- RFQ Number generated using Number Series.
- Multiple RFQs may exist for one Requisition.
- Only one RFQ can be selected for Purchase Order creation.
- Expired RFQs cannot be approved.
- Closed RFQs become read-only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RFQCreated
- RFQSent
- RFQApproved
- RFQRejected
- RFQClosed

---

# Developer Notes

- Supports competitive bidding.
- Supports vendor comparison.
- Supports future e-procurement integration.

---

# ====================================================
# 004 RequestForQuotationLine
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** RequestForQuotationLine

**Classification:** Transaction Detail

**Aggregate Root:** RequestForQuotation

---

# Purpose

Stores every Item requested from the Vendor.

Each line contains vendor pricing, delivery commitments, taxes and commercial information submitted by the vendor.

---

# Dependencies

Depends On

- RequestForQuotation
- Item

Referenced By

- PurchaseOrderLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RequestForQuotationLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| RequestForQuotationId | BIGINT | No | | | ✔ | RFQ |
| ItemId | BIGINT | No | | | ✔ | Item |
| Quantity | DECIMAL(18,4) | No | 0 | | | Requested Quantity |
| UnitPrice | DECIMAL(18,2) | No | 0 | | | Vendor Price |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total |
| DeliveryDays | INT | Yes | NULL | | | Delivery Lead Time |
| WarrantyMonths | INT | Yes | NULL | | | Warranty |
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

PK_RequestForQuotationLine

## Foreign Keys

- RequestForQuotationId → RequestForQuotation
- ItemId → Item

## Check Constraints

- Quantity > 0
- UnitPrice >= 0
- LineTotal >= 0

---

# Indexes

## Clustered

PK_RequestForQuotationLine

## Non Clustered

IX_RFQ

IX_Item

IX_Status

---

# Relationships

RequestForQuotation (1) → RequestForQuotationLine (Many)

Item (1) → RequestForQuotationLine (Many)

---

# Business Rules

- Item appears only once per RFQ.
- Vendor pricing stored as historical snapshot.
- Line Total calculated automatically.
- Read-only after RFQ approval.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RFQLineAdded
- RFQLineUpdated
- RFQLineApproved

---

# Developer Notes

- Preserves vendor pricing history.
- Used during vendor comparison.

---

# ====================================================
# 005 PurchaseOrder
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

PurchaseOrder represents the official commercial contract issued to a Vendor authorizing the purchase of goods or services.

Once approved and posted, the Purchase Order becomes legally binding and may be fulfilled through one or more Goods Receipt Notes (GRNs).

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- RequestForQuotation
- NumberSeries

Referenced By

- PurchaseOrderLine
- GoodsReceipt
- PurchaseInvoice

...

# ====================================================
# 005 PurchaseOrder
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

PurchaseOrder represents the official purchasing contract issued to a Vendor.

It authorizes the Vendor to supply products, spare parts, consumables or Assets under agreed commercial terms.

Once posted, the Purchase Order becomes legally binding and may be fulfilled through one or multiple Goods Receipt Notes (GRNs).

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- RequestForQuotation
- NumberSeries
- Currency

Referenced By

- PurchaseOrderLine
- GoodsReceipt
- PurchaseInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseOrderId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| VendorId | BIGINT | No | | | ✔ | Vendor |
| RequestForQuotationId | BIGINT | Yes | NULL | | ✔ | Source RFQ |
| PurchaseOrderNo | NVARCHAR(30) | No | Number Series | | | Purchase Order Number |
| PurchaseOrderDate | DATE | No | GETDATE() | | | Order Date |
| ExpectedDeliveryDate | DATE | Yes | NULL | | | Delivery Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Closed / Cancelled |
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

PK_PurchaseOrder

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- VendorId → Vendor
- RequestForQuotationId → RequestForQuotation
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_PurchaseOrder_No

## Check Constraints

- ExpectedDeliveryDate >= PurchaseOrderDate
- NetAmount >= 0

---

# Indexes

## Clustered

PK_PurchaseOrder

## Non Clustered

IX_PurchaseOrderNo

IX_Vendor

IX_PurchaseOrderDate

IX_Status

IX_RFQ

---

# Relationships

Vendor (1) → PurchaseOrder (Many)

RequestForQuotation (1) → PurchaseOrder (Optional One)

PurchaseOrder (1) → PurchaseOrderLine (Many)

PurchaseOrder (1) → GoodsReceipt (Many)

---

# Business Rules

- Purchase Order Number generated using Number Series.
- Posting locks commercial terms.
- Purchase Order may generate multiple GRNs.
- Closed Purchase Orders cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseOrderCreated
- PurchaseOrderPosted
- PurchaseOrderClosed
- PurchaseOrderCancelled

---

# Developer Notes

- Central procurement document.
- Integrates with Inventory, Vendor and Accounting.

---

# ====================================================
# 006 PurchaseOrderLine
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseOrderLine

**Classification:** Transaction Detail

**Aggregate Root:** PurchaseOrder

---

# Purpose

PurchaseOrderLine stores the Items ordered from the Vendor.

Each line maintains purchasing quantity, pricing, taxes, discounts and receipt progress.

Partial deliveries are supported through multiple Goods Receipt Notes.

---

# Dependencies

Depends On

- PurchaseOrder
- Item

Referenced By

- GoodsReceiptLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseOrderLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
| ItemId | BIGINT | No | | | ✔ | Purchased Item |
| OrderedQuantity | DECIMAL(18,4) | No | 0 | | | Ordered Qty |
| ReceivedQuantity | DECIMAL(18,4) | No | 0 | | | Received Qty |
| UnitPrice | DECIMAL(18,2) | No | 0 | | | Purchase Price |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total |
| ExpectedDeliveryDate | DATE | Yes | NULL | | | Delivery Date |
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

PK_PurchaseOrderLine

## Foreign Keys

- PurchaseOrderId → PurchaseOrder
- ItemId → Item

## Check Constraints

- OrderedQuantity > 0
- ReceivedQuantity >= 0
- UnitPrice >= 0

---

# Indexes

## Clustered

PK_PurchaseOrderLine

## Non Clustered

IX_PurchaseOrder

IX_Item

IX_Status

---

# Relationships

PurchaseOrder (1) → PurchaseOrderLine (Many)

Item (1) → PurchaseOrderLine (Many)

GoodsReceiptLine (Many) → PurchaseOrderLine (One)

---

# Business Rules

- Received Quantity cannot exceed Ordered Quantity.
- Partial receipt supported.
- Pricing becomes immutable after posting.
- Remaining quantity calculated automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseOrderLineCreated
- PurchaseOrderLineReceived
- PurchaseOrderLineClosed

---

# Developer Notes

- Supports partial deliveries.
- Maintains receipt progress.

---

# ====================================================
# 007 GoodsReceipt
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** GoodsReceipt

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

GoodsReceipt (GRN) records the physical receipt of purchased goods from a Vendor.

Posting a GRN updates Inventory quantities, Warehouse stock and Inventory Ledger while also updating Purchase Order fulfillment status.

A single Purchase Order may generate multiple GRNs until fully received.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- PurchaseOrder
- Warehouse
- NumberSeries

Referenced By

- GoodsReceiptLine
- PurchaseInvoice
- InventoryTransaction

...

# ====================================================
# 007 GoodsReceipt
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** GoodsReceipt

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

GoodsReceipt (GRN) records the physical receipt of purchased goods from a Vendor.

Posting a Goods Receipt updates Warehouse inventory, Inventory Ledger, Item Cost, Purchase Order balances and makes the received Items available for use.

A Purchase Order may generate multiple Goods Receipts until the ordered quantity has been completely received.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- PurchaseOrder
- Warehouse
- NumberSeries

Referenced By

- GoodsReceiptLine
- PurchaseInvoice
- InventoryTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| GoodsReceiptId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| VendorId | BIGINT | No | | | ✔ | Vendor |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
| WarehouseId | BIGINT | No | | | ✔ | Receiving Warehouse |
| GoodsReceiptNo | NVARCHAR(30) | No | Number Series | | | GRN Number |
| ReceiptDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Receipt Date |
| VendorDeliveryNo | NVARCHAR(50) | Yes | NULL | | | Delivery Challan |
| VehicleNo | NVARCHAR(30) | Yes | NULL | | | Vehicle Number |
| ReceivedBy | BIGINT | No | | | ✔ | Employee |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Cancelled |
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

PK_GoodsReceipt

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- VendorId → Vendor
- PurchaseOrderId → PurchaseOrder
- WarehouseId → Warehouse
- ReceivedBy → Employee
- PostedBy → Employee

## Unique Keys

- UQ_GoodsReceipt_No

## Check Constraints

- NetAmount >= 0

---

# Indexes

## Clustered

PK_GoodsReceipt

## Non Clustered

IX_GoodsReceiptNo

IX_PurchaseOrder

IX_Vendor

IX_Warehouse

IX_ReceiptDate

IX_Status

---

# Relationships

PurchaseOrder (1) → GoodsReceipt (Many)

Vendor (1) → GoodsReceipt (Many)

Warehouse (1) → GoodsReceipt (Many)

GoodsReceipt (1) → GoodsReceiptLine (Many)

---

# Business Rules

- GRN Number generated using Number Series.
- Posting updates Inventory.
- Posting updates Warehouse Stock.
- Posting updates Item Cost.
- Posting creates Inventory Transactions.
- Multiple GRNs allowed for one Purchase Order.
- Posted GRNs cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- GoodsReceived
- InventoryIncreased
- PurchaseOrderUpdated
- GoodsReceiptPosted

---

# Developer Notes

- Main document for inventory receiving.
- Integrates directly with Inventory Domain.
- Updates Item availability immediately.

---

# ====================================================
# 008 GoodsReceiptLine
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** GoodsReceiptLine

**Classification:** Transaction Detail

**Aggregate Root:** GoodsReceipt

---

# Purpose

Stores every Item physically received from the Vendor.

Each line records received quantity, accepted quantity, rejected quantity, warehouse location and costing information.

Supports partial acceptance and quality inspection.

---

# Dependencies

Depends On

- GoodsReceipt
- PurchaseOrderLine
- Item
- WarehouseBin

Referenced By

- InventoryTransaction
- PurchaseReturn

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| GoodsReceiptLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| GoodsReceiptId | BIGINT | No | | | ✔ | Goods Receipt |
| PurchaseOrderLineId | BIGINT | No | | | ✔ | Purchase Order Line |
| ItemId | BIGINT | No | | | ✔ | Item |
| WarehouseBinId | BIGINT | Yes | NULL | | ✔ | Bin Location |
| OrderedQuantity | DECIMAL(18,4) | No | 0 | | | Ordered Qty |
| ReceivedQuantity | DECIMAL(18,4) | No | 0 | | | Received Qty |
| AcceptedQuantity | DECIMAL(18,4) | No | 0 | | | Accepted Qty |
| RejectedQuantity | DECIMAL(18,4) | No | 0 | | | Rejected Qty |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Purchase Cost |
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

PK_GoodsReceiptLine

## Foreign Keys

- GoodsReceiptId → GoodsReceipt
- PurchaseOrderLineId → PurchaseOrderLine
- ItemId → Item
- WarehouseBinId → WarehouseBin

## Check Constraints

- ReceivedQuantity >= 0
- AcceptedQuantity >= 0
- RejectedQuantity >= 0
- AcceptedQuantity + RejectedQuantity = ReceivedQuantity

---

# Indexes

## Clustered

PK_GoodsReceiptLine

## Non Clustered

IX_GoodsReceipt

IX_Item

IX_PO_Line

IX_WarehouseBin

IX_Status

---

# Relationships

GoodsReceipt (1) → GoodsReceiptLine (Many)

PurchaseOrderLine (1) → GoodsReceiptLine (Many)

Item (1) → GoodsReceiptLine (Many)

---

# Business Rules

- Accepted Quantity updates Inventory.
- Rejected Quantity available for Vendor Return.
- Partial receipt supported.
- Warehouse Bin optional.
- Unit Cost updates Inventory valuation.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- GoodsReceiptLineCreated
- InventoryStockReceived
- ItemCostUpdated

---

# Developer Notes

- Supports quality inspection.
- Supports warehouse put-away.
- Supports inventory valuation updates.

---

# ====================================================
# 009 PurchaseReturn
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseReturn

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

PurchaseReturn records goods returned to a Vendor due to damage, quality issues, incorrect deliveries or warranty claims.

Posting a Purchase Return decreases Inventory quantities, updates Warehouse stock and creates Vendor credit transactions.

---

# Dependencies

Depends On

- Vendor
- GoodsReceipt
- NumberSeries

Referenced By

- PurchaseInvoice
- Accounting
- InventoryTransaction

...

# ====================================================
# 009 PurchaseReturn
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseReturn

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

PurchaseReturn records Items returned to a Vendor due to damage, incorrect supply, quality issues, warranty claims or over-delivery.

Posting a Purchase Return decreases Inventory quantities, updates Warehouse stock, creates Inventory Out transactions and generates Vendor credit information for Accounts Payable.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- GoodsReceipt
- Warehouse
- NumberSeries

Referenced By

- PurchaseInvoice
- Accounting
- InventoryTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseReturnId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| VendorId | BIGINT | No | | | ✔ | Vendor |
| GoodsReceiptId | BIGINT | No | | | ✔ | Goods Receipt |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| PurchaseReturnNo | NVARCHAR(30) | No | Number Series | | | Return Number |
| ReturnDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Return Date |
| ReturnReason | SMALLINT | No | | | | Damaged / Wrong Item / Warranty / Excess |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax Amount |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Cancelled |
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

PK_PurchaseReturn

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- VendorId → Vendor
- GoodsReceiptId → GoodsReceipt
- WarehouseId → Warehouse
- PostedBy → Employee

## Unique Keys

- UQ_PurchaseReturn_No

## Check Constraints

- NetAmount >= 0

---

# Indexes

## Clustered

PK_PurchaseReturn

## Non Clustered

IX_PurchaseReturnNo

IX_Vendor

IX_GoodsReceipt

IX_ReturnDate

IX_Status

---

# Relationships

Vendor (1) → PurchaseReturn (Many)

GoodsReceipt (1) → PurchaseReturn (Many)

PurchaseReturn (1) → InventoryTransaction (Many)

---

# Business Rules

- Return Number generated using Number Series.
- Returned quantity cannot exceed received quantity.
- Posting decreases Inventory.
- Posting creates Vendor Credit.
- Posting creates Inventory Issue transaction.
- Posted documents cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseReturnCreated
- PurchaseReturnPosted
- InventoryReturnedToVendor
- VendorCreditGenerated

---

# Developer Notes

- Updates Inventory immediately.
- Integrates with Vendor Ledger.
- Supports warranty returns.

---

# ====================================================
# 010 PurchaseInvoice
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseInvoice

**Classification:** Transaction Header

**Aggregate Root:** PurchaseOrder

---

# Purpose

PurchaseInvoice records the Vendor's financial invoice for supplied goods and services.

It represents the Accounts Payable document used to settle vendor liabilities.

Posting a Purchase Invoice creates Journal Entries and Vendor Ledger transactions.

---

# Dependencies

Depends On

- Company
- Branch
- Vendor
- PurchaseOrder
- GoodsReceipt
- Currency
- NumberSeries

Referenced By

- VendorPayment
- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseInvoiceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| VendorId | BIGINT | No | | | ✔ | Vendor |
| PurchaseOrderId | BIGINT | Yes | NULL | | ✔ | Purchase Order |
| GoodsReceiptId | BIGINT | Yes | NULL | | ✔ | Goods Receipt |
| PurchaseInvoiceNo | NVARCHAR(30) | No | Number Series | | | Invoice Number |
| VendorInvoiceNo | NVARCHAR(50) | No | | | | Vendor Invoice |
| InvoiceDate | DATE | No | GETDATE() | | | Invoice Date |
| DueDate | DATE | Yes | NULL | | | Due Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| OutstandingAmount | DECIMAL(18,2) | No | 0 | | | Outstanding Balance |
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

PK_PurchaseInvoice

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- VendorId → Vendor
- PurchaseOrderId → PurchaseOrder
- GoodsReceiptId → GoodsReceipt
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_PurchaseInvoice_No
- UQ_VendorInvoice_No (VendorId, VendorInvoiceNo)

## Check Constraints

- DueDate >= InvoiceDate
- OutstandingAmount >= 0
- NetAmount >= 0

---

# Indexes

## Clustered

PK_PurchaseInvoice

## Non Clustered

IX_PurchaseInvoiceNo

IX_VendorInvoiceNo

IX_Vendor

IX_InvoiceDate

IX_Status

---

# Relationships

Vendor (1) → PurchaseInvoice (Many)

PurchaseOrder (1) → PurchaseInvoice (Many)

GoodsReceipt (1) → PurchaseInvoice (Many)

---

# Business Rules

- Purchase Invoice Number generated using Number Series.
- Vendor Invoice Number must be unique per Vendor.
- Posting creates Accounts Payable entries.
- Outstanding Amount updated after payments.
- Posted invoices cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseInvoiceCreated
- PurchaseInvoicePosted
- AccountsPayableCreated
- VendorLiabilityRecorded

---

# Developer Notes

- Integrates directly with Accounting.
- Supports partial payments.
- Supports multi-currency purchasing.

---

# ====================================================
# 011 PurchaseAttachment
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Purchase Orders with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Vendor Quotations
- Signed Purchase Orders
- Vendor Invoices
- Delivery Challans
- Quality Inspection Reports
- Import Documents

---

# Dependencies

Depends On

- PurchaseOrder
- Attachment

Referenced By

- Purchase Detail Screen

...

# ====================================================
# 011 PurchaseAttachment
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Purchase Orders with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Vendor Quotations
- Signed Purchase Orders
- Vendor Invoices
- Delivery Challans
- Quality Inspection Reports
- Import Documents
- Customs Documents
- Packing Lists

---

# Dependencies

Depends On

- PurchaseOrder
- Attachment

Referenced By

- Purchase Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
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

PK_PurchaseAttachment

## Foreign Keys

- PurchaseOrderId → PurchaseOrder
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_PurchaseAttachment

## Non Clustered

IX_PurchaseOrder

IX_Attachment

---

# Relationships

PurchaseOrder (1) → PurchaseAttachment (Many)

Attachment (1) → PurchaseAttachment (Many)

---

# Business Rules

- Attachments stored in Shared Kernel.
- Unlimited attachments supported.
- Business ownership belongs to Purchase Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseAttachmentAdded
- PurchaseAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.

---

# ====================================================
# 012 PurchaseNote
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Purchase Orders with reusable Note records maintained within the Shared Kernel.

Notes capture procurement discussions, negotiation comments, delivery instructions, approval remarks and internal purchasing communications.

---

# Dependencies

Depends On

- PurchaseOrder
- Note

Referenced By

- Purchase Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
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

PK_PurchaseNote

## Foreign Keys

- PurchaseOrderId → PurchaseOrder
- NoteId → Note

---

# Indexes

## Clustered

PK_PurchaseNote

## Non Clustered

IX_PurchaseOrder

IX_Note

---

# Relationships

PurchaseOrder (1) → PurchaseNote (Many)

Note (1) → PurchaseNote (Many)

---

# Business Rules

- Notes remain reusable within Shared Kernel.
- Business ownership belongs to Purchase Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseNoteAdded
- PurchaseNoteUpdated
- PurchaseNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Maintains procurement communication history.

---

# ====================================================
# 013 PurchaseActivity
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Purchase Orders with reusable Activity records maintained within the Shared Kernel.

Activities record every procurement event occurring throughout the purchasing lifecycle.

Examples include:

- Purchase Requisition Created
- RFQ Sent
- Vendor Selected
- Purchase Order Approved
- Goods Received
- Purchase Return Processed
- Purchase Invoice Posted
- Vendor Payment Recorded

---

# Dependencies

Depends On

- PurchaseOrder
- Activity

Referenced By

- Purchase Dashboard
- Workflow Engine
- Activity Widget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
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

PK_PurchaseActivity

## Foreign Keys

- PurchaseOrderId → PurchaseOrder
- ActivityId → Activity

## Unique Keys

- UQ_Purchase_Activity

---

# Indexes

## Clustered

PK_PurchaseActivity

## Non Clustered

IX_PurchaseOrder

IX_Activity

IX_Status

---

# Relationships

PurchaseOrder (1) → PurchaseActivity (Many)

Activity (1) → PurchaseActivity (Many)

---

# Business Rules

- Activities remain reusable within Shared Kernel.
- Activity history is append-only.
- Business ownership belongs to Purchase Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseActivityCreated
- PurchaseActivityUpdated
- PurchaseActivityDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports Workflow Engine.
- Maintains procurement audit history.

---

# ====================================================
# 014 PurchaseTimeline
# ====================================================

# Table Classification

**Domain:** Purchase Domain

**Table Name:** PurchaseTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Purchase Orders with reusable Timeline records maintained within the Shared Kernel.

Timeline records every major procurement event in chronological order.

Examples include:

- Requisition Created
- RFQ Sent
- Vendor Selected
- Purchase Order Posted
- Goods Receipt Completed
- Purchase Return Processed
- Invoice Posted
- Vendor Payment Completed

---

# Dependencies

Depends On

- PurchaseOrder
- Timeline

Referenced By

- Purchase Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PurchaseTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PurchaseOrderId | BIGINT | No | | | ✔ | Purchase Order |
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

PK_PurchaseTimeline

## Foreign Keys

- PurchaseOrderId → PurchaseOrder
- TimelineId → Timeline

## Unique Keys

- UQ_Purchase_Timeline

---

# Indexes

## Clustered

PK_PurchaseTimeline

## Non Clustered

IX_PurchaseOrder

IX_Timeline

IX_Status

---

# Relationships

PurchaseOrder (1) → PurchaseTimeline (Many)

Timeline (1) → PurchaseTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline history is append-only.
- Business ownership belongs to Purchase Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PurchaseTimelineCreated
- PurchaseTimelineUpdated
- PurchaseTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for timeline rendering.
- Maintains complete procurement lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Purchase Domain manages the complete procurement lifecycle from internal requisition through vendor selection, purchase ordering, goods receipt, purchase returns and vendor invoicing. It ensures procurement activities integrate seamlessly with Inventory, Warehouse and Accounting while maintaining complete financial and operational traceability.

---

## Aggregate Roots

- PurchaseRequisition
- RequestForQuotation
- PurchaseOrder
- GoodsReceipt
- PurchaseReturn

---

## Supporting Entities

- PurchaseRequisitionLine
- RequestForQuotationLine
- PurchaseOrderLine
- GoodsReceiptLine
- PurchaseInvoice

---

## Bridge Entities

- PurchaseAttachment
- PurchaseNote
- PurchaseActivity
- PurchaseTimeline

---

## Major Business Capabilities

- Purchase Requisitions
- Vendor RFQs
- Vendor Comparison
- Purchase Orders
- Partial Goods Receipts
- Purchase Returns
- Purchase Invoicing
- Vendor Liability Management
- Inventory Updates
- Procurement Cost Analysis
- Complete Audit Trail
- Shared Kernel Integration

---

## Published Domain Events

The Purchase Domain publishes events including:

- PurchaseRequisitionApproved
- RFQSent
- PurchaseOrderPosted
- GoodsReceived
- InventoryIncreased
- PurchaseReturnPosted
- PurchaseInvoicePosted
- AccountsPayableCreated

These events integrate with:

- Vendor Domain
- Product Domain
- Warehouse Domain
- Inventory Domain
- Asset Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Purchase Domain integrates directly with:

- Foundation
- Shared Kernel
- Vendor Domain
- Product Domain
- Warehouse Domain
- Inventory Domain
- Asset Domain
- Accounting Domain
- Administration

---

# Purchase Domain Status

**Status:** ✅ Complete

**Total Tables:** 14

1. PurchaseRequisition
2. PurchaseRequisitionLine
3. RequestForQuotation
4. RequestForQuotationLine
5. PurchaseOrder
6. PurchaseOrderLine
7. GoodsReceipt
8. GoodsReceiptLine
9. PurchaseReturn
10. PurchaseInvoice
11. PurchaseAttachment
12. PurchaseNote
13. PurchaseActivity
14. PurchaseTimeline

---

