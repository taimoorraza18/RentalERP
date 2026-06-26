# RentalERP v1.0

# AccountingDomain.docx

---

# Document Information

**Project:** RentalERP v1.0

**Domain:** Accounting

**Architecture:** Domain Driven Design (DDD)

**Database:** Microsoft SQL Server

**Application:** .NET Core Web API + Angular

**Status:** In Progress

**Version:** 1.0

---

# Revision History

| Version | Date | Description | Author |
|----------|------|-------------|--------|
| 1.0 | June 2026 | Initial Accounting Domain Documentation | ChatGPT |

---

# Table of Contents

1. Domain Overview

2. Business Objectives

3. Aggregate Root

4. Implementation Order

5. ChartOfAccount

6. FiscalYear

7. FiscalPeriod

---

# Domain Overview

The Accounting Domain is the financial backbone of RentalERP.

It is responsible for recording every financial transaction generated throughout the ERP, including Purchase, Sales, Rental, Service, Inventory, Asset Depreciation and General Journal transactions.

Rather than allowing business domains to directly manipulate ledger balances, all financial events are translated into accounting entries through the Accounting Domain.

The Accounting Domain owns the General Ledger and guarantees financial integrity, auditability and regulatory compliance.

---

# Business Objectives

The Accounting Domain provides:

- Chart of Accounts
- Fiscal Years
- Fiscal Periods
- Voucher Types
- Journal Entries
- Journal Lines
- General Ledger
- Trial Balance
- Customer Receivables
- Vendor Payables
- Bank Transactions
- Cash Transactions
- Revenue Recognition
- Expense Recognition
- Financial Statements
- Complete Audit Trail

---

# Aggregate Root

## Primary Aggregate Roots

- JournalEntry

## Supporting Entities

- ChartOfAccount
- FiscalYear
- FiscalPeriod
- VoucherType
- JournalEntryLine
- AccountBalance
- AccountTransaction

## Bridge Entities

- AccountingAttachment
- AccountingNote
- AccountingActivity
- AccountingTimeline

---

# Implementation Order

001 ChartOfAccount

002 FiscalYear

003 FiscalPeriod

004 VoucherType

005 JournalEntry

006 JournalEntryLine

007 AccountBalance

008 AccountTransaction

009 AccountingAttachment

010 AccountingNote

011 AccountingActivity

012 AccountingTimeline

---

# ====================================================
# 001 ChartOfAccount
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** ChartOfAccount

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

ChartOfAccount (COA) stores every financial account used throughout the ERP.

Every financial transaction ultimately posts debit and credit entries against these accounts.

The COA supports a hierarchical structure, allowing organizations to define Assets, Liabilities, Equity, Revenue, Expenses and Cost of Sales accounts.

---

# Dependencies

Depends On

- Company
- Currency

Referenced By

- JournalEntryLine
- AccountBalance
- AccountTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| ChartOfAccountId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| AccountCode | NVARCHAR(30) | No | | | | Account Code |
| AccountName | NVARCHAR(250) | No | | | | Account Name |
| ParentAccountId | BIGINT | Yes | NULL | | ✔ | Parent Account |
| AccountType | SMALLINT | No | | | | Asset / Liability / Equity / Revenue / Expense |
| NormalBalance | SMALLINT | No | | | | Debit / Credit |
| CurrencyId | BIGINT | Yes | NULL | | ✔ | Currency |
| IsControlAccount | BIT | No | 0 | | | Control Account |
| AllowManualPosting | BIT | No | 1 | | | Manual Posting |
| IsActive | BIT | No | 1 | | | Active |
| StatusId | SMALLINT | No | 1 | | | Active / Inactive |
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

PK_ChartOfAccount

## Foreign Keys

- CompanyId → Company
- ParentAccountId → ChartOfAccount
- CurrencyId → Currency

## Unique Keys

- UQ_AccountCode

---

# Indexes

## Clustered

PK_ChartOfAccount

## Non Clustered

IX_AccountCode

IX_ParentAccount

IX_AccountType

IX_Status

---

# Relationships

ChartOfAccount (1) → ChartOfAccount (Many)

ChartOfAccount (1) → JournalEntryLine (Many)

---

# Business Rules

- Account Code must be unique.
- Parent Account optional.
- Posting allowed only to leaf accounts.
- Control Accounts cannot be posted manually.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- ChartOfAccountCreated
- ChartOfAccountUpdated
- ChartOfAccountActivated
- ChartOfAccountDeactivated

---

# Developer Notes

- Supports unlimited hierarchy.
- Root of all financial postings.
- Used throughout the ERP.

---

# ====================================================
# 002 FiscalYear
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** FiscalYear

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

Represents an Accounting Fiscal Year.

Fiscal Years control financial reporting periods and determine whether accounting transactions are permitted.

Only one Fiscal Year may be active at any given time.

---

# Dependencies

Depends On

- Company

Referenced By

- FiscalPeriod
- JournalEntry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| FiscalYearId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| FiscalYearCode | NVARCHAR(20) | No | | | | FY2026 |
| StartDate | DATE | No | | | | Start Date |
| EndDate | DATE | No | | | | End Date |
| IsCurrent | BIT | No | 0 | | | Current Fiscal Year |
| IsClosed | BIT | No | 0 | | | Closed |
| ClosingDate | DATE | Yes | NULL | | | Closing Date |
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

PK_FiscalYear

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_FiscalYear_Code

## Check Constraints

- EndDate > StartDate

---

# Indexes

## Clustered

PK_FiscalYear

## Non Clustered

IX_FiscalYearCode

IX_Current

IX_Closed

---

# Relationships

FiscalYear (1) → FiscalPeriod (Many)

---

# Business Rules

- Only one Current Fiscal Year.
- Closed Fiscal Years cannot accept transactions.
- Fiscal Year cannot overlap another.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- FiscalYearOpened
- FiscalYearClosed

---

# Developer Notes

- Used for financial reporting.
- Controls accounting period availability.

---

# ====================================================
# 003 FiscalPeriod
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** FiscalPeriod

**Classification:** Master Table

**Aggregate Root:** FiscalYear

---

# Purpose

FiscalPeriod represents monthly (or configurable) accounting periods within a Fiscal Year.

Transactions can only be posted into open periods.

Closing a period prevents further accounting entries while preserving historical financial integrity.

---

# Dependencies

Depends On

- FiscalYear

Referenced By

- JournalEntry

....

# ====================================================
# 003 FiscalPeriod
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** FiscalPeriod

**Classification:** Master Table

**Aggregate Root:** FiscalYear

---

# Purpose

FiscalPeriod represents individual accounting periods within a Fiscal Year.

Typically, one Fiscal Year consists of twelve monthly periods, although quarterly or custom periods are also supported.

Journal Entries may only be posted into open Fiscal Periods.

---

# Dependencies

Depends On

- FiscalYear

Referenced By

- JournalEntry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| FiscalPeriodId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| FiscalYearId | BIGINT | No | | | ✔ | Fiscal Year |
| PeriodNo | SMALLINT | No | | | | Period Number |
| PeriodName | NVARCHAR(50) | No | | | | January 2026 |
| StartDate | DATE | No | | | | Start Date |
| EndDate | DATE | No | | | | End Date |
| IsOpen | BIT | No | 1 | | | Open Period |
| IsClosed | BIT | No | 0 | | | Closed |
| ClosingDate | DATE | Yes | NULL | | | Closing Date |
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

PK_FiscalPeriod

## Foreign Keys

- FiscalYearId → FiscalYear

## Unique Keys

- UQ_FiscalYear_PeriodNo

## Check Constraints

- EndDate >= StartDate

---

# Indexes

## Clustered

PK_FiscalPeriod

## Non Clustered

IX_FiscalYear

IX_PeriodNo

IX_Open

IX_Closed

---

# Relationships

FiscalYear (1) → FiscalPeriod (Many)

FiscalPeriod (1) → JournalEntry (Many)

---

# Business Rules

- Period Number unique within Fiscal Year.
- Closed Periods cannot receive Journal Entries.
- Only one status (Open/Closed).
- Closing is irreversible without authorization.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- FiscalPeriodOpened
- FiscalPeriodClosed

---

# Developer Notes

- Controls accounting posting windows.
- Used for period-end processing.

---

# ====================================================
# 004 VoucherType
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** VoucherType

**Classification:** Master Table

**Aggregate Root:** No

---

# Purpose

VoucherType defines the various accounting documents used throughout the ERP.

Each Journal Entry is categorized by a Voucher Type to identify its business origin.

Examples include:

- General Journal
- Cash Receipt
- Cash Payment
- Bank Receipt
- Bank Payment
- Purchase Invoice
- Sales Invoice
- Rental Invoice
- Asset Depreciation
- Inventory Adjustment

---

# Dependencies

Depends On

- Company

Referenced By

- JournalEntry

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| VoucherTypeId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| VoucherCode | NVARCHAR(20) | No | | | | JV / BPV / BRV |
| VoucherName | NVARCHAR(100) | No | | | | Voucher Name |
| NumberPrefix | NVARCHAR(20) | Yes | NULL | | | Prefix |
| AutoNumbering | BIT | No | 1 | | | Auto Number |
| IsSystemGenerated | BIT | No | 0 | | | System Voucher |
| AllowManualEntry | BIT | No | 1 | | | Manual Journal |
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

PK_VoucherType

## Foreign Keys

- CompanyId → Company

## Unique Keys

- UQ_Voucher_Code

---

# Indexes

## Clustered

PK_VoucherType

## Non Clustered

IX_VoucherCode

IX_Status

IX_SystemGenerated

---

# Relationships

VoucherType (1) → JournalEntry (Many)

---

# Business Rules

- Voucher Code must be unique.
- System Vouchers cannot be deleted.
- Manual Journals require AllowManualEntry.
- Auto Numbering uses Number Series.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- VoucherTypeCreated
- VoucherTypeUpdated

---

# Developer Notes

- Maps ERP transactions to Accounting.
- Used by Posting Engine.

---

# ====================================================
# 005 JournalEntry
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** JournalEntry

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

JournalEntry represents the official accounting transaction within RentalERP.

Every financial event generated by Purchase, Sales, Rental, Service, Inventory or Assets is converted into one Journal Entry.

Each Journal Entry contains one or more Journal Entry Lines that satisfy the double-entry accounting principle.

Only posted Journal Entries affect the General Ledger.

---

# Dependencies

Depends On

- Company
- Branch
- FiscalYear
- FiscalPeriod
- VoucherType
- NumberSeries

Referenced By

- JournalEntryLine
- AccountBalance
- AccountTransaction

...

# ====================================================
# 005 JournalEntry
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** JournalEntry

**Classification:** Transaction Header

**Aggregate Root:** Yes

---

# Purpose

JournalEntry represents the official accounting transaction within RentalERP.

Every financial event generated by Purchase, Sales, Rental, Service, Inventory, Asset Management and General Accounting is converted into one Journal Entry.

Each Journal Entry consists of one or more Journal Entry Lines that satisfy the Double Entry Accounting principle.

Only Posted Journal Entries update the General Ledger.

---

# Dependencies

Depends On

- Company
- Branch
- FiscalYear
- FiscalPeriod
- VoucherType
- NumberSeries

Referenced By

- JournalEntryLine
- AccountBalance
- AccountTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JournalEntryId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| CompanyId | BIGINT | No | | | ✔ | Company |
| BranchId | BIGINT | No | | | ✔ | Branch |
| FiscalYearId | BIGINT | No | | | ✔ | Fiscal Year |
| FiscalPeriodId | BIGINT | No | | | ✔ | Fiscal Period |
| VoucherTypeId | BIGINT | No | | | ✔ | Voucher Type |
| JournalNo | NVARCHAR(30) | No | Number Series | | | Journal Number |
| JournalDate | DATE | No | GETDATE() | | | Journal Date |
| ReferenceNo | NVARCHAR(100) | Yes | NULL | | | External Reference |
| SourceDocument | NVARCHAR(100) | Yes | NULL | | | Purchase Invoice, Sales Invoice, Rental Contract |
| SourceDocumentId | BIGINT | Yes | NULL | | | Source Record |
| Description | NVARCHAR(1000) | Yes | NULL | | | Description |
| TotalDebit | DECIMAL(18,2) | No | 0 | | | Debit Total |
| TotalCredit | DECIMAL(18,2) | No | 0 | | | Credit Total |
| IsPosted | BIT | No | 0 | | | Posted |
| PostedDate | DATETIME2(7) | Yes | NULL | | | Posted Date |
| PostedBy | BIGINT | Yes | NULL | | ✔ | Employee |
| StatusId | SMALLINT | No | 1 | | | Draft / Posted / Reversed |
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

PK_JournalEntry

## Foreign Keys

- CompanyId → Company
- BranchId → Branch
- FiscalYearId → FiscalYear
- FiscalPeriodId → FiscalPeriod
- VoucherTypeId → VoucherType
- PostedBy → Employee

## Unique Keys

- UQ_Journal_No

## Check Constraints

- TotalDebit >= 0
- TotalCredit >= 0
- TotalDebit = TotalCredit

---

# Indexes

## Clustered

PK_JournalEntry

## Non Clustered

IX_JournalNo

IX_JournalDate

IX_FiscalPeriod

IX_VoucherType

IX_Status

---

# Relationships

JournalEntry (1) → JournalEntryLine (Many)

VoucherType (1) → JournalEntry (Many)

FiscalPeriod (1) → JournalEntry (Many)

---

# Business Rules

- Journal Number generated using Number Series.
- Total Debit must equal Total Credit.
- Posted Journal Entries become read-only.
- Only open Fiscal Periods allow posting.
- Every ERP financial transaction produces one Journal Entry.
- Reversals create new Journal Entries.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JournalEntryCreated
- JournalEntryPosted
- JournalEntryReversed

---

# Developer Notes

- Root of the General Ledger.
- Implements Double Entry Accounting.
- Never directly modifies account balances until posting.

---

# ====================================================
# 006 JournalEntryLine
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** JournalEntryLine

**Classification:** Transaction Detail

**Aggregate Root:** JournalEntry

---

# Purpose

JournalEntryLine stores every debit and credit posting belonging to a Journal Entry.

Each Journal Entry contains at least two Journal Entry Lines, maintaining the accounting equation.

These lines ultimately update Account Balances and Account Transactions after posting.

---

# Dependencies

Depends On

- JournalEntry
- ChartOfAccount

Referenced By

- AccountBalance
- AccountTransaction

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| JournalEntryLineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
| ChartOfAccountId | BIGINT | No | | | ✔ | Account |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| DebitAmount | DECIMAL(18,2) | No | 0 | | | Debit |
| CreditAmount | DECIMAL(18,2) | No | 0 | | | Credit |
| CurrencyId | BIGINT | Yes | NULL | | ✔ | Currency |
| ExchangeRate | DECIMAL(18,8) | No | 1 | | | Exchange Rate |
| ReferenceNo | NVARCHAR(100) | Yes | NULL | | | Reference |
| CostCenterId | BIGINT | Yes | NULL | | ✔ | Cost Center |
| ProjectId | BIGINT | Yes | NULL | | ✔ | Project |
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

PK_JournalEntryLine

## Foreign Keys

- JournalEntryId → JournalEntry
- ChartOfAccountId → ChartOfAccount
- CurrencyId → Currency
- CostCenterId → CostCenter
- ProjectId → Project

## Check Constraints

- DebitAmount >= 0
- CreditAmount >= 0
- NOT (DebitAmount > 0 AND CreditAmount > 0)

---

# Indexes

## Clustered

PK_JournalEntryLine

## Non Clustered

IX_JournalEntry

IX_ChartOfAccount

IX_CostCenter

IX_Project

---

# Relationships

JournalEntry (1) → JournalEntryLine (Many)

ChartOfAccount (1) → JournalEntryLine (Many)

---

# Business Rules

- Each line must be Debit or Credit, never both.
- At least two lines required.
- Lines become read-only after posting.
- Supports Cost Center accounting.
- Supports Project accounting.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- JournalEntryLineCreated
- AccountPosted

---

# Developer Notes

- Supports financial dimensions.
- Used by Posting Engine.
- Updates General Ledger during posting.

---

# ====================================================
# 007 AccountBalance
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountBalance

**Classification:** Summary Table

**Aggregate Root:** ChartOfAccount

---

# Purpose

AccountBalance stores summarized balances for every General Ledger account by Fiscal Period.

This table exists for reporting performance and avoids recalculating balances from millions of Journal Entry Lines.

It is updated automatically whenever Journal Entries are posted or reversed.

---

# Dependencies

Depends On

- ChartOfAccount
- FiscalPeriod

Referenced By

- Trial Balance
- Balance Sheet
- Profit & Loss
- Financial Dashboard

...

# ====================================================
# 007 AccountBalance
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountBalance

**Classification:** Summary Table

**Aggregate Root:** ChartOfAccount

---

# Purpose

AccountBalance stores summarized balances for every General Ledger account by Fiscal Period.

Instead of calculating balances from millions of Journal Entry Lines, this table maintains opening balances, period activity and closing balances for each accounting period.

The Posting Engine automatically updates this table whenever Journal Entries are posted or reversed.

---

# Dependencies

Depends On

- ChartOfAccount
- FiscalPeriod

Referenced By

- Trial Balance
- Balance Sheet
- Profit & Loss
- Financial Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountBalanceId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| ChartOfAccountId | BIGINT | No | | | ✔ | Account |
| FiscalPeriodId | BIGINT | No | | | ✔ | Fiscal Period |
| OpeningDebit | DECIMAL(18,2) | No | 0 | | | Opening Debit |
| OpeningCredit | DECIMAL(18,2) | No | 0 | | | Opening Credit |
| PeriodDebit | DECIMAL(18,2) | No | 0 | | | Period Debit |
| PeriodCredit | DECIMAL(18,2) | No | 0 | | | Period Credit |
| ClosingDebit | DECIMAL(18,2) | No | 0 | | | Closing Debit |
| ClosingCredit | DECIMAL(18,2) | No | 0 | | | Closing Credit |
| LastPostedJournalId | BIGINT | Yes | NULL | | ✔ | Last Journal |
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

PK_AccountBalance

## Foreign Keys

- ChartOfAccountId → ChartOfAccount
- FiscalPeriodId → FiscalPeriod
- LastPostedJournalId → JournalEntry

## Unique Keys

- UQ_Account_Period (ChartOfAccountId, FiscalPeriodId)

---

# Indexes

## Clustered

PK_AccountBalance

## Non Clustered

IX_Account

IX_FiscalPeriod

IX_LastPostedJournal

---

# Relationships

ChartOfAccount (1) → AccountBalance (Many)

FiscalPeriod (1) → AccountBalance (Many)

---

# Business Rules

- One balance record per Account per Fiscal Period.
- Updated only through Posting Engine.
- Manual updates prohibited.
- Closing Balance becomes Opening Balance of next period.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountBalanceUpdated
- PeriodBalanceCalculated

---

# Developer Notes

- Optimized for financial reports.
- Never edited manually.
- Used by Trial Balance and Financial Statements.

---

# ====================================================
# 008 AccountTransaction
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountTransaction

**Classification:** Ledger Table

**Aggregate Root:** ChartOfAccount

---

# Purpose

AccountTransaction represents the detailed General Ledger transaction history for each account.

Every posted Journal Entry Line generates one AccountTransaction record.

Unlike AccountBalance, which stores summaries, this table stores the complete financial audit trail.

---

# Dependencies

Depends On

- JournalEntry
- JournalEntryLine
- ChartOfAccount

Referenced By

- General Ledger
- Account Statement
- Financial Reports
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountTransactionId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
| JournalEntryLineId | BIGINT | No | | | ✔ | Journal Line |
| ChartOfAccountId | BIGINT | No | | | ✔ | Account |
| TransactionDate | DATE | No | | | | Transaction Date |
| DebitAmount | DECIMAL(18,2) | No | 0 | | | Debit |
| CreditAmount | DECIMAL(18,2) | No | 0 | | | Credit |
| RunningBalance | DECIMAL(18,2) | No | 0 | | | Running Balance |
| SourceDocument | NVARCHAR(100) | Yes | NULL | | | Source Document |
| SourceDocumentId | BIGINT | Yes | NULL | | | Source Record |
| Description | NVARCHAR(500) | Yes | NULL | | | Description |
| FiscalPeriodId | BIGINT | No | | | ✔ | Fiscal Period |
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

PK_AccountTransaction

## Foreign Keys

- JournalEntryId → JournalEntry
- JournalEntryLineId → JournalEntryLine
- ChartOfAccountId → ChartOfAccount
- FiscalPeriodId → FiscalPeriod

---

# Indexes

## Clustered

PK_AccountTransaction

## Non Clustered

IX_Account

IX_JournalEntry

IX_TransactionDate

IX_FiscalPeriod

---

# Relationships

ChartOfAccount (1) → AccountTransaction (Many)

JournalEntry (1) → AccountTransaction (Many)

JournalEntryLine (1) → AccountTransaction (One)

---

# Business Rules

- Created only during Journal Posting.
- Immutable after creation.
- Running Balance automatically calculated.
- Source Document retained for traceability.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountTransactionCreated
- GeneralLedgerUpdated

---

# Developer Notes

- Source for General Ledger reports.
- Maintains complete financial history.
- Supports audit and reconciliation.

---

# ====================================================
# 009 AccountingAttachment
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Attachment records maintained in the Shared Kernel.

Examples include:

- Vendor Invoice PDFs
- Customer Invoice PDFs
- Bank Statements
- Cheque Images
- Payment Receipts
- Supporting Documents
- Audit Evidence
- Tax Certificates

---

# Dependencies

Depends On

- JournalEntry
- Attachment

Referenced By

- Journal Entry Detail Screen

...

# ====================================================
# 009 AccountingAttachment
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingAttachment

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Attachment records maintained within the Shared Kernel.

Examples include:

- Vendor Invoice PDFs
- Customer Invoice PDFs
- Bank Statements
- Cheque Images
- Payment Receipts
- Journal Supporting Documents
- Tax Certificates
- External Audit Evidence

The actual file is stored in the Shared Kernel Attachment table while AccountingAttachment establishes the business relationship.

---

# Dependencies

Depends On

- JournalEntry
- Attachment

Referenced By

- Journal Entry Detail Screen
- Financial Reports
- Audit Module

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountingAttachmentId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
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

PK_AccountingAttachment

## Foreign Keys

- JournalEntryId → JournalEntry
- AttachmentId → Attachment

## Unique Keys

- UQ_Journal_Attachment (JournalEntryId, AttachmentId)

---

# Indexes

## Clustered

PK_AccountingAttachment

## Non Clustered

IX_JournalEntry

IX_Attachment

---

# Relationships

JournalEntry (1) → AccountingAttachment (Many)

Attachment (1) → AccountingAttachment (Many)

---

# Business Rules

- Unlimited attachments allowed.
- Attachment records remain in Shared Kernel.
- Business ownership belongs to Accounting Domain.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountingAttachmentAdded
- AccountingAttachmentRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Used for audit evidence and compliance.

---

# ====================================================
# 010 AccountingNote
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingNote

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Note records maintained within the Shared Kernel.

Accounting Notes document financial explanations, audit remarks, reconciliation comments, approval notes and adjustment justifications.

The actual note text remains stored in the Shared Kernel.

---

# Dependencies

Depends On

- JournalEntry
- Note

Referenced By

- Journal Entry Screen
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountingNoteId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
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

PK_AccountingNote

## Foreign Keys

- JournalEntryId → JournalEntry
- NoteId → Note

## Unique Keys

- UQ_Journal_Note (JournalEntryId, NoteId)

---

# Indexes

## Clustered

PK_AccountingNote

## Non Clustered

IX_JournalEntry

IX_Note

---

# Relationships

JournalEntry (1) → AccountingNote (Many)

Note (1) → AccountingNote (Many)

---

# Business Rules

- Unlimited notes supported.
- Shared Note reused across domains.
- Notes remain immutable after posting unless user has financial audit permissions.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountingNoteAdded
- AccountingNoteUpdated
- AccountingNoteRemoved

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Supports audit explanations and reconciliation comments.

---

# ====================================================
# 011 AccountingActivity
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingActivity

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Activity records maintained within the Shared Kernel.

Activities record every operational event occurring during the accounting lifecycle.

Examples include:

- Journal Created
- Journal Approved
- Journal Posted
- Journal Reversed
- Fiscal Period Closed
- Fiscal Year Closed
- Trial Balance Generated
- Financial Statements Published

---

# Dependencies

Depends On

- JournalEntry
- Activity

Referenced By

- Accounting Dashboard
- Workflow Engine
- Audit Reports

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountingActivityId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
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

PK_AccountingActivity

## Foreign Keys

- JournalEntryId → JournalEntry
- ActivityId → Activity

## Unique Keys

- UQ_Journal_Activity (JournalEntryId, ActivityId)

---

# Indexes

## Clustered

PK_AccountingActivity

## Non Clustered

IX_JournalEntry

IX_Activity

IX_Status

---

# Relationships

JournalEntry (1) → AccountingActivity (Many)

Activity (1) → AccountingActivity (Many)

---

# Business Rules

- Activities are append-only.
- Business ownership belongs to Accounting Domain.
- Shared Activity reused across ERP.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountingActivityCreated
- AccountingActivityUpdated

---

# Developer Notes

- Integrates with Workflow Engine.
- Provides complete accounting audit history.

---

# ====================================================
# 012 AccountingTimeline
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Timeline records maintained within the Shared Kernel.

Timeline displays the chronological history of each accounting transaction from creation through posting and possible reversal.

Examples include:

- Journal Created
- Journal Approved
- Journal Posted
- Bank Reconciled
- Fiscal Period Closed
- Journal Reversed
- Audit Reviewed

---

# Dependencies

Depends On

- JournalEntry
- Timeline

Referenced By

- Journal Detail Screen
- Timeline Widget
- Audit Reports

...


# ====================================================
# 012 AccountingTimeline
# ====================================================

# Table Classification

**Domain:** Accounting Domain

**Table Name:** AccountingTimeline

**Classification:** Bridge Table

**Aggregate Root:** No

---

# Purpose

Associates Journal Entries with reusable Timeline records maintained within the Shared Kernel.

The Accounting Timeline provides a complete chronological history of every accounting transaction from creation until financial period closure.

Examples include:

- Journal Created
- Journal Approved
- Journal Posted
- Journal Reversed
- Payment Reconciled
- Fiscal Period Closed
- Fiscal Year Closed
- External Audit Completed

The actual timeline record resides in the Shared Kernel while AccountingTimeline links it to the accounting transaction.

---

# Dependencies

Depends On

- JournalEntry
- Timeline

Referenced By

- Journal Entry Detail Screen
- Timeline Widget
- Audit Reports
- Financial Dashboard

---

# Complete Database Schema

| Column Name | SQL Data Type | Nullable | Default | PK | FK | Description |
|--------------|---------------|----------|---------|----|----|-------------|
| AccountingTimelineId | BIGINT IDENTITY(1,1) | No | Identity | ✔ | | Primary Key |
| JournalEntryId | BIGINT | No | | | ✔ | Journal Entry |
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

PK_AccountingTimeline

---

## Foreign Keys

- JournalEntryId → JournalEntry
- TimelineId → Timeline

---

## Unique Keys

- UQ_Journal_Timeline (JournalEntryId, TimelineId)

---

# Indexes

## Clustered

PK_AccountingTimeline

---

## Non Clustered

IX_JournalEntry

IX_Timeline

IX_Status

---

# Relationships

JournalEntry (1) → AccountingTimeline (Many)

Timeline (1) → AccountingTimeline (Many)

---

# Business Rules

- Timeline entries are append-only.
- Timeline history is immutable.
- Business ownership belongs to Accounting Domain.
- Shared Timeline records remain reusable.
- Soft Delete only.
- Audit fields mandatory.
- RowVersion mandatory.

---

# Published Domain Events

- AccountingTimelineCreated
- AccountingTimelineUpdated

---

# Developer Notes

- Implements Shared Kernel Bridge Pattern.
- Optimized for timeline rendering.
- Provides complete accounting lifecycle history.

---

# ====================================================
# Domain Summary
# ====================================================

## Domain Overview

The Accounting Domain is the financial engine of RentalERP.

Every financial event originating from operational domains is ultimately converted into balanced Journal Entries and posted into the General Ledger.

The domain maintains financial integrity through Double Entry Accounting while supporting period control, account hierarchies, auditability and statutory reporting.

---

## Aggregate Roots

- JournalEntry

---

## Supporting Entities

- ChartOfAccount
- FiscalYear
- FiscalPeriod
- VoucherType
- JournalEntryLine
- AccountBalance
- AccountTransaction

---

## Bridge Entities

- AccountingAttachment
- AccountingNote
- AccountingActivity
- AccountingTimeline

---

## Major Business Capabilities

- Chart of Accounts
- General Ledger
- Double Entry Accounting
- Journal Vouchers
- Fiscal Year Management
- Fiscal Period Management
- Trial Balance
- Balance Sheet
- Profit & Loss Statement
- Cash Flow Support
- Cost Center Accounting
- Project Accounting
- Financial Audit Trail
- Multi-Currency Accounting
- Shared Kernel Integration

---

## Published Domain Events

The Accounting Domain publishes events including:

- JournalEntryCreated
- JournalEntryPosted
- JournalEntryReversed
- AccountBalanceUpdated
- GeneralLedgerUpdated
- FiscalPeriodClosed
- FiscalYearClosed
- FinancialStatementsGenerated

These events integrate with:

- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Inventory Domain
- Warehouse Domain
- Asset Domain
- Customer Domain
- Vendor Domain
- Reporting Module
- Notification Module
- Workflow Engine
- Audit Module

---

## Integration Points

The Accounting Domain integrates directly with:

- Foundation
- Shared Kernel
- Purchase Domain
- Sales Domain
- Rental Domain
- Service Domain
- Inventory Domain
- Warehouse Domain
- Asset Domain
- Customer Domain
- Vendor Domain
- Administration

---

# Posting Flow

Every business domain follows the same posting pipeline:

```
Purchase Invoice
        │
        ▼
Purchase Domain Event
        │
        ▼
Accounting Posting Service
        │
        ▼
JournalEntry
        │
        ▼
JournalEntryLines
        │
        ▼
AccountTransaction
        │
        ▼
AccountBalance
        │
        ▼
General Ledger Updated
```

The same process applies to:

- Sales Invoices
- Rental Invoices
- Service Invoices
- Customer Payments
- Vendor Payments
- Asset Depreciation
- Inventory Adjustments
- Manual Journals

---

# Accounting Domain Status

**Status:** ✅ Complete

**Total Tables:** 12

1. ChartOfAccount
2. FiscalYear
3. FiscalPeriod
4. VoucherType
5. JournalEntry
6. JournalEntryLine
7. AccountBalance
8. AccountTransaction
9. AccountingAttachment
10. AccountingNote
11. AccountingActivity
12. AccountingTimeline

---

