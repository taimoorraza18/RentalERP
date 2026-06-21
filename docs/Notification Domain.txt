# RentalERP v1.0

# NotificationDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Notification

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Notification Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. NotificationTemplate

6. NotificationChannel

7. NotificationPreference

---

# Domain Overview

The Notification Domain provides centralized messaging capabilities for RentalERP.

Rather than allowing each business module to send emails, SMS messages or WhatsApp notifications directly, every notification request is routed through this domain.

The Notification Domain manages templates, channels, delivery, scheduling, retries, user preferences and notification history.

It supports real-time and scheduled messaging while providing complete delivery auditing.

---

# Business Objectives

The Notification Domain provides:

- Notification Templates
- Multi-Channel Messaging
- Email Notifications
- SMS Notifications
- WhatsApp Notifications
- Push Notifications
- In-App Notifications
- User Notification Preferences
- Scheduled Notifications
- Retry Management
- Delivery History
- Read Tracking
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Root

- Notification

## Supporting Entities

- NotificationTemplate
- NotificationChannel
- NotificationPreference
- NotificationDelivery
- NotificationRecipient
- NotificationSchedule

## Bridge Entities

- NotificationAttachment
- NotificationNote
- NotificationActivity
- NotificationTimeline

---

# Implementation Order

001 NotificationTemplate

002 NotificationChannel

003 NotificationPreference

004 Notification

005 NotificationRecipient

006 NotificationDelivery

007 NotificationSchedule

008 NotificationAttachment

009 NotificationNote

010 NotificationActivity

011 NotificationTimeline

---

# ====================================================
# 001 NotificationTemplate
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationTemplate

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

NotificationTemplate stores reusable message templates used throughout RentalERP.

Templates allow business domains to send consistent notifications without hardcoding message text.

Supported placeholders include dynamic values such as:

- Customer Name
- Vendor Name
- Invoice Number
- Purchase Order Number
- Rental Contract Number
- Employee Name
- Approval Status
- Due Date

---

# Dependencies

Depends On

- Company

Referenced By

- Notification

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationTemplateId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| TemplateCode | NVARCHAR(50) | No | | | | Template Code |
| TemplateName | NVARCHAR(200) | No | | | | Template Name |
| Subject | NVARCHAR(500) | Yes | NULL | | | Email Subject |
| MessageBody | NVARCHAR(MAX) | No | | | | HTML/Text Template |
| SupportedChannels | NVARCHAR(200) | No | | | | Email,SMS,WhatsApp |
| IsHtml | BIT | No | 1 | | | HTML Template |
| IsSystem | BIT | No | 0 | | | System Template |
| VersionNo | INT | No | 1 | | | Version |
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

PK_NotificationTemplate

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Template_Code

---

# Indexes

## Clustered

PK_NotificationTemplate

## Non Clustered

IX_TemplateCode

IX_TemplateName

IX_Status

IX_System

---

# Relationships

NotificationTemplate (1) → Notification (Many)

---

# Business Rules

- Template Code must be unique.
- System Templates cannot be deleted.
- Supports placeholder replacement.
- Multiple channels may use one template.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationTemplateCreated
- NotificationTemplateUpdated
- NotificationTemplatePublished

---

# Developer Notes

- Supports HTML and Plain Text.
- Shared across all ERP modules.

---

# ====================================================
# 002 NotificationChannel
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationChannel

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

Defines the available communication channels used by the Notification Engine.

Examples include:

- Email
- SMS
- WhatsApp
- Push Notification
- In-App Notification
- Microsoft Teams
- Slack
- Webhook

Each channel contains provider configuration and operational settings.

---

# Dependencies

Depends On

- Company

Referenced By

- NotificationDelivery

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationChannelId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| ChannelCode | NVARCHAR(30) | No | | | | EMAIL / SMS / WA |
| ChannelName | NVARCHAR(100) | No | | | | Display Name |
| ProviderName | NVARCHAR(150) | Yes | NULL | | | SendGrid / Twilio |
| ConfigurationJson | NVARCHAR(MAX) | Yes | NULL | | | Provider Config |
| IsEnabled | BIT | No | 1 | | | Enabled |
| Priority | INT | No | 1 | | | Delivery Priority |
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

PK_NotificationChannel

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Channel_Code

---

# Indexes

## Clustered

PK_NotificationChannel

## Non Clustered

IX_ChannelCode

IX_Enabled

IX_Priority

---

# Relationships

NotificationChannel (1) → NotificationDelivery (Many)

---

# Business Rules

- Channel Code must be unique.
- Disabled channels cannot send messages.
- Priority controls fallback order.
- Configuration stored as JSON.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationChannelCreated
- NotificationChannelEnabled
- NotificationChannelDisabled

---

# Developer Notes

- Supports pluggable providers.
- Enables provider replacement without code changes.

---

# ====================================================
# 003 NotificationPreference
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationPreference

**Classification:** Master Table

**Aggregate Root:** User

---

# Purpose

Stores notification preferences for individual users.

Users can choose which notification channels they wish to receive for different business events.

Examples:

- Email only
- WhatsApp + In-App
- Push Notifications
- Disable Marketing Notifications
- Disable Weekend Notifications

---

# Dependencies

Depends On

- User
- NotificationChannel

Referenced By

- Notification Engine

...

# ====================================================
# 003 NotificationPreference
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationPreference

**Classification:** Master Table

**Aggregate Root:** User

---

# Purpose

NotificationPreference stores notification settings for each user.

It allows users to control which business events generate notifications and through which communication channels they should be delivered.

Examples include:

- Purchase Order Approval
- Sales Invoice Posted
- Rental Contract Expiring
- Service Ticket Assigned
- Inventory Below Minimum
- Workflow Approval Request

Each notification type can be independently enabled or disabled.

---

# Dependencies

Depends On

- User
- NotificationChannel

Referenced By

- Notification
- Notification Engine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationPreferenceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | User |
| NotificationType | NVARCHAR(100) | No | | | | Event Name |
| NotificationChannelId | BIGINT | No | | | ✔ | Channel |
| IsEnabled | BIT | No | 1 | | | Enabled |
| QuietHoursStart | TIME | Yes | NULL | | | Quiet Hours Start |
| QuietHoursEnd | TIME | Yes | NULL | | | Quiet Hours End |
| WeekendEnabled | BIT | No | 1 | | | Weekend Notifications |
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

PK_NotificationPreference

## Foreign Keys

- UserId → User
- NotificationChannelId → NotificationChannel

## Unique Keys

- UQ_User_NotificationType_Channel

---

# Indexes

## Clustered

PK_NotificationPreference

## Non Clustered

IX_User

IX_Channel

IX_Enabled

---

# Relationships

User (1) → NotificationPreference (Many)

NotificationChannel (1) → NotificationPreference (Many)

---

# Business Rules

- Preferences are maintained per user.
- Multiple channels supported.
- Quiet Hours suppress non-critical notifications.
- Critical alerts ignore Quiet Hours.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationPreferenceUpdated
- NotificationPreferenceEnabled
- NotificationPreferenceDisabled

---

# Developer Notes

- Supports personalized notifications.
- Used by Notification Engine during delivery.

---

# ====================================================
# 004 Notification
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** Notification

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

Notification represents a notification request created by any business domain.

Examples include:

- Purchase Order Approved
- Invoice Posted
- Payment Received
- Workflow Pending
- Asset Due for Maintenance
- Rental Contract Expiring

A Notification may have one or many recipients and one or many delivery channels.

---

# Dependencies

Depends On

- NotificationTemplate

Referenced By

- NotificationRecipient
- NotificationDelivery
- NotificationSchedule

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationTemplateId | BIGINT | Yes | NULL | | ✔ | Template |
| NotificationType | NVARCHAR(100) | No | | | | Business Event |
| Subject | NVARCHAR(500) | Yes | NULL | | | Subject |
| MessageBody | NVARCHAR(MAX) | No | | | | Final Message |
| Priority | SMALLINT | No | 2 | | | Low / Normal / High / Critical |
| SourceModule | NVARCHAR(100) | No | | | | Purchase / Sales |
| SourceEntity | NVARCHAR(100) | No | | | | PurchaseOrder |
| SourceEntityId | BIGINT | No | | | | Entity Id |
| ScheduledDate | DATETIME2(7) | Yes | NULL | | | Schedule |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiry |
| StatusId | SMALLINT | No | 1 | | | Pending / Sent / Failed |
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

PK_Notification

## Foreign Keys

- NotificationTemplateId → NotificationTemplate

---

# Indexes

## Clustered

PK_Notification

## Non Clustered

IX_Type

IX_Source

IX_Status

IX_ScheduledDate

---

# Relationships

Notification (1) → NotificationRecipient (Many)

Notification (1) → NotificationDelivery (Many)

---

# Business Rules

- Notification immutable after sending.
- Multiple recipients supported.
- Multiple delivery channels supported.
- Critical notifications processed immediately.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationCreated
- NotificationQueued
- NotificationSent
- NotificationFailed

---

# Developer Notes

- Root entity for Notification Engine.
- Created by every business domain.

---

# ====================================================
# 005 NotificationRecipient
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationRecipient

**Classification:** Transaction Detail

**Aggregate Root:** Notification

---

# Purpose

Stores every recipient associated with a Notification.

Recipients may be:

- Employee
- Customer
- Vendor
- User Group
- External Email
- External Phone Number

Each recipient can receive the notification through multiple channels.

---

# Dependencies

Depends On

- Notification

Referenced By

- NotificationDelivery

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationRecipientId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
| RecipientType | SMALLINT | No | | | | User / Customer / Vendor |
| RecipientId | BIGINT | Yes | NULL | | | Entity Id |
| RecipientName | NVARCHAR(250) | Yes | NULL | | | Display Name |
| Email | NVARCHAR(250) | Yes | NULL | | | Email |
| Mobile | NVARCHAR(50) | Yes | NULL | | | Phone |
| IsRead | BIT | No | 0 | | | Read Status |
| ReadDate | DATETIME2(7) | Yes | NULL | | | Read Time |
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

PK_NotificationRecipient

## Foreign Keys

- NotificationId → Notification

---

# Indexes

## Clustered

PK_NotificationRecipient

## Non Clustered

IX_Notification

IX_Recipient

IX_IsRead

---

# Relationships

Notification (1) → NotificationRecipient (Many)

NotificationRecipient (1) → NotificationDelivery (Many)

---

# Business Rules

- Unlimited recipients supported.
- Read status tracked independently.
- Supports internal and external recipients.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationRecipientAdded
- NotificationRead
- NotificationUnread

---

# Developer Notes

- Supports broadcast messaging.
- Enables in-app notification tracking.

...

# ====================================================
# 006 NotificationDelivery
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationDelivery

**Classification:** Transaction Table

**Aggregate Root:** Notification

---

# Purpose

NotificationDelivery records every delivery attempt made by the Notification Engine.

A single notification may generate multiple delivery records when sent through multiple channels.

Examples:

- Email Sent
- SMS Delivered
- WhatsApp Read
- Push Notification Failed
- In-App Delivered

Delivery history provides complete auditing and retry management.

---

# Dependencies

Depends On

- Notification
- NotificationRecipient
- NotificationChannel

Referenced By

- Delivery Dashboard
- Retry Engine

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationDeliveryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
| NotificationRecipientId | BIGINT | No | | | ✔ | Recipient |
| NotificationChannelId | BIGINT | No | | | ✔ | Channel |
| DeliveryStatus | SMALLINT | No | | | | Pending / Sent / Delivered / Failed |
| ProviderMessageId | NVARCHAR(200) | Yes | NULL | | | Provider Reference |
| SentDate | DATETIME2(7) | Yes | NULL | | | Sent Time |
| DeliveredDate | DATETIME2(7) | Yes | NULL | | | Delivered Time |
| RetryCount | INT | No | 0 | | | Retry Attempts |
| FailureReason | NVARCHAR(1000) | Yes | NULL | | | Failure Reason |
| ProviderResponse | NVARCHAR(MAX) | Yes | NULL | | | API Response |
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

PK_NotificationDelivery

## Foreign Keys

- NotificationId → Notification
- NotificationRecipientId → NotificationRecipient
- NotificationChannelId → NotificationChannel

---

# Indexes

## Clustered

PK_NotificationDelivery

## Non Clustered

IX_Notification

IX_Recipient

IX_Channel

IX_DeliveryStatus

IX_SentDate

---

# Relationships

Notification (1) → NotificationDelivery (Many)

NotificationRecipient (1) → NotificationDelivery (Many)

NotificationChannel (1) → NotificationDelivery (Many)

---

# Business Rules

- One record per Recipient per Channel.
- Failed deliveries eligible for retry.
- Delivery history immutable.
- Provider responses retained.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationDelivered
- NotificationDeliveryFailed
- NotificationRetried

---

# Developer Notes

- Used by Retry Service.
- Provides complete delivery audit.

---

# ====================================================
# 007 NotificationSchedule
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationSchedule

**Classification:** Transaction Table

**Aggregate Root:** Notification

---

# Purpose

NotificationSchedule manages deferred and recurring notifications.

Instead of immediate delivery, notifications may be scheduled for future dates or recurring intervals.

Examples:

- Rental Reminder (7 Days Before)
- Maintenance Reminder
- Warranty Expiry Reminder
- Subscription Renewal
- Birthday Greetings
- Daily Summary

---

# Dependencies

Depends On

- Notification

Referenced By

- Background Scheduler

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationScheduleId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
| ScheduleType | SMALLINT | No | | | | Once / Daily / Weekly / Monthly |
| CronExpression | NVARCHAR(100) | Yes | NULL | | | Cron Expression |
| StartDate | DATETIME2(7) | No | | | | Start Date |
| EndDate | DATETIME2(7) | Yes | NULL | | | End Date |
| NextRunDate | DATETIME2(7) | Yes | NULL | | | Next Run |
| LastRunDate | DATETIME2(7) | Yes | NULL | | | Previous Run |
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

PK_NotificationSchedule

## Foreign Keys

- NotificationId → Notification

---

# Indexes

## Clustered

PK_NotificationSchedule

## Non Clustered

IX_Notification

IX_NextRunDate

IX_Enabled

---

# Relationships

Notification (1) → NotificationSchedule (Many)

---

# Business Rules

- Disabled schedules never execute.
- Cron Expression required for custom schedules.
- Scheduler updates NextRunDate automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationScheduled
- NotificationScheduleExecuted

---

# Developer Notes

- Executed by Background Scheduler.
- Supports recurring reminders.

---

# ====================================================
# 008 NotificationAttachment
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Notifications with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Invoice PDF
- Purchase Order PDF
- Rental Agreement
- Service Report
- Images
- User Manuals

---

# Dependencies

Depends On

- Notification
- Attachment

Referenced By

- Email Delivery
- WhatsApp Delivery

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
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

PK_NotificationAttachment

## Foreign Keys

- NotificationId → Notification
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_NotificationAttachment

## Non Clustered

IX_Notification

IX_Attachment

---

# Relationships

Notification (1) → NotificationAttachment (Many)

Attachment (1) → NotificationAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationAttachmentAdded
- NotificationAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports multi-channel attachments.

...

# ====================================================
# 009 NotificationNote
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

NotificationNote associates Notifications with reusable Note records maintained within the Shared Kernel.

Notification Notes allow administrators and support teams to document message-related information without modifying the notification itself.

Examples include:

- Delivery Failure Explanation
- Manual Retry Notes
- Customer Communication Notes
- Provider Investigation Notes
- Administrator Comments
- Support Remarks

---

# Dependencies

Depends On

- Notification
- Note

Referenced By

- Notification Detail Screen
- Support Center
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
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

PK_NotificationNote

## Foreign Keys

- NotificationId → Notification
- NoteId → Note

## Unique Keys

- UQ_Notification_Note (NotificationId, NoteId)

---

# Indexes

## Clustered

PK_NotificationNote

## Non Clustered

IX_Notification

IX_Note

IX_Status

---

# Relationships

Notification (1) → NotificationNote (Many)

Note (1) → NotificationNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Notes remain reusable in Shared Kernel.
- Business ownership belongs to Notification Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationNoteAdded
- NotificationNoteUpdated
- NotificationNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports operational documentation.

---

# ====================================================
# 010 NotificationActivity
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

NotificationActivity associates Notifications with reusable Activity records maintained within the Shared Kernel.

Activities record every operational event throughout the notification lifecycle.

Examples include:

- Notification Created
- Notification Queued
- Delivery Started
- Email Sent
- SMS Delivered
- WhatsApp Read
- Delivery Failed
- Retry Scheduled
- Notification Expired

---

# Dependencies

Depends On

- Notification
- Activity

Referenced By

- Notification Dashboard
- Audit Module
- Monitoring Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
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

PK_NotificationActivity

## Foreign Keys

- NotificationId → Notification
- ActivityId → Activity

## Unique Keys

- UQ_Notification_Activity (NotificationId, ActivityId)

---

# Indexes

## Clustered

PK_NotificationActivity

## Non Clustered

IX_Notification

IX_Activity

IX_Status

---

# Relationships

Notification (1) → NotificationActivity (Many)

Activity (1) → NotificationActivity (Many)

---

# Business Rules

- Activities are append-only.
- Delivery history cannot be modified.
- Shared Activity reused across ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationActivityCreated
- NotificationActivityUpdated

---

# Developer Notes

- Integrates with monitoring dashboards.
- Maintains complete notification audit history.

---

# ====================================================
# 011 NotificationTimeline
# ====================================================

# Table Classification

**Domain:** Notification Domain

**Table Name:** NotificationTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

NotificationTimeline associates Notifications with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of every notification from creation until final delivery or expiration.

Examples include:

- Notification Created
- Recipient Added
- Queued
- Sent
- Delivered
- Read
- Failed
- Retried
- Expired

---

# Dependencies

Depends On

- Notification
- Timeline

Referenced By

- Notification Detail Screen
- Timeline Widget
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NotificationTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| NotificationId | BIGINT | No | | | ✔ | Notification |
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

PK_NotificationTimeline

## Foreign Keys

- NotificationId → Notification
- TimelineId → Timeline

## Unique Keys

- UQ_Notification_Timeline (NotificationId, TimelineId)

---

# Indexes

## Clustered

PK_NotificationTimeline

## Non Clustered

IX_Notification

IX_Timeline

IX_Status

---

# Relationships

Notification (1) → NotificationTimeline (Many)

Timeline (1) → NotificationTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Notification Domain.
- Shared Timeline reused across ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NotificationTimelineCreated
- NotificationTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for notification history visualization.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Notification Domain centralizes all messaging capabilities within RentalERP.

Instead of allowing individual business domains to communicate directly with users, every notification request flows through this domain, ensuring consistency, auditing, retries and user preference management.

The Notification Engine supports synchronous and asynchronous delivery across multiple communication channels.

---

## Aggregate Roots

- Notification

---

## Supporting Entities

- NotificationTemplate
- NotificationChannel
- NotificationPreference
- NotificationRecipient
- NotificationDelivery
- NotificationSchedule

---

## Bridge Entities

- NotificationAttachment
- NotificationNote
- NotificationActivity
- NotificationTimeline

---

## Major Business Capabilities

- Email Notifications
- SMS Notifications
- WhatsApp Notifications
- Push Notifications
- In-App Notifications
- Multi-Channel Delivery
- Notification Templates
- User Preferences
- Delivery Tracking
- Read Tracking
- Retry Management
- Scheduled Notifications
- Shared Kernel Integration
- Complete Audit Trail

---

## Published Domain Events

The Notification Domain publishes events including:

- NotificationCreated
- NotificationQueued
- NotificationSent
- NotificationDelivered
- NotificationDeliveryFailed
- NotificationRetried
- NotificationRead
- NotificationExpired

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
- Workflow Domain
- Reporting Domain
- Dashboard Module

---

## Integration Points

The Notification Domain integrates directly with:

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
- Workflow Domain
- Reporting Domain

---

# Notification Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. NotificationTemplate
2. NotificationChannel
3. NotificationPreference
4. Notification
5. NotificationRecipient
6. NotificationDelivery
7. NotificationSchedule
8. NotificationAttachment
9. NotificationNote
10. NotificationActivity
11. NotificationTimeline

---
