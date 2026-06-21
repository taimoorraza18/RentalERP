# RentalERP v1.0

# SecurityDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Security

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Security Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. SecurityPolicy

6. UserSession

7. ApiKey

---

# Domain Overview

The Security Domain provides enterprise-grade authentication, authorization and access control for RentalERP.

While the Administration Domain manages users, roles and permissions, the Security Domain is responsible for enforcing security policies during runtime.

It manages authentication sessions, API security, multi-factor authentication (MFA), password history, token management, security policies and access restrictions.

---

# Business Objectives

The Security Domain provides:

- Authentication
- Authorization
- JWT Token Management
- Refresh Tokens
- User Sessions
- API Keys
- Multi-Factor Authentication (MFA)
- Password History
- Security Policies
- Device Trust
- Session Revocation
- Security Tokens
- Access Restrictions

---

# Aggregate Root

## Primary Aggregate Root

- SecurityPolicy

## Supporting Entities

- UserSession
- ApiKey
- PasswordHistory
- UserDevice
- MfaVerification

## Bridge Entities

- SecurityAttachment
- SecurityNote
- SecurityActivity
- SecurityTimeline

---

# Implementation Order

001 SecurityPolicy

002 UserSession

003 ApiKey

004 PasswordHistory

005 UserDevice

006 MfaVerification

007 RefreshToken

008 SecurityAttachment

009 SecurityNote

010 SecurityActivity

011 SecurityTimeline

---

# ====================================================
# 001 SecurityPolicy
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** SecurityPolicy

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

SecurityPolicy defines global security rules enforced throughout RentalERP.

Examples include:

- Password Complexity
- Password Expiry
- Login Attempt Limits
- Session Timeout
- MFA Requirement
- Device Trust
- IP Restrictions
- API Rate Limits

These settings allow security administrators to change enterprise security behavior without application code changes.

---

# Dependencies

Depends On

- Company

Referenced By

- UserSession
- PasswordHistory
- MfaVerification

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SecurityPolicyId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| PolicyName | NVARCHAR(200) | No | | | | Policy Name |
| MinimumPasswordLength | SMALLINT | No | 8 | | | Password Length |
| RequireUpperCase | BIT | No | 1 | | | Complexity |
| RequireLowerCase | BIT | No | 1 | | | Complexity |
| RequireNumeric | BIT | No | 1 | | | Complexity |
| RequireSpecialCharacter | BIT | No | 1 | | | Complexity |
| PasswordExpiryDays | INT | No | 90 | | | Password Expiry |
| MaxLoginAttempts | SMALLINT | No | 5 | | | Lock Threshold |
| SessionTimeoutMinutes | INT | No | 30 | | | Session Timeout |
| RequireMfa | BIT | No | 0 | | | MFA Required |
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

PK_SecurityPolicy

## Foreign Keys

- CompanyId → Company

---

## Indexes

### Clustered

PK_SecurityPolicy

### Non Clustered

IX_Company

IX_Status

---

# Relationships

SecurityPolicy (1) → UserSession (Many)

---

# Business Rules

- Only one active policy per company.
- Password settings enforced globally.
- Session timeout mandatory.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SecurityPolicyCreated
- SecurityPolicyUpdated
- SecurityPolicyActivated

---

# Developer Notes

- Central runtime security configuration.
- Cached by Authentication Middleware.

---

# ====================================================
# 002 UserSession
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** UserSession

**Classification:** Transaction Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

UserSession tracks every authenticated user session within RentalERP.

It enables:

- Session Timeout
- Concurrent Session Control
- Device Tracking
- Force Logout
- Token Revocation

Sessions remain active until expiration or manual revocation.

---

# Dependencies

Depends On

- User
- SecurityPolicy

Referenced By

- RefreshToken

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| UserSessionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | User |
| SecurityPolicyId | BIGINT | No | | | ✔ | Policy |
| SessionToken | UNIQUEIDENTIFIER | No | NEWID() | | | Session Token |
| LoginTime | DATETIME2(7) | No | SYSUTCDATETIME() | | | Login |
| LastActivity | DATETIME2(7) | No | SYSUTCDATETIME() | | | Last Activity |
| ExpiryTime | DATETIME2(7) | No | | | | Session Expiry |
| IPAddress | NVARCHAR(50) | Yes | NULL | | | Client IP |
| DeviceName | NVARCHAR(200) | Yes | NULL | | | Device |
| Browser | NVARCHAR(200) | Yes | NULL | | | Browser |
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

PK_UserSession

## Foreign Keys

- UserId → User
- SecurityPolicyId → SecurityPolicy

---

## Indexes

### Clustered

PK_UserSession

### Non Clustered

IX_User

IX_SessionToken

IX_LastActivity

IX_IsRevoked

---

# Relationships

User (1) → UserSession (Many)

---

# Business Rules

- Multiple sessions configurable.
- Revoked sessions immediately invalid.
- Last Activity updated continuously.
- Expired sessions archived automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- UserSessionCreated
- UserSessionExpired
- UserSessionRevoked

---

# Developer Notes

- Supports JWT + Refresh Token.
- Enables Force Logout feature.

---

# ====================================================
# 003 ApiKey
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** ApiKey

**Classification:** Master Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

ApiKey manages API authentication for external systems integrating with RentalERP.

API Keys support:

- Third-Party Integrations
- Mobile Applications
- Vendor APIs
- Customer Portals
- IoT Devices
- Internal Services

Each key has independent permissions, expiration and usage limits.

---

# Dependencies

Depends On

- User

Referenced By

- Integration Domain

...

# ====================================================
# 003 ApiKey
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** ApiKey

**Classification:** Master Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

ApiKey manages secure authentication for external systems integrating with RentalERP.

API Keys provide controlled access to ERP services without requiring interactive user authentication.

Supported integrations include:

- Mobile Applications
- Customer Portal
- Vendor Portal
- IoT Devices
- Third-Party Systems
- Webhooks
- Internal Microservices

Each key has its own permissions, expiration policy, usage limits and revocation status.

---

# Dependencies

Depends On

- User

Referenced By

- Integration Domain
- API Gateway

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ApiKeyId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | Owner |
| KeyName | NVARCHAR(200) | No | | | | Display Name |
| ApiKey | NVARCHAR(500) | No | | | | Encrypted Key |
| SecretKey | NVARCHAR(500) | No | | | | Encrypted Secret |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiration |
| AllowedIPs | NVARCHAR(MAX) | Yes | NULL | | | IP Whitelist |
| RequestsPerMinute | INT | No | 100 | | | Rate Limit |
| IsRevoked | BIT | No | 0 | | | Revoked |
| LastUsedDate | DATETIME2(7) | Yes | NULL | | | Last Used |
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

PK_ApiKey

## Foreign Keys

- UserId → User

---

# Indexes

## Clustered

PK_ApiKey

## Non Clustered

IX_User

IX_ExpiryDate

IX_IsRevoked

---

# Relationships

User (1) → ApiKey (Many)

---

# Business Rules

- API Keys stored encrypted.
- Secret Key displayed only once.
- Revoked keys immediately invalid.
- Expired keys automatically disabled.
- IP whitelist optional.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ApiKeyCreated
- ApiKeyRevoked
- ApiKeyExpired

---

# Developer Notes

- Integrates with API Gateway.
- Supports rate limiting.

---

# ====================================================
# 004 PasswordHistory
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** PasswordHistory

**Classification:** Security Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

PasswordHistory stores historical password hashes for each user.

The history prevents password reuse and enforces password rotation policies.

Examples include:

- Last 5 Passwords
- Last 10 Passwords
- Password Expiry Validation

Only password hashes are stored.

---

# Dependencies

Depends On

- User

Referenced By

- Authentication Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| PasswordHistoryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | User |
| PasswordHash | NVARCHAR(500) | No | | | | Hashed Password |
| ChangedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Changed Date |
| ChangedBy | BIGINT | No | | | ✔ | Administrator/User |
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

PK_PasswordHistory

## Foreign Keys

- UserId → User
- ChangedBy → User

---

# Indexes

## Clustered

PK_PasswordHistory

## Non Clustered

IX_User

IX_ChangedDate

---

# Relationships

User (1) → PasswordHistory (Many)

---

# Business Rules

- Passwords stored as hashes only.
- Previous passwords cannot be reused.
- History retention configurable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- PasswordChanged
- PasswordHistoryRecorded

---

# Developer Notes

- Supports password rotation policies.
- Never stores plaintext passwords.

---

# ====================================================
# 005 UserDevice
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** UserDevice

**Classification:** Security Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

UserDevice stores trusted devices used by users to access RentalERP.

Trusted devices can bypass repeated verification depending on company security policies.

Examples include:

- Employee Laptop
- Office Desktop
- Mobile Phone
- Tablet

---

# Dependencies

Depends On

- User

Referenced By

- UserSession
- MfaVerification

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| UserDeviceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | User |
| DeviceIdentifier | NVARCHAR(300) | No | | | | Browser Fingerprint |
| DeviceName | NVARCHAR(200) | Yes | NULL | | | Friendly Name |
| OperatingSystem | NVARCHAR(150) | Yes | NULL | | | OS |
| Browser | NVARCHAR(150) | Yes | NULL | | | Browser |
| FirstLoginDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | First Seen |
| LastLoginDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Last Seen |
| IsTrusted | BIT | No | 0 | | | Trusted Device |
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

PK_UserDevice

## Foreign Keys

- UserId → User

---

# Indexes

## Clustered

PK_UserDevice

## Non Clustered

IX_User

IX_DeviceIdentifier

IX_IsTrusted

---

# Relationships

User (1) → UserDevice (Many)

---

# Business Rules

- Device fingerprint must be unique per user.
- Trusted devices configurable.
- Device history retained.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- DeviceRegistered
- DeviceTrusted
- DeviceRevoked

---

# Developer Notes

- Supports device recognition.
- Used by MFA engine.

...

# ====================================================
# 006 MfaVerification
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** MfaVerification

**Classification:** Security Table

**Aggregate Root:** SecurityPolicy

---

# Purpose

MfaVerification records every Multi-Factor Authentication (MFA) challenge performed within RentalERP.

It supports multiple MFA providers and verification methods.

Supported methods include:

- Email OTP
- SMS OTP
- Authenticator App (TOTP)
- Push Notification
- Hardware Token
- Backup Recovery Codes

Every verification attempt is recorded for auditing and fraud detection.

---

# Dependencies

Depends On

- User
- UserSession

Referenced By

- Authentication Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| MfaVerificationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserId | BIGINT | No | | | ✔ | User |
| UserSessionId | BIGINT | Yes | NULL | | ✔ | Session |
| VerificationMethod | SMALLINT | No | | | | Email / SMS / TOTP |
| VerificationCodeHash | NVARCHAR(500) | Yes | NULL | | | Hashed OTP |
| GeneratedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Generated |
| ExpiryDate | DATETIME2(7) | No | | | | Expiry |
| VerifiedDate | DATETIME2(7) | Yes | NULL | | | Verified |
| AttemptCount | SMALLINT | No | 0 | | | Attempts |
| IsSuccessful | BIT | No | 0 | | | Success |
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

PK_MfaVerification

## Foreign Keys

- UserId → User
- UserSessionId → UserSession

---

# Indexes

## Clustered

PK_MfaVerification

## Non Clustered

IX_User

IX_UserSession

IX_ExpiryDate

IX_IsSuccessful

---

# Relationships

User (1) → MfaVerification (Many)

UserSession (1) → MfaVerification (Many)

---

# Business Rules

- Verification codes stored only as hashes.
- OTP expires automatically.
- Maximum attempts configurable.
- Successful verification cannot be reused.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- MfaChallengeCreated
- MfaVerified
- MfaVerificationFailed

---

# Developer Notes

- Supports pluggable MFA providers.
- Integrates with Authentication Middleware.

---

# ====================================================
# 007 RefreshToken
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** RefreshToken

**Classification:** Security Table

**Aggregate Root:** UserSession

---

# Purpose

RefreshToken manages JWT refresh tokens issued during authentication.

Refresh Tokens allow clients to obtain new JWT access tokens without requiring the user to log in again.

Features include:

- Token Rotation
- Automatic Expiration
- Token Revocation
- Device Binding
- Session Validation

---

# Dependencies

Depends On

- UserSession

Referenced By

- Authentication Service

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| RefreshTokenId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| UserSessionId | BIGINT | No | | | ✔ | User Session |
| TokenHash | NVARCHAR(500) | No | | | | Token Hash |
| IssuedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Issued |
| ExpiryDate | DATETIME2(7) | No | | | | Expiry |
| RevokedDate | DATETIME2(7) | Yes | NULL | | | Revoked |
| ReplacedByTokenHash | NVARCHAR(500) | Yes | NULL | | | Rotation |
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

PK_RefreshToken

## Foreign Keys

- UserSessionId → UserSession

---

# Indexes

## Clustered

PK_RefreshToken

## Non Clustered

IX_UserSession

IX_ExpiryDate

IX_IsRevoked

---

# Relationships

UserSession (1) → RefreshToken (Many)

---

# Business Rules

- Refresh Tokens stored as hashes.
- Token rotation supported.
- Revoked tokens invalid immediately.
- Expired tokens cleaned automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- RefreshTokenIssued
- RefreshTokenRevoked
- RefreshTokenRotated

---

# Developer Notes

- Supports JWT authentication.
- Prevents replay attacks.

---

# ====================================================
# 008 SecurityAttachment
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** SecurityAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Security records with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Security Investigation Files
- Device Certificates
- Compliance Evidence
- Security Reports
- Incident Documents

---

# Dependencies

Depends On

- SecurityPolicy
- Attachment

Referenced By

- Security Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SecurityAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SecurityPolicyId | BIGINT | No | | | ✔ | Security Policy |
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

PK_SecurityAttachment

## Foreign Keys

- SecurityPolicyId → SecurityPolicy
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_SecurityAttachment

## Non Clustered

IX_SecurityPolicy

IX_Attachment

---

# Relationships

SecurityPolicy (1) → SecurityAttachment (Many)

Attachment (1) → SecurityAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SecurityAttachmentAdded
- SecurityAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores security-related evidence.

...

# ====================================================
# 009 SecurityNote
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** SecurityNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SecurityNote associates Security Policies with reusable Note records maintained within the Shared Kernel.

Security Notes allow administrators, security officers and auditors to document security-related observations without modifying the original security records.

Examples include:

- Security Policy Changes
- Penetration Test Notes
- Compliance Remarks
- Incident Investigation Notes
- Risk Assessment
- Administrator Comments

---

# Dependencies

Depends On

- SecurityPolicy
- Note

Referenced By

- Security Center
- Compliance Reports
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SecurityNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SecurityPolicyId | BIGINT | No | | | ✔ | Security Policy |
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

PK_SecurityNote

## Foreign Keys

- SecurityPolicyId → SecurityPolicy
- NoteId → Note

## Unique Keys

- UQ_Security_Note (SecurityPolicyId, NoteId)

---

# Indexes

## Clustered

PK_SecurityNote

## Non Clustered

IX_SecurityPolicy

IX_Note

IX_Status

---

# Relationships

SecurityPolicy (1) → SecurityNote (Many)

Note (1) → SecurityNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Notes remain reusable within Shared Kernel.
- Security policies own only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SecurityNoteAdded
- SecurityNoteUpdated
- SecurityNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports security investigations.

---

# ====================================================
# 010 SecurityActivity
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** SecurityActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SecurityActivity associates Security Policies with reusable Activity records maintained within the Shared Kernel.

Activities record operational events throughout the security lifecycle.

Examples include:

- Security Policy Created
- Policy Updated
- Password Policy Changed
- MFA Enabled
- API Key Revoked
- User Session Revoked
- Security Alert Triggered
- Device Trusted

---

# Dependencies

Depends On

- SecurityPolicy
- Activity

Referenced By

- Security Dashboard
- Security Operations Center

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SecurityActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SecurityPolicyId | BIGINT | No | | | ✔ | Security Policy |
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

PK_SecurityActivity

## Foreign Keys

- SecurityPolicyId → SecurityPolicy
- ActivityId → Activity

## Unique Keys

- UQ_Security_Activity (SecurityPolicyId, ActivityId)

---

# Indexes

## Clustered

PK_SecurityActivity

## Non Clustered

IX_SecurityPolicy

IX_Activity

IX_Status

---

# Relationships

SecurityPolicy (1) → SecurityActivity (Many)

Activity (1) → SecurityActivity (Many)

---

# Business Rules

- Activities are append-only.
- Security history cannot be modified.
- Shared Activity reused throughout ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SecurityActivityCreated
- SecurityActivityUpdated

---

# Developer Notes

- Integrates with Security Operations Center.
- Maintains operational security history.

---

# ====================================================
# 011 SecurityTimeline
# ====================================================

# Table Classification

**Domain:** Security Domain

**Table Name:** SecurityTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

SecurityTimeline associates Security Policies with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of security events.

Examples include:

- Policy Created
- Policy Updated
- User Logged In
- MFA Verified
- API Key Generated
- Session Revoked
- Password Changed
- Device Registered
- Security Incident

---

# Dependencies

Depends On

- SecurityPolicy
- Timeline

Referenced By

- Security Detail Screen
- Timeline Widget
- Compliance Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SecurityTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SecurityPolicyId | BIGINT | No | | | ✔ | Security Policy |
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

PK_SecurityTimeline

## Foreign Keys

- SecurityPolicyId → SecurityPolicy
- TimelineId → Timeline

## Unique Keys

- UQ_Security_Timeline (SecurityPolicyId, TimelineId)

---

# Indexes

## Clustered

PK_SecurityTimeline

## Non Clustered

IX_SecurityPolicy

IX_Timeline

IX_Status

---

# Relationships

SecurityPolicy (1) → SecurityTimeline (Many)

Timeline (1) → SecurityTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to Security Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SecurityTimelineCreated
- SecurityTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for security investigations.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Security Domain provides runtime authentication, authorization and enterprise security enforcement for RentalERP.

Unlike the Administration Domain, which manages users, roles and permissions, the Security Domain enforces security policies during application execution. It manages sessions, refresh tokens, API keys, MFA, trusted devices and password history while ensuring secure access to every business module.

---

## Aggregate Roots

- SecurityPolicy

---

## Supporting Entities

- UserSession
- ApiKey
- PasswordHistory
- UserDevice
- MfaVerification
- RefreshToken

---

## Bridge Entities

- SecurityAttachment
- SecurityNote
- SecurityActivity
- SecurityTimeline

---

## Major Business Capabilities

- Security Policy Management
- JWT Authentication
- Refresh Token Management
- Session Management
- API Key Authentication
- Password History Enforcement
- Multi-Factor Authentication (MFA)
- Trusted Device Management
- Session Revocation
- Token Rotation
- Runtime Security Enforcement
- Shared Kernel Integration

---

## Published Domain Events

The Security Domain publishes events including:

- SecurityPolicyCreated
- SecurityPolicyUpdated
- UserSessionCreated
- UserSessionExpired
- UserSessionRevoked
- ApiKeyCreated
- ApiKeyRevoked
- PasswordChanged
- DeviceRegistered
- DeviceTrusted
- MfaVerified
- RefreshTokenIssued
- RefreshTokenRevoked

These events integrate with:

- Administration Domain
- Audit Domain
- Notification Domain
- Integration Domain
- Workflow Domain
- Dashboard Domain
- Reporting Domain
- Every Business Domain requiring authentication

---

## Integration Points

The Security Domain integrates directly with:

- Foundation
- Shared Kernel
- Administration Domain
- Audit Domain
- Notification Domain
- Integration Domain
- Workflow Domain
- Dashboard Domain
- Reporting Domain
- Authentication Middleware
- Authorization Middleware

---

# Security Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. SecurityPolicy
2. UserSession
3. ApiKey
4. PasswordHistory
5. UserDevice
6. MfaVerification
7. RefreshToken
8. SecurityAttachment
9. SecurityNote
10. SecurityActivity
11. SecurityTimeline

---
