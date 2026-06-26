# RentalERP v1.0

# IntegrationDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Integration

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Integration Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. IntegrationEndpoint

6. IntegrationConnection

7. WebhookSubscription

---

# Domain Overview

The Integration Domain enables secure communication between RentalERP and external systems.

It provides a centralized integration framework for inbound and outbound APIs, webhooks, file imports/exports, synchronization jobs, event publishing, message queues, and third-party connectors.

Unlike the Notification Domain, which focuses on user communication, the Integration Domain is responsible for system-to-system communication while ensuring reliability, scalability, monitoring, retry mechanisms, and auditability.

---

# Business Objectives

The Integration Domain provides:

- REST API Integrations
- Webhook Management
- Import / Export Jobs
- Message Queue Integration
- Event Publishing
- Event Subscription
- External System Synchronization
- Retry Management
- API Monitoring
- Integration Health Checks
- Third-Party Connectors
- Integration Audit Trail

---

# Aggregate Root

## Primary Aggregate Root

- IntegrationEndpoint

## Supporting Entities

- IntegrationConnection
- WebhookSubscription
- IntegrationJob
- IntegrationMessage
- IntegrationRetry

## Bridge Entities

- IntegrationAttachment
- IntegrationNote
- IntegrationActivity
- IntegrationTimeline

---

# Implementation Order

001 IntegrationEndpoint

002 IntegrationConnection

003 WebhookSubscription

004 IntegrationJob

005 IntegrationMessage

006 IntegrationRetry

007 IntegrationHealth

008 IntegrationAttachment

009 IntegrationNote

010 IntegrationActivity

011 IntegrationTimeline

---

# ====================================================
# 001 IntegrationEndpoint
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationEndpoint

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

IntegrationEndpoint defines every external endpoint that RentalERP communicates with.

Supported endpoint types include:

- REST API
- SOAP Service
- GraphQL API
- Webhook
- FTP
- SFTP
- Azure Service Bus
- RabbitMQ
- Kafka

Each endpoint stores communication details, authentication method, timeout configuration and operational status.

---

# Dependencies

Depends On

- Company

Referenced By

- IntegrationConnection
- IntegrationJob

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationEndpointId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| EndpointCode | NVARCHAR(50) | No | | | | Endpoint Code |
| EndpointName | NVARCHAR(200) | No | | | | Endpoint Name |
| EndpointType | SMALLINT | No | | | | REST / SOAP / Webhook |
| BaseUrl | NVARCHAR(1000) | No | | | | Base URL |
| AuthenticationType | SMALLINT | No | | | | None / Basic / OAuth2 / API Key |
| TimeoutSeconds | INT | No | 30 | | | Timeout |
| IsEnabled | BIT | No | 1 | | | Enabled |
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

PK_IntegrationEndpoint

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Endpoint_Code

---

# Indexes

## Clustered

PK_IntegrationEndpoint

## Non Clustered

IX_EndpointCode

IX_EndpointType

IX_IsEnabled

---

# Relationships

IntegrationEndpoint (1) → IntegrationConnection (Many)

IntegrationEndpoint (1) → IntegrationJob (Many)

---

# Business Rules

- Endpoint Code must be unique.
- Disabled endpoints cannot execute.
- Authentication handled by IntegrationConnection.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationEndpointCreated
- IntegrationEndpointUpdated
- IntegrationEndpointDisabled

---

# Developer Notes

- Aggregate Root for Integration Domain.
- Supports multiple transport protocols.

---

# ====================================================
# 002 IntegrationConnection
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationConnection

**Classification:** Configuration Table

**Aggregate Root:** IntegrationEndpoint

---

# Purpose

IntegrationConnection stores authentication credentials and connection settings for external systems.

Supported authentication methods include:

- API Key
- OAuth 2.0
- JWT
- Basic Authentication
- Certificate Authentication
- Client Credentials

Sensitive values are encrypted before storage.

---

# Dependencies

Depends On

- IntegrationEndpoint

Referenced By

- IntegrationJob
- IntegrationMessage

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationConnectionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Endpoint |
| ConnectionName | NVARCHAR(200) | No | | | | Connection Name |
| AuthenticationType | SMALLINT | No | | | | OAuth2 / API Key |
| ClientId | NVARCHAR(500) | Yes | NULL | | | Client Id |
| ClientSecret | NVARCHAR(MAX) | Yes | NULL | | | Encrypted Secret |
| AccessToken | NVARCHAR(MAX) | Yes | NULL | | | Cached Token |
| RefreshToken | NVARCHAR(MAX) | Yes | NULL | | | Refresh Token |
| CertificateThumbprint | NVARCHAR(200) | Yes | NULL | | | Certificate |
| TokenExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiry |
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

PK_IntegrationConnection

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint

---

# Indexes

## Clustered

PK_IntegrationConnection

## Non Clustered

IX_Endpoint

IX_AuthenticationType

IX_TokenExpiry

---

# Relationships

IntegrationEndpoint (1) → IntegrationConnection (Many)

---

# Business Rules

- Secrets always encrypted.
- Access tokens refreshed automatically.
- Multiple connections per endpoint allowed.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationConnectionCreated
- IntegrationTokenRefreshed
- IntegrationConnectionUpdated

---

# Developer Notes

- Supports secure credential management.
- Integrates with Security Domain.

---

# ====================================================
# 003 WebhookSubscription
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** WebhookSubscription

**Classification:** Configuration Table

**Aggregate Root:** IntegrationEndpoint

---

# Purpose

WebhookSubscription manages inbound and outbound webhook registrations.

Webhooks allow RentalERP to publish or receive business events in real time.

Examples include:

- Purchase Order Created
- Invoice Paid
- Rental Returned
- Customer Created
- Payment Received
- Inventory Updated

...


# ====================================================
# 003 WebhookSubscription
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** WebhookSubscription

**Classification:** Configuration Table

**Aggregate Root:** IntegrationEndpoint

---

# Purpose

WebhookSubscription manages inbound and outbound webhook registrations.

Webhooks enable RentalERP to publish business events to external systems or receive events from third-party applications in real time.

Examples include:

- Purchase Order Approved
- Sales Invoice Posted
- Rental Contract Created
- Rental Returned
- Payment Received
- Customer Registered
- Inventory Updated
- Service Ticket Closed

---

# Dependencies

Depends On

- IntegrationEndpoint

Referenced By

- IntegrationMessage

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| WebhookSubscriptionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Endpoint |
| EventName | NVARCHAR(200) | No | | | | Published Event |
| WebhookUrl | NVARCHAR(1000) | No | | | | Callback URL |
| SecretKey | NVARCHAR(500) | Yes | NULL | | | HMAC Secret |
| HttpMethod | NVARCHAR(20) | No | POST | | | HTTP Method |
| RetryCount | SMALLINT | No | 3 | | | Retry Attempts |
| TimeoutSeconds | INT | No | 30 | | | Timeout |
| IsEnabled | BIT | No | 1 | | | Enabled |
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

PK_WebhookSubscription

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint

---

# Indexes

## Clustered

PK_WebhookSubscription

## Non Clustered

IX_Endpoint

IX_EventName

IX_IsEnabled

---

# Relationships

IntegrationEndpoint (1) → WebhookSubscription (Many)

---

# Business Rules

- Multiple webhooks may subscribe to one event.
- Secret Key used for HMAC signature verification.
- Disabled subscriptions ignored.
- Retry Count configurable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- WebhookRegistered
- WebhookUpdated
- WebhookDisabled

---

# Developer Notes

- Supports inbound and outbound webhooks.
- HMAC verification recommended.

---

# ====================================================
# 004 IntegrationJob
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationJob

**Classification:** Transaction Table

**Aggregate Root:** IntegrationEndpoint

---

# Purpose

IntegrationJob represents long-running synchronization or integration operations.

Jobs may execute:

- Data Import
- Data Export
- ERP Synchronization
- Customer Synchronization
- Product Synchronization
- Scheduled Batch Jobs

Jobs may execute manually or automatically.

---

# Dependencies

Depends On

- IntegrationEndpoint

Referenced By

- IntegrationMessage
- IntegrationRetry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationJobId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Endpoint |
| JobName | NVARCHAR(250) | No | | | | Job Name |
| JobType | SMALLINT | No | | | | Import / Export / Sync |
| StartDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Started |
| EndDate | DATETIME2(7) | Yes | NULL | | | Completed |
| JobStatus | SMALLINT | No | | | | Pending / Running / Completed / Failed |
| TotalRecords | INT | No | 0 | | | Total Records |
| ProcessedRecords | INT | No | 0 | | | Processed |
| FailedRecords | INT | No | 0 | | | Failed |
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

PK_IntegrationJob

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint

---

# Indexes

## Clustered

PK_IntegrationJob

## Non Clustered

IX_Endpoint

IX_JobStatus

IX_StartDate

---

# Relationships

IntegrationEndpoint (1) → IntegrationJob (Many)

---

# Business Rules

- Jobs execute independently.
- Job history retained permanently.
- Progress updated continuously.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationJobStarted
- IntegrationJobCompleted
- IntegrationJobFailed

---

# Developer Notes

- Supports scheduled and manual execution.
- Integrates with Scheduler Domain.

---

# ====================================================
# 005 IntegrationMessage
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationMessage

**Classification:** Transaction Table

**Aggregate Root:** IntegrationJob

---

# Purpose

IntegrationMessage stores every inbound and outbound message exchanged with external systems.

Examples include:

- REST Request
- REST Response
- Queue Message
- Webhook Payload
- XML Message
- JSON Document

Each message can be independently retried and audited.

---

# Dependencies

Depends On

- IntegrationJob

Referenced By

- IntegrationRetry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationMessageId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationJobId | BIGINT | No | | | ✔ | Integration Job |
| Direction | SMALLINT | No | | | | Inbound / Outbound |
| MessageType | NVARCHAR(100) | No | | | | JSON / XML |
| Payload | NVARCHAR(MAX) | No | | | | Message Body |
| CorrelationId | UNIQUEIDENTIFIER | Yes | NULL | | | Correlation |
| SentDate | DATETIME2(7) | Yes | NULL | | | Sent |
| ReceivedDate | DATETIME2(7) | Yes | NULL | | | Received |
| ProcessingStatus | SMALLINT | No | | | | Pending / Processed / Failed |
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

PK_IntegrationMessage

## Foreign Keys

- IntegrationJobId → IntegrationJob

---

# Indexes

## Clustered

PK_IntegrationMessage

## Non Clustered

IX_IntegrationJob

IX_CorrelationId

IX_ProcessingStatus

---

# Relationships

IntegrationJob (1) → IntegrationMessage (Many)

---

# Business Rules

- Every message retained for auditing.
- Large payloads may be archived.
- CorrelationId links distributed transactions.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationMessageReceived
- IntegrationMessageSent
- IntegrationMessageProcessed
- IntegrationMessageFailed

---

# Developer Notes

- Central message repository.
- Supports distributed tracing.

...

# ====================================================
# 006 IntegrationRetry
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationRetry

**Classification:** Transaction Table

**Aggregate Root:** IntegrationJob

---

# Purpose

IntegrationRetry records every retry attempt performed after an integration failure.

Retries may occur automatically or be triggered manually by an administrator.

Examples include:

- API Timeout
- HTTP 500 Error
- Authentication Failure
- Network Failure
- Queue Processing Failure
- Webhook Delivery Failure

---

# Dependencies

Depends On

- IntegrationJob
- IntegrationMessage

Referenced By

- Integration Dashboard
- Retry Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationRetryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationJobId | BIGINT | No | | | ✔ | Integration Job |
| IntegrationMessageId | BIGINT | No | | | ✔ | Integration Message |
| RetryNumber | SMALLINT | No | 1 | | | Retry Attempt |
| RetryDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Retry Date |
| RetryReason | NVARCHAR(1000) | Yes | NULL | | | Reason |
| RetryStatus | SMALLINT | No | | | | Pending / Success / Failed |
| ResponseCode | INT | Yes | NULL | | | HTTP Status |
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

PK_IntegrationRetry

## Foreign Keys

- IntegrationJobId → IntegrationJob
- IntegrationMessageId → IntegrationMessage

---

# Indexes

## Clustered

PK_IntegrationRetry

## Non Clustered

IX_IntegrationJob

IX_IntegrationMessage

IX_RetryDate

IX_RetryStatus

---

# Relationships

IntegrationJob (1) → IntegrationRetry (Many)

IntegrationMessage (1) → IntegrationRetry (Many)

---

# Business Rules

- Retry attempts are append-only.
- Retry history retained permanently.
- Retry count configurable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationRetryStarted
- IntegrationRetrySucceeded
- IntegrationRetryFailed

---

# Developer Notes

- Supports exponential backoff.
- Integrates with Background Scheduler.

---

# ====================================================
# 007 IntegrationHealth
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationHealth

**Classification:** Monitoring Table

**Aggregate Root:** IntegrationEndpoint

---

# Purpose

IntegrationHealth records the operational health of external integrations.

The monitoring service periodically checks endpoint availability and performance.

Examples include:

- REST API Reachable
- Database Connected
- FTP Available
- Queue Online
- Webhook Healthy

---

# Dependencies

Depends On

- IntegrationEndpoint

Referenced By

- Monitoring Dashboard
- Dashboard Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationHealthId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Endpoint |
| HealthCheckDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Check Date |
| ResponseTimeMs | INT | No | 0 | | | Response Time |
| IsHealthy | BIT | No | 1 | | | Health Status |
| HttpStatusCode | INT | Yes | NULL | | | Status Code |
| FailureReason | NVARCHAR(1000) | Yes | NULL | | | Failure Reason |
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

PK_IntegrationHealth

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint

---

# Indexes

## Clustered

PK_IntegrationHealth

## Non Clustered

IX_Endpoint

IX_HealthCheckDate

IX_IsHealthy

---

# Relationships

IntegrationEndpoint (1) → IntegrationHealth (Many)

---

# Business Rules

- Health checks are append-only.
- Latest record determines endpoint health.
- Monitoring service writes automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationHealthChecked
- IntegrationEndpointOffline
- IntegrationEndpointRecovered

---

# Developer Notes

- Used by health dashboards.
- Supports proactive monitoring.

---

# ====================================================
# 008 IntegrationAttachment
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Integration Endpoints with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- API Specifications
- OpenAPI Documents
- WSDL Files
- Integration Manuals
- Certificates
- Mapping Documents

---

# Dependencies

Depends On

- IntegrationEndpoint
- Attachment

Referenced By

- Integration Configuration

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Integration Endpoint |
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

PK_IntegrationAttachment

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_IntegrationAttachment

## Non Clustered

IX_IntegrationEndpoint

IX_Attachment

---

# Relationships

IntegrationEndpoint (1) → IntegrationAttachment (Many)

Attachment (1) → IntegrationAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationAttachmentAdded
- IntegrationAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores integration documentation.

...

# ====================================================
# 009 IntegrationNote
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

IntegrationNote associates Integration Endpoints with reusable Note records maintained within the Shared Kernel.

These notes document configuration details, deployment instructions, troubleshooting steps and operational observations without modifying the integration configuration itself.

Examples include:

- API Configuration Notes
- Deployment Instructions
- Authentication Details
- Vendor Contact Information
- Troubleshooting Notes
- Version Upgrade Notes
- Operational Remarks

---

# Dependencies

Depends On

- IntegrationEndpoint
- Note

Referenced By

- Integration Administration
- Support Center
- Operations Team

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Integration Endpoint |
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

PK_IntegrationNote

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint
- NoteId → Note

## Unique Keys

- UQ_Integration_Note (IntegrationEndpointId, NoteId)

---

# Indexes

## Clustered

PK_IntegrationNote

## Non Clustered

IX_IntegrationEndpoint

IX_Note

IX_Status

---

# Relationships

IntegrationEndpoint (1) → IntegrationNote (Many)

Note (1) → IntegrationNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Shared Note reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationNoteAdded
- IntegrationNoteUpdated
- IntegrationNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores operational documentation.

---

# ====================================================
# 010 IntegrationActivity
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

IntegrationActivity associates Integration Endpoints with reusable Activity records maintained within the Shared Kernel.

Activities capture operational events generated throughout the integration lifecycle.

Examples include:

- Endpoint Created
- Connection Updated
- Token Refreshed
- Synchronization Started
- Synchronization Completed
- Webhook Delivered
- Retry Executed
- Endpoint Disabled

---

# Dependencies

Depends On

- IntegrationEndpoint
- Activity

Referenced By

- Integration Dashboard
- Monitoring Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Integration Endpoint |
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

PK_IntegrationActivity

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint
- ActivityId → Activity

## Unique Keys

- UQ_Integration_Activity (IntegrationEndpointId, ActivityId)

---

# Indexes

## Clustered

PK_IntegrationActivity

## Non Clustered

IX_IntegrationEndpoint

IX_Activity

IX_Status

---

# Relationships

IntegrationEndpoint (1) → IntegrationActivity (Many)

Activity (1) → IntegrationActivity (Many)

---

# Business Rules

- Activities are append-only.
- Operational history cannot be modified.
- Shared Activity reused across ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationActivityCreated
- IntegrationActivityUpdated

---

# Developer Notes

- Integrates with Monitoring Dashboard.
- Maintains operational history.

---

# ====================================================
# 011 IntegrationTimeline
# ====================================================

# Table Classification

**Domain:** Integration Domain

**Table Name:** IntegrationTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

IntegrationTimeline associates Integration Endpoints with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of integration events.

Examples include:

- Endpoint Created
- Connection Established
- Authentication Successful
- Synchronization Started
- Synchronization Completed
- Retry Executed
- Endpoint Disabled
- Endpoint Restored

---

# Dependencies

Depends On

- IntegrationEndpoint
- Timeline

Referenced By

- Integration Detail Screen
- Timeline Widget
- Monitoring Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| IntegrationTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| IntegrationEndpointId | BIGINT | No | | | ✔ | Integration Endpoint |
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

PK_IntegrationTimeline

## Foreign Keys

- IntegrationEndpointId → IntegrationEndpoint
- TimelineId → Timeline

## Unique Keys

- UQ_Integration_Timeline (IntegrationEndpointId, TimelineId)

---

# Indexes

## Clustered

PK_IntegrationTimeline

## Non Clustered

IX_IntegrationEndpoint

IX_Timeline

IX_Status

---

# Relationships

IntegrationEndpoint (1) → IntegrationTimeline (Many)

Timeline (1) → IntegrationTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Integration Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- IntegrationTimelineCreated
- IntegrationTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for integration history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Integration Domain provides secure, reliable and monitored communication between RentalERP and external systems.

It supports REST APIs, SOAP services, webhooks, message queues, scheduled synchronization, retry mechanisms, health monitoring and third-party integrations while ensuring security, scalability and auditability.

---

## Aggregate Roots

- IntegrationEndpoint

---

## Supporting Entities

- IntegrationConnection
- WebhookSubscription
- IntegrationJob
- IntegrationMessage
- IntegrationRetry
- IntegrationHealth

---

## Bridge Entities

- IntegrationAttachment
- IntegrationNote
- IntegrationActivity
- IntegrationTimeline

---

## Major Business Capabilities

- REST API Integration
- SOAP Integration
- GraphQL Integration
- Webhook Publishing
- Webhook Consumption
- Import / Export Jobs
- Synchronization Services
- Message Processing
- Retry Management
- Endpoint Health Monitoring
- Distributed Tracing
- Shared Kernel Integration

---

## Published Domain Events

The Integration Domain publishes events including:

- IntegrationEndpointCreated
- IntegrationConnectionCreated
- IntegrationTokenRefreshed
- WebhookRegistered
- IntegrationJobStarted
- IntegrationJobCompleted
- IntegrationMessageReceived
- IntegrationRetrySucceeded
- IntegrationHealthChecked
- IntegrationEndpointOffline
- IntegrationEndpointRecovered

These events integrate with:

- Security Domain
- Notification Domain
- Audit Domain
- Scheduler Domain
- Dashboard Domain
- Reporting Domain
- Workflow Domain
- Every Business Domain requiring external communication

---

## Integration Points

The Integration Domain integrates directly with:

- Foundation
- Shared Kernel
- Security Domain
- Audit Domain
- Notification Domain
- Scheduler Domain
- Dashboard Domain
- Reporting Domain
- Workflow Domain
- All Business Domains

---

# Integration Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. IntegrationEndpoint
2. IntegrationConnection
3. WebhookSubscription
4. IntegrationJob
5. IntegrationMessage
6. IntegrationRetry
7. IntegrationHealth
8. IntegrationAttachment
9. IntegrationNote
10. IntegrationActivity
11. IntegrationTimeline

---
