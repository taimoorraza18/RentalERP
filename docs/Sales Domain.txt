# RentalERP v1.0

# SalesDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Sales

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Sales Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. SalesQuotation

6. SalesQuotationLine

7. SalesOrder

---

# Domain Overview

The Sales Domain manages the complete product sales lifecycle from customer quotation through sales order processing, delivery, invoicing, customer returns and payment collection.

Unlike the Rental Domain, where Assets are temporarily issued and later returned, the Sales Domain permanently transfers ownership of products to customers while updating inventory, customer receivables and revenue.

The Sales Domain integrates directly with Customer, Product, Warehouse, Inventory and Accounting domains.

---

# Business Objectives

The Sales Domain provides:

- Sales Quotations
- Sales Orders
- Product Reservation
- Delivery Orders
- Sales Invoices
- Customer Returns
- Customer Payments
- Credit Limit Validation
- Inventory Deduction
- Revenue Recognition
- Customer Ledger
- Sales Analysis
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Roots

- SalesQuotation
- SalesOrder
- DeliveryOrder

## Supporting Entities

- SalesQuotationLine
- SalesOrderLine
- DeliveryOrderLine
- SalesReturn
- SalesInvoice
- CustomerPayment

## Bridge Entities

- SalesAttachment
- SalesNote
- SalesActivity
- SalesTimeline

---

# Implementation Order

001 SalesQuotation

002 SalesQuotationLine

003 SalesOrder

004 SalesOrderLine

005 DeliveryOrder

006 DeliveryOrderLine

007 SalesReturn

008 SalesInvoice

009 CustomerPayment

010 SalesAttachment

011 SalesNote

012 SalesActivity

013 SalesTimeline

---

# ====================================================
# 001 SalesQuotation
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesQuotation

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

SalesQuotation represents a commercial offer prepared for a customer before confirming a sale.

It contains pricing, discounts, taxes, payment terms, validity period and proposed products.

Sales Quotations do not affect inventory or accounting.

Approved quotations may be converted into Sales Orders.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- Currency
- NumberSeries

Referenced By

- SalesQuotationLine
- SalesOrder

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesQuotationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| SalesQuotationNo | NVARCHAR(30) | No | Number Series | | | Quotation Number |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| QuotationDate | DATE | No | GETDATE() | | | Quotation Date |
| ValidUntil | DATE | No | | | | Expiry Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Draft / Approved / Cancelled / Expired |
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

PK_SalesQuotation

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- CurrencyId → Currency

## Unique Keys

- UQ_SalesQuotation_No

## Check Constraints

- ValidUntil >= QuotationDate
- NetAmount >= 0

---

# Indexes

## Clustered

PK_SalesQuotation

## Non Clustered

IX_SalesQuotationNo

IX_Customer

IX_QuotationDate

IX_Status

---

# Relationships

Customer (1) → SalesQuotation (Many)

SalesQuotation (1) → SalesQuotationLine (Many)

SalesQuotation (1) → SalesOrder (Optional One)

---

# Business Rules

- Quotation Number generated using Number Series.
- Expired quotations cannot be converted.
- Inventory is not reserved.
- No accounting impact.
- Approved quotations become read-only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesQuotationCreated
- SalesQuotationApproved
- SalesQuotationRejected
- SalesQuotationExpired
- SalesQuotationConverted

---

# Developer Notes

- Entry point of sales workflow.
- Supports future quotation revisions.
- Does not affect Inventory or Accounting.

---

# ====================================================
# 002 SalesQuotationLine
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesQuotationLine

**Classification:** Transaction Detail

**Aggregate Root:** SalesQuotation

---

# Purpose

SalesQuotationLine stores the individual products quoted to the customer.

Each line records quantity, selling price, discounts, taxes and commercial information.

Pricing is preserved for future comparison when converted into a Sales Order.

---

# Dependencies

Depends On

- SalesQuotation
- Item
- TaxProfile

Referenced By

- SalesOrderLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesQuotationLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesQuotationId | BIGINT | No | | | ✔ | Sales Quotation |
| ItemId | BIGINT | No | | | ✔ | Product |
| Quantity | DECIMAL(18,4) | No | 0 | | | Quantity |
| UnitPrice | DECIMAL(18,2) | No | 0 | | | Selling Price |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxProfileId | BIGINT | Yes | NULL | | ✔ | Tax Profile |
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

PK_SalesQuotationLine

## Foreign Keys

- SalesQuotationId → SalesQuotation
- ItemId → Item
- TaxProfileId → TaxProfile

## Check Constraints

- Quantity > 0
- UnitPrice >= 0
- LineTotal >= 0

---

# Indexes

## Clustered

PK_SalesQuotationLine

## Non Clustered

IX_SalesQuotation

IX_Item

IX_Status

---

# Relationships

SalesQuotation (1) → SalesQuotationLine (Many)

Item (1) → SalesQuotationLine (Many)

---

# Business Rules

- One Item appears only once per quotation.
- Pricing snapshot preserved.
- No Inventory reservation.
- Read-only after approval.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesQuotationLineAdded
- SalesQuotationLineUpdated
- SalesQuotationLineRemoved

---

# Developer Notes

- Supports quotation comparison.
- Used during Sales Order creation.

---

# ====================================================
# 003 SalesOrder
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

SalesOrder represents the customer's confirmed order to purchase products.

Once approved, the Sales Order reserves inventory (optional based on business configuration), validates customer credit limits and becomes the source document for Delivery Orders and Sales Invoices.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesQuotation
- NumberSeries

Referenced By

- SalesOrderLine
- DeliveryOrder
- SalesInvoice

...

# ====================================================
# 003 SalesOrder
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

SalesOrder represents the customer's confirmed purchase order.

It is the official sales contract between the company and the customer.

After approval, the Sales Order validates customer credit limits, optionally reserves inventory (depending on company configuration), and becomes the source document for Delivery Orders, Sales Invoices and Customer Receivables.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesQuotation
- NumberSeries
- Currency

Referenced By

- SalesOrderLine
- DeliveryOrder
- SalesInvoice

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesOrderId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| SalesQuotationId | BIGINT | Yes | NULL | | ✔ | Source Quotation |
| SalesOrderNo | NVARCHAR(30) | No | Number Series | | | Sales Order Number |
| OrderDate | DATE | No | GETDATE() | | | Order Date |
| RequiredDeliveryDate | DATE | Yes | NULL | | | Delivery Date |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| IsCreditApproved | BIT | No | 0 | | | Credit Approved |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Delivered / Closed / Cancelled |
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

PK_SalesOrder

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- SalesQuotationId → SalesQuotation
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_SalesOrder_No

## Check Constraints

- RequiredDeliveryDate >= OrderDate
- NetAmount >= 0

---

# Indexes

## Clustered

PK_SalesOrder

## Non Clustered

IX_SalesOrderNo

IX_Customer

IX_OrderDate

IX_Status

IX_SalesQuotation

---

# Relationships

Customer (1) → SalesOrder (Many)

SalesQuotation (1) → SalesOrder (Optional One)

SalesOrder (1) → SalesOrderLine (Many)

SalesOrder (1) → DeliveryOrder (Many)

SalesOrder (1) → SalesInvoice (Many)

---

# Business Rules

- Sales Order Number generated using Number Series.
- Credit limit validation required before posting.
- Inventory reservation optional.
- Posted Sales Orders become read-only.
- Multiple deliveries allowed.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesOrderCreated
- SalesOrderApproved
- SalesOrderPosted
- SalesOrderCancelled

---

# Developer Notes

- Central document of Sales Domain.
- Integrates with Customer, Inventory and Accounting.

---

# ====================================================
# 004 SalesOrderLine
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesOrderLine

**Classification:** Transaction Detail

**Aggregate Root:** SalesOrder

---

# Purpose

Stores every Item sold under a Sales Order.

Each line records ordered quantity, delivered quantity, pricing, taxes and delivery progress.

Supports partial deliveries and backorders.

---

# Dependencies

Depends On

- SalesOrder
- Item

Referenced By

- DeliveryOrderLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesOrderLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
| ItemId | BIGINT | No | | | ✔ | Product |
| OrderedQuantity | DECIMAL(18,4) | No | 0 | | | Ordered Qty |
| DeliveredQuantity | DECIMAL(18,4) | No | 0 | | | Delivered Qty |
| UnitPrice | DECIMAL(18,2) | No | 0 | | | Selling Price |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Total |
| Remarks | NVARCHAR(500) | Yes | NULL | | | Remarks |
| StatusId | SMALLINT | No | 1 | | | Status |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATTIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Concurrency |

---

# Constraints

## Primary Key

PK_SalesOrderLine

## Foreign Keys

- SalesOrderId → SalesOrder
- ItemId → Item

## Check Constraints

- OrderedQuantity > 0
- DeliveredQuantity >= 0
- DeliveredQuantity <= OrderedQuantity

---

# Indexes

## Clustered

PK_SalesOrderLine

## Non Clustered

IX_SalesOrder

IX_Item

IX_Status

---

# Relationships

SalesOrder (1) → SalesOrderLine (Many)

Item (1) → SalesOrderLine (Many)

DeliveryOrderLine (Many) → SalesOrderLine (One)

---

# Business Rules

- Partial delivery supported.
- Remaining Quantity calculated automatically.
- Selling Price locked after posting.
- Inventory reserved (optional).
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesOrderLineCreated
- SalesOrderLineReserved
- SalesOrderLineDelivered

---

# Developer Notes

- Supports backorders.
- Supports partial shipments.

---

# ====================================================
# 005 DeliveryOrder
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** DeliveryOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

DeliveryOrder records the physical dispatch of goods to the customer.

Posting a Delivery Order decreases Warehouse inventory, updates Inventory Ledger, records shipment information and transfers ownership of products to the customer.

A Sales Order may generate multiple Delivery Orders until all products have been delivered.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesOrder
- Warehouse
- NumberSeries

Referenced By

- DeliveryOrderLine
- SalesInvoice
- InventoryTransaction

...

# ====================================================
# 005 DeliveryOrder
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** DeliveryOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

DeliveryOrder records the physical dispatch of goods to the customer.

Posting a Delivery Order decreases Warehouse inventory, creates Inventory Issue transactions, updates Sales Order fulfillment status and records shipment information.

A single Sales Order may generate multiple Delivery Orders until all ordered products have been delivered.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesOrder
- Warehouse
- NumberSeries

Referenced By

- DeliveryOrderLine
- SalesInvoice
- InventoryTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DeliveryOrderId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
| WarehouseId | BIGINT | No | | | ✔ | Dispatch Warehouse |
| DeliveryOrderNo | NVARCHAR(30) | No | Number Series | | | Delivery Number |
| DeliveryDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Delivery Date |
| DeliveryAddress | NVARCHAR(500) | Yes | NULL | | | Delivery Address |
| VehicleNo | NVARCHAR(30) | Yes | NULL | | | Vehicle Number |
| DriverName | NVARCHAR(200) | Yes | NULL | | | Driver Name |
| DeliveredBy | BIGINT | No | | | ✔ | Employee |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| TaxAmount | DECIMAL(18,2) | No | 0 | | | Tax Amount |
| NetAmount | DECIMAL(18,2) | No | 0 | | | Net Amount |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Delivered / Cancelled |
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

PK_DeliveryOrder

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- SalesOrderId → SalesOrder
- WarehouseId → Warehouse
- DeliveredBy → Employee
- PostedBy → Employee

## Unique Keys

- UQ_DeliveryOrder_No

## Check Constraints

- NetAmount >= 0

---

# Indexes

## Clustered

PK_DeliveryOrder

## Non Clustered

IX_DeliveryOrderNo

IX_SalesOrder

IX_Customer

IX_Warehouse

IX_DeliveryDate

IX_Status

---

# Relationships

SalesOrder (1) → DeliveryOrder (Many)

Warehouse (1) → DeliveryOrder (Many)

Customer (1) → DeliveryOrder (Many)

DeliveryOrder (1) → DeliveryOrderLine (Many)

---

# Business Rules

- Delivery Number generated using Number Series.
- Multiple deliveries allowed.
- Posting decreases Inventory.
- Posting creates Inventory Issue transaction.
- Posting updates Sales Order balance.
- Posted deliveries cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DeliveryOrderCreated
- DeliveryOrderPosted
- InventoryIssued
- CustomerDeliveryCompleted

---

# Developer Notes

- Main shipping document.
- Integrates directly with Inventory Domain.
- Supports partial deliveries.

---

# ====================================================
# 006 DeliveryOrderLine
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** DeliveryOrderLine

**Classification:** Transaction Detail

**Aggregate Root:** DeliveryOrder

---

# Purpose

Stores every product delivered to the customer.

Each line records dispatched quantity, warehouse location, batch/serial information and shipment costing.

Supports partial deliveries and multiple dispatches.

---

# Dependencies

Depends On

- DeliveryOrder
- SalesOrderLine
- Item
- WarehouseBin

Referenced By

- InventoryTransaction
- SalesReturn

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DeliveryOrderLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DeliveryOrderId | BIGINT | No | | | ✔ | Delivery Order |
| SalesOrderLineId | BIGINT | No | | | ✔ | Sales Order Line |
| ItemId | BIGINT | No | | | ✔ | Product |
| WarehouseBinId | BIGINT | Yes | NULL | | ✔ | Bin Location |
| OrderedQuantity | DECIMAL(18,4) | No | 0 | | | Ordered Qty |
| DeliveredQuantity | DECIMAL(18,4) | No | 0 | | | Delivered Qty |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Inventory Cost |
| LineTotal | DECIMAL(18,2) | No | 0 | | | Line Cost |
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

PK_DeliveryOrderLine

## Foreign Keys

- DeliveryOrderId → DeliveryOrder
- SalesOrderLineId → SalesOrderLine
- ItemId → Item
- WarehouseBinId → WarehouseBin

## Check Constraints

- DeliveredQuantity > 0
- DeliveredQuantity <= OrderedQuantity

---

# Indexes

## Clustered

PK_DeliveryOrderLine

## Non Clustered

IX_DeliveryOrder

IX_SalesOrderLine

IX_Item

IX_WarehouseBin

IX_Status

---

# Relationships

DeliveryOrder (1) → DeliveryOrderLine (Many)

SalesOrderLine (1) → DeliveryOrderLine (Many)

Item (1) → DeliveryOrderLine (Many)

---

# Business Rules

- Delivered quantity cannot exceed remaining Sales Order quantity.
- Warehouse Bin optional.
- Inventory deducted on posting.
- Cost captured for COGS calculation.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DeliveryOrderLineCreated
- ProductDelivered
- InventoryReduced

---

# Developer Notes

- Supports warehouse picking.
- Supports batch and serial tracking.
- Used for inventory valuation.

---

# ====================================================
# 007 SalesReturn
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesReturn

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

SalesReturn records products returned by customers due to damage, warranty claims, incorrect shipments or customer dissatisfaction.

Posting a Sales Return increases Inventory, updates Warehouse stock, adjusts Customer Receivables and generates Customer Credit Notes where applicable.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- DeliveryOrder
- Warehouse
- NumberSeries

Referenced By

- SalesInvoice
- InventoryTransaction
- CustomerCreditNote

...

# ====================================================
# 007 SalesReturn
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesReturn

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

SalesReturn records products returned by customers due to damaged goods, warranty claims, incorrect shipments, over-delivery or customer dissatisfaction.

Posting a Sales Return increases Inventory, updates Warehouse stock, reverses Cost of Goods Sold (COGS), adjusts Customer Receivables and generates Customer Credit Notes where applicable.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- DeliveryOrder
- Warehouse
- NumberSeries

Referenced By

- SalesInvoice
- InventoryTransaction
- CustomerCreditNote

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesReturnId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| DeliveryOrderId | BIGINT | No | | | ✔ | Delivery Order |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| SalesReturnNo | NVARCHAR(30) | No | Number Series | | | Return Number |
| ReturnDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Return Date |
| ReturnReason | SMALLINT | No | | | | Damaged / Warranty / Wrong Item / Customer Return |
| GrossAmount | DECIMAL(18,2) | No | 0 | | | Gross Amount |
| DiscountAmount | DECIMAL(18,2) | No | 0 | | | Discount |
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

PK_SalesReturn

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- DeliveryOrderId → DeliveryOrder
- WarehouseId → Warehouse
- PostedBy → Employee

## Unique Keys

- UQ_SalesReturn_No

## Check Constraints

- NetAmount >= 0

---

# Indexes

## Clustered

PK_SalesReturn

## Non Clustered

IX_SalesReturnNo

IX_Customer

IX_DeliveryOrder

IX_ReturnDate

IX_Status

---

# Relationships

Customer (1) → SalesReturn (Many)

DeliveryOrder (1) → SalesReturn (Many)

Warehouse (1) → SalesReturn (Many)

---

# Business Rules

- Return Number generated using Number Series.
- Returned quantity cannot exceed delivered quantity.
- Posting increases Inventory.
- Posting updates Customer balance.
- Posting creates Inventory Receipt transaction.
- Posted returns cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesReturnCreated
- SalesReturnPosted
- InventoryReturned
- CustomerCreditIssued

---

# Developer Notes

- Integrates directly with Inventory Domain.
- Supports warranty claims.
- Supports customer credit adjustments.

---

# ====================================================
# 008 SalesInvoice
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesInvoice

**Classification:** Transaction Header

**Aggregate Root:** SalesOrder

---

# Purpose

SalesInvoice represents the official financial document issued to a customer after goods have been delivered.

Posting a Sales Invoice recognizes revenue, creates Accounts Receivable entries and updates the Customer Ledger.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesOrder
- DeliveryOrder
- Currency
- NumberSeries

Referenced By

- CustomerPayment
- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesInvoiceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| SalesOrderId | BIGINT | Yes | NULL | | ✔ | Sales Order |
| DeliveryOrderId | BIGINT | Yes | NULL | | ✔ | Delivery Order |
| SalesInvoiceNo | NVARCHAR(30) | No | Number Series | | | Invoice Number |
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

PK_SalesInvoice

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- SalesOrderId → SalesOrder
- DeliveryOrderId → DeliveryOrder
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_SalesInvoice_No

## Check Constraints

- DueDate >= InvoiceDate
- OutstandingAmount >= 0

---

# Indexes

## Clustered

PK_SalesInvoice

## Non Clustered

IX_SalesInvoiceNo

IX_Customer

IX_InvoiceDate

IX_Status

---

# Relationships

Customer (1) → SalesInvoice (Many)

SalesOrder (1) → SalesInvoice (Many)

DeliveryOrder (1) → SalesInvoice (Many)

---

# Business Rules

- Invoice Number generated using Number Series.
- Posting creates Accounts Receivable.
- Outstanding Amount updated after payments.
- Revenue recognized on posting.
- Posted invoices cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesInvoiceCreated
- SalesInvoicePosted
- AccountsReceivableCreated
- RevenueRecognized

---

# Developer Notes

- Integrates directly with Accounting.
- Supports partial invoicing.
- Supports multi-currency sales.

---

# ====================================================
# 009 CustomerPayment
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** CustomerPayment

**Classification:** Transaction Header

**Aggregate Root:** SalesInvoice

---

# Purpose

CustomerPayment records payments received from customers against one or more Sales Invoices.

Posting a Customer Payment reduces customer outstanding balances, updates Accounts Receivable and creates Cash/Bank journal entries.

---

# Dependencies

Depends On

- Customer
- SalesInvoice
- BankAccount
- NumberSeries

Referenced By

- Accounting
- CustomerLedger

...

# ====================================================
# 009 CustomerPayment
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** CustomerPayment

**Classification:** Transaction Header

**Aggregate Root:** SalesInvoice

---

# Purpose

CustomerPayment records payments received from customers against one or more Sales Invoices.

Posting a Customer Payment reduces outstanding balances, updates Accounts Receivable, posts Cash/Bank transactions and creates the corresponding Accounting journal entries.

Supports advance payments, partial payments and settlement of multiple invoices.

---

# Dependencies

Depends On

- Company
- Branch
- Customer
- SalesInvoice
- BankAccount
- Currency
- NumberSeries

Referenced By

- CustomerLedger
- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| CustomerPaymentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| CustomerId | BIGINT | No | | | ✔ | Customer |
| SalesInvoiceId | BIGINT | Yes | NULL | | ✔ | Sales Invoice |
| BankAccountId | BIGINT | No | | | ✔ | Bank/Cash Account |
| PaymentNo | NVARCHAR(30) | No | Number Series | | | Payment Number |
| PaymentDate | DATE | No | GETDATE() | | | Payment Date |
| PaymentMethod | SMALLINT | No | | | | Cash / Bank / Cheque / Online |
| ReferenceNo | NVARCHAR(100) | Yes | NULL | | | Cheque/Transaction No |
| CurrencyId | BIGINT | No | | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| PaymentAmount | DECIMAL(18,2) | No | 0 | | | Amount Received |
| AdjustmentAmount | DECIMAL(18,2) | No | 0 | | | Write-off/Adjustment |
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

PK_CustomerPayment

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- CustomerId → Customer
- SalesInvoiceId → SalesInvoice
- BankAccountId → BankAccount
- CurrencyId → Currency
- PostedBy → Employee

## Unique Keys

- UQ_CustomerPayment_No

## Check Constraints

- PaymentAmount > 0
- AdjustmentAmount >= 0

---

# Indexes

## Clustered

PK_CustomerPayment

## Non Clustered

IX_PaymentNo

IX_Customer

IX_SalesInvoice

IX_PaymentDate

IX_Status

---

# Relationships

Customer (1) → CustomerPayment (Many)

SalesInvoice (1) → CustomerPayment (Many)

BankAccount (1) → CustomerPayment (Many)

---

# Business Rules

- Payment Number generated using Number Series.
- Supports partial invoice settlement.
- Supports advance customer payments.
- Posting updates Customer Ledger.
- Posting updates Accounts Receivable.
- Posted payments cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- CustomerPaymentReceived
- CustomerPaymentPosted
- AccountsReceivableReduced
- CustomerBalanceUpdated

---

# Developer Notes

- Integrates directly with Accounting.
- Supports multiple payment methods.
- Supports invoice allocation.

---

# ====================================================
# 010 SalesAttachment
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Sales Orders with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Customer Purchase Orders
- Signed Quotations
- Delivery Receipts
- Customer Acknowledgements
- Sales Contracts
- Shipping Documents

---

# Dependencies

Depends On

- SalesOrder
- Attachment

Referenced By

- Sales Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
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

PK_SalesAttachment

## Foreign Keys

- SalesOrderId → SalesOrder
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_SalesAttachment

## Non Clustered

IX_SalesOrder

IX_Attachment

---

# Relationships

SalesOrder (1) → SalesAttachment (Many)

Attachment (1) → SalesAttachment (Many)

---

# Business Rules

- Attachments stored in Shared Kernel.
- Unlimited attachments supported.
- Business ownership belongs to Sales Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesAttachmentAdded
- SalesAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.

---

# ====================================================
# 011 SalesNote
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Sales Orders with reusable Note records maintained within the Shared Kernel.

Notes capture customer communications, delivery instructions, sales negotiations, approval comments and internal sales discussions.

---

# Dependencies

Depends On

- SalesOrder
- Note

Referenced By

- Sales Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
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

PK_SalesNote

## Foreign Keys

- SalesOrderId → SalesOrder
- NoteId → Note

---

# Indexes

## Clustered

PK_SalesNote

## Non Clustered

IX_SalesOrder

IX_Note

---

# Relationships

SalesOrder (1) → SalesNote (Many)

Note (1) → SalesNote (Many)

---

# Business Rules

- Notes remain reusable within Shared Kernel.
- Business ownership belongs to Sales Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesNoteAdded
- SalesNoteUpdated
- SalesNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Maintains complete customer communication history.

# ====================================================
# 012 SalesActivity
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Sales Orders with reusable Activity records maintained within the Shared Kernel.

Activities capture every operational event throughout the sales lifecycle, providing complete traceability from quotation to customer payment.

Examples include:

- Quotation Created
- Quotation Approved
- Sales Order Posted
- Inventory Reserved
- Delivery Created
- Delivery Posted
- Invoice Generated
- Customer Payment Received

---

# Dependencies

Depends On

- SalesOrder
- Activity

Referenced By

- Sales Dashboard
- Workflow Engine
- Activity Widget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
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

PK_SalesActivity

## Foreign Keys

- SalesOrderId → SalesOrder
- ActivityId → Activity

## Unique Keys

- UQ_Sales_Activity

---

# Indexes

## Clustered

PK_SalesActivity

## Non Clustered

IX_SalesOrder

IX_Activity

IX_Status

---

# Relationships

SalesOrder (1) → SalesActivity (Many)

Activity (1) → SalesActivity (Many)

---

# Business Rules

- Activities remain reusable within Shared Kernel.
- Activity history is append-only.
- Business ownership belongs to Sales Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesActivityCreated
- SalesActivityUpdated
- SalesActivityDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports Workflow Engine.
- Maintains complete sales audit history.

---

# ====================================================
# 013 SalesTimeline
# ====================================================

# Table Classification

**Domain:** Sales Domain

**Table Name:** SalesTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Sales Orders with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological view of every important sales event from quotation creation until payment completion.

Examples include:

- Quotation Created
- Quotation Approved
- Sales Order Created
- Inventory Reserved
- Delivery Completed
- Invoice Posted
- Customer Payment Received
- Sales Order Closed

---

# Dependencies

Depends On

- SalesOrder
- Timeline

Referenced By

- Sales Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SalesTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SalesOrderId | BIGINT | No | | | ✔ | Sales Order |
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

PK_SalesTimeline

## Foreign Keys

- SalesOrderId → SalesOrder
- TimelineId → Timeline

## Unique Keys

- UQ_Sales_Timeline

---

# Indexes

## Clustered

PK_SalesTimeline

## Non Clustered

IX_SalesOrder

IX_Timeline

IX_Status

---

# Relationships

SalesOrder (1) → SalesTimeline (Many)

Timeline (1) → SalesTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline history is append-only.
- Business ownership belongs to Sales Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SalesTimelineCreated
- SalesTimelineUpdated
- SalesTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for timeline rendering.
- Maintains complete sales lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Sales Domain manages the complete sales lifecycle from customer quotation through order processing, delivery, invoicing, customer returns and payment collection.

It integrates with Inventory to manage stock movements, with Accounting to recognize revenue and receivables, and with Customer Management to maintain credit limits and payment history.

---

## Aggregate Roots

- SalesQuotation
- SalesOrder
- DeliveryOrder

---

## Supporting Entities

- SalesQuotationLine
- SalesOrderLine
- DeliveryOrderLine
- SalesReturn
- SalesInvoice
- CustomerPayment

---

## Bridge Entities

- SalesAttachment
- SalesNote
- SalesActivity
- SalesTimeline

---

## Major Business Capabilities

- Sales Quotations
- Sales Orders
- Credit Limit Validation
- Inventory Reservation
- Delivery Management
- Sales Invoicing
- Customer Returns
- Customer Payment Processing
- Accounts Receivable
- Revenue Recognition
- Sales Analytics
- Complete Audit Trail
- Shared Kernel Integration

---

## Published Domain Events

The Sales Domain publishes events including:

- SalesQuotationApproved
- SalesOrderPosted
- InventoryReserved
- DeliveryOrderPosted
- InventoryReduced
- SalesInvoicePosted
- RevenueRecognized
- CustomerPaymentReceived
- AccountsReceivableReduced

These events integrate with:

- Customer Domain
- Product Domain
- Warehouse Domain
- Inventory Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Sales Domain integrates directly with:

- Foundation
- Shared Kernel
- Customer Domain
- Product Domain
- Warehouse Domain
- Inventory Domain
- Accounting Domain
- Administration

---

# Sales Domain Status

**Status:** ✅ Complete

**Total Tables:** 13

1. SalesQuotation
2. SalesQuotationLine
3. SalesOrder
4. SalesOrderLine
5. DeliveryOrder
6. DeliveryOrderLine
7. SalesReturn
8. SalesInvoice
9. CustomerPayment
10. SalesAttachment
11. SalesNote
12. SalesActivity
13. SalesTimeline

---

