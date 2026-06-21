# RentalERP v1.0

# PlatformDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Platform

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Platform Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. PlatformEnvironment

6. ApplicationVersion

7. License

---

# Domain Overview

The Platform Domain provides infrastructure-level capabilities shared across the entire RentalERP ecosystem.

Unlike business domains that manage operational data, the Platform Domain manages application lifecycle, environments, licensing, deployments, health monitoring, diagnostics, infrastructure metrics and tenant-level platform information.

It provides the operational foundation required to run RentalERP reliably in on-premises, cloud and SaaS deployments.

---

# Business Objectives

The Platform Domain provides:

- Environment Management
- Application Versioning
- Licensing
- Tenant Management
- Deployment Tracking
- Health Monitoring
- Platform Diagnostics
- Infrastructure Metrics
- System Maintenance
- Platform Configuration
- Application Lifecycle Management
- Infrastructure Metadata

---

# Aggregate Root

## Primary Aggregate Root

- PlatformEnvironment

## Supporting Entities

- ApplicationVersion
- License
- Tenant
- Deployment
- PlatformMetric
- HealthCheck

## Bridge Entities

- PlatformAttachment
- PlatformNote
- PlatformActivity
- PlatformTimeline

---

# Implementation Order

001 PlatformEnvironment

002 ApplicationVersion

003 License

004 Tenant

005 Deployment

006 PlatformMetric

007 HealthCheck

008 PlatformAttachment

009 PlatformNote

010 PlatformActivity

011 PlatformTimeline

---

# ====================================================
# 001 PlatformEnvironment
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformEnvironment

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

PlatformEnvironment represents the execution environment where RentalERP is deployed.

Supported environments include:

- Development
- Testing
- UAT
- Staging
- Production
- Disaster Recovery

Each environment maintains its own deployment information, health status and operational metadata.

---

# Dependencies

Depends On

- Company

Referenced By

- ApplicationVersion
- Deployment
- HealthCheck

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformEnvironmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| EnvironmentCode | NVARCHAR(50) | No | | | | DEV / UAT / PROD |
| EnvironmentName | NVARCHAR(200) | No | | | | Display Name |
| EnvironmentType | SMALLINT | No | | | | Development / Production |
| BaseUrl | NVARCHAR(1000) | Yes | NULL | | | Application URL |
| DatabaseServer | NVARCHAR(250) | Yes | NULL | | | Database Server |
| IsProduction | BIT | No | 0 | | | Production Environment |
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

PK_PlatformEnvironment

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Environment_Code

---

# Indexes

## Clustered

PK_PlatformEnvironment

## Non Clustered

IX_EnvironmentCode

IX_EnvironmentType

IX_IsProduction

---

# Relationships

PlatformEnvironment (1) → ApplicationVersion (Many)

PlatformEnvironment (1) → Deployment (Many)

PlatformEnvironment (1) → HealthCheck (Many)

---

# Business Rules

- Environment Code must be unique.
- Only one production environment per company.
- Production environments cannot be deleted.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformEnvironmentCreated
- PlatformEnvironmentUpdated
- ProductionEnvironmentChanged

---

# Developer Notes

- Aggregate Root of Platform Domain.
- Used by deployment pipeline.

---

# ====================================================
# 002 ApplicationVersion
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** ApplicationVersion

**Classification:** Master Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

ApplicationVersion stores every version of RentalERP deployed across environments.

Supports semantic versioning and release management.

Examples include:

- v1.0.0
- v1.1.0
- v2.0.0-beta
- Hotfix Releases
- Patch Releases

---

# Dependencies

Depends On

- PlatformEnvironment

Referenced By

- Deployment

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ApplicationVersionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Environment |
| VersionNumber | NVARCHAR(50) | No | | | | Semantic Version |
| BuildNumber | NVARCHAR(100) | Yes | NULL | | | Build |
| ReleaseDate | DATETIME2(7) | No | | | | Release Date |
| ReleaseNotes | NVARCHAR(MAX) | Yes | NULL | | | Notes |
| IsCurrent | BIT | No | 0 | | | Current Version |
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

PK_ApplicationVersion

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment

---

# Indexes

## Clustered

PK_ApplicationVersion

## Non Clustered

IX_VersionNumber

IX_IsCurrent

IX_ReleaseDate

---

# Relationships

PlatformEnvironment (1) → ApplicationVersion (Many)

---

# Business Rules

- Only one current version per environment.
- Version numbers follow semantic versioning.
- Release history retained permanently.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ApplicationVersionReleased
- ApplicationVersionActivated

---

# Developer Notes

- Supports CI/CD deployments.
- Tracks software releases.

---

# ====================================================
# 003 License
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** License

**Classification:** Master Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

License manages software licensing and subscription information for RentalERP deployments.

Supports:

- Perpetual License
- Subscription License
- SaaS License
- Trial License
- Enterprise License

Each license controls system capabilities and expiration policies.

---

# Dependencies

Depends On

- PlatformEnvironment

Referenced By

- Tenant
- Security Domain

...

# ====================================================
# 003 License
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** License

**Classification:** Master Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

License manages software licensing, subscription plans and entitlement validation for RentalERP deployments.

The licensing system determines which modules, features and capacities are available to a customer.

Supported license types include:

- Trial
- Standard
- Professional
- Enterprise
- SaaS Subscription
- Perpetual

---

# Dependencies

Depends On

- PlatformEnvironment

Referenced By

- Tenant
- System Configuration
- Security Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| LicenseId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
| LicenseKey | NVARCHAR(500) | No | | | | Encrypted License Key |
| LicenseType | SMALLINT | No | | | | Trial / Subscription / Enterprise |
| CustomerName | NVARCHAR(250) | No | | | | Customer |
| MaxUsers | INT | No | 10 | | | Licensed Users |
| MaxCompanies | INT | No | 1 | | | Company Limit |
| ActivationDate | DATETIME2(7) | No | | | | Activated |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiration |
| IsActive | BIT | No | 1 | | | Active |
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

PK_License

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment

## Unique Keys

- UQ_License_Key

---

# Indexes

## Clustered

PK_License

## Non Clustered

IX_LicenseKey

IX_ExpiryDate

IX_IsActive

---

# Relationships

PlatformEnvironment (1) → License (Many)

---

# Business Rules

- License Key must be unique.
- Expired licenses become inactive automatically.
- User count cannot exceed licensed limit.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- LicenseActivated
- LicenseExpired
- LicenseRenewed

---

# Developer Notes

- Supports offline license validation.
- Encrypted license storage required.

---

# ====================================================
# 004 Tenant
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** Tenant

**Classification:** Master Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

Tenant represents an isolated organization hosted within RentalERP.

Supports SaaS multi-tenancy while maintaining complete data isolation.

Examples include:

- ABC Construction
- XYZ Rentals
- Demo Tenant
- Internal Tenant

---

# Dependencies

Depends On

- PlatformEnvironment
- License

Referenced By

- Every Business Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| TenantId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Environment |
| LicenseId | BIGINT | No | | | ✔ | License |
| TenantCode | NVARCHAR(50) | No | | | | Tenant Code |
| TenantName | NVARCHAR(250) | No | | | | Tenant Name |
| DatabaseName | NVARCHAR(250) | No | | | | Database |
| DomainName | NVARCHAR(250) | Yes | NULL | | | Custom Domain |
| IsActive | BIT | No | 1 | | | Active |
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

PK_Tenant

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- LicenseId → License

---

# Indexes

## Clustered

PK_Tenant

## Non Clustered

IX_TenantCode

IX_DatabaseName

IX_IsActive

---

# Relationships

PlatformEnvironment (1) → Tenant (Many)

License (1) → Tenant (Many)

---

# Business Rules

- Tenant Code must be unique.
- Database isolation required.
- Inactive tenants cannot authenticate.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- TenantCreated
- TenantActivated
- TenantSuspended

---

# Developer Notes

- Supports SaaS deployments.
- Enables multi-tenant architecture.

---

# ====================================================
# 005 Deployment
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** Deployment

**Classification:** Transaction Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

Deployment records every application deployment performed within RentalERP.

Examples include:

- Initial Installation
- Production Release
- Hotfix
- Rollback
- Patch Deployment
- Database Migration

Deployment history enables release management and auditing.

---

# Dependencies

Depends On

- PlatformEnvironment
- ApplicationVersion

Referenced By

- HealthCheck

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| DeploymentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Environment |
| ApplicationVersionId | BIGINT | No | | | ✔ | Version |
| DeploymentDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Deployment Time |
| DeploymentType | SMALLINT | No | | | | Release / Patch / Rollback |
| DeploymentStatus | SMALLINT | No | | | | Success / Failed |
| ExecutedBy | NVARCHAR(200) | No | | | | User / Pipeline |
| DurationSeconds | INT | No | 0 | | | Duration |
| DeploymentNotes | NVARCHAR(MAX) | Yes | NULL | | | Notes |
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

PK_Deployment

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- ApplicationVersionId → ApplicationVersion

---

# Indexes

## Clustered

PK_Deployment

## Non Clustered

IX_DeploymentDate

IX_DeploymentStatus

IX_ApplicationVersion

---

# Relationships

PlatformEnvironment (1) → Deployment (Many)

ApplicationVersion (1) → Deployment (Many)

---

# Business Rules

- Every deployment permanently recorded.
- Rollback deployments retained.
- Deployment history immutable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DeploymentStarted
- DeploymentCompleted
- DeploymentFailed
- DeploymentRolledBack

---

# Developer Notes

- Integrates with CI/CD pipelines.
- Supports release auditing.

...

# ====================================================
# 006 PlatformMetric
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformMetric

**Classification:** Monitoring Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

PlatformMetric stores infrastructure and application performance metrics collected from the RentalERP platform.

These metrics enable real-time monitoring, capacity planning and operational analytics.

Examples include:

- CPU Usage
- Memory Usage
- Disk Usage
- Database Connections
- Active Users
- API Requests
- Background Jobs
- Response Time

---

# Dependencies

Depends On

- PlatformEnvironment

Referenced By

- Dashboard Domain
- Reporting Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformMetricId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
| MetricName | NVARCHAR(150) | No | | | | Metric Name |
| MetricCategory | NVARCHAR(100) | No | | | | Infrastructure / Database / API |
| MetricValue | DECIMAL(18,4) | No | 0 | | | Value |
| Unit | NVARCHAR(50) | Yes | NULL | | | %, MB, Seconds |
| RecordedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Recorded Date |
| WarningThreshold | DECIMAL(18,4) | Yes | NULL | | | Warning |
| CriticalThreshold | DECIMAL(18,4) | Yes | NULL | | | Critical |
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

PK_PlatformMetric

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment

---

# Indexes

## Clustered

PK_PlatformMetric

## Non Clustered

IX_PlatformEnvironment

IX_MetricName

IX_RecordedDate

IX_MetricCategory

---

# Relationships

PlatformEnvironment (1) → PlatformMetric (Many)

---

# Business Rules

- Metrics are append-only.
- Historical metrics retained for trend analysis.
- Thresholds generate alerts.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformMetricRecorded
- MetricThresholdExceeded
- PlatformMetricArchived

---

# Developer Notes

- Supports infrastructure monitoring.
- Used by Dashboard Domain.

---

# ====================================================
# 007 HealthCheck
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** HealthCheck

**Classification:** Monitoring Table

**Aggregate Root:** PlatformEnvironment

---

# Purpose

HealthCheck records periodic health assessments of the RentalERP platform.

Health checks verify the operational status of application components and infrastructure.

Examples include:

- Database Connectivity
- API Availability
- Redis Cache
- Background Workers
- SMTP Service
- File Storage
- External APIs

---

# Dependencies

Depends On

- PlatformEnvironment

Referenced By

- Dashboard Domain
- Scheduler Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| HealthCheckId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
| ComponentName | NVARCHAR(200) | No | | | | Component |
| CheckDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Check Time |
| IsHealthy | BIT | No | 1 | | | Healthy |
| ResponseTimeMs | INT | No | 0 | | | Response Time |
| FailureReason | NVARCHAR(1000) | Yes | NULL | | | Failure |
| Severity | SMALLINT | No | | | | Low / Medium / High |
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

PK_HealthCheck

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment

---

# Indexes

## Clustered

PK_HealthCheck

## Non Clustered

IX_PlatformEnvironment

IX_CheckDate

IX_IsHealthy

IX_ComponentName

---

# Relationships

PlatformEnvironment (1) → HealthCheck (Many)

---

# Business Rules

- Health checks are append-only.
- Health status automatically refreshed.
- Critical failures generate alerts.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- HealthCheckCompleted
- PlatformHealthy
- PlatformUnhealthy

---

# Developer Notes

- Supports monitoring dashboards.
- Integrates with Notification Domain.

---

# ====================================================
# 008 PlatformAttachment
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Platform Environments with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Deployment Guides
- Infrastructure Diagrams
- License Files
- SSL Certificates
- Architecture Documents
- Disaster Recovery Plans

---

# Dependencies

Depends On

- PlatformEnvironment
- Attachment

Referenced By

- Platform Administration

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
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

PK_PlatformAttachment

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_PlatformAttachment

## Non Clustered

IX_PlatformEnvironment

IX_Attachment

---

# Relationships

PlatformEnvironment (1) → PlatformAttachment (Many)

Attachment (1) → PlatformAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformAttachmentAdded
- PlatformAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores infrastructure documentation.

...

# ====================================================
# 009 PlatformNote
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

PlatformNote associates Platform Environments with reusable Note records maintained within the Shared Kernel.

Platform Notes document infrastructure procedures, deployment guidance, maintenance instructions and operational observations without modifying the platform configuration itself.

Examples include:

- Deployment Notes
- Environment Configuration
- Maintenance Procedures
- Infrastructure Documentation
- Disaster Recovery Notes
- Operational Remarks

---

# Dependencies

Depends On

- PlatformEnvironment
- Note

Referenced By

- Platform Administration
- Operations Team

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
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

PK_PlatformNote

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- NoteId → Note

## Unique Keys

- UQ_Platform_Note (PlatformEnvironmentId, NoteId)

---

# Indexes

## Clustered

PK_PlatformNote

## Non Clustered

IX_PlatformEnvironment

IX_Note

IX_Status

---

# Relationships

PlatformEnvironment (1) → PlatformNote (Many)

Note (1) → PlatformNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Shared Note reused throughout ERP.
- Platform owns only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformNoteAdded
- PlatformNoteUpdated
- PlatformNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports operational documentation.

---

# ====================================================
# 010 PlatformActivity
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

PlatformActivity associates Platform Environments with reusable Activity records maintained within the Shared Kernel.

Activities record infrastructure and operational events occurring throughout the platform lifecycle.

Examples include:

- Environment Created
- Deployment Started
- Deployment Completed
- License Activated
- Health Check Failed
- Platform Updated
- Maintenance Started
- Maintenance Completed

---

# Dependencies

Depends On

- PlatformEnvironment
- Activity

Referenced By

- Platform Dashboard
- Operations Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
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

PK_PlatformActivity

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- ActivityId → Activity

## Unique Keys

- UQ_Platform_Activity (PlatformEnvironmentId, ActivityId)

---

# Indexes

## Clustered

PK_PlatformActivity

## Non Clustered

IX_PlatformEnvironment

IX_Activity

IX_Status

---

# Relationships

PlatformEnvironment (1) → PlatformActivity (Many)

Activity (1) → PlatformActivity (Many)

---

# Business Rules

- Activities are append-only.
- Operational history cannot be modified.
- Shared Activity reused throughout ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformActivityCreated
- PlatformActivityUpdated

---

# Developer Notes

- Integrates with Platform Dashboard.
- Maintains operational history.

---

# ====================================================
# 011 PlatformTimeline
# ====================================================

# Table Classification

**Domain:** Platform Domain

**Table Name:** PlatformTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

PlatformTimeline associates Platform Environments with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of every infrastructure and platform event.

Examples include:

- Environment Created
- Version Released
- Deployment Completed
- License Activated
- Tenant Created
- Health Check Failed
- Maintenance Started
- Maintenance Completed

---

# Dependencies

Depends On

- PlatformEnvironment
- Timeline

Referenced By

- Platform Detail Screen
- Timeline Widget
- Dashboard Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PlatformTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| PlatformEnvironmentId | BIGINT | No | | | ✔ | Platform Environment |
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

PK_PlatformTimeline

## Foreign Keys

- PlatformEnvironmentId → PlatformEnvironment
- TimelineId → Timeline

## Unique Keys

- UQ_Platform_Timeline (PlatformEnvironmentId, TimelineId)

---

# Indexes

## Clustered

PK_PlatformTimeline

## Non Clustered

IX_PlatformEnvironment

IX_Timeline

IX_Status

---

# Relationships

PlatformEnvironment (1) → PlatformTimeline (Many)

Timeline (1) → PlatformTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Platform Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PlatformTimelineCreated
- PlatformTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for infrastructure history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Platform Domain provides infrastructure-level services required to operate RentalERP in cloud, on-premises and SaaS environments.

It manages environments, application versions, licensing, tenants, deployments, monitoring, health checks and infrastructure metrics while providing a complete operational history for platform administration.

---

## Aggregate Roots

- PlatformEnvironment

---

## Supporting Entities

- ApplicationVersion
- License
- Tenant
- Deployment
- PlatformMetric
- HealthCheck

---

## Bridge Entities

- PlatformAttachment
- PlatformNote
- PlatformActivity
- PlatformTimeline

---

## Major Business Capabilities

- Environment Management
- Application Versioning
- License Management
- Tenant Management
- Deployment Tracking
- Infrastructure Monitoring
- Health Checks
- Platform Metrics
- Operational Diagnostics
- SaaS Multi-Tenancy
- Release Management
- Shared Kernel Integration

---

## Published Domain Events

The Platform Domain publishes events including:

- PlatformEnvironmentCreated
- ApplicationVersionReleased
- LicenseActivated
- LicenseExpired
- TenantCreated
- DeploymentCompleted
- DeploymentFailed
- PlatformMetricRecorded
- HealthCheckCompleted
- PlatformHealthy
- PlatformUnhealthy

These events integrate with:

- Dashboard Domain
- Notification Domain
- Security Domain
- Audit Domain
- Scheduler Domain
- Integration Domain
- Reporting Domain
- All Business Domains

---

## Integration Points

The Platform Domain integrates directly with:

- Foundation
- Shared Kernel
- Security Domain
- Audit Domain
- Notification Domain
- Scheduler Domain
- Integration Domain
- Dashboard Domain
- Reporting Domain
- System Configuration Domain
- All Business Domains

---

# Platform Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. PlatformEnvironment
2. ApplicationVersion
3. License
4. Tenant
5. Deployment
6. PlatformMetric
7. HealthCheck
8. PlatformAttachment
9. PlatformNote
10. PlatformActivity
11. PlatformTimeline

---

# 🎉 RentalERP Domain Documentation Status

**Status:** ✅ All Planned Domains Complete

**Total Domains:** 24

**Total Domain Documents:** 24

**Architecture:** Domain Driven Design (DDD)

**Coverage:** Complete Enterprise Rental ERP

---

# Next Documents to Generate

With all domain documents complete, the recommended enterprise architecture documents are:

1. **MasterERD.docx** — Complete database ERD across all domains.
2. **CrossDomainRelationshipMatrix.docx** — Relationships and dependencies between all aggregates.
3. **AggregateBoundaryMap.docx** — Aggregate roots, entity ownership and bounded contexts.
4. **DomainEventCatalog.docx** — Complete list of published and subscribed domain events.
5. **DatabaseDictionary.docx** — Master data dictionary for every table and column.
6. **ImplementationRoadmap.docx** — Suggested implementation order and milestones.
7. **ArchitectureDecisionRecords (ADR).docx** — Key architectural decisions and rationale.

# Platform Domain Status

**Status:** ✅ Complete
