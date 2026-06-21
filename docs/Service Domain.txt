# RentalERP v1.0

# ServiceDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Service

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Service Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. ServiceRequest

6. ServiceRequestLine

7. ServiceWorkOrder

---

# Domain Overview

The Service Domain manages the complete maintenance and repair lifecycle of Assets.

It covers preventive maintenance, corrective maintenance, breakdown calls, inspections, technician assignments, spare parts consumption, labor costing, warranty tracking and service completion.

Unlike the Rental Domain, which focuses on customer rentals, the Service Domain ensures Assets remain operational, safe and available for rental by maintaining complete service history.

The Service Domain integrates tightly with Asset, Inventory, Rental, Vendor and Accounting domains.

---

# Business Objectives

The Service Domain provides:

- Preventive Maintenance
- Corrective Maintenance
- Breakdown Service
- Customer Service Requests
- Internal Maintenance
- Work Orders
- Technician Assignment
- Labor Tracking
- Spare Parts Consumption
- Service Costing
- Warranty Management
- Service Completion
- Maintenance History
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Roots

- ServiceRequest
- ServiceWorkOrder

## Supporting Entities

- ServiceSchedule
- ServiceTechnicianAssignment
- ServicePartConsumption
- ServiceLabor

## Bridge Entities

- ServiceAttachment
- ServiceNote
- ServiceActivity
- ServiceTimeline

---

# Implementation Order

001 ServiceRequest

002 ServiceRequestLine

003 ServiceWorkOrder

004 ServiceWorkOrderLine

005 ServiceSchedule

006 ServiceTechnicianAssignment

007 ServicePartConsumption

008 ServiceLabor

009 ServiceCompletion

010 ServiceAttachment

011 ServiceNote

012 ServiceActivity

013 ServiceTimeline

---

# ====================================================
# 001 ServiceRequest
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceRequest

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

ServiceRequest represents a request to perform maintenance or repair on an Asset.

Requests may originate from:

- Preventive Maintenance Schedule
- Rental Return Inspection
- Customer Complaint
- Internal Inspection
- Breakdown Report
- Manual Entry

Approved Service Requests are converted into Service Work Orders.

---

# Dependencies

Depends On

- Company
- Branch
- Asset
- Customer
- NumberSeries

Referenced By

- ServiceRequestLine
- ServiceWorkOrder

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceRequestId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| RequestNo | NVARCHAR(30) | No | Number Series | | | Service Request Number |
| AssetId | BIGINT | No | | | ✔ | Asset |
| CustomerId | BIGINT | Yes | NULL | | ✔ | Customer |
| RequestDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Request Date |
| RequestSource | SMALLINT | No | | | | Rental / PM / Breakdown / Manual |
| Priority | SMALLINT | No | 2 | | | Low / Medium / High / Critical |
| RequestedCompletionDate | DATE | Yes | NULL | | | Target Completion |
| ProblemDescription | NVARCHAR(MAX) | No | | | | Problem Description |
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

PK_ServiceRequest

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- AssetId → Asset
- CustomerId → Customer
- ApprovedBy → Employee

## Unique Keys

- UQ_ServiceRequest_No

## Check Constraints

- RequestedCompletionDate >= RequestDate

---

# Indexes

## Clustered

PK_ServiceRequest

## Non Clustered

IX_RequestNo

IX_Asset

IX_Customer

IX_RequestDate

IX_Priority

IX_Status

---

# Relationships

Asset (1) → ServiceRequest (Many)

Customer (1) → ServiceRequest (Many)

ServiceRequest (1) → ServiceRequestLine (Many)

ServiceRequest (1) → ServiceWorkOrder (One Optional)

---

# Business Rules

- Request Number generated using Number Series.
- Every request belongs to one Asset.
- Request must be approved before Work Order creation.
- Critical priority requires immediate scheduling.
- Closed requests cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceRequestCreated
- ServiceRequestApproved
- ServiceRequestRejected
- ServiceRequestClosed

---

# Developer Notes

- Entry point of Service workflow.
- Can originate automatically from Rental Return or Asset Inspection.
- Does not consume inventory.
- Does not create accounting entries.

---

# ====================================================
# 002 ServiceRequestLine
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceRequestLine

**Classification:** Transaction Detail

**Aggregate Root:** ServiceRequest

---

# Purpose

ServiceRequestLine stores one or more reported problems associated with a Service Request.

Each line represents a specific fault, symptom or maintenance task that needs to be addressed.

Multiple issues may be grouped under a single Service Request.

---

# Dependencies

Depends On

- ServiceRequest

Referenced By

- ServiceWorkOrderLine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceRequestLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceRequestId | BIGINT | No | | | ✔ | Service Request |
| ProblemCategory | SMALLINT | No | | | | Mechanical / Electrical / Hydraulic / Cosmetic |
| FaultCode | NVARCHAR(50) | Yes | NULL | | | Standard Fault Code |
| ProblemDescription | NVARCHAR(1000) | No | | | | Reported Problem |
| Severity | SMALLINT | No | 2 | | | Minor / Major / Critical |
| RequiresReplacement | BIT | No | 0 | | | Parts Required |
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

PK_ServiceRequestLine

## Foreign Keys

- ServiceRequestId → ServiceRequest

---

# Indexes

## Clustered

PK_ServiceRequestLine

## Non Clustered

IX_ServiceRequest

IX_Severity

IX_Status

---

# Relationships

ServiceRequest (1) → ServiceRequestLine (Many)

---

# Business Rules

- Every problem belongs to one Service Request.
- Severity drives technician priority.
- Cannot remove lines after Work Order generation.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceRequestLineAdded
- ServiceRequestLineUpdated
- ServiceRequestLineRemoved

---

# Developer Notes

- Supports multiple issues per request.
- Used when generating Service Work Orders.

# ====================================================
# 003 ServiceWorkOrder
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceWorkOrder

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

ServiceWorkOrder authorizes and controls the execution of maintenance work on an Asset.

It is the primary operational document used by technicians to perform inspections, repairs, servicing, preventive maintenance and replacement of parts.

A Work Order is generated from an approved Service Request or a scheduled preventive maintenance activity.

---

# Dependencies

Depends On

- Company
- Branch
- ServiceRequest
- Asset
- NumberSeries

Referenced By

- ServiceWorkOrderLine
- ServiceTechnicianAssignment
- ServiceLabor
- ServicePartConsumption
- ServiceCompletion

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceWorkOrderId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| WorkOrderNo | NVARCHAR(30) | No | Number Series | | | Work Order Number |
| ServiceRequestId | BIGINT | Yes | NULL | | ✔ | Source Request |
| AssetId | BIGINT | No | | | ✔ | Asset |
| WorkOrderDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Work Order Date |
| WorkOrderType | SMALLINT | No | | | | Preventive / Corrective / Breakdown |
| Priority | SMALLINT | No | 2 | | | Low / Medium / High / Critical |
| ScheduledStartDate | DATETIME2(7) | Yes | NULL | | | Planned Start |
| ScheduledEndDate | DATETIME2(7) | Yes | NULL | | | Planned Completion |
| EstimatedCost | DECIMAL(18,2) | No | 0 | | | Estimated Cost |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Released / Completed / Cancelled |
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

PK_ServiceWorkOrder

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- ServiceRequestId → ServiceRequest
- AssetId → Asset
- PostedBy → Employee

## Unique Keys

- UQ_ServiceWorkOrder_No

## Check Constraints

- EstimatedCost >= 0
- ScheduledEndDate >= ScheduledStartDate

---

# Indexes

## Clustered

PK_ServiceWorkOrder

## Non Clustered

IX_WorkOrderNo

IX_Asset

IX_ServiceRequest

IX_WorkOrderDate

IX_Priority

IX_Status

---

# Relationships

ServiceRequest (1) → ServiceWorkOrder (One Optional)

Asset (1) → ServiceWorkOrder (Many)

ServiceWorkOrder (1) → ServiceWorkOrderLine (Many)

---

# Business Rules

- Work Order Number generated using Number Series.
- Asset becomes unavailable while Work Order is active.
- Only approved Service Requests may create Work Orders.
- Completed Work Orders cannot be modified.
- Posting starts maintenance execution.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceWorkOrderCreated
- ServiceWorkOrderReleased
- ServiceWorkOrderCompleted
- ServiceWorkOrderCancelled

---

# Developer Notes

- Primary operational document for technicians.
- Integrates with Inventory and Asset Domains.
- Updates Asset lifecycle.

---

# ====================================================
# 004 ServiceWorkOrderLine
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceWorkOrderLine

**Classification:** Transaction Detail

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

ServiceWorkOrderLine stores individual maintenance tasks that technicians must perform.

Each line represents one repair activity, inspection task or maintenance operation associated with the Work Order.

---

# Dependencies

Depends On

- ServiceWorkOrder
- ServiceRequestLine

Referenced By

- ServiceCompletion

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceWorkOrderLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Work Order |
| ServiceRequestLineId | BIGINT | Yes | NULL | | ✔ | Source Request Line |
| TaskDescription | NVARCHAR(1000) | No | | | | Maintenance Task |
| EstimatedHours | DECIMAL(18,2) | No | 0 | | | Estimated Hours |
| ActualHours | DECIMAL(18,2) | Yes | NULL | | | Actual Hours |
| CompletionPercentage | DECIMAL(5,2) | No | 0 | | | Progress |
| IsCompleted | BIT | No | 0 | | | Completed |
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

PK_ServiceWorkOrderLine

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- ServiceRequestLineId → ServiceRequestLine

## Check Constraints

- EstimatedHours >= 0
- ActualHours >= 0
- CompletionPercentage BETWEEN 0 AND 100

---

# Indexes

## Clustered

PK_ServiceWorkOrderLine

## Non Clustered

IX_WorkOrder

IX_RequestLine

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceWorkOrderLine (Many)

ServiceRequestLine (1) → ServiceWorkOrderLine (Optional Many)

---

# Business Rules

- Work Order must contain at least one task.
- Completion percentage updated automatically.
- Completed tasks become read-only.
- Actual Hours captured upon completion.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceTaskAdded
- ServiceTaskStarted
- ServiceTaskCompleted

---

# Developer Notes

- Represents technician work items.
- Supports progress tracking.
- Supports labor costing.

---

# ====================================================
# 005 ServiceSchedule
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceSchedule

**Classification:** Transaction Table

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

ServiceSchedule manages preventive maintenance schedules for Assets.

Schedules may be based on:

- Calendar Date
- Operating Hours
- Odometer Reading
- Runtime Hours
- Rental Cycles

When a schedule becomes due, the system automatically generates a Service Request.

---

# Dependencies

Depends On

- Asset
- ServiceWorkOrder

Referenced By

- ServiceRequest

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceScheduleId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| AssetId | BIGINT | No | | | ✔ | Asset |
| ServiceType | SMALLINT | No | | | | PM Type |
| FrequencyType | SMALLINT | No | | | | Days / Hours / KM / Rental Cycle |
| FrequencyValue | INT | No | | | | Interval |
| LastServiceDate | DATE | Yes | NULL | | | Previous Service |
| NextServiceDate | DATE | Yes | NULL | | | Next Service |
| LastMeterReading | DECIMAL(18,2) | Yes | NULL | | | Previous Reading |
| NextMeterReading | DECIMAL(18,2) | Yes | NULL | | | Next Reading |
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

PK_ServiceSchedule

## Foreign Keys

- AssetId → Asset

## Check Constraints

- FrequencyValue > 0

---

# Indexes

## Clustered

PK_ServiceSchedule

## Non Clustered

IX_Asset

IX_NextServiceDate

IX_NextMeterReading

IX_Status

---

# Relationships

Asset (1) → ServiceSchedule (Many)

---

# Business Rules

- Frequency Value must be greater than zero.
- System automatically creates Service Requests when due.
- Schedule updates after Work Order completion.
- Inactive schedules ignored.
- Audit fields mandatory.
- RowVersion mandatory.

---

#Published Domain Events

- PreventiveMaintenanceDue
- ServiceScheduleCreated
- ServiceScheduleUpdated

---

# Developer Notes

- Enables automated preventive maintenance.
- Supports multiple scheduling strategies.
- Integrates directly with Asset lifecycle.

# ====================================================
# 006 ServiceTechnicianAssignment
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceTechnicianAssignment

**Classification:** Transaction Table

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

ServiceTechnicianAssignment assigns one or more technicians to a Service Work Order.

It records technician responsibilities, assignment duration, labor hours and assignment status.

This enables multiple technicians to work simultaneously on the same maintenance job.

---

# Dependencies

Depends On

- ServiceWorkOrder
- Employee

Referenced By

- ServiceLabor

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceTechnicianAssignmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Work Order |
| EmployeeId | BIGINT | No | | | ✔ | Technician |
| AssignmentDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Assignment Date |
| StartDateTime | DATETIME2(7) | Yes | NULL | | | Work Started |
| EndDateTime | DATETIME2(7) | Yes | NULL | | | Work Completed |
| EstimatedHours | DECIMAL(18,2) | No | 0 | | | Estimated Hours |
| ActualHours | DECIMAL(18,2) | Yes | NULL | | | Actual Hours |
| HourlyRate | DECIMAL(18,2) | No | 0 | | | Labor Rate |
| LaborCost | DECIMAL(18,2) | No | 0 | | | Labor Cost |
| AssignmentStatus | SMALLINT | No | 1 | | | Assigned / Working / Completed |
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

PK_ServiceTechnicianAssignment

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- EmployeeId → Employee

## Check Constraints

- EstimatedHours >= 0
- ActualHours >= 0
- HourlyRate >= 0
- LaborCost >= 0

---

# Indexes

## Clustered

PK_ServiceTechnicianAssignment

## Non Clustered

IX_WorkOrder

IX_Employee

IX_AssignmentDate

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceTechnicianAssignment (Many)

Employee (1) → ServiceTechnicianAssignment (Many)

---

# Business Rules

- Multiple technicians may be assigned.
- Technician cannot be assigned twice to same Work Order.
- Labor Cost = Actual Hours × Hourly Rate.
- Assignment cannot be modified after Work Order completion.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- TechnicianAssigned
- TechnicianStartedWork
- TechnicianCompletedWork

---

# Developer Notes

- Supports multiple technicians.
- Used for technician productivity analysis.
- Integrates with Payroll in future.

---

# ====================================================
# 007 ServicePartConsumption
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServicePartConsumption

**Classification:** Transaction Table

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

Records spare parts and consumables used during maintenance.

Each record automatically generates an Inventory Issue transaction and contributes to maintenance costing.

---

# Dependencies

Depends On

- ServiceWorkOrder
- InventoryItem
- Warehouse
- InventoryTransaction

Referenced By

- Accounting

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServicePartConsumptionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Work Order |
| ItemId | BIGINT | No | | | ✔ | Spare Part |
| WarehouseId | BIGINT | No | | | ✔ | Warehouse |
| InventoryTransactionId | BIGINT | Yes | NULL | | ✔ | Inventory Issue |
| Quantity | DECIMAL(18,4) | No | 0 | | | Consumed Quantity |
| UnitCost | DECIMAL(18,4) | No | 0 | | | Unit Cost |
| TotalCost | DECIMAL(18,2) | No | 0 | | | Total Cost |
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

PK_ServicePartConsumption

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- ItemId → Inventory Item
- WarehouseId → Warehouse
- InventoryTransactionId → InventoryTransaction

## Check Constraints

- Quantity > 0
- UnitCost >= 0
- TotalCost >= 0

---

# Indexes

## Clustered

PK_ServicePartConsumption

## Non Clustered

IX_WorkOrder

IX_Item

IX_Warehouse

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServicePartConsumption (Many)

Inventory Item (1) → ServicePartConsumption (Many)

Warehouse (1) → ServicePartConsumption (Many)

---

# Business Rules

- Quantity must be available in Inventory.
- Inventory Issue created automatically.
- Total Cost = Quantity × Unit Cost.
- Consumption cannot be modified after completion.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServicePartIssued
- ServicePartReturned
- InventoryConsumed

---

# Developer Notes

- Direct integration with Inventory Domain.
- Supports spare part costing.
- Updates Asset maintenance cost.

---

# ====================================================
# 008 ServiceLabor
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceLabor

**Classification:** Transaction Table

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

Records labor performed during a Service Work Order.

Tracks labor hours, technician costs and labor expenses for profitability analysis.

---

# Dependencies

Depends On

- ServiceWorkOrder
- Employee

Referenced By

- Accounting
- ServiceCompletion

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceLaborId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Work Order |
| EmployeeId | BIGINT | No | | | ✔ | Technician |
| WorkDate | DATE | No | GETDATE() | | | Work Date |
| HoursWorked | DECIMAL(18,2) | No | 0 | | | Labor Hours |
| HourlyRate | DECIMAL(18,2) | No | 0 | | | Hourly Rate |
| LaborCost | DECIMAL(18,2) | No | 0 | | | Labor Cost |
| OvertimeHours | DECIMAL(18,2) | No | 0 | | | Overtime |
| OvertimeCost | DECIMAL(18,2) | No | 0 | | | Overtime Cost |
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

PK_ServiceLabor

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- EmployeeId → Employee

## Check Constraints

- HoursWorked >= 0
- HourlyRate >= 0
- LaborCost >= 0
- OvertimeHours >= 0
- OvertimeCost >= 0

---

# Indexes

## Clustered

PK_ServiceLabor

## Non Clustered

IX_WorkOrder

IX_Employee

IX_WorkDate

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceLabor (Many)

Employee (1) → ServiceLabor (Many)

---

# Business Rules

- Labor Cost = Hours Worked × Hourly Rate.
- Overtime calculated separately.
- Labor becomes read-only after completion.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- LaborRecorded
- LaborUpdated
- LaborCompleted

---

# Developer Notes

- Used for maintenance costing.
- Supports technician productivity KPIs.
- Integrates with Accounting.

# ====================================================
# 009 ServiceCompletion
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceCompletion

**Classification:** Transaction Header

**Aggregate Root:** ServiceWorkOrder

---

# Purpose

ServiceCompletion represents the official completion of a Service Work Order.

It verifies that all maintenance tasks have been completed, labor has been recorded, spare parts have been consumed, inspections have passed and the Asset is ready for operation.

Posting a Service Completion closes the Work Order and updates the Asset status accordingly.

---

# Dependencies

Depends On

- ServiceWorkOrder
- Asset
- Employee

Referenced By

- AssetMaintenance
- Accounting
- AssetTimeline

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceCompletionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Work Order |
| CompletionDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Completion Date |
| CompletedBy | BIGINT | No | | | ✔ | Employee |
| InspectionResult | SMALLINT | No | | | | Pass / Fail |
| TotalLaborCost | DECIMAL(18,2) | No | 0 | | | Labor Cost |
| TotalPartsCost | DECIMAL(18,2) | No | 0 | | | Parts Cost |
| ExternalCost | DECIMAL(18,2) | No | 0 | | | Vendor Cost |
| TotalMaintenanceCost | DECIMAL(18,2) | No | 0 | | | Total Cost |
| AssetStatusAfterCompletion | SMALLINT | No | | | | Available / Under Observation / Out of Service |
| CompletionRemarks | NVARCHAR(1000) | Yes | NULL | | | Remarks |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted |
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

PK_ServiceCompletion

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- CompletedBy → Employee
- PostedBy → Employee

## Check Constraints

- TotalLaborCost >= 0
- TotalPartsCost >= 0
- ExternalCost >= 0
- TotalMaintenanceCost >= 0

---

# Indexes

## Clustered

PK_ServiceCompletion

## Non Clustered

IX_WorkOrder

IX_CompletionDate

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceCompletion (One)

---

# Business Rules

- Every Work Order has only one completion.
- Total Maintenance Cost is calculated automatically.
- Posting closes Work Order.
- Posting updates Asset Status.
- Posting creates Asset Maintenance History.
- Completed records cannot be modified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceCompleted
- AssetReturnedToService
- MaintenanceHistoryUpdated

---

# Developer Notes

- Final document in Service workflow.
- Updates Asset lifecycle.
- Updates maintenance history.
- Integrates with Accounting.

---

# ====================================================
# 010 ServiceAttachment
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Service Work Orders with reusable Attachment records stored within the Shared Kernel.

Examples include:

- Before Repair Photos
- After Repair Photos
- Vendor Invoice
- Warranty Documents
- Calibration Certificates
- Inspection Reports

---

# Dependencies

Depends On

- ServiceWorkOrder
- Attachment

Referenced By

- Service Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Service Work Order |
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

PK_ServiceAttachment

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_ServiceAttachment

## Non Clustered

IX_ServiceWorkOrder

IX_Attachment

---

# Relationships

ServiceWorkOrder (1) → ServiceAttachment (Many)

Attachment (1) → ServiceAttachment (Many)

---

# Business Rules

- Attachment stored in Shared Kernel.
- Unlimited attachments supported.
- Business ownership belongs to Service Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceAttachmentAdded
- ServiceAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.

---

# ====================================================
# 011 ServiceNote
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Service Work Orders with reusable Note records maintained within the Shared Kernel.

Notes capture technician observations, repair recommendations, customer comments, vendor remarks and maintenance findings.

---

# Dependencies

Depends On

- ServiceWorkOrder
- Note

Referenced By

- Service Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Service Work Order |
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

PK_ServiceNote

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- NoteId → Note

---

# Indexes

## Clustered

PK_ServiceNote

## Non Clustered

IX_ServiceWorkOrder

IX_Note

---

#Relationships

ServiceWorkOrder (1) → ServiceNote (Many)

Note (1) → ServiceNote (Many)

---

# Business Rules

- Notes remain reusable in Shared Kernel.
- Business ownership belongs to Service Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceNoteAdded
- ServiceNoteUpdated
- ServiceNoteRemoved

---

# Developer Notes

- Shared Kernel bridge implementation.
- Maintains maintenance communication history.

# ====================================================
# 012 ServiceActivity
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Service Work Orders with reusable Activity records maintained within the Shared Kernel.

Activities capture every operational event performed during the maintenance lifecycle, allowing complete traceability of service operations.

Examples include:

- Service Request Created
- Work Order Released
- Technician Assigned
- Parts Issued
- Repair Started
- Repair Completed
- Asset Tested
- Service Completed

---

# Dependencies

Depends On

- ServiceWorkOrder
- Activity

Referenced By

- Service Dashboard
- Workflow Engine
- Activity Widget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Service Work Order |
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

PK_ServiceActivity

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- ActivityId → Activity

## Unique Keys

- UQ_Service_Activity

---

# Indexes

## Clustered

PK_ServiceActivity

## Non Clustered

IX_ServiceWorkOrder

IX_Activity

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceActivity (Many)

Activity (1) → ServiceActivity (Many)

---

# Business Rules

- Activities remain reusable within Shared Kernel.
- Activity history is append-only.
- Business ownership belongs to Service Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceActivityCreated
- ServiceActivityUpdated
- ServiceActivityDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports Workflow Engine.
- Supports Notification Engine.
- Maintains complete maintenance audit history.

---

# ====================================================
# 013 ServiceTimeline
# ====================================================

# Table Classification

**Domain:** Service Domain

**Table Name:** ServiceTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Service Work Orders with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of every maintenance event from Service Request creation until Work Order completion.

Examples include:

- Service Request Created
- Request Approved
- Work Order Created
- Technician Assigned
- Parts Issued
- Labor Recorded
- Inspection Passed
- Service Completed
- Asset Returned to Service

---

# Dependencies

Depends On

- ServiceWorkOrder
- Timeline

Referenced By

- Service Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ServiceTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ServiceWorkOrderId | BIGINT | No | | | ✔ | Service Work Order |
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

PK_ServiceTimeline

## Foreign Keys

- ServiceWorkOrderId → ServiceWorkOrder
- TimelineId → Timeline

## Unique Keys

- UQ_Service_Timeline

---

# Indexes

## Clustered

PK_ServiceTimeline

## Non Clustered

IX_ServiceWorkOrder

IX_Timeline

IX_Status

---

# Relationships

ServiceWorkOrder (1) → ServiceTimeline (Many)

Timeline (1) → ServiceTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline history is append-only.
- Business ownership belongs to Service Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ServiceTimelineCreated
- ServiceTimelineUpdated
- ServiceTimelineDeleted

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for timeline rendering.
- Maintains complete maintenance lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Service Domain manages the complete lifecycle of Asset maintenance and repair operations. It covers preventive maintenance scheduling, corrective maintenance, breakdown repairs, technician assignments, spare parts consumption, labor costing and service completion.

The domain ensures every maintenance activity is fully traceable while integrating with Inventory, Asset, Rental and Accounting to maintain operational efficiency and accurate costing.

---

## Aggregate Roots

- ServiceRequest
- ServiceWorkOrder

---

## Supporting Entities

- ServiceRequestLine
- ServiceWorkOrderLine
- ServiceSchedule
- ServiceTechnicianAssignment
- ServicePartConsumption
- ServiceLabor
- ServiceCompletion

---

## Bridge Entities

- ServiceAttachment
- ServiceNote
- ServiceActivity
- ServiceTimeline

---

## Major Business Capabilities

- Preventive Maintenance
- Corrective Maintenance
- Breakdown Repairs
- Work Order Management
- Technician Assignment
- Labor Tracking
- Spare Parts Consumption
- Maintenance Costing
- Warranty Tracking
- Asset Maintenance History
- Complete Audit Trail
- Shared Kernel Integration

---

## Published Domain Events

The Service Domain publishes events including:

- ServiceRequestCreated
- ServiceRequestApproved
- ServiceWorkOrderReleased
- TechnicianAssigned
- ServicePartIssued
- LaborRecorded
- ServiceCompleted
- AssetReturnedToService
- PreventiveMaintenanceDue

These events integrate with:

- Asset Domain
- Inventory Domain
- Rental Domain
- Vendor Domain
- Customer Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Reporting Module

---

## Integration Points

The Service Domain integrates directly with:

- Foundation
- Shared Kernel
- Asset Domain
- Warehouse Domain
- Inventory Domain
- Rental Domain
- Vendor Domain
- Customer Domain
- Accounting Domain
- Administration

---

# Service Domain Status

**Status:** ✅ Complete

**Total Tables:** 13

1. ServiceRequest
2. ServiceRequestLine
3. ServiceWorkOrder
4. ServiceWorkOrderLine
5. ServiceSchedule
6. ServiceTechnicianAssignment
7. ServicePartConsumption
8. ServiceLabor
9. ServiceCompletion
10. ServiceAttachment
11. ServiceNote
12. ServiceActivity
13. ServiceTimeline

---

