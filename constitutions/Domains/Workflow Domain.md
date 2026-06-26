# RentalERP v1.0

# WorkflowDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Workflow

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Workflow Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. WorkflowDefinition

6. WorkflowStep

7. WorkflowApprover

---

# Domain Overview

The Workflow Domain provides a configurable approval engine for RentalERP.

Instead of embedding approval logic inside Purchase, Sales, Rental, Service or Accounting modules, all approval processes are defined centrally within this domain.

Business documents simply initiate workflows, while the Workflow Domain controls approvals, rejections, escalations, delegation and completion.

The engine is completely metadata-driven, allowing administrators to modify approval processes without changing application code.

---

# Business Objectives

The Workflow Domain provides:

- Workflow Definitions
- Approval Steps
- Dynamic Approvers
- Approval Routing
- Conditional Workflows
- Parallel Approvals
- Sequential Approvals
- Workflow Instances
- Approval History
- Delegation
- Escalation
- SLA Monitoring
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Root

- WorkflowDefinition

## Supporting Entities

- WorkflowStep
- WorkflowApprover
- WorkflowInstance
- WorkflowAction
- WorkflowEscalation
- WorkflowDelegation

## Bridge Entities

- WorkflowAttachment
- WorkflowNote
- WorkflowActivity
- WorkflowTimeline

---

# Implementation Order

001 WorkflowDefinition

002 WorkflowStep

003 WorkflowApprover

004 WorkflowInstance

005 WorkflowAction

006 WorkflowEscalation

007 WorkflowDelegation

008 WorkflowAttachment

009 WorkflowNote

010 WorkflowActivity

011 WorkflowTimeline

---

# ====================================================
# 001 WorkflowDefinition
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowDefinition

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

WorkflowDefinition defines approval processes used throughout RentalERP.

Each workflow belongs to a specific business document type and determines how approvals should progress.

Examples include:

- Purchase Order Approval
- Purchase Invoice Approval
- Sales Order Approval
- Rental Contract Approval
- Service Order Approval
- Inventory Adjustment Approval
- Asset Disposal Approval
- Journal Entry Approval

---

# Dependencies

Depends On

- Company

Referenced By

- WorkflowStep
- WorkflowInstance

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowDefinitionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| WorkflowCode | NVARCHAR(30) | No | | | | Workflow Code |
| WorkflowName | NVARCHAR(200) | No | | | | Workflow Name |
| ModuleName | NVARCHAR(100) | No | | | | Purchase / Sales / Rental |
| EntityName | NVARCHAR(100) | No | | | | PurchaseOrder |
| Description | NVARCHAR(1000) | Yes | NULL | | | Description |
| IsActive | BIT | No | 1 | | | Active |
| IsSystem | BIT | No | 0 | | | System Workflow |
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

PK_WorkflowDefinition

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Workflow_Code

---

# Indexes

## Clustered

PK_WorkflowDefinition

## Non Clustered

IX_WorkflowCode

IX_Module

IX_Entity

IX_Status

---

# Relationships

WorkflowDefinition (1) → WorkflowStep (Many)

WorkflowDefinition (1) → WorkflowInstance (Many)

---

# Business Rules

- Workflow Code must be unique.
- System Workflows cannot be deleted.
- Only one active workflow per Entity.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowCreated
- WorkflowUpdated
- WorkflowActivated
- WorkflowDeactivated

---

# Developer Notes

- Root configuration for approval engine.
- Consumed by all business domains.

---

# ====================================================
# 002 WorkflowStep
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowStep

**Classification:** Master Detail

**Aggregate Root:** WorkflowDefinition

---

# Purpose

WorkflowStep defines each approval stage within a Workflow Definition.

A workflow may contain one or many approval steps executed sequentially or in parallel.

Examples include:

- Department Manager
- Finance Manager
- General Manager
- CEO Approval
- Auto Approval

---

# Dependencies

Depends On

- WorkflowDefinition

Referenced By

- WorkflowApprover
- WorkflowInstance

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowStepId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowDefinitionId | BIGINT | No | | | ✔ | Workflow |
| StepNo | INT | No | | | | Step Number |
| StepName | NVARCHAR(200) | No | | | | Approval Step |
| ApprovalType | SMALLINT | No | | | | Sequential / Parallel |
| MinimumApprovals | INT | No | 1 | | | Required Approvals |
| AutoApprove | BIT | No | 0 | | | Auto Approval |
| AllowReject | BIT | No | 1 | | | Reject Allowed |
| AllowSkip | BIT | No | 0 | | | Skip Allowed |
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

PK_WorkflowStep

## Foreign Keys

- WorkflowDefinitionId → WorkflowDefinition

## Unique Keys

- UQ_Workflow_StepNo

---

# Indexes

## Clustered

PK_WorkflowStep

## Non Clustered

IX_Workflow

IX_StepNo

IX_Status

---

# Relationships

WorkflowDefinition (1) → WorkflowStep (Many)

WorkflowStep (1) → WorkflowApprover (Many)

---

# Business Rules

- Step Numbers must be sequential.
- Minimum Approvals cannot exceed assigned approvers.
- Auto Approval bypasses manual approval.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowStepCreated
- WorkflowStepUpdated

---

# Developer Notes

- Supports unlimited workflow depth.
- Supports sequential and parallel approvals.

---

# ====================================================
# 003 WorkflowApprover
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowApprover

**Classification:** Master Detail

**Aggregate Root:** WorkflowDefinition

---

# Purpose

WorkflowApprover defines who is authorized to approve a specific Workflow Step.

Approvers may be:

- Individual Employees
- Roles
- Departments
- Designations
- Dynamic Approvers (e.g., Document Owner's Manager)

The approval engine resolves dynamic approvers at runtime.

---

# Dependencies

Depends On

- WorkflowStep
- Employee
- Role

Referenced By

- WorkflowInstance

...

# ====================================================
# 003 WorkflowApprover
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowApprover

**Classification:** Master Detail

**Aggregate Root:** WorkflowDefinition

---

# Purpose

WorkflowApprover defines who is authorized to approve a specific Workflow Step.

Approvers may be assigned using multiple strategies:

- Employee
- Role
- Department
- Designation
- Reporting Manager
- Department Head
- Document Owner's Manager
- Dynamic Expression

This flexibility allows business workflows to remain configurable without application code changes.

---

# Dependencies

Depends On

- WorkflowStep
- Employee
- Role
- Department
- Designation

Referenced By

- WorkflowInstance
- WorkflowAction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowApproverId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowStepId | BIGINT | No | | | ✔ | Workflow Step |
| ApproverType | SMALLINT | No | | | | Employee / Role / Department / Dynamic |
| EmployeeId | BIGINT | Yes | NULL | | ✔ | Employee |
| RoleId | BIGINT | Yes | NULL | | ✔ | Role |
| DepartmentId | BIGINT | Yes | NULL | | ✔ | Department |
| DesignationId | BIGINT | Yes | NULL | | ✔ | Designation |
| DynamicExpression | NVARCHAR(500) | Yes | NULL | | | Runtime Resolver |
| Priority | SMALLINT | No | 1 | | | Resolution Priority |
| IsMandatory | BIT | No | 1 | | | Mandatory Approval |
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

PK_WorkflowApprover

## Foreign Keys

- WorkflowStepId → WorkflowStep
- EmployeeId → Employee
- RoleId → Role
- DepartmentId → Department
- DesignationId → Designation

---

## Indexes

### Clustered

PK_WorkflowApprover

### Non Clustered

IX_WorkflowStep

IX_Employee

IX_Role

IX_Department

---

# Relationships

WorkflowStep (1) → WorkflowApprover (Many)

---

# Business Rules

- At least one approver required.
- Dynamic approvers resolved at runtime.
- Priority controls approver resolution.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowApproverAssigned
- WorkflowApproverRemoved

---

# Developer Notes

- Supports organization hierarchy.
- Supports rule-based approver resolution.

---

# ====================================================
# 004 WorkflowInstance
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowInstance

**Classification:** Transaction Header

**Aggregate Root:** WorkflowDefinition

---

# Purpose

WorkflowInstance represents a running approval process for a specific business document.

Each Purchase Order, Sales Order, Rental Contract or Journal Entry requiring approval creates its own Workflow Instance.

The instance tracks approval progress until completion.

---

# Dependencies

Depends On

- WorkflowDefinition
- WorkflowStep

Referenced By

- WorkflowAction
- WorkflowEscalation
- WorkflowDelegation

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowInstanceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowDefinitionId | BIGINT | No | | | ✔ | Workflow |
| EntityName | NVARCHAR(100) | No | | | | PurchaseOrder |
| EntityId | BIGINT | No | | | | Document Id |
| CurrentStepId | BIGINT | Yes | NULL | | ✔ | Active Step |
| WorkflowStatus | SMALLINT | No | | | | Running / Approved / Rejected / Cancelled |
| StartedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Started |
| CompletedDate | DATETIME2(7) | Yes | NULL | | | Completed |
| InitiatedBy | BIGINT | No | | | ✔ | Employee |
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

PK_WorkflowInstance

## Foreign Keys

- WorkflowDefinitionId → WorkflowDefinition
- CurrentStepId → WorkflowStep
- InitiatedBy → Employee

---

## Indexes

### Clustered

PK_WorkflowInstance

### Non Clustered

IX_Workflow

IX_Entity

IX_Status

IX_CurrentStep

---

# Relationships

WorkflowDefinition (1) → WorkflowInstance (Many)

WorkflowInstance (1) → WorkflowAction (Many)

---

# Business Rules

- One active workflow per document.
- Completed workflows become read-only.
- Rejected workflow ends immediately unless restart configured.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowStarted
- WorkflowCompleted
- WorkflowRejected
- WorkflowCancelled

---

# Developer Notes

- Runtime workflow engine.
- Drives approval state transitions.

---

# ====================================================
# 005 WorkflowAction
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowAction

**Classification:** Transaction Detail

**Aggregate Root:** WorkflowInstance

---

# Purpose

WorkflowAction records every action performed during workflow execution.

Examples include:

- Approved
- Rejected
- Returned
- Delegated
- Escalated
- Cancelled
- Auto Approved

Each action forms part of the permanent approval history.

---

# Dependencies

Depends On

- WorkflowInstance
- WorkflowStep
- Employee

Referenced By

- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowActionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
| WorkflowStepId | BIGINT | No | | | ✔ | Workflow Step |
| EmployeeId | BIGINT | No | | | ✔ | Action By |
| ActionType | SMALLINT | No | | | | Approve / Reject / Return |
| ActionDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Action Date |
| Remarks | NVARCHAR(1000) | Yes | NULL | | | Comments |
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

PK_WorkflowAction

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- WorkflowStepId → WorkflowStep
- EmployeeId → Employee

---

## Indexes

### Clustered

PK_WorkflowAction

### Non Clustered

IX_WorkflowInstance

IX_WorkflowStep

IX_Employee

IX_ActionDate

---

# Relationships

WorkflowInstance (1) → WorkflowAction (Many)

WorkflowStep (1) → WorkflowAction (Many)

---

# Business Rules

- Actions are immutable.
- Every approval decision recorded.
- Remarks mandatory for rejection.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowApproved
- WorkflowRejected
- WorkflowReturned
- WorkflowDelegated

---

# Developer Notes

- Permanent approval audit history.
- Supports workflow analytics.

...

# ====================================================
# 006 WorkflowEscalation
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowEscalation

**Classification:** Transaction Table

**Aggregate Root:** WorkflowInstance

---

# Purpose

WorkflowEscalation defines escalation rules and records escalation events when workflow approvals exceed configured Service Level Agreements (SLAs).

Escalations ensure business documents are processed within expected timelines by notifying supervisors, reassigning approvals or automatically escalating to higher authorities.

Examples include:

- Notify Manager after 24 hours
- Escalate to Department Head after 48 hours
- Escalate to CEO after 72 hours
- Auto Reject after 7 days

---

# Dependencies

Depends On

- WorkflowInstance
- WorkflowStep
- Employee

Referenced By

- Notification Module
- Dashboard
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowEscalationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
| WorkflowStepId | BIGINT | No | | | ✔ | Workflow Step |
| EscalatedToEmployeeId | BIGINT | Yes | NULL | | ✔ | Escalated Employee |
| EscalationLevel | SMALLINT | No | 1 | | | Level |
| EscalationReason | NVARCHAR(500) | Yes | NULL | | | Reason |
| EscalationDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Escalation Date |
| SLAHours | INT | No | 0 | | | SLA Threshold |
| IsResolved | BIT | No | 0 | | | Resolved |
| ResolvedDate | DATETIME2(7) | Yes | NULL | | | Resolution Date |
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

PK_WorkflowEscalation

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- WorkflowStepId → WorkflowStep
- EscalatedToEmployeeId → Employee

---

# Indexes

## Clustered

PK_WorkflowEscalation

## Non Clustered

IX_WorkflowInstance

IX_WorkflowStep

IX_EscalationDate

IX_Resolved

---

# Relationships

WorkflowInstance (1) → WorkflowEscalation (Many)

WorkflowStep (1) → WorkflowEscalation (Many)

---

# Business Rules

- Escalation generated automatically by Scheduler.
- Multiple escalation levels supported.
- Escalations remain in history after resolution.
- SLA monitored continuously.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowEscalated
- WorkflowEscalationResolved

---

# Developer Notes

- Integrates with Notification Module.
- Supports configurable SLA policies.

---

# ====================================================
# 007 WorkflowDelegation
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowDelegation

**Classification:** Transaction Table

**Aggregate Root:** WorkflowInstance

---

# Purpose

WorkflowDelegation allows an approver to temporarily delegate approval authority to another employee.

Delegations support leave management, vacations, business travel and temporary reassignment without interrupting approval processes.

---

# Dependencies

Depends On

- WorkflowInstance
- Employee

Referenced By

- WorkflowAction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowDelegationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
| FromEmployeeId | BIGINT | No | | | ✔ | Original Approver |
| ToEmployeeId | BIGINT | No | | | ✔ | Delegate |
| DelegationReason | NVARCHAR(500) | Yes | NULL | | | Reason |
| EffectiveFrom | DATETIME2(7) | No | | | | Start Date |
| EffectiveTo | DATETIME2(7) | No | | | | End Date |
| IsRevoked | BIT | No | 0 | | | Revoked |
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

PK_WorkflowDelegation

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- FromEmployeeId → Employee
- ToEmployeeId → Employee

## Check Constraints

- EffectiveTo >= EffectiveFrom

---

# Indexes

## Clustered

PK_WorkflowDelegation

## Non Clustered

IX_WorkflowInstance

IX_FromEmployee

IX_ToEmployee

IX_EffectiveFrom

---

# Relationships

WorkflowInstance (1) → WorkflowDelegation (Many)

Employee (1) → WorkflowDelegation (Many)

---

# Business Rules

- Delegator and Delegate cannot be the same employee.
- Delegation valid only during effective period.
- Revoked delegations become inactive immediately.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowDelegated
- WorkflowDelegationRevoked

---

# Developer Notes

- Supports temporary authority transfer.
- Integrated with Leave Management.

---

# ====================================================
# 008 WorkflowAttachment
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Workflow Instances with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Approval Documents
- Supporting Files
- Contracts
- Signed Forms
- Compliance Documents

---

# Dependencies

Depends On

- WorkflowInstance
- Attachment

Referenced By

- Workflow Detail Screen

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
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

PK_WorkflowAttachment

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_WorkflowAttachment

## Non Clustered

IX_WorkflowInstance

IX_Attachment

---

# Relationships

WorkflowInstance (1) → WorkflowAttachment (Many)

Attachment (1) → WorkflowAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Attachment records remain in Shared Kernel.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowAttachmentAdded
- WorkflowAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Used for approval supporting documents.

...

# ====================================================
# 009 WorkflowNote
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

WorkflowNote associates Workflow Instances with reusable Note records maintained within the Shared Kernel.

Workflow Notes provide a centralized mechanism for documenting approval discussions, rejection reasons, escalation comments and administrative observations without storing duplicate note content.

Examples include:

- Approval Comments
- Rejection Reasons
- Escalation Notes
- Compliance Remarks
- Internal Discussions
- Auditor Comments
- Exception Justifications

---

# Dependencies

Depends On

- WorkflowInstance
- Note

Referenced By

- Workflow Detail Screen
- Approval History
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
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

PK_WorkflowNote

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- NoteId → Note

## Unique Keys

- UQ_Workflow_Note (WorkflowInstanceId, NoteId)

---

# Indexes

## Clustered

PK_WorkflowNote

## Non Clustered

IX_WorkflowInstance

IX_Note

IX_Status

---

# Relationships

WorkflowInstance (1) → WorkflowNote (Many)

Note (1) → WorkflowNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Notes remain reusable within Shared Kernel.
- Workflow owns only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowNoteAdded
- WorkflowNoteUpdated
- WorkflowNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports complete approval documentation.

---

# ====================================================
# 010 WorkflowActivity
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

WorkflowActivity associates Workflow Instances with reusable Activity records maintained within the Shared Kernel.

Activities record every operational event occurring during workflow execution.

Examples include:

- Workflow Started
- Step Assigned
- Approval Granted
- Approval Rejected
- Workflow Returned
- Escalated
- Delegated
- Workflow Completed
- Workflow Cancelled

---

# Dependencies

Depends On

- WorkflowInstance
- Activity

Referenced By

- Workflow Dashboard
- Workflow Engine
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
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

PK_WorkflowActivity

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- ActivityId → Activity

## Unique Keys

- UQ_Workflow_Activity (WorkflowInstanceId, ActivityId)

---

# Indexes

## Clustered

PK_WorkflowActivity

## Non Clustered

IX_WorkflowInstance

IX_Activity

IX_Status

---

# Relationships

WorkflowInstance (1) → WorkflowActivity (Many)

Activity (1) → WorkflowActivity (Many)

---

# Business Rules

- Activities are append-only.
- Workflow history cannot be modified.
- Shared Activity reused throughout ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowActivityCreated
- WorkflowActivityUpdated

---

# Developer Notes

- Integrates with Workflow Engine.
- Provides complete approval audit history.

---

# ====================================================
# 011 WorkflowTimeline
# ====================================================

# Table Classification

**Domain:** Workflow Domain

**Table Name:** WorkflowTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

WorkflowTimeline associates Workflow Instances with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a chronological history of every workflow event from initiation until completion.

Examples include:

- Workflow Created
- Step Assigned
- Approval Completed
- Rejected
- Returned
- Escalated
- Delegated
- Completed
- Cancelled

---

# Dependencies

Depends On

- WorkflowInstance
- Timeline

Referenced By

- Workflow Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WorkflowTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| WorkflowInstanceId | BIGINT | No | | | ✔ | Workflow Instance |
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

PK_WorkflowTimeline

## Foreign Keys

- WorkflowInstanceId → WorkflowInstance
- TimelineId → Timeline

## Unique Keys

- UQ_Workflow_Timeline (WorkflowInstanceId, TimelineId)

---

# Indexes

## Clustered

PK_WorkflowTimeline

## Non Clustered

IX_WorkflowInstance

IX_Timeline

IX_Status

---

# Relationships

WorkflowInstance (1) → WorkflowTimeline (Many)

Timeline (1) → WorkflowTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Workflow Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WorkflowTimelineCreated
- WorkflowTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for approval history visualization.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Workflow Domain provides a centralized, configurable approval engine for RentalERP.

Business domains initiate workflows but never contain approval logic themselves. Workflow definitions, approval routing, escalations, delegations and audit history are managed entirely within this domain.

The engine supports sequential approvals, parallel approvals, dynamic approvers, SLA monitoring and complete traceability.

---

## Aggregate Roots

- WorkflowDefinition

---

## Supporting Entities

- WorkflowStep
- WorkflowApprover
- WorkflowInstance
- WorkflowAction
- WorkflowEscalation
- WorkflowDelegation

---

## Bridge Entities

- WorkflowAttachment
- WorkflowNote
- WorkflowActivity
- WorkflowTimeline

---

## Major Business Capabilities

- Configurable Approval Workflows
- Dynamic Approver Resolution
- Sequential Approvals
- Parallel Approvals
- Conditional Routing
- Workflow Instances
- Approval History
- Delegation
- Escalation & SLA Monitoring
- Workflow Analytics
- Shared Kernel Integration
- Complete Audit Trail

---

## Published Domain Events

The Workflow Domain publishes events including:

- WorkflowStarted
- WorkflowApproved
- WorkflowRejected
- WorkflowReturned
- WorkflowDelegated
- WorkflowEscalated
- WorkflowCompleted
- WorkflowCancelled

These events integrate with:

- Administration Domain
- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Inventory Domain
- Warehouse Domain
- Asset Domain
- Accounting Domain
- Notification Domain
- Dashboard Domain
- Audit Module

---

## Integration Points

The Workflow Domain integrates directly with:

- Foundation
- Shared Kernel
- Administration Domain
- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Inventory Domain
- Warehouse Domain
- Asset Domain
- Accounting Domain
- Notification Domain

---

# Workflow Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. WorkflowDefinition
2. WorkflowStep
3. WorkflowApprover
4. WorkflowInstance
5. WorkflowAction
6. WorkflowEscalation
7. WorkflowDelegation
8. WorkflowAttachment
9. WorkflowNote
10. WorkflowActivity
11. WorkflowTimeline

---
