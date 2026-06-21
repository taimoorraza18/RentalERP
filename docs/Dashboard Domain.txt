# RentalERP v1.0

# DashboardDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Dashboard

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Dashboard Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. Dashboard

6. DashboardWidget

7. WidgetDataSource

---

# Domain Overview

The Dashboard Domain provides configurable, role-based dashboards for RentalERP.

Unlike Reporting, which focuses on generating reports, the Dashboard Domain focuses on real-time visualization of business information through KPIs, charts, widgets, gauges, summary cards and drill-down capabilities.

Dashboards aggregate information from every ERP domain and present it in a customizable layout for executives, managers and operational users.

---

# Business Objectives

The Dashboard Domain provides:

- Executive Dashboards
- User Dashboards
- Role Based Dashboards
- Dashboard Widgets
- KPI Cards
- Charts
- Gauges
- Summary Cards
- Real-time Metrics
- Widget Personalization
- Dashboard Sharing
- Dashboard Templates
- Drill-down Navigation
- Refresh Scheduling

---

# Aggregate Root

## Primary Aggregate Root

- Dashboard

## Supporting Entities

- DashboardWidget
- WidgetDataSource
- DashboardLayout
- DashboardFilter
- DashboardFavorite

## Bridge Entities

- DashboardAttachment
- DashboardNote
- DashboardActivity
- DashboardTimeline

---

# Implementation Order

001 Dashboard

002 DashboardWidget

003 WidgetDataSource

004 DashboardLayout

005 DashboardFilter

006 DashboardFavorite

007 DashboardSnapshot

008 DashboardAttachment

009 DashboardNote

010 DashboardActivity

011 DashboardTimeline

---

# ====================================================
# 001 Dashboard
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** Dashboard

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

Dashboard represents a configurable collection of widgets presented to a user.

Dashboards may be:

- System Dashboards
- Personal Dashboards
- Department Dashboards
- Executive Dashboards
- Shared Dashboards

Users may customize layouts without affecting the underlying business data.

---

# Dependencies

Depends On

- Company
- User

Referenced By

- DashboardWidget
- DashboardLayout

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| UserId | BIGINT | Yes | NULL | | ✔ | Owner |
| DashboardCode | NVARCHAR(30) | No | | | | Dashboard Code |
| DashboardName | NVARCHAR(200) | No | | | | Dashboard Name |
| Description | NVARCHAR(1000) | Yes | NULL | | | Description |
| DashboardType | SMALLINT | No | | | | Personal / Shared / System |
| IsDefault | BIT | No | 0 | | | Default Dashboard |
| IsSystem | BIT | No | 0 | | | System Dashboard |
| RefreshIntervalSeconds | INT | No | 300 | | | Auto Refresh |
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

PK_Dashboard

## Foreign Keys

- CompanyId → Company
- UserId → User

## Unique Keys

- UQ_Dashboard_Code

---

# Indexes

## Clustered

PK_Dashboard

## Non Clustered

IX_DashboardCode

IX_User

IX_DashboardType

IX_Default

---

# Relationships

Dashboard (1) → DashboardWidget (Many)

Dashboard (1) → DashboardLayout (One)

---

# Business Rules

- Dashboard Code must be unique.
- One default dashboard per user.
- System dashboards cannot be deleted.
- Shared dashboards require permissions.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardCreated
- DashboardUpdated
- DashboardPublished

---

# Developer Notes

- Root aggregate of Dashboard Domain.
- Supports customizable user experience.

---

# ====================================================
# 002 DashboardWidget
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardWidget

**Classification:** Master Detail

**Aggregate Root:** Dashboard

---

# Purpose

DashboardWidget represents an individual visual component placed on a dashboard.

Supported widget types include:

- KPI Card
- Chart
- Grid
- Gauge
- Calendar
- Timeline
- Activity Feed
- Recent Transactions
- Financial Summary
- Inventory Summary

Each widget references its own data source and rendering configuration.

---

# Dependencies

Depends On

- Dashboard

Referenced By

- WidgetDataSource

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardWidgetId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
| WidgetName | NVARCHAR(200) | No | | | | Widget Name |
| WidgetType | SMALLINT | No | | | | KPI / Chart / Grid |
| DisplayOrder | INT | No | 1 | | | Display Order |
| Width | SMALLINT | No | 4 | | | Grid Width |
| Height | SMALLINT | No | 4 | | | Grid Height |
| PositionX | SMALLINT | No | 0 | | | Grid Position |
| PositionY | SMALLINT | No | 0 | | | Grid Position |
| RefreshIntervalSeconds | INT | No | 300 | | | Refresh |
| IsVisible | BIT | No | 1 | | | Visible |
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

PK_DashboardWidget

## Foreign Keys

- DashboardId → Dashboard

---

## Indexes

### Clustered

PK_DashboardWidget

### Non Clustered

IX_Dashboard

IX_DisplayOrder

IX_WidgetType

---

# Relationships

Dashboard (1) → DashboardWidget (Many)

DashboardWidget (1) → WidgetDataSource (One)

---

# Business Rules

- Widget positions cannot overlap.
- Display Order controls rendering.
- Hidden widgets remain configurable.
- Refresh interval overrides dashboard default if specified.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WidgetAdded
- WidgetUpdated
- WidgetRemoved

---

# Developer Notes

- Supports drag-and-drop layout.
- Supports responsive grid rendering.

---

# ====================================================
# 003 WidgetDataSource
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** WidgetDataSource

**Classification:** Configuration Table

**Aggregate Root:** DashboardWidget

---

# Purpose

WidgetDataSource defines where a widget retrieves its data.

A widget may use:

- SQL View
- Stored Procedure
- API Endpoint
- Materialized View
- Cache
- Real-time SignalR Stream

This abstraction allows widgets to remain independent from business logic.

---

# Dependencies

Depends On

- DashboardWidget

Referenced By

- Dashboard Rendering Engine

...

# ====================================================
# 003 WidgetDataSource
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** WidgetDataSource

**Classification:** Configuration Table

**Aggregate Root:** DashboardWidget

---

# Purpose

WidgetDataSource defines the source from which a Dashboard Widget retrieves its data.

The Dashboard Engine remains completely independent from business domains by consuming data through configurable data sources.

Supported data sources include:

- SQL View
- Stored Procedure
- REST API
- Materialized View
- Cache
- SignalR Stream
- Elasticsearch
- Redis Cache

---

# Dependencies

Depends On

- DashboardWidget

Referenced By

- Dashboard Rendering Engine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WidgetDataSourceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardWidgetId | BIGINT | No | | | ✔ | Widget |
| DataSourceType | SMALLINT | No | | | | View / Procedure / API |
| ConnectionName | NVARCHAR(100) | Yes | NULL | | | Connection |
| DataSourceName | NVARCHAR(300) | No | | | | Object Name |
| ParameterJson | NVARCHAR(MAX) | Yes | NULL | | | Runtime Parameters |
| RefreshIntervalSeconds | INT | No | 300 | | | Refresh Rate |
| CacheDurationSeconds | INT | No | 60 | | | Cache Time |
| IsRealTime | BIT | No | 0 | | | SignalR |
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

PK_WidgetDataSource

## Foreign Keys

- DashboardWidgetId → DashboardWidget

---

# Indexes

## Clustered

PK_WidgetDataSource

## Non Clustered

IX_Widget

IX_DataSourceType

IX_RealTime

---

# Relationships

DashboardWidget (1) → WidgetDataSource (One)

---

# Business Rules

- One active data source per widget.
- Real-time widgets use SignalR.
- Cache duration configurable.
- Parameters stored as JSON.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WidgetDataSourceCreated
- WidgetDataSourceUpdated

---

# Developer Notes

- Decouples UI from business logic.
- Enables dynamic widget rendering.

---

# ====================================================
# 004 DashboardLayout
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardLayout

**Classification:** Configuration Table

**Aggregate Root:** Dashboard

---

# Purpose

DashboardLayout stores the visual arrangement of widgets.

Layouts are independent of widget definitions, allowing users to switch layouts without modifying widget configuration.

Supports:

- Desktop Layout
- Tablet Layout
- Mobile Layout

---

# Dependencies

Depends On

- Dashboard

Referenced By

- Dashboard Renderer

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardLayoutId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
| LayoutJson | NVARCHAR(MAX) | No | | | | Grid Layout |
| ScreenType | SMALLINT | No | | | | Desktop / Tablet / Mobile |
| VersionNo | INT | No | 1 | | | Layout Version |
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

PK_DashboardLayout

## Foreign Keys

- DashboardId → Dashboard

---

# Indexes

## Clustered

PK_DashboardLayout

## Non Clustered

IX_Dashboard

IX_ScreenType

---

# Relationships

Dashboard (1) → DashboardLayout (Many)

---

# Business Rules

- One layout per screen type.
- Layout stored as JSON.
- Version maintained automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardLayoutUpdated

---

# Developer Notes

- Supports responsive dashboard rendering.
- Enables drag-and-drop persistence.

---

# ====================================================
# 005 DashboardFilter
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardFilter

**Classification:** Configuration Table

**Aggregate Root:** Dashboard

---

# Purpose

DashboardFilter stores reusable dashboard-level filters.

Filters automatically apply to all compatible widgets.

Examples include:

- Company
- Branch
- Fiscal Year
- Warehouse
- Customer
- Vendor
- Date Range
- Product Category

---

# Dependencies

Depends On

- Dashboard

Referenced By

- DashboardWidget

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardFilterId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
| FilterName | NVARCHAR(150) | No | | | | Filter Name |
| DisplayName | NVARCHAR(200) | No | | | | UI Caption |
| DataType | SMALLINT | No | | | | Date / Lookup / Text |
| DefaultValue | NVARCHAR(MAX) | Yes | NULL | | | Default |
| LookupSource | NVARCHAR(300) | Yes | NULL | | | Lookup API |
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

PK_DashboardFilter

## Foreign Keys

- DashboardId → Dashboard

---

# Indexes

## Clustered

PK_DashboardFilter

## Non Clustered

IX_Dashboard

IX_DisplayOrder

---

# Relationships

Dashboard (1) → DashboardFilter (Many)

---

# Business Rules

- Filters shared across widgets.
- Display Order controls UI.
- Lookup filters support APIs.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardFilterCreated
- DashboardFilterUpdated

---

# Developer Notes

- Enables global dashboard filtering.

---

# ====================================================
# 006 DashboardFavorite
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardFavorite

**Classification:** User Configuration

**Aggregate Root:** Dashboard

---

# Purpose

DashboardFavorite stores dashboards bookmarked by users for quick access.

Users may have multiple favorites while only one dashboard may be marked as the default landing dashboard.

---

# Dependencies

Depends On

- Dashboard
- User

Referenced By

- Dashboard Home Page

...

# ====================================================
# 006 DashboardFavorite
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardFavorite

**Classification:** User Configuration

**Aggregate Root:** Dashboard

---

# Purpose

DashboardFavorite stores dashboards bookmarked by users for quick access.

Users can maintain multiple favorite dashboards while selecting one as their default landing dashboard after login.

This improves user productivity by providing immediate access to frequently used dashboards.

---

# Dependencies

Depends On

- Dashboard
- User

Referenced By

- Dashboard Home Page

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardFavoriteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
| UserId | BIGINT | No | | | ✔ | User |
| IsDefault | BIT | No | 0 | | | Default Dashboard |
| DisplayOrder | INT | No | 1 | | | Favorites Order |
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

PK_DashboardFavorite

## Foreign Keys

- DashboardId → Dashboard
- UserId → User

## Unique Keys

- UQ_User_Dashboard (UserId, DashboardId)

---

# Indexes

## Clustered

PK_DashboardFavorite

## Non Clustered

IX_User

IX_Dashboard

IX_Default

---

# Relationships

Dashboard (1) → DashboardFavorite (Many)

User (1) → DashboardFavorite (Many)

---

# Business Rules

- Multiple favorites allowed.
- Only one default dashboard per user.
- Default dashboard loads after login.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardFavorited
- DashboardUnfavorited
- DefaultDashboardChanged

---

# Developer Notes

- Improves dashboard accessibility.
- Supports personalized home pages.

---

# ====================================================
# 007 DashboardSnapshot
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardSnapshot

**Classification:** Transaction Table

**Aggregate Root:** Dashboard

---

# Purpose

DashboardSnapshot stores captured dashboard data at a specific point in time.

Snapshots provide historical dashboard views for trend analysis and auditing without recalculating historical KPIs.

Examples include:

- Daily Executive Dashboard
- Month-End Financial Dashboard
- Inventory Closing Snapshot
- Weekly Sales Summary

---

# Dependencies

Depends On

- Dashboard

Referenced By

- Dashboard History
- Trend Analysis

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardSnapshotId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
| SnapshotDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Snapshot Date |
| SnapshotJson | NVARCHAR(MAX) | No | | | | Serialized Dashboard Data |
| GeneratedBy | BIGINT | Yes | NULL | | ✔ | User |
| SnapshotType | SMALLINT | No | | | | Manual / Scheduled |
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

PK_DashboardSnapshot

## Foreign Keys

- DashboardId → Dashboard
- GeneratedBy → User

---

# Indexes

## Clustered

PK_DashboardSnapshot

## Non Clustered

IX_Dashboard

IX_SnapshotDate

IX_SnapshotType

---

# Relationships

Dashboard (1) → DashboardSnapshot (Many)

---

# Business Rules

- Snapshots are immutable.
- Historical snapshots cannot be modified.
- Scheduled snapshots supported.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardSnapshotCreated

---

# Developer Notes

- Used for historical KPI comparisons.
- Supports executive trend analysis.

---

# ====================================================
# 008 DashboardAttachment
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Dashboards with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Dashboard Background Images
- Company Logos
- User Guides
- Widget Icons
- Dashboard Documentation

---

# Dependencies

Depends On

- Dashboard
- Attachment

Referenced By

- Dashboard Designer

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
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
| RowVersion | ROWVERSION | No | Auto | | | Optimistic Concurrency |

---

# Constraints

## Primary Key

PK_DashboardAttachment

## Foreign Keys

- DashboardId → Dashboard
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_DashboardAttachment

## Non Clustered

IX_Dashboard

IX_Attachment

---

# Relationships

Dashboard (1) → DashboardAttachment (Many)

Attachment (1) → DashboardAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardAttachmentAdded
- DashboardAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Used by Dashboard Designer.

...

# ====================================================
# 009 DashboardNote
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

DashboardNote associates Dashboards with reusable Note records maintained within the Shared Kernel.

Dashboard Notes provide documentation, design decisions and operational information without duplicating note storage.

Examples include:

- Dashboard Description
- KPI Calculation Notes
- Business Rules
- Widget Documentation
- Administrator Comments
- Change History
- Performance Notes

---

# Dependencies

Depends On

- Dashboard
- Note

Referenced By

- Dashboard Designer
- Administration
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
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

PK_DashboardNote

## Foreign Keys

- DashboardId → Dashboard
- NoteId → Note

## Unique Keys

- UQ_Dashboard_Note (DashboardId, NoteId)

---

# Indexes

## Clustered

PK_DashboardNote

## Non Clustered

IX_Dashboard

IX_Note

IX_Status

---

# Relationships

Dashboard (1) → DashboardNote (Many)

Note (1) → DashboardNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Notes remain reusable within Shared Kernel.
- Dashboard owns only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardNoteAdded
- DashboardNoteUpdated
- DashboardNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports dashboard documentation.

---

# ====================================================
# 010 DashboardActivity
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

DashboardActivity associates Dashboards with reusable Activity records maintained within the Shared Kernel.

Activities capture operational events throughout the dashboard lifecycle.

Examples include:

- Dashboard Created
- Dashboard Published
- Widget Added
- Widget Removed
- Layout Updated
- Dashboard Shared
- Snapshot Generated
- Dashboard Archived

---

# Dependencies

Depends On

- Dashboard
- Activity

Referenced By

- Dashboard Activity Feed
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
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

PK_DashboardActivity

## Foreign Keys

- DashboardId → Dashboard
- ActivityId → Activity

## Unique Keys

- UQ_Dashboard_Activity (DashboardId, ActivityId)

---

# Indexes

## Clustered

PK_DashboardActivity

## Non Clustered

IX_Dashboard

IX_Activity

IX_Status

---

# Relationships

Dashboard (1) → DashboardActivity (Many)

Activity (1) → DashboardActivity (Many)

---

# Business Rules

- Activities are append-only.
- Dashboard history cannot be modified.
- Shared Activity reused throughout ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardActivityCreated
- DashboardActivityUpdated

---

# Developer Notes

- Integrates with Activity Center.
- Provides dashboard audit history.

---

# ====================================================
# 011 DashboardTimeline
# ====================================================

# Table Classification

**Domain:** Dashboard Domain

**Table Name:** DashboardTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

DashboardTimeline associates Dashboards with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological history of every dashboard event from creation until retirement.

Examples include:

- Dashboard Created
- Widget Added
- Layout Changed
- Filter Updated
- Shared
- Published
- Snapshot Generated
- Archived

---

# Dependencies

Depends On

- Dashboard
- Timeline

Referenced By

- Dashboard Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DashboardTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| DashboardId | BIGINT | No | | | ✔ | Dashboard |
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

PK_DashboardTimeline

## Foreign Keys

- DashboardId → Dashboard
- TimelineId → Timeline

## Unique Keys

- UQ_Dashboard_Timeline (DashboardId, TimelineId)

---

# Indexes

## Clustered

PK_DashboardTimeline

## Non Clustered

IX_Dashboard

IX_Timeline

IX_Status

---

# Relationships

Dashboard (1) → DashboardTimeline (Many)

Timeline (1) → DashboardTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Dashboard Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DashboardTimelineCreated
- DashboardTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for dashboard history visualization.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Dashboard Domain provides configurable, role-based dashboards that present real-time and analytical information from every business domain.

Unlike the Reporting Domain, which focuses on report generation, the Dashboard Domain focuses on visualization, KPIs, charts, gauges and interactive business intelligence.

---

## Aggregate Roots

- Dashboard

---

## Supporting Entities

- DashboardWidget
- WidgetDataSource
- DashboardLayout
- DashboardFilter
- DashboardFavorite
- DashboardSnapshot

---

## Bridge Entities

- DashboardAttachment
- DashboardNote
- DashboardActivity
- DashboardTimeline

---

## Major Business Capabilities

- Executive Dashboards
- Operational Dashboards
- KPI Cards
- Charts & Graphs
- Gauges
- Real-Time Widgets
- Dashboard Layout Designer
- Responsive Layouts
- Global Dashboard Filters
- Dashboard Favorites
- Historical Snapshots
- Shared Dashboards
- Role-Based Dashboards
- Shared Kernel Integration

---

## Published Domain Events

The Dashboard Domain publishes events including:

- DashboardCreated
- DashboardPublished
- WidgetAdded
- WidgetRemoved
- DashboardLayoutUpdated
- DashboardSnapshotCreated
- DashboardFavorited
- DefaultDashboardChanged

These events integrate with:

- Administration Domain
- Customer Domain
- Vendor Domain
- Product Domain
- Inventory Domain
- Warehouse Domain
- Rental Domain
- Service Domain
- Purchase Domain
- Sales Domain
- Accounting Domain
- Reporting Domain
- Notification Domain

---

## Integration Points

The Dashboard Domain integrates directly with:

- Foundation
- Shared Kernel
- Reporting Domain
- Notification Domain
- Administration Domain
- Customer Domain
- Vendor Domain
- Product Domain
- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Inventory Domain
- Warehouse Domain
- Accounting Domain

---

# Dashboard Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. Dashboard
2. DashboardWidget
3. WidgetDataSource
4. DashboardLayout
5. DashboardFilter
6. DashboardFavorite
7. DashboardSnapshot
8. DashboardAttachment
9. DashboardNote
10. DashboardActivity
11. DashboardTimeline

---
