# RentalERP v1.0

# SystemConfigurationDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** System Configuration

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial System Configuration Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. SystemSetting

6. FeatureFlag

7. NumberSequence

---

# Domain Overview

The System Configuration Domain centralizes application-wide configuration used throughout RentalERP.

Unlike the Administration Domain, which manages users and organizational structures, the System Configuration Domain manages application behavior, numbering sequences, localization, feature availability, regional settings and business preferences.

All other domains consume configuration values from this domain instead of storing duplicate configuration.

---

# Business Objectives

The System Configuration Domain provides:

- Global Settings
- Company Settings
- Feature Flags
- Numbering Sequences
- Localization
- Regional Settings
- Currency Configuration
- Tax Configuration
- Business Preferences
- UI Preferences
- Environment Configuration
- Application Parameters

---

# Aggregate Root

## Primary Aggregate Root

- SystemSetting

## Supporting Entities

- FeatureFlag
- NumberSequence
- Localization
- CurrencyConfiguration
- TaxConfiguration
- ApplicationPreference

## Bridge Entities

- ConfigurationAttachment
- ConfigurationNote
- ConfigurationActivity
- ConfigurationTimeline

---

# Implementation Order

001 SystemSetting

002 FeatureFlag

003 NumberSequence

004 Localization

005 CurrencyConfiguration

006 TaxConfiguration

007 ApplicationPreference

008 ConfigurationAttachment

009 ConfigurationNote

010 ConfigurationActivity

011 ConfigurationTimeline

---

# ====================================================
# 001 SystemSetting
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** SystemSetting

**Classification:** Master Table

**Aggregate Root:** Yes

---

# Purpose

SystemSetting stores configurable application-wide settings consumed by every domain.

Examples include:

- Default Currency
- Default Time Zone
- Fiscal Year
- Decimal Precision
- Date Format
- Time Format
- Session Timeout
- Default Language
- Company Logo
- Application Theme

Settings are editable without modifying application code.

---

# Dependencies

Depends On

- Company

Referenced By

- Every Business Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| SystemSettingId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| SettingCode | NVARCHAR(100) | No | | | | Setting Code |
| SettingName | NVARCHAR(250) | No | | | | Display Name |
| SettingValue | NVARCHAR(MAX) | Yes | NULL | | | Value |
| DataType | NVARCHAR(50) | No | | | | String / Int / JSON |
| Category | NVARCHAR(100) | No | | | | General / UI / Finance |
| IsSystem | BIT | No | 0 | | | System Setting |
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

PK_SystemSetting

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Setting_Code

---

# Indexes

## Clustered

PK_SystemSetting

## Non Clustered

IX_SettingCode

IX_Category

IX_Company

---

# Relationships

SystemSetting (1) → FeatureFlag (Many)

SystemSetting (1) → NumberSequence (Many)

---

# Business Rules

- Setting Code must be unique.
- System settings cannot be deleted.
- JSON settings supported.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- SystemSettingCreated
- SystemSettingUpdated
- SystemSettingDeleted

---

# Developer Notes

- Aggregate Root for configuration.
- Cached by Configuration Service.

---

# ====================================================
# 002 FeatureFlag
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** FeatureFlag

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

FeatureFlag enables or disables application functionality without requiring deployment.

Feature flags support gradual rollout, beta testing and company-specific features.

Examples include:

- WhatsApp Integration
- AI Assistant
- Rental Module
- Payroll Module
- Advanced Reporting
- Inventory Forecasting
- Multi-Branch Support

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- Every Business Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| FeatureFlagId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| FeatureCode | NVARCHAR(100) | No | | | | Feature Code |
| FeatureName | NVARCHAR(250) | No | | | | Feature Name |
| IsEnabled | BIT | No | 0 | | | Enabled |
| RolloutPercentage | DECIMAL(5,2) | No | 100.00 | | | Feature Rollout |
| EffectiveDate | DATETIME2(7) | Yes | NULL | | | Activation Date |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiry |
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

PK_FeatureFlag

## Foreign Keys

- SystemSettingId → SystemSetting

---

# Indexes

## Clustered

PK_FeatureFlag

## Non Clustered

IX_FeatureCode

IX_IsEnabled

IX_EffectiveDate

---

# Relationships

SystemSetting (1) → FeatureFlag (Many)

---

# Business Rules

- Feature Code unique.
- Rollout percentage between 0–100.
- Expired features disabled automatically.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- FeatureEnabled
- FeatureDisabled
- FeatureRolloutChanged

---

# Developer Notes

- Supports progressive feature rollout.
- Cached for application startup.

---

# ====================================================
# 003 NumberSequence
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** NumberSequence

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

NumberSequence manages automatic document numbering throughout RentalERP.

Examples include:

- Sales Invoice
- Purchase Order
- Rental Contract
- Asset Number
- Customer Code
- Vendor Code
- Journal Voucher
- Warehouse Transfer

Supports configurable prefixes, suffixes and fiscal-year based numbering.

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- Sales Domain
- Purchase Domain
- Accounting Domain
- Rental Domain
- Asset Domain

...

# ====================================================
# 003 NumberSequence
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** NumberSequence

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

NumberSequence manages automatic numbering for every business document within RentalERP.

Each sequence is independently configurable and supports prefixes, suffixes, fiscal-year resets, branch-specific numbering and padding rules.

Examples include:

- Sales Invoice
- Purchase Order
- Rental Contract
- Asset Number
- Customer Code
- Vendor Code
- Journal Voucher
- Service Order
- Goods Receipt
- Payment Voucher

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- Sales Domain
- Purchase Domain
- Rental Domain
- Service Domain
- Asset Domain
- Accounting Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| NumberSequenceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| SequenceCode | NVARCHAR(100) | No | | | | Sequence Code |
| SequenceName | NVARCHAR(250) | No | | | | Sequence Name |
| Prefix | NVARCHAR(50) | Yes | NULL | | | Prefix |
| Suffix | NVARCHAR(50) | Yes | NULL | | | Suffix |
| CurrentNumber | BIGINT | No | 1 | | | Current Number |
| IncrementBy | INT | No | 1 | | | Increment |
| NumberLength | SMALLINT | No | 6 | | | Zero Padding |
| ResetPolicy | SMALLINT | No | | | | Never / Monthly / Yearly / Fiscal |
| LastResetDate | DATETIME2(7) | Yes | NULL | | | Last Reset |
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

PK_NumberSequence

## Foreign Keys

- SystemSettingId → SystemSetting

## Unique Keys

- UQ_Sequence_Code

---

# Indexes

## Clustered

PK_NumberSequence

## Non Clustered

IX_SequenceCode

IX_CurrentNumber

IX_ResetPolicy

---

# Relationships

SystemSetting (1) → NumberSequence (Many)

---

# Business Rules

- Sequence Code must be unique.
- Current Number cannot decrease.
- Reset policy automatically executed.
- Sequence generation must be transactional.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- NumberGenerated
- NumberSequenceReset
- NumberSequenceUpdated

---

# Developer Notes

- Thread-safe number generation required.
- Supports fiscal year numbering.

---

# ====================================================
# 004 Localization
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** Localization

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

Localization stores language and regional settings used throughout RentalERP.

Examples include:

- English
- Arabic
- Urdu
- French
- Currency Symbols
- Date Formats
- Time Formats
- Number Formats

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- UI Layer

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| LocalizationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| CultureCode | NVARCHAR(20) | No | | | | en-US |
| LanguageName | NVARCHAR(100) | No | | | | English |
| CurrencySymbol | NVARCHAR(10) | Yes | NULL | | | Currency |
| DateFormat | NVARCHAR(50) | No | | | | dd/MM/yyyy |
| TimeFormat | NVARCHAR(50) | No | | | | HH:mm:ss |
| DecimalSeparator | NVARCHAR(5) | No | "." | | | Decimal |
| ThousandSeparator | NVARCHAR(5) | No | "," | | | Thousands |
| IsDefault | BIT | No | 0 | | | Default Language |
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

PK_Localization

## Foreign Keys

- SystemSettingId → SystemSetting

---

# Indexes

## Clustered

PK_Localization

## Non Clustered

IX_CultureCode

IX_IsDefault

---

# Relationships

SystemSetting (1) → Localization (Many)

---

# Business Rules

- Only one default localization allowed.
- Culture Code must be unique.
- Localization affects entire UI.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- LocalizationAdded
- LocalizationUpdated
- DefaultLocalizationChanged

---

# Developer Notes

- Supports multilingual UI.
- Used during application startup.

---

# ====================================================
# 005 CurrencyConfiguration
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** CurrencyConfiguration

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

CurrencyConfiguration defines currencies supported by RentalERP and their formatting rules.

Examples include:

- PKR
- USD
- EUR
- GBP
- AED
- SAR

Supports multi-currency environments.

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- Accounting Domain
- Sales Domain
- Purchase Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| CurrencyConfigurationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| CurrencyCode | NVARCHAR(10) | No | | | | ISO Code |
| CurrencyName | NVARCHAR(100) | No | | | | Currency Name |
| CurrencySymbol | NVARCHAR(10) | No | | | | Symbol |
| DecimalPlaces | SMALLINT | No | 2 | | | Precision |
| ExchangeRate | DECIMAL(18,6) | No | 1 | | | Base Rate |
| IsBaseCurrency | BIT | No | 0 | | | Base Currency |
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

PK_CurrencyConfiguration

## Foreign Keys

- SystemSettingId → SystemSetting

---

# Indexes

## Clustered

PK_CurrencyConfiguration

## Non Clustered

IX_CurrencyCode

IX_IsBaseCurrency

IX_IsEnabled

---

# Relationships

SystemSetting (1) → CurrencyConfiguration (Many)

---

# Business Rules

- Only one base currency permitted.
- ISO Currency Codes required.
- Exchange rates configurable.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- CurrencyAdded
- CurrencyUpdated
- BaseCurrencyChanged

---

# Developer Notes

- Supports multi-currency accounting.
- Used by financial calculations.

...

# ====================================================
# 006 TaxConfiguration
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** TaxConfiguration

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

TaxConfiguration defines taxation rules used throughout RentalERP.

It centralizes all tax-related settings to ensure consistent tax calculation across Purchasing, Sales, Rental and Accounting.

Examples include:

- Sales Tax
- VAT
- GST
- Withholding Tax
- Service Tax
- Zero Rated Tax
- Exempt Tax

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- Sales Domain
- Purchase Domain
- Rental Domain
- Accounting Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| TaxConfigurationId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| TaxCode | NVARCHAR(50) | No | | | | Tax Code |
| TaxName | NVARCHAR(200) | No | | | | Tax Name |
| TaxRate | DECIMAL(9,4) | No | 0 | | | Percentage |
| TaxType | SMALLINT | No | | | | VAT / GST / WHT |
| CalculationMethod | SMALLINT | No | | | | Inclusive / Exclusive |
| EffectiveDate | DATETIME2(7) | No | | | | Effective Date |
| ExpiryDate | DATETIME2(7) | Yes | NULL | | | Expiry |
| IsDefault | BIT | No | 0 | | | Default Tax |
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

PK_TaxConfiguration

## Foreign Keys

- SystemSettingId → SystemSetting

---

# Indexes

## Clustered

PK_TaxConfiguration

## Non Clustered

IX_TaxCode

IX_EffectiveDate

IX_IsDefault

---

# Relationships

SystemSetting (1) → TaxConfiguration (Many)

---

# Business Rules

- Tax Code must be unique.
- Only one default tax per tax type.
- Expired taxes cannot be used.
- Tax rate cannot be negative.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- TaxConfigurationCreated
- TaxConfigurationUpdated
- DefaultTaxChanged

---

# Developer Notes

- Supports future tax changes.
- Used by pricing engine.

---

# ====================================================
# 007 ApplicationPreference
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** ApplicationPreference

**Classification:** Configuration Table

**Aggregate Root:** SystemSetting

---

# Purpose

ApplicationPreference stores configurable application behavior and user interface preferences shared across the organization.

Examples include:

- Default Landing Page
- Theme
- Sidebar Mode
- Grid Page Size
- Default Dashboard
- Default Printer
- Notification Preferences
- Auto Save
- Dark Mode

---

# Dependencies

Depends On

- SystemSetting

Referenced By

- UI Layer
- Dashboard Domain

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ApplicationPreferenceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
| PreferenceCode | NVARCHAR(100) | No | | | | Preference Code |
| PreferenceName | NVARCHAR(200) | No | | | | Preference Name |
| PreferenceValue | NVARCHAR(MAX) | Yes | NULL | | | Value |
| DataType | NVARCHAR(50) | No | | | | String / JSON / Boolean |
| Category | NVARCHAR(100) | No | | | | UI / General |
| IsEditable | BIT | No | 1 | | | Editable |
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

PK_ApplicationPreference

## Foreign Keys

- SystemSettingId → SystemSetting

---

# Indexes

## Clustered

PK_ApplicationPreference

## Non Clustered

IX_PreferenceCode

IX_Category

IX_IsEditable

---

# Relationships

SystemSetting (1) → ApplicationPreference (Many)

---

# Business Rules

- Preference Code must be unique.
- System preferences may be read-only.
- JSON preferences supported.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ApplicationPreferenceCreated
- ApplicationPreferenceUpdated

---

# Developer Notes

- Used by Angular UI.
- Supports configurable UX.

---

# ====================================================
# 008 ConfigurationAttachment
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** ConfigurationAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates System Settings with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Company Logo
- Branding Assets
- Configuration Documents
- Tax Documents
- Localization Files
- Import Templates

---

# Dependencies

Depends On

- SystemSetting
- Attachment

Referenced By

- Configuration Administration

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ConfigurationAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
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

PK_ConfigurationAttachment

## Foreign Keys

- SystemSettingId → SystemSetting
- AttachmentId → Attachment

---

# Indexes

## Clustered

PK_ConfigurationAttachment

## Non Clustered

IX_SystemSetting

IX_Attachment

---

# Relationships

SystemSetting (1) → ConfigurationAttachment (Many)

Attachment (1) → ConfigurationAttachment (Many)

---

# Business Rules

- Unlimited attachments supported.
- Shared Attachment reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ConfigurationAttachmentAdded
- ConfigurationAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Stores configuration resources.

...

# ====================================================
# 009 ConfigurationNote
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** ConfigurationNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

ConfigurationNote associates System Settings with reusable Note records maintained within the Shared Kernel.

Configuration Notes document implementation guidance, operational procedures, deployment instructions and administrator remarks without modifying the configuration itself.

Examples include:

- Configuration Documentation
- Deployment Notes
- Upgrade Instructions
- Administrator Comments
- Business Rules
- Operational Guidelines

---

# Dependencies

Depends On

- SystemSetting
- Note

Referenced By

- Configuration Administration
- Operations Team

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ConfigurationNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
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

PK_ConfigurationNote

## Foreign Keys

- SystemSettingId → SystemSetting
- NoteId → Note

## Unique Keys

- UQ_Configuration_Note (SystemSettingId, NoteId)

---

# Indexes

## Clustered

PK_ConfigurationNote

## Non Clustered

IX_SystemSetting

IX_Note

IX_Status

---

# Relationships

SystemSetting (1) → ConfigurationNote (Many)

Note (1) → ConfigurationNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Shared Note reused throughout ERP.
- Configuration owns only the relationship.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ConfigurationNoteAdded
- ConfigurationNoteUpdated
- ConfigurationNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports configuration documentation.

---

# ====================================================
# 010 ConfigurationActivity
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** ConfigurationActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

ConfigurationActivity associates System Settings with reusable Activity records maintained within the Shared Kernel.

Activities capture operational events occurring during the lifecycle of configuration changes.

Examples include:

- Setting Created
- Setting Updated
- Feature Enabled
- Number Sequence Reset
- Currency Updated
- Tax Configuration Changed
- Localization Added
- Preference Modified

---

# Dependencies

Depends On

- SystemSetting
- Activity

Referenced By

- Configuration Dashboard
- Administration Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ConfigurationActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
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

PK_ConfigurationActivity

## Foreign Keys

- SystemSettingId → SystemSetting
- ActivityId → Activity

## Unique Keys

- UQ_Configuration_Activity (SystemSettingId, ActivityId)

---

# Indexes

## Clustered

PK_ConfigurationActivity

## Non Clustered

IX_SystemSetting

IX_Activity

IX_Status

---

# Relationships

SystemSetting (1) → ConfigurationActivity (Many)

Activity (1) → ConfigurationActivity (Many)

---

# Business Rules

- Activities are append-only.
- Operational history cannot be modified.
- Shared Activity reused across ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ConfigurationActivityCreated
- ConfigurationActivityUpdated

---

# Developer Notes

- Integrates with Administration Dashboard.
- Maintains configuration history.

---

# ====================================================
# 011 ConfigurationTimeline
# ====================================================

# Table Classification

**Domain:** System Configuration Domain

**Table Name:** ConfigurationTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

ConfigurationTimeline associates System Settings with reusable Timeline records maintained within the Shared Kernel.

Timeline provides a complete chronological history of every configuration event.

Examples include:

- Setting Created
- Setting Modified
- Feature Enabled
- Currency Added
- Tax Updated
- Number Sequence Reset
- Localization Changed
- Preference Updated

---

# Dependencies

Depends On

- SystemSetting
- Timeline

Referenced By

- Configuration Detail Screen
- Timeline Widget
- Administration Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ConfigurationTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| SystemSettingId | BIGINT | No | | | ✔ | System Setting |
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

PK_ConfigurationTimeline

## Foreign Keys

- SystemSettingId → SystemSetting
- TimelineId → Timeline

## Unique Keys

- UQ_Configuration_Timeline (SystemSettingId, TimelineId)

---

# Indexes

## Clustered

PK_ConfigurationTimeline

## Non Clustered

IX_SystemSetting

IX_Timeline

IX_Status

---

# Relationships

SystemSetting (1) → ConfigurationTimeline (Many)

Timeline (1) → ConfigurationTimeline (Many)

---

# Business Rules

- Timeline entries are immutable.
- Timeline is append-only.
- Business ownership belongs to System Configuration Domain.
- Shared Timeline reused throughout ERP.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ConfigurationTimelineCreated
- ConfigurationTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for configuration history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The System Configuration Domain centralizes all application-wide configuration required by RentalERP.

It manages global settings, feature flags, numbering sequences, localization, currencies, taxes and application preferences, ensuring every business domain consumes consistent configuration while avoiding duplication.

---

## Aggregate Roots

- SystemSetting

---

## Supporting Entities

- FeatureFlag
- NumberSequence
- Localization
- CurrencyConfiguration
- TaxConfiguration
- ApplicationPreference

---

## Bridge Entities

- ConfigurationAttachment
- ConfigurationNote
- ConfigurationActivity
- ConfigurationTimeline

---

## Major Business Capabilities

- Global Application Settings
- Company Configuration
- Feature Flag Management
- Automatic Number Sequences
- Localization
- Currency Configuration
- Tax Configuration
- Application Preferences
- UI Preferences
- Business Preferences
- Environment Configuration
- Shared Kernel Integration

---

## Published Domain Events

The System Configuration Domain publishes events including:

- SystemSettingCreated
- SystemSettingUpdated
- FeatureEnabled
- FeatureDisabled
- NumberGenerated
- NumberSequenceReset
- LocalizationAdded
- CurrencyUpdated
- BaseCurrencyChanged
- TaxConfigurationUpdated
- ApplicationPreferenceUpdated

These events integrate with:

- Administration Domain
- Security Domain
- Audit Domain
- Dashboard Domain
- Reporting Domain
- Notification Domain
- Scheduler Domain
- Every Business Domain

---

## Integration Points

The System Configuration Domain integrates directly with:

- Foundation
- Shared Kernel
- Administration Domain
- Security Domain
- Audit Domain
- Dashboard Domain
- Reporting Domain
- Notification Domain
- Scheduler Domain
- All Business Domains

---

# System Configuration Domain Status

**Status:** ✅ Complete

**Total Tables:** 11

1. SystemSetting
2. FeatureFlag
3. NumberSequence
4. Localization
5. CurrencyConfiguration
6. TaxConfiguration
7. ApplicationPreference
8. ConfigurationAttachment
9. ConfigurationNote
10. ConfigurationActivity
11. ConfigurationTimeline

---
