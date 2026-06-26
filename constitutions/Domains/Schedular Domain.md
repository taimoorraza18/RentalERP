# RentalERP v1.0

# SchedulerDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Scheduler

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Scheduler Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. ScheduledJob

6. JobExecution

7. JobParameter

---

# Domain Overview

The Scheduler Domain is responsible for executing background and recurring operations throughout RentalERP.

It centralizes all scheduled processing, including recurring jobs, batch processing, recurring reports, notification delivery, synchronization, housekeeping tasks, backups and maintenance activities.

The Scheduler Domain abstracts the underlying scheduling framework (Hangfire, Quartz.NET, Azure Functions, Windows Service, etc.) from the business domains.

---

# Business Objectives

The Scheduler Domain provides:

- Scheduled Jobs
- Background Processing
- Recurring Jobs
- Batch Processing
- Queue Processing
- Retry Scheduling
- Job Prioritization
- Execution History
- Cron Scheduling
- Maintenance Jobs
- Automatic Cleanup
- Report Scheduling
- Notification Scheduling
- Synchronization Scheduling

---

# Aggregate Root

## Primary Aggregate Root

- ScheduledJob

## Supporting Entities

- JobExecution
- JobParameter
- JobQueue
- JobRetry
- JobDependency

## Bridge Entities

- SchedulerAttachment
- SchedulerNote
- SchedulerActivity
- SchedulerTimeline

---

# Implementation Order

001 ScheduledJob

002 JobExecution

003 JobParameter

004 JobQueue

005 JobRetry

006 JobDependency

007 SchedulerWorker

008 SchedulerAttachment

009 SchedulerNote

010 SchedulerActivity

011 SchedulerTimeline

---

# ====================================================
# 001 ScheduledJob
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** ScheduledJob

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

ScheduledJob defines a background task executed automatically by the scheduling engine.

Examples include:

- Daily Database Backup
- Nightly Inventory Recalculation
- Weekly Report Generation
- Monthly Depreciation
- Daily Rental Reminder
- Hourly Synchronization
- Notification Dispatch
- Cleanup Old Logs

Each job contains scheduling information independent of its execution history.

---

# Dependencies

Depends On

- Company

Referenced By

- JobExecution
- JobParameter
- JobQueue

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ScheduledJobId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| JobCode | NVARCHAR(50) | No | | | | Job Code |
| JobName | NVARCHAR(250) | No | | | | Job Name |
| JobType | SMALLINT | No | | | | Recurring / One Time |
| CronExpression | NVARCHAR(100) | Yes | NULL | | | Cron Schedule |
| NextExecutionDate | DATETIME2(7) | Yes | NULL | | | Next Run |
| LastExecutionDate | DATETIME2(7) | Yes | NULL | | | Last Run |
| IsEnabled | BIT | No | 1 | | | Enabled |
| Priority | SMALLINT | No | 5 | | | Priority |
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

PK_ScheduledJob

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Job_Code

---

# Indexes

## Clustered

PK_ScheduledJob

## Non Clustered

IX_JobCode

IX_NextExecutionDate

IX_IsEnabled

IX_Priority

---

# Relationships

ScheduledJob (1) → JobExecution (Many)

ScheduledJob (1) → JobParameter (Many)

ScheduledJob (1) → JobQueue (Many)

---

# Business Rules

- Job Code must be unique.
- Disabled jobs cannot execute.
- Cron Expression required for recurring jobs.
- One-time jobs execute only once.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ScheduledJobCreated
- ScheduledJobEnabled
- ScheduledJobDisabled

---

# Developer Notes

- Aggregate Root of Scheduler Domain.
- Independent of scheduling framework implementation.

---

# ====================================================
# 002 JobExecution
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobExecution

**Classification:** Transaction Table

**Aggregate Root:** ScheduledJob

---

# Purpose

JobExecution records every execution attempt of a scheduled job.

Each execution stores timing, duration, result and diagnostic information.

Examples include:

- Started
- Running
- Completed
- Failed
- Cancelled
- Retried

Execution history enables monitoring, troubleshooting and reporting.

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- JobRetry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JobExecutionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
| ExecutionStart | DATETIME2(7) | No | SYSUTCDATETIME() | | | Start Time |
| ExecutionEnd | DATETIME2(7) | Yes | NULL | | | End Time |
| DurationMilliseconds | BIGINT | No | 0 | | | Duration |
| ExecutionStatus | SMALLINT | No | | | | Running / Success / Failed |
| ServerName | NVARCHAR(200) | Yes | NULL | | | Worker Server |
| WorkerName | NVARCHAR(200) | Yes | NULL | | | Worker |
| ErrorMessage | NVARCHAR(MAX) | Yes | NULL | | | Error |
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

PK_JobExecution

## Foreign Keys

- ScheduledJobId → ScheduledJob

---

# Indexes

## Clustered

PK_JobExecution

## Non Clustered

IX_ScheduledJob

IX_ExecutionStart

IX_ExecutionStatus

---

# Relationships

ScheduledJob (1) → JobExecution (Many)

---

# Business Rules

- Every execution recorded.
- Execution history immutable.
- Errors retained for troubleshooting.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JobExecutionStarted
- JobExecutionCompleted
- JobExecutionFailed

---

# Developer Notes

- Used for monitoring dashboards.
- Supports SLA reporting.

---

# ====================================================
# 003 JobParameter
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobParameter

**Classification:** Configuration Table

**Aggregate Root:** ScheduledJob

---

# Purpose

JobParameter stores configurable parameters passed to scheduled jobs during execution.

Examples include:

- CompanyId
- FiscalYearId
- ReportId
- WarehouseId
- EmailTemplateId
- RetentionDays
- BatchSize

Job parameters allow the same job implementation to execute for different contexts.

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- Scheduler Engine

...

# ====================================================
# 003 JobParameter
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobParameter

**Classification:** Configuration Table

**Aggregate Root:** ScheduledJob

---

# Purpose

JobParameter stores configurable parameters supplied to scheduled jobs during execution.

This allows a single job implementation to execute with different runtime configurations.

Examples include:

- CompanyId
- WarehouseId
- FiscalYearId
- ReportId
- EmailTemplateId
- NotificationType
- BatchSize
- RetentionDays

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- Scheduler Engine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JobParameterId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
| ParameterName | NVARCHAR(200) | No | | | | Parameter Name |
| ParameterValue | NVARCHAR(MAX) | Yes | NULL | | | Parameter Value |
| DataType | NVARCHAR(50) | No | | | | String / Int / Date |
| IsEncrypted | BIT | No | 0 | | | Encrypt Value |
| DisplayOrder | INT | No | 1 | | | UI Order |
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

PK_JobParameter

## Foreign Keys

- ScheduledJobId → ScheduledJob

---

# Indexes

## Clustered

PK_JobParameter

## Non Clustered

IX_ScheduledJob

IX_ParameterName

---

# Relationships

ScheduledJob (1) → JobParameter (Many)

---

# Business Rules

- Parameter names must be unique within a job.
- Sensitive values may be encrypted.
- Parameters loaded before execution.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JobParameterCreated
- JobParameterUpdated

---

# Developer Notes

- Enables reusable job implementations.
- Supports dynamic job configuration.

---

# ====================================================
# 004 JobQueue
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobQueue

**Classification:** Transaction Table

**Aggregate Root:** ScheduledJob

---

# Purpose

JobQueue stores jobs waiting for execution.

The Scheduler Engine continuously monitors the queue and dispatches jobs to available workers according to priority.

Examples include:

- Email Queue
- Report Queue
- Notification Queue
- Synchronization Queue
- Cleanup Queue

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- SchedulerWorker

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JobQueueId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
| QueueName | NVARCHAR(150) | No | | | | Queue Name |
| QueuePriority | SMALLINT | No | 5 | | | Priority |
| EnqueuedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Enqueued |
| ScheduledExecutionDate | DATETIME2(7) | No | | | | Execute At |
| QueueStatus | SMALLINT | No | | | | Waiting / Processing / Completed |
| LockedByWorker | NVARCHAR(200) | Yes | NULL | | | Worker |
| LockDate | DATETIME2(7) | Yes | NULL | | | Lock Time |
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

PK_JobQueue

## Foreign Keys

- ScheduledJobId → ScheduledJob

---

# Indexes

## Clustered

PK_JobQueue

## Non Clustered

IX_QueueName

IX_ScheduledExecutionDate

IX_QueueStatus

IX_QueuePriority

---

# Relationships

ScheduledJob (1) → JobQueue (Many)

---

# Business Rules

- Higher priority jobs execute first.
- Locked jobs cannot be processed by another worker.
- Queue records retained for auditing.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JobQueued
- JobDequeued
- JobProcessingStarted

---

# Developer Notes

- Supports distributed workers.
- Queue locking prevents duplicate execution.

---

# ====================================================
# 005 JobRetry
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobRetry

**Classification:** Transaction Table

**Aggregate Root:** JobExecution

---

# Purpose

JobRetry stores retry attempts for failed job executions.

Retry behavior is configurable using retry policies and exponential backoff.

Examples include:

- Network Failure
- Database Timeout
- API Failure
- Deadlock
- Temporary Service Unavailable

---

# Dependencies

Depends On

- JobExecution

Referenced By

- Monitoring Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JobRetryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JobExecutionId | BIGINT | No | | | ✔ | Job Execution |
| RetryNumber | SMALLINT | No | 1 | | | Attempt |
| RetryDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Retry Time |
| RetryReason | NVARCHAR(1000) | Yes | NULL | | | Reason |
| RetryStatus | SMALLINT | No | | | | Success / Failed |
| NextRetryDate | DATETIME2(7) | Yes | NULL | | | Scheduled Retry |
| ErrorMessage | NVARCHAR(MAX) | Yes | NULL | | | Error Details |
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

PK_JobRetry

## Foreign Keys

- JobExecutionId → JobExecution

---

# Indexes

## Clustered

PK_JobRetry

## Non Clustered

IX_JobExecution

IX_RetryStatus

IX_NextRetryDate

---

# Relationships

JobExecution (1) → JobRetry (Many)

---

# Business Rules

- Retry attempts are immutable.
- Maximum retries configurable.
- Retry history retained permanently.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JobRetryStarted
- JobRetrySucceeded
- JobRetryFailed

---

# Developer Notes

- Supports retry policies.
- Enables automatic recovery.

...

# ====================================================
# 006 JobDependency
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** JobDependency

**Classification:** Configuration Table

**Aggregate Root:** ScheduledJob

---

# Purpose

JobDependency defines execution dependencies between scheduled jobs.

A dependent job cannot execute until all prerequisite jobs have completed successfully.

Examples include:

- Inventory Recalculation → Financial Posting
- Database Backup → Log Cleanup
- Customer Synchronization → Report Generation
- Import Products → Inventory Update

This ensures business workflows execute in the correct sequence.

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- Scheduler Engine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JobDependencyId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Parent Job |
| DependsOnJobId | BIGINT | No | | | ✔ | Required Job |
| DependencyType | SMALLINT | No | | | | Success / Completion |
| IsMandatory | BIT | No | 1 | | | Mandatory Dependency |
| DisplayOrder | INT | No | 1 | | | Evaluation Order |
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

PK_JobDependency

## Foreign Keys

- ScheduledJobId → ScheduledJob
- DependsOnJobId → ScheduledJob

---

# Indexes

## Clustered

PK_JobDependency

## Non Clustered

IX_ScheduledJob

IX_DependsOnJob

---

# Relationships

ScheduledJob (1) → JobDependency (Many)

ScheduledJob (1) → ScheduledJob (Self Reference)

---

# Business Rules

- Circular dependencies are prohibited.
- Dependencies evaluated before execution.
- Multiple dependencies supported.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JobDependencyCreated
- JobDependencyUpdated

---

# Developer Notes

- Supports workflow orchestration.
- Enables execution chaining.

---

# ====================================================
# 007 SchedulerWorker
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** SchedulerWorker

**Classification:** Monitoring Table

**Aggregate Root:** ScheduledJob

---

# Purpose

SchedulerWorker stores information about worker processes executing scheduled jobs.

Supports distributed processing across multiple servers.

Examples include:

- Hangfire Server
- Quartz Worker
- Windows Service
- Kubernetes Worker
- Azure Function Worker

---

# Dependencies

Depends On

- ScheduledJob

Referenced By

- JobQueue
- JobExecution

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SchedulerWorkerId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkerName | NVARCHAR(200) | No | | | | Worker Name |
| ServerName | NVARCHAR(200) | No | | | | Server |
| IPAddress | NVARCHAR(50) | Yes | NULL | | | IP Address |
| WorkerVersion | NVARCHAR(100) | Yes | NULL | | | Version |
| LastHeartbeat | DATETIME2(7) | No | SYSUTCDATETIME() | | | Heartbeat |
| ActiveJobs | INT | No | 0 | | | Running Jobs |
| WorkerStatus | SMALLINT | No | | | | Online / Offline |
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

PK_SchedulerWorker

---

# Indexes

## Clustered

PK_SchedulerWorker

## Non Clustered

IX_WorkerStatus

IX_LastHeartbeat

IX_ServerName

---

# Relationships

Referenced by JobExecution and JobQueue through WorkerName.

---

# Business Rules

- Heartbeat updated periodically.
- Offline workers cannot receive jobs.
- Worker statistics refreshed automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkerRegistered
- WorkerHeartbeatReceived
- WorkerOfflineDetected

---

# Developer Notes

- Supports distributed execution.
- Used by Scheduler Monitoring Dashboard.

---

# ====================================================
# 008 SchedulerAttachment
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** SchedulerAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Scheduled Jobs with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Batch Scripts
- Configuration Files
- Job Documentation
- Execution Manuals
- SQL Scripts
- Maintenance Instructions

---

# Dependencies

Depends On

- ScheduledJob
- Attachment

Referenced By

- Scheduler Administration

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SchedulerAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
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

PK_SchedulerAttachment

## Foreign Keys

- ScheduledJobId → ScheduledJob
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_SchedulerAttachment

## Non Clustered

IX_ScheduledJob

IX_Attachment

---

# Relationships

ScheduledJob (1) → SchedulerAttachment (Many)

Attachment (1) → SchedulerAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused across ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SchedulerAttachmentAdded
- SchedulerAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores operational documentation.

...

# ====================================================
# 009 SchedulerNote
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** SchedulerNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SchedulerNote associates Scheduled Jobs with reusable Note records maintained within the Shared Kernel.

Scheduler Notes document operational procedures, troubleshooting guidance, execution instructions and maintenance information without modifying the job definition itself.

Examples include:

- Job Description
- Deployment Instructions
- Maintenance Notes
- Troubleshooting Steps
- Operational Remarks
- Execution Guidelines

---

# Dependencies

Depends On

- ScheduledJob
- Note

Referenced By

- Scheduler Administration
- Operations Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SchedulerNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
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

PK_SchedulerNote

## Foreign Keys

- ScheduledJobId → ScheduledJob
- NoteId → Note

## Unique Keys

- UQ_Scheduler_Note (ScheduledJobId, NoteId)

---

# Indexes

## Clustered

PK_SchedulerNote

## Non Clustered

IX_ScheduledJob

IX_Note

IX_Status

---

# Relationships

ScheduledJob (1) → SchedulerNote (Many)

Note (1) → SchedulerNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Notes remain reusable within Shared Kernel.
- Scheduled Jobs own only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SchedulerNoteAdded
- SchedulerNoteUpdated
- SchedulerNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports operational documentation.

---

# ====================================================
# 010 SchedulerActivity
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** SchedulerActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SchedulerActivity associates Scheduled Jobs with reusable Activity records maintained within the Shared Kernel.

Activities record operational events occurring throughout the lifecycle of scheduled jobs.

Examples include:

- Job Created
- Job Enabled
- Job Disabled
- Job Started
- Job Completed
- Job Failed
- Job Retried
- Job Cancelled

---

# Dependencies

Depends On

- ScheduledJob
- Activity

Referenced By

- Scheduler Dashboard
- Operations Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SchedulerActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
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

PK_SchedulerActivity

## Foreign Keys

- ScheduledJobId → ScheduledJob
- ActivityId → Activity

## Unique Keys

- UQ_Scheduler_Activity (ScheduledJobId, ActivityId)

---

# Indexes

## Clustered

PK_SchedulerActivity

## Non Clustered

IX_ScheduledJob

IX_Activity

IX_Status

---

# Relationships

ScheduledJob (1) → SchedulerActivity (Many)

Activity (1) → SchedulerActivity (Many)

---

# Business Rules

- Activities are append-only.
- Operational history cannot be modified.
- Shared Activity reused throughout ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SchedulerActivityCreated
- SchedulerActivityUpdated

---

# Developer Notes

- Integrates with Operations Dashboard.
- Maintains execution history.

---

# ====================================================
# 011 SchedulerTimeline
# ====================================================

# Table Classification

**Domain:** Scheduler Domain

**Table Name:** SchedulerTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SchedulerTimeline associates Scheduled Jobs with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of every scheduled job event.

Examples include:

- Job Created
- Job Scheduled
- Queue Created
- Worker Assigned
- Execution Started
- Execution Completed
- Retry Performed
- Job Disabled

---

# Dependencies

Depends On

- ScheduledJob
- Timeline

Referenced By

- Scheduler Detail Screen
- Timeline Widget
- Monitoring Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SchedulerTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ScheduledJobId | BIGINT | No | | | ✔ | Scheduled Job |
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

PK_SchedulerTimeline

## Foreign Keys

- ScheduledJobId → ScheduledJob
- TimelineId → Timeline

## Unique Keys

- UQ_Scheduler_Timeline (ScheduledJobId, TimelineId)

---

# Indexes

## Clustered

PK_SchedulerTimeline

## Non Clustered

IX_ScheduledJob

IX_Timeline

IX_Status

---

# Relationships

ScheduledJob (1) → SchedulerTimeline (Many)

Timeline (1) → SchedulerTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Scheduler Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SchedulerTimelineCreated
- SchedulerTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for background processing history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Scheduler Domain centralizes all background processing within RentalERP.

It manages recurring jobs, queues, execution history, retries, worker monitoring and job dependencies while remaining independent of the underlying scheduling framework (Hangfire, Quartz.NET, Azure Functions or Windows Services).

---

## Aggregate Roots

- ScheduledJob

---

## Supporting Entities

- JobExecution
- JobParameter
- JobQueue
- JobRetry
- JobDependency
- SchedulerWorker

---

## Bridge Entities

- SchedulerAttachment
- SchedulerNote
- SchedulerActivity
- SchedulerTimeline

---

## Major Business Capabilities

- Recurring Jobs
- One-Time Jobs
- Cron Scheduling
- Queue Management
- Background Processing
- Worker Management
- Job Retry Policies
- Job Dependencies
- Execution Monitoring
- Distributed Processing
- Scheduled Maintenance
- Shared Kernel Integration

---

## Published Domain Events

The Scheduler Domain publishes events including:

- ScheduledJobCreated
- ScheduledJobEnabled
- ScheduledJobDisabled
- JobExecutionStarted
- JobExecutionCompleted
- JobExecutionFailed
- JobQueued
- JobRetryStarted
- WorkerRegistered
- WorkerOfflineDetected

These events integrate with:

- Notification Domain
- Integration Domain
- Reporting Domain
- Audit Domain
- Dashboard Domain
- Workflow Domain
- All Business Domains requiring background processing

---

## Integration Points

The Scheduler Domain integrates directly with:

- Foundation
- Shared Kernel
- Notification Domain
- Integration Domain
- Reporting Domain
- Dashboard Domain
- Workflow Domain
- Audit Domain
- Security Domain
- All Business Domains

---

# Scheduler Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. ScheduledJob
2. JobExecution
3. JobParameter
4. JobQueue
5. JobRetry
6. JobDependency
7. SchedulerWorker
8. SchedulerAttachment
9. SchedulerNote
10. SchedulerActivity
11. SchedulerTimeline

---
