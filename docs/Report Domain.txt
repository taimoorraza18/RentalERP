# RentalERP v1.0

# ReportingDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Reporting

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Reporting Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. ReportCategory

6. ReportDefinition

7. ReportParameter

---

# Domain Overview

The Reporting Domain provides centralized reporting capabilities across the entire RentalERP platform.

Unlike operational domains (Purchase, Sales, Inventory, Rental, Service, Accounting), the Reporting Domain **does not own business transactions**. Instead, it consumes data from all business domains to generate operational, analytical, management and statutory reports.

The Reporting Domain supports interactive reports, dashboards, scheduled reports, exports and report security while keeping report definitions independent from business logic.

---

# Business Objectives

The Reporting Domain provides:

- Report Categories
- Report Definitions
- Report Parameters
- Saved Report Templates
- Report Execution
- Report Scheduling
- Dashboard Reports
- Report Export
- Report History
- Report Security
- User Favorites
- KPI Reporting
- Financial Reporting
- Operational Reporting

---

# Aggregate Root

## Primary Aggregate Root

- ReportDefinition

## Supporting Entities

- ReportCategory
- ReportParameter
- ReportExecution
- SavedReport
- ReportSchedule
- ReportExport

## Bridge Entities

- ReportAttachment
- ReportNote
- ReportActivity
- ReportTimeline

---

# Implementation Order

001 ReportCategory

002 ReportDefinition

003 ReportParameter

004 SavedReport

005 ReportExecution

006 ReportSchedule

007 ReportExport

008 ReportAttachment

009 ReportNote

010 ReportActivity

011 ReportTimeline

---

# ====================================================
# 001 ReportCategory
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportCategory

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

ReportCategory organizes reports into logical business groups.

Categories simplify report navigation and allow reports to be displayed under appropriate ERP modules.

Examples include:

- Administration
- Customer
- Vendor
- Product
- Inventory
- Warehouse
- Rental
- Service
- Purchase
- Sales
- Accounting
- Executive Dashboard

---

# Dependencies

Depends On

- Company

Referenced By

- ReportDefinition

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportCategoryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| CategoryCode | NVARCHAR(30) | No | | | | Category Code |
| CategoryName | NVARCHAR(150) | No | | | | Category Name |
| Icon | NVARCHAR(100) | Yes | NULL | | | UI Icon |
| DisplayOrder | INT | No | 1 | | | Display Order |
| IsSystem | BIT | No | 0 | | | System Category |
| StatusId | SMALLINT | No | 1 | | | Active |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Optimistic Concurrency |

---

# Constraints

## Primary Key

PK_ReportCategory

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_ReportCategory_Code
- UQ_ReportCategory_Name

---

# Indexes

## Clustered

PK_ReportCategory

## Non Clustered

IX_CategoryCode

IX_CategoryName

IX_DisplayOrder

IX_Status

---

# Relationships

ReportCategory (1) → ReportDefinition (Many)

---

# Business Rules

- Category Code must be unique.
- Category Name must be unique.
- Display Order controls menu order.
- System Categories cannot be deleted.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportCategoryCreated
- ReportCategoryUpdated
- ReportCategoryDeleted

---

# Developer Notes

- Used by Report Explorer.
- Supports hierarchical report organization.

---

# ====================================================
# 002 ReportDefinition
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportDefinition

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

ReportDefinition stores metadata describing every report available within RentalERP.

It contains report configuration, data source, layout, security and execution settings.

The Reporting Engine uses this table to dynamically render reports.

---

# Dependencies

Depends On

- Company
- ReportCategory

Referenced By

- ReportParameter
- SavedReport
- ReportExecution
- ReportSchedule

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportDefinitionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| ReportCategoryId | BIGINT | No | | | ✔ | Report Category |
| ReportCode | NVARCHAR(50) | No | | | | Report Code |
| ReportName | NVARCHAR(250) | No | | | | Report Name |
| Description | NVARCHAR(1000) | Yes | NULL | | | Description |
| DataSource | NVARCHAR(500) | No | | | | Stored Procedure / View |
| ReportType | SMALLINT | No | | | | Tabular / Chart / Dashboard / Matrix |
| OutputFormat | NVARCHAR(100) | No | PDF, Excel | | | Supported Formats |
| AllowExport | BIT | No | 1 | | | Export Allowed |
| AllowScheduling | BIT | No | 1 | | | Schedule Allowed |
| CacheDuration | INT | No | 0 | | | Minutes |
| IsSystem | BIT | No | 0 | | | System Report |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportDefinition

## Foreign Keys

- CompanyId → Company
- ReportCategoryId → ReportCategory

## Unique Keys

- UQ_Report_Code

---

# Indexes

## Clustered

PK_ReportDefinition

## Non Clustered

IX_ReportCategory

IX_ReportCode

IX_ReportName

IX_Status

---

# Relationships

ReportCategory (1) → ReportDefinition (Many)

ReportDefinition (1) → ReportParameter (Many)

ReportDefinition (1) → ReportExecution (Many)

ReportDefinition (1) → SavedReport (Many)

---

# Business Rules

- Report Code must be unique.
- System Reports cannot be deleted.
- Data Source must exist.
- Export permission configurable.
- Scheduling optional.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportCreated
- ReportUpdated
- ReportPublished

---

# Developer Notes

- Core aggregate of Reporting Domain.
- Referenced by all reporting features.

---

# ====================================================
# 003 ReportParameter
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportParameter

**Classification:** Master Detail

**Aggregate Root:** ReportDefinition

---

# Purpose

ReportParameter defines all user-selectable filters required before a report executes.

Parameters allow the Reporting Engine to dynamically generate report input screens without hardcoding UI logic.

Typical parameters include:

- Date From
- Date To
- Company
- Branch
- Customer
- Vendor
- Warehouse
- Product
- Fiscal Year
- Status
- Employee

---

# Dependencies

Depends On

- ReportDefinition

Referenced By

- Report Execution Engine

...

# ====================================================
# 003 ReportParameter
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportParameter

**Classification:** Master Detail

**Aggregate Root:** ReportDefinition

---

# Purpose

ReportParameter defines every input parameter required before executing a report.

The Reporting Engine dynamically builds parameter screens from this table, eliminating the need for hardcoded report filters.

Examples include:

- Date Range
- Company
- Branch
- Customer
- Vendor
- Warehouse
- Product
- Fiscal Year
- Status
- Employee
- Currency
- Category

---

# Dependencies

Depends On

- ReportDefinition

Referenced By

- Report Execution Engine
- SavedReport

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportParameterId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report |
| ParameterName | NVARCHAR(100) | No | | | | Internal Name |
| DisplayName | NVARCHAR(150) | No | | | | UI Caption |
| DataType | SMALLINT | No | | | | Date / Text / Number / Boolean / Lookup |
| LookupSource | NVARCHAR(300) | Yes | NULL | | | Stored Procedure / API |
| DefaultValue | NVARCHAR(500) | Yes | NULL | | | Default Value |
| IsRequired | BIT | No | 0 | | | Mandatory |
| AllowMultiple | BIT | No | 0 | | | Multi Select |
| DisplayOrder | INT | No | 1 | | | UI Order |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportParameter

## Foreign Keys

- ReportDefinitionId → ReportDefinition

---

## Indexes

### Clustered

PK_ReportParameter

### Non Clustered

IX_ReportDefinition

IX_DisplayOrder

IX_Status

---

# Relationships

ReportDefinition (1) → ReportParameter (Many)

---

# Business Rules

- Parameter names must be unique within a report.
- Display Order controls parameter sequence.
- Required parameters validated before execution.
- Multi-select parameters supported.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportParameterCreated
- ReportParameterUpdated
- ReportParameterDeleted

---

# Developer Notes

- Enables dynamic report parameter UI.
- Supports lookup-based filters.
- Supports reusable reporting engine.

---

# ====================================================
# 004 SavedReport
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** SavedReport

**Classification:** User Configuration

**Aggregate Root:** ReportDefinition

---

# Purpose

SavedReport stores user-defined report configurations.

Instead of repeatedly entering parameters, users can save parameter sets and execute reports instantly.

Examples:

- Monthly Sales Report
- Karachi Warehouse Inventory
- Outstanding Customer Receivables
- Vendor Purchase Summary

---

# Dependencies

Depends On

- ReportDefinition
- User

Referenced By

- ReportExecution

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SavedReportId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report |
| UserId | BIGINT | No | | | ✔ | User |
| ReportName | NVARCHAR(200) | No | | | | Saved Report Name |
| ParameterJson | NVARCHAR(MAX) | No | | | | Serialized Parameters |
| IsDefault | BIT | No | 0 | | | Default Configuration |
| IsPublic | BIT | No | 0 | | | Shared Report |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_SavedReport

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- UserId → User

---

# Indexes

### Clustered

PK_SavedReport

### Non Clustered

IX_ReportDefinition

IX_User

IX_Default

---

# Relationships

ReportDefinition (1) → SavedReport (Many)

User (1) → SavedReport (Many)

---

# Business Rules

- Multiple saved reports allowed per user.
- Only one default per report.
- Public reports visible according to permissions.
- Parameter values stored as JSON.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SavedReportCreated
- SavedReportUpdated
- SavedReportDeleted

---

# Developer Notes

- Improves user productivity.
- Enables reusable report presets.

---

# ====================================================
# 005 ReportExecution
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportExecution

**Classification:** Transaction Table

**Aggregate Root:** ReportDefinition

---

# Purpose

ReportExecution records every execution of a report.

Execution history provides auditing, performance monitoring and troubleshooting information.

Each execution records:

- User
- Parameters
- Execution Time
- Duration
- Status
- Export Format

---

# Dependencies

Depends On

- ReportDefinition
- User

Referenced By

- ReportExport

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportExecutionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report |
| UserId | BIGINT | No | | | ✔ | Executed By |
| ExecutionDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Execution Time |
| ParameterJson | NVARCHAR(MAX) | Yes | NULL | | | Parameters |
| DurationMs | INT | No | 0 | | | Execution Time |
| RecordCount | INT | No | 0 | | | Returned Records |
| ExecutionStatus | SMALLINT | No | | | | Success / Failed / Cancelled |
| ErrorMessage | NVARCHAR(MAX) | Yes | NULL | | | Failure Message |
| ExportFormat | NVARCHAR(30) | Yes | NULL | | | PDF / Excel / CSV |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportExecution

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- UserId → User

---

# Indexes

### Clustered

PK_ReportExecution

### Non Clustered

IX_Report

IX_User

IX_ExecutionDate

IX_Status

---

# Relationships

ReportDefinition (1) → ReportExecution (Many)

User (1) → ReportExecution (Many)

---

# Business Rules

- Every report execution logged.
- Failed executions retain error details.
- Execution history cannot be modified.
- Parameter snapshot preserved.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportExecuted
- ReportExecutionFailed

---

# Developer Notes

- Used for report analytics.
- Supports execution auditing.

---

# ====================================================
# 006 ReportSchedule
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportSchedule

**Classification:** Transaction Table

**Aggregate Root:** ReportDefinition

---

# Purpose

ReportSchedule defines automatic execution schedules for reports.

Users can schedule reports to execute automatically and deliver results via Email, Dashboard or File Storage.

Supports:

- Daily
- Weekly
- Monthly
- Quarterly
- Yearly
- Cron Expressions

---

# Dependencies

Depends On

- ReportDefinition
- User

Referenced By

- Background Scheduler

...

# ====================================================
# 006 ReportSchedule
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportSchedule

**Classification:** Transaction Table

**Aggregate Root:** ReportDefinition

---

# Purpose

ReportSchedule defines automatic execution schedules for reports.

Users can schedule reports to execute automatically and deliver results through Email, Dashboard, Network Folder or Cloud Storage.

Supported scheduling options include:

- Once
- Daily
- Weekly
- Monthly
- Quarterly
- Yearly
- Custom Cron Expression

---

# Dependencies

Depends On

- ReportDefinition
- User

Referenced By

- ReportExecution
- Notification Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportScheduleId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report |
| UserId | BIGINT | No | | | ✔ | Owner |
| ScheduleName | NVARCHAR(200) | No | | | | Schedule Name |
| ParameterJson | NVARCHAR(MAX) | Yes | NULL | | | Saved Parameters |
| Frequency | SMALLINT | No | | | | Daily / Weekly / Monthly / Cron |
| CronExpression | NVARCHAR(100) | Yes | NULL | | | Cron Schedule |
| StartDate | DATETIME2(7) | No | | | | Start Date |
| EndDate | DATETIME2(7) | Yes | NULL | | | End Date |
| NextRunDate | DATETIME2(7) | Yes | NULL | | | Next Execution |
| LastRunDate | DATETIME2(7) | Yes | NULL | | | Previous Execution |
| DeliveryMethod | SMALLINT | No | | | | Email / Dashboard / Storage |
| IsEnabled | BIT | No | 1 | | | Enabled |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportSchedule

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- UserId → User

---

# Indexes

## Clustered

PK_ReportSchedule

## Non Clustered

IX_Report

IX_User

IX_NextRunDate

IX_Enabled

---

# Relationships

ReportDefinition (1) → ReportSchedule (Many)

User (1) → ReportSchedule (Many)

---

# Business Rules

- Multiple schedules allowed per report.
- Disabled schedules never execute.
- Cron Expression required for custom schedules.
- Scheduler updates NextRunDate automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportScheduled
- ReportScheduleUpdated
- ReportScheduleExecuted

---

# Developer Notes

- Executed by Background Scheduler.
- Supports recurring report delivery.

---

# ====================================================
# 007 ReportExport
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportExport

**Classification:** Transaction Table

**Aggregate Root:** ReportExecution

---

# Purpose

Stores metadata for every exported report generated by the Reporting Engine.

Allows users to download previously generated reports without rerunning expensive queries.

Supports:

- PDF
- Excel
- CSV
- Word
- JSON

---

# Dependencies

Depends On

- ReportExecution

Referenced By

- Download Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportExportId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportExecutionId | BIGINT | No | | | ✔ | Report Execution |
| FileName | NVARCHAR(300) | No | | | | Export File |
| FileExtension | NVARCHAR(20) | No | | | | PDF / XLSX |
| FileSize | BIGINT | No | 0 | | | Bytes |
| StoragePath | NVARCHAR(1000) | No | | | | Storage Path |
| DownloadCount | INT | No | 0 | | | Downloads |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Cleanup Date |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportExport

## Foreign Keys

- ReportExecutionId → ReportExecution

---

# Indexes

## Clustered

PK_ReportExport

## Non Clustered

IX_ReportExecution

IX_ExpiryDate

IX_Status

---

# Relationships

ReportExecution (1) → ReportExport (Many)

---

# Business Rules

- Export generated only after successful execution.
- Expired exports removed by cleanup job.
- Download count maintained automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportExportGenerated
- ReportDownloaded

---

# Developer Notes

- Supports export cache.
- Integrates with File Storage Service.

---

# ====================================================
# 008 ReportAttachment
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates reports with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Report Templates
- Company Logos
- Supporting Documents
- Regulatory Guidelines
- Sample Reports

---

# Dependencies

Depends On

- ReportDefinition
- Attachment

Referenced By

- Report Designer

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report |
| AttachmentId | BIGINT | No | | | ✔ | Shared Attachment |
| DisplayOrder | INT | No | 1 | | | Display Order |
| StatusId | SMALLINT | No | 1 | | | Active |
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

PK_ReportAttachment

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- AttachmentId → Attachment

---

#Indexes

## Clustered

PK_ReportAttachment

## Non Clustered

IX_Report

IX_Attachment

---

# Relationships

ReportDefinition (1) → ReportAttachment (Many)

Attachment (1) → ReportAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Attachments remain in Shared Kernel.
- Business ownership belongs to Reporting Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportAttachmentAdded
- ReportAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.

---

# ====================================================
# 009 ReportNote
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates reports with reusable Note records maintained within the Shared Kernel.

Allows report designers and administrators to document report purpose, implementation notes, SQL optimization comments and business rules.

---

# Dependencies

Depends On

- ReportDefinition
- Note

Referenced By

- Report Designer

...

# ====================================================
# 009 ReportNote
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Report Definitions with reusable Note records maintained within the Shared Kernel.

Notes are used by report designers, business analysts and administrators to document report logic, business rules, implementation details, optimization notes and maintenance history.

Examples include:

- Business Rule Notes
- SQL Optimization Notes
- Change Requests
- Functional Documentation
- Report Assumptions
- Known Limitations

---

# Dependencies

Depends On

- ReportDefinition
- Note

Referenced By

- Report Designer
- Administration
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report Definition |
| NoteId | BIGINT | No | | | ✔ | Shared Note |
| StatusId | SMALLINT | No | 1 | | | Active |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Optimistic Concurrency |

---

# Constraints

## Primary Key

PK_ReportNote

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- NoteId → Note

## Unique Keys

- UQ_Report_Note (ReportDefinitionId, NoteId)

---

# Indexes

## Clustered

PK_ReportNote

## Non Clustered

IX_Report

IX_Note

IX_Status

---

# Relationships

ReportDefinition (1) → ReportNote (Many)

Note (1) → ReportNote (Many)

---

# Business Rules

- Unlimited notes allowed.
- Shared Notes remain reusable.
- Report ownership belongs to Reporting Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportNoteAdded
- ReportNoteUpdated
- ReportNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports report maintenance documentation.

---

# ====================================================
# 010 ReportActivity
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Report Definitions with reusable Activity records maintained within the Shared Kernel.

Activities record every operational event throughout the report lifecycle.

Examples include:

- Report Created
- Report Modified
- Report Published
- Report Executed
- Report Scheduled
- Export Generated
- Security Updated
- Report Archived

---

# Dependencies

Depends On

- ReportDefinition
- Activity

Referenced By

- Reporting Dashboard
- Workflow Engine
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report Definition |
| ActivityId | BIGINT | No | | | ✔ | Shared Activity |
| StatusId | SMALLINT | No | 1 | | | Active |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Optimistic Concurrency |

---

# Constraints

## Primary Key

PK_ReportActivity

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- ActivityId → Activity

## Unique Keys

- UQ_Report_Activity (ReportDefinitionId, ActivityId)

---

# Indexes

## Clustered

PK_ReportActivity

## Non Clustered

IX_Report

IX_Activity

IX_Status

---

# Relationships

ReportDefinition (1) → ReportActivity (Many)

Activity (1) → ReportActivity (Many)

---

# Business Rules

- Activities are append-only.
- Shared Activity reused throughout ERP.
- Business ownership belongs to Reporting Domain.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportActivityCreated
- ReportActivityUpdated

---

# Developer Notes

- Integrates with Workflow Engine.
- Maintains complete reporting audit history.

---

# ====================================================
# 011 ReportTimeline
# ====================================================

# Table Classification

**Domain:** Reporting Domain

**Table Name:** ReportTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Report Definitions with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological history of report development, publication, execution and maintenance.

Examples include:

- Report Created
- Parameters Modified
- Report Published
- Schedule Created
- Report Executed
- Export Downloaded
- Security Updated
- Report Archived

---

# Dependencies

Depends On

- ReportDefinition
- Timeline

Referenced By

- Report Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ReportTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ReportDefinitionId | BIGINT | No | | | ✔ | Report Definition |
| TimelineId | BIGINT | No | | | ✔ | Shared Timeline |
| StatusId | SMALLINT | No | 1 | | | Active |
| CreatedBy | BIGINT | No | | | | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | NULL | | | Audit |
| ModifiedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| DeletedBy | BIGINT | Yes | NULL | | | Audit |
| DeletedDate | DATETIME2(7) | Yes | NULL | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | Auto | | | Optimistic Concurrency |

---

# Constraints

## Primary Key

PK_ReportTimeline

## Foreign Keys

- ReportDefinitionId → ReportDefinition
- TimelineId → Timeline

## Unique Keys

- UQ_Report_Timeline (ReportDefinitionId, TimelineId)

---

# Indexes

## Clustered

PK_ReportTimeline

## Non Clustered

IX_Report

IX_Timeline

IX_Status

---

# Relationships

ReportDefinition (1) → ReportTimeline (Many)

Timeline (1) → ReportTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline history is append-only.
- Business ownership belongs to Reporting Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ReportTimelineCreated
- ReportTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for timeline visualization.
- Maintains complete report lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Reporting Domain provides centralized reporting capabilities for every business domain within RentalERP.

Rather than owning transactional data, it consumes data from operational domains to produce operational reports, management dashboards, KPIs, financial statements and analytical reports.

It supports dynamic report definitions, scheduling, execution history, exports and reusable report configurations.

---

## Aggregate Roots

- ReportDefinition

---

## Supporting Entities

- ReportCategory
- ReportParameter
- SavedReport
- ReportExecution
- ReportSchedule
- ReportExport

---

## Bridge Entities

- ReportAttachment
- ReportNote
- ReportActivity
- ReportTimeline

---

## Major Business Capabilities

- Dynamic Report Definitions
- Parameterized Reports
- Saved Report Templates
- Scheduled Reports
- Background Report Execution
- Report Export (PDF, Excel, CSV, Word, JSON)
- Dashboard Integration
- Execution History
- Performance Monitoring
- KPI Reporting
- Financial Reporting
- Operational Reporting
- Shared Kernel Integration

---

## Published Domain Events

The Reporting Domain publishes events including:

- ReportCreated
- ReportPublished
- ReportExecuted
- ReportExecutionFailed
- ReportScheduled
- ReportExportGenerated
- ReportDownloaded

These events integrate with:

- Administration Domain
- Customer Domain
- Vendor Domain
- Product Domain
- Purchase Domain
- Sales Domain
- Inventory Domain
- Warehouse Domain
- Rental Domain
- Service Domain
- Accounting Domain
- Notification Module
- Workflow Engine
- Dashboard Module

---

## Integration Points

The Reporting Domain integrates directly with:

- Foundation
- Shared Kernel
- Administration Domain
- Customer Domain
- Vendor Domain
- Product Domain
- Purchase Domain
- Sales Domain
- Inventory Domain
- Warehouse Domain
- Rental Domain
- Service Domain
- Accounting Domain

---

# Reporting Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. ReportCategory
2. ReportDefinition
3. ReportParameter
4. SavedReport
5. ReportExecution
6. ReportSchedule
7. ReportExport
8. ReportAttachment
9. ReportNote
10. ReportActivity
11. ReportTimeline

---
