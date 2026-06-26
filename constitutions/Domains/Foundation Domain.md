# RentalERP v1.0 — Foundation Module

> **Architecture:** Domain Driven Design (DDD)
> **Database:** Microsoft SQL Server
> **Application:** .NET Core Web API + Angular
> **Status:** In Progress | **Version:** 1.0

---

## Table of Contents

1. [Company](#01-company)
2. [Branch](#02-branch)
3. [FiscalYear](#03-fiscalyear)
4. [Currency](#04-currency)
5. [ExchangeRate](#05-exchangerate)
6. [Country](#06-country)
7. [State / Province](#07-state--province)
8. [User](#09-user)
9. [Role](#10-role)
10. [Permission](#11-permission)
11. [RolePermission](#12-rolepermission)
12. [UserRole](#13-userrole)
13. [Address](#14-address)
14. [Contact](#15-contact)
15. [Attachment](#16-attachment)
16. [Note](#17-note)
17. [Activity](#18-activity)
18. [Timeline](#19-timeline)
19. [NumberSeries](#20-numberseries)

---

## 01 Company

### Purpose

Stores legal entities (tenants) within the ERP. Every business record belongs to a Company.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| CompanyId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyCode | NVARCHAR(20) | | | No | | Unique Business Code |
| CompanyName | NVARCHAR(200) | | | No | | Display Name |
| LegalName | NVARCHAR(250) | | | No | | Registered Legal Name |
| NTN | NVARCHAR(30) | | | Yes | | National Tax Number |
| STRN | NVARCHAR(30) | | | Yes | | Sales Tax Registration |
| CurrencyId | BIGINT | | Currency | No | | Base Currency |
| FiscalYearId | BIGINT | | FiscalYear | No | | Current Fiscal Year |
| Email | NVARCHAR(150) | | | Yes | | Primary Email |
| Phone | NVARCHAR(30) | | | Yes | | Primary Phone |
| Website | NVARCHAR(200) | | | Yes | | Website |
| LogoPath | NVARCHAR(500) | | | Yes | | Logo File |
| Address | NVARCHAR(500) | | | Yes | | Head Office |
| City | NVARCHAR(100) | | City | Yes | | City |
| Country | NVARCHAR(100) | | Country | Yes | | Country |
| IsActive | BIT | | | No | 1 | Active Flag |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| DeletedBy | BIGINT | | User | Yes | | Audit |
| DeletedDate | DATETIME2 | | | Yes | | Soft Delete |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Company (CompanyId)`
- `UQ_Company_CompanyCode`
- `FK_Company_Currency`
- `FK_Company_FiscalYear`
- `FK_Company_CreatedBy`
- `FK_Company_ModifiedBy`
- `FK_Company_DeletedBy`
- `CHECK (CompanyCode <> '')`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on CompanyId |
| Unique Non-Clustered | CompanyCode |
| Non-Clustered | CompanyName |
| Non-Clustered | IsActive |
| Composite | (IsActive, CompanyName) |

### Relationships

- `Company (1) -> (N) Branch`
- `Company (1) -> (N) User`
- `Company (1) -> (N) Customer`
- `Company (1) -> (N) Vendor`
- `Company (1) -> (N) Warehouse`
- `Company (1) -> (N) FiscalYear`
- `Company (1) -> (N) AuditLog`
- `Company (1) -> (N) Attachment`

### Business Rules

- CompanyCode must be unique.
- Company cannot be deleted if dependent records exist.
- Only one active default fiscal year per company.
- All transactional tables must contain CompanyId.

### Accounting Impact

None. This is a setup/master table and does not create journal entries.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/companies | List companies |
| GET | /api/companies/{id} | Company details |
| POST | /api/companies | Create |
| PUT | /api/companies/{id} | Update |
| DELETE | /api/companies/{id} | Deactivate |

### Angular Screens

- Company List
- Company Detail
- Create/Edit Company Dialog
- Company Settings

### SQL Notes

Use BIGINT identity keys throughout the ERP. All foreign keys should use BIGINT for consistency.

---

## 02 Branch

### Purpose

Represents a physical or logical operating branch of a company. Every transaction belongs to a branch.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| BranchId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyId | BIGINT | | Company | No | | Owner Company |
| BranchCode | NVARCHAR(20) | | | No | | Unique within Company |
| BranchName | NVARCHAR(200) | | | No | | Branch Name |
| ManagerUserId | BIGINT | | User | Yes | | Branch Manager |
| Phone | NVARCHAR(30) | | | Yes | | Phone |
| Email | NVARCHAR(150) | | | Yes | | Email |
| Address | NVARCHAR(500) | | | Yes | | Address |
| CityId | BIGINT | | City | Yes | | City |
| StateId | BIGINT | | State | Yes | | State |
| CountryId | BIGINT | | Country | Yes | | Country |
| PostalCode | NVARCHAR(20) | | | Yes | | ZIP/Postal |
| IsHeadOffice | BIT | | | No | 0 | Head Office Flag |
| IsActive | BIT | | | No | 1 | Status |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| DeletedBy | BIGINT | | User | Yes | | Audit |
| DeletedDate | DATETIME2 | | | Yes | | Soft Delete |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Branch (BranchId)`
- `FK_Branch_Company`
- `FK_Branch_ManagerUser`
- `FK_Branch_City`
- `FK_Branch_State`
- `FK_Branch_Country`
- `UQ_Branch (CompanyId, BranchCode)`
- `CHECK (BranchCode <> '')`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on BranchId |
| Unique Composite | (CompanyId, BranchCode) |
| Non-Clustered | (CompanyId, BranchName) |
| Non-Clustered | ManagerUserId |
| Non-Clustered | IsActive |

### Relationships

- `Company (1) -> (N) Branch`
- `Branch (1) -> (N) User`
- `Branch (1) -> (N) Warehouse`
- `Branch (1) -> (N) Customer` *(default servicing branch)*
- `Branch (1) -> (N) Vendor` *(optional default branch)*
- `Branch (1) -> (N) All Transactional Tables`

### Business Rules

- Each Company must have at least one Branch.
- Only one branch can be marked as Head Office per Company.
- BranchCode must be unique within a Company.
- A branch cannot be deleted if transactions exist.
- Inactive branches cannot be selected in new transactions.

### Accounting Impact

No direct journal entries. BranchId will be stored on accounting transactions for branch-wise financial reporting.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/branches | List |
| GET | /api/branches/{id} | Details |
| POST | /api/branches | Create |
| PUT | /api/branches/{id} | Update |
| DELETE | /api/branches/{id} | Deactivate |

### Angular Screens

- Branch List
- Branch Detail
- Create/Edit Branch
- Assign Branch Manager

### SQL Notes

BranchId should exist on every operational and accounting transaction to enable branch-wise reporting and security filtering.

---

## 03 FiscalYear

### Purpose

Defines accounting years and periods for each company. Every accounting transaction, invoice, bill and rental posting belongs to a fiscal year.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| FiscalYearId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyId | BIGINT | | Company | No | | Owner Company |
| FiscalYearCode | NVARCHAR(20) | | | No | | Unique Code |
| FiscalYearName | NVARCHAR(100) | | | No | | e.g. FY2026 |
| StartDate | DATE | | | No | | Start Date |
| EndDate | DATE | | | No | | End Date |
| Status | TINYINT | | | No | 1 | 0=Draft, 1=Open, 2=Closed, 3=Archived |
| IsDefault | BIT | | | No | 0 | Default Fiscal Year |
| AllowBackDatedEntries | BIT | | | No | 0 | Allow Historical Postings |
| ClosingDate | DATE | | | Yes | | Close Date |
| ClosedBy | BIGINT | | User | Yes | | Closed User |
| ClosedDate | DATETIME2 | | | Yes | | Closed Timestamp |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| DeletedBy | BIGINT | | User | Yes | | Audit |
| DeletedDate | DATETIME2 | | | Yes | | Soft Delete |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_FiscalYear`
- `FK_FiscalYear_Company`
- `FK_FiscalYear_ClosedBy`
- `UQ_FiscalYear (CompanyId, FiscalYearCode)`
- `CHECK (StartDate < EndDate)`
- Only one `IsDefault = 1` per Company

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on FiscalYearId |
| Unique Composite | (CompanyId, FiscalYearCode) |
| Composite | (CompanyId, Status) |
| Composite | (CompanyId, StartDate, EndDate) |

### Relationships

- `Company (1) -> (N) FiscalYear`
- `FiscalYear (1) -> (N) JournalEntry`
- `FiscalYear (1) -> (N) Invoice`
- `FiscalYear (1) -> (N) VendorBill`
- `FiscalYear (1) -> (N) RentalBilling`

### Business Rules

- Every company must have one active default fiscal year.
- No overlapping fiscal years for the same company.
- Closed fiscal years cannot accept new transactions unless `AllowBackDatedEntries = True`.
- Closing a fiscal year requires all accounting periods to be closed.
- Fiscal year cannot be deleted after accounting transactions exist.

### Accounting Impact

Critical accounting master. All journal entries, invoices, bills, payments and reports must reference a FiscalYear. No journal entry is created by this table itself.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/fiscal-years | List |
| GET | /api/fiscal-years/{id} | Details |
| POST | /api/fiscal-years | Create |
| PUT | /api/fiscal-years/{id} | Update |
| POST | /api/fiscal-years/{id}/close | Close Fiscal Year |

### Angular Screens

- Fiscal Year List
- Fiscal Year Detail
- Create/Edit Fiscal Year
- Fiscal Year Closing Wizard

### SQL Notes

FiscalYear should not contain accounting periods. Periods (monthly/quarterly) will be implemented in the Accounting module as a separate AccountingPeriod table.

---

## 04 Currency

### Purpose

Stores all currencies supported by the ERP. Used throughout Sales, Purchasing, Accounting, Banking, Rental and Reporting.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| CurrencyId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CurrencyCode | CHAR(3) | | | No | | ISO-4217 (USD, PKR) |
| CurrencyName | NVARCHAR(100) | | | No | | Currency Name |
| Symbol | NVARCHAR(10) | | | No | | ₹, $, Rs |
| CountryId | BIGINT | | Country | Yes | | Default Country |
| DecimalPlaces | TINYINT | | | No | 2 | Precision |
| RoundingMethod | TINYINT | | | No | 0 | 0=Normal, 1=Up, 2=Down |
| IsBaseCurrency | BIT | | | No | 0 | Base Currency |
| IsActive | BIT | | | No | 1 | Status |
| DisplayOrder | INT | | | No | 0 | Sorting |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| DeletedBy | BIGINT | | User | Yes | | Audit |
| DeletedDate | DATETIME2 | | | Yes | | Soft Delete |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Currency`
- `UQ_Currency_CurrencyCode`
- `FK_Currency_Country`
- Only one `IsBaseCurrency = 1` (system-wide or per company)
- `CHECK (DecimalPlaces BETWEEN 0 AND 6)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on CurrencyId |
| Unique | CurrencyCode |
| Non-Clustered | CurrencyName |
| Composite | (IsActive, DisplayOrder) |

### Relationships

- `Currency (1) -> (N) Company`
- `Currency (1) -> (N) ExchangeRate`
- `Currency (1) -> (N) Customer`
- `Currency (1) -> (N) Vendor`
- `Currency (1) -> (N) Invoice`
- `Currency (1) -> (N) VendorBill`
- `Currency (1) -> (N) BankAccount`

### Business Rules

- CurrencyCode must follow ISO-4217.
- Base currency cannot be deleted.
- Inactive currencies cannot be used in new transactions.
- Historical transactions retain original currency.

### Accounting Impact

No journal entries. Used for valuation, foreign currency transactions and exchange gains/losses.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/currencies | List |
| GET | /api/currencies/{id} | Details |
| POST | /api/currencies | Create |
| PUT | /api/currencies/{id} | Update |
| DELETE | /api/currencies/{id} | Deactivate |

### Angular Screens

- Currency List
- Currency Detail
- Create/Edit Currency

### SQL Notes

Exchange rates are intentionally separated into an ExchangeRate table to preserve history and support daily rates.

---

## 05 ExchangeRate

### Purpose

Stores historical exchange rates. Transactions store the applied rate at posting time, while this table provides the reference rates used for conversions.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| ExchangeRateId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyId | BIGINT | | Company | No | | Owner Company |
| FromCurrencyId | BIGINT | | Currency | No | | Source Currency |
| ToCurrencyId | BIGINT | | Currency | No | | Target/Base Currency |
| RateDate | DATE | | | No | | Effective Date |
| ExchangeRate | DECIMAL(18,8) | | | No | | Standard Rate |
| BuyingRate | DECIMAL(18,8) | | | Yes | | Optional Bank Buying Rate |
| SellingRate | DECIMAL(18,8) | | | Yes | | Optional Bank Selling Rate |
| Source | NVARCHAR(100) | | | Yes | | Manual/API/Bank |
| Remarks | NVARCHAR(500) | | | Yes | | Notes |
| IsActive | BIT | | | No | 1 | Status |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_ExchangeRate`
- `FK_ExchangeRate_Company`
- `FK_ExchangeRate_FromCurrency`
- `FK_ExchangeRate_ToCurrency`
- `UQ_ExchangeRate (CompanyId, FromCurrencyId, ToCurrencyId, RateDate)`
- `CHECK (ExchangeRate > 0)`
- `CHECK (FromCurrencyId <> ToCurrencyId)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on ExchangeRateId |
| Unique Composite | (CompanyId, FromCurrencyId, ToCurrencyId, RateDate) |
| Composite | (RateDate, FromCurrencyId, ToCurrencyId) |
| Non-Clustered | IsActive |

### Relationships

- `Company (1) -> (N) ExchangeRate`
- `Currency (1) -> (N) ExchangeRate` *(FromCurrency)*
- `Currency (1) -> (N) ExchangeRate` *(ToCurrency)*
- Invoice references ExchangeRate (stored value only)
- VendorBill references ExchangeRate (stored value only)
- JournalEntry stores applied exchange rate

### Business Rules

- Only one rate per currency pair per company per date.
- Historical rates must never be updated after financial posting; insert a new rate instead.
- Transactions copy the exchange rate at posting time to preserve auditability.
- Base currency to base currency rate is always `1.00000000`.

### Accounting Impact

Used by Accounts Receivable, Accounts Payable and General Ledger for foreign currency postings. No journal entries are created by this table directly.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/exchange-rates | List |
| GET | /api/exchange-rates/latest | Latest Rates |
| POST | /api/exchange-rates | Create |
| PUT | /api/exchange-rates/{id} | Update |
| DELETE | /api/exchange-rates/{id} | Deactivate |

### Angular Screens

- Exchange Rate List
- Exchange Rate Maintenance
- Import Rates
- Exchange Rate History

### SQL Notes

Do not calculate exchange rates dynamically during reporting. Persist the applied rate on every financial transaction for historical accuracy.

---

## 06 Country

### Purpose

Stores all countries supported by the ERP. Used by addresses, taxation, currency defaults, banking and localization.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| CountryId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CountryCode | CHAR(2) | | | No | | ISO-3166 Alpha-2 |
| CountryCode3 | CHAR(3) | | | No | | ISO-3166 Alpha-3 |
| CountryName | NVARCHAR(150) | | | No | | Official Name |
| Nationality | NVARCHAR(100) | | | Yes | | e.g. Pakistani, Canadian |
| PhoneCode | NVARCHAR(10) | | | Yes | | e.g. +92 |
| CurrencyId | BIGINT | | Currency | Yes | | Default Currency |
| TimeZone | NVARCHAR(100) | | | Yes | | Default Timezone |
| DateFormat | NVARCHAR(20) | | | No | dd/MM/yyyy | Display Format |
| IsActive | BIT | | | No | 1 | Status |
| DisplayOrder | INT | | | No | 0 | Sort Order |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Country`
- `UQ_Country_CountryCode`
- `UQ_Country_CountryCode3`
- `FK_Country_Currency`
- `CHECK (CountryCode <> CountryCode3)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on CountryId |
| Unique | CountryCode |
| Unique | CountryCode3 |
| Non-Clustered | CountryName |
| Composite | (IsActive, DisplayOrder) |

### Relationships

- `Country (1) -> (N) State`
- `Country (1) -> (N) Company`
- `Country (1) -> (N) Branch`
- `Country (1) -> (N) CustomerAddress`
- `Country (1) -> (N) VendorAddress`
- `Country (1) -> (N) Warehouse`
- `Country (1) -> (N) Bank` *(future)*

### Business Rules

- Use ISO country codes only.
- Country codes cannot be modified after creation.
- Inactive countries cannot be used in new records.
- Default currency should match the country's primary currency where possible.

### Accounting Impact

No direct journal entries. Country influences taxation, currency and regulatory reporting.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/countries | List |
| GET | /api/countries/{id} | Details |
| POST | /api/countries | Create |
| PUT | /api/countries/{id} | Update |
| DELETE | /api/countries/{id} | Deactivate |

### Angular Screens

- Country List
- Country Detail
- Create/Edit Country

### SQL Notes

Seed this table with ISO-3166 country data. Country records should rarely change.

---

## 07 State / Province

### Purpose

Stores states/provinces belonging to a country. Used for address normalization, taxation, reporting and regional business rules.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| StateId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CountryId | BIGINT | | Country | No | | Parent Country |
| StateCode | NVARCHAR(10) | | | No | | Unique within Country |
| StateName | NVARCHAR(150) | | | No | | State/Province Name |
| ISOCode | NVARCHAR(20) | | | Yes | | ISO-3166-2 |
| TaxRegionCode | NVARCHAR(20) | | | Yes | | Optional Tax Region |
| CapitalCity | NVARCHAR(100) | | | Yes | | Capital |
| IsActive | BIT | | | No | 1 | Status |
| DisplayOrder | INT | | | No | 0 | Sort Order |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_State`
- `FK_State_Country`
- `UQ_State (CountryId, StateCode)`
- `UQ_State (CountryId, StateName)`
- `CHECK (StateCode <> '')`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on StateId |
| Unique Composite | (CountryId, StateCode) |
| Unique Composite | (CountryId, StateName) |
| Composite | (CountryId, IsActive) |
| Non-Clustered | DisplayOrder |

### Relationships

- `Country (1) -> (N) State`
- `State (1) -> (N) City`
- `State (1) -> (N) Company`
- `State (1) -> (N) Branch`
- `State (1) -> (N) CustomerAddress`
- `State (1) -> (N) VendorAddress`
- `State (1) -> (N) Warehouse`

### Business Rules

- StateCode must be unique within a country.
- State cannot be deleted if referenced by any address.
- Inactive states cannot be selected for new records.
- Import ISO state/province codes during system initialization.

### Accounting Impact

No direct accounting entries. Used for tax determination, regional reporting and compliance.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/states | List |
| GET | /api/states/{id} | Details |
| POST | /api/states | Create |
| PUT | /api/states/{id} | Update |
| DELETE | /api/states/{id} | Deactivate |

### Angular Screens

- State List
- State Detail
- Create/Edit State

### SQL Notes

Seed with official state/province data where available. Keep codes aligned with ISO-3166-2.

---

## 09 User

### Purpose

Stores application users. A user belongs to a Company and optionally a default Branch. Permissions are granted through Roles.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| UserId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyId | BIGINT | | Company | No | | Owner Company |
| DefaultBranchId | BIGINT | | Branch | Yes | | Default Branch |
| EmployeeId | BIGINT | | Employee *(Future)* | Yes | | Employee Link |
| UserCode | NVARCHAR(20) | | | No | | Unique Code |
| Username | NVARCHAR(100) | | | No | | Login Name |
| PasswordHash | NVARCHAR(MAX) | | | No | | Password Hash |
| PasswordSalt | NVARCHAR(500) | | | Yes | | Salt |
| FirstName | NVARCHAR(100) | | | No | | First Name |
| LastName | NVARCHAR(100) | | | Yes | | Last Name |
| DisplayName | NVARCHAR(200) | | | No | | Display Name |
| Email | NVARCHAR(200) | | | No | | Unique Email |
| Mobile | NVARCHAR(30) | | | Yes | | Mobile |
| LastLoginDate | DATETIME2 | | | Yes | | Audit |
| FailedLoginAttempts | INT | | | No | 0 | Security |
| IsLocked | BIT | | | No | 0 | Locked |
| IsSystemAdmin | BIT | | | No | 0 | Global Admin |
| IsActive | BIT | | | No | 1 | Status |
| CreatedBy | BIGINT | | User | Yes | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_User`
- `FK_User_Company`
- `FK_User_DefaultBranch`
- `UQ_User (CompanyId, UserCode)`
- `UQ_User_Email`
- `UQ_User_Username`
- `CHECK (FailedLoginAttempts >= 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on UserId |
| Unique Composite | (CompanyId, UserCode) |
| Unique | Username |
| Unique | Email |
| Composite | (CompanyId, IsActive) |
| Non-Clustered | DefaultBranchId |

### Relationships

- `Company (1) -> (N) User`
- `Branch (1) -> (N) User`
- `User (N) -> (N) Role` *(via UserRole)*
- `User (1) -> (N) AuditLog`
- `User (1) -> (N) Attachment` *(CreatedBy)*
- `User (1) -> (N) All Business Transactions` *(CreatedBy/ModifiedBy)*

### Business Rules

- Username and Email must be unique.
- Passwords are never stored in plain text.
- Inactive or locked users cannot authenticate.
- Permissions are assigned through Roles only.
- Deleting users is prohibited; deactivate instead.

### Accounting Impact

No journal entries. UserId is stored on accounting transactions for auditability.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/users | List |
| GET | /api/users/{id} | Details |
| POST | /api/users | Create |
| PUT | /api/users/{id} | Update |
| POST | /api/users/{id}/reset-password | Reset Password |
| POST | /api/users/{id}/lock | Lock/Unlock |

### Angular Screens

- User List
- User Detail
- Create/Edit User
- Reset Password Dialog
- User Profile
- User Activity

### SQL Notes

Authentication should use JWT/OAuth with refresh tokens. Passwords must be hashed using Argon2id or bcrypt; never use reversible encryption.

---

## 10 Role

### Purpose

Defines security roles. Roles group permissions and are assigned to users through UserRole.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| RoleId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| CompanyId | BIGINT | | Company | No | | Owner Company |
| RoleCode | NVARCHAR(30) | | | No | | Unique Code |
| RoleName | NVARCHAR(150) | | | No | | Display Name |
| Description | NVARCHAR(500) | | | Yes | | Description |
| IsSystemRole | BIT | | | No | 0 | Protected Role |
| IsActive | BIT | | | No | 1 | Status |
| CreatedBy | BIGINT | | User | Yes | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Role`
- `FK_Role_Company`
- `UQ_Role (CompanyId, RoleCode)`
- `UQ_Role (CompanyId, RoleName)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on RoleId |
| Unique Composite | (CompanyId, RoleCode) |
| Unique Composite | (CompanyId, RoleName) |
| Composite | (CompanyId, IsActive) |

### Relationships

- `Company (1) -> (N) Role`
- `Role (N) -> (N) User` *(via UserRole)*
- `Role (N) -> (N) Permission` *(via RolePermission)*

### Business Rules

- Role codes are unique within a company.
- System roles cannot be deleted.
- Permissions are assigned only through RolePermission.
- Deactivate instead of deleting roles in use.

### Accounting Impact

None.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/roles | List |
| POST | /api/roles | Create |
| PUT | /api/roles/{id} | Update |
| DELETE | /api/roles/{id} | Deactivate |

### Angular Screens

- Role List
- Role Detail
- Role Permission Matrix

### SQL Notes

Roles should never directly reference modules. Permissions provide the fine-grained authorization layer.

---

## 11 Permission

### Purpose

Defines every granular permission available in the ERP. Permissions are assigned to Roles via RolePermission and optionally directly to users via UserPermission.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| PermissionId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| PermissionKey | NVARCHAR(150) | | | No | | Unique Key e.g. `SALES.INVOICE.CREATE` |
| Module | NVARCHAR(50) | | | No | | Sales, Purchase, Rental |
| Feature | NVARCHAR(100) | | | No | | Invoice, Customer |
| Action | NVARCHAR(30) | | | No | | View/Create/Edit/Delete/Approve |
| DisplayName | NVARCHAR(150) | | | No | | UI Name |
| Description | NVARCHAR(500) | | | Yes | | Description |
| IsSystemPermission | BIT | | | No | 1 | Protected |
| DisplayOrder | INT | | | No | 0 | Ordering |
| IsActive | BIT | | | No | 1 | Status |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_Permission`
- `UQ_Permission_PermissionKey`
- `CHECK (Action IN (View, Create, Edit, Delete, Approve, Post, Print, Export, Import))`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on PermissionId |
| Unique | PermissionKey |
| Composite | (Module, Feature) |
| Composite | (Module, Action) |

### Relationships

- `Permission (N) -> (N) Role` *(via RolePermission)*
- `Permission (N) -> (N) User` *(via UserPermission)*
- Referenced by authorization middleware

### Business Rules

- PermissionKey is immutable after release.
- Permissions are global and shared across companies.
- Never physically delete system permissions.

### Accounting Impact

None.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/permissions | List |
| GET | /api/permissions/{id} | Details |
| POST | /api/permissions | Create |
| PUT | /api/permissions/{id} | Update |

### Angular Screens

- Permission List
- Permission Detail
- Permission Matrix

### SQL Notes

Use dot notation for PermissionKey: `MODULE.FEATURE.ACTION`.

---

## 12 RolePermission

### Purpose

Junction table that assigns permissions to roles. A role can have many permissions and a permission can belong to many roles.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| RolePermissionId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| RoleId | BIGINT | | Role | No | | Assigned Role |
| PermissionId | BIGINT | | Permission | No | | Assigned Permission |
| IsAllowed | BIT | | | No | 1 | Allow/Deny |
| EffectiveFrom | DATETIME2 | | | Yes | | Start |
| EffectiveTo | DATETIME2 | | | Yes | | End |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_RolePermission`
- `FK_RolePermission_Role`
- `FK_RolePermission_Permission`
- `FK_RolePermission_CreatedBy`
- `UQ_RolePermission (RoleId, PermissionId)`
- `CHECK (EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on RolePermissionId |
| Unique Composite | (RoleId, PermissionId) |
| Composite | (RoleId, IsAllowed) |
| Composite | (PermissionId, IsAllowed) |

### Relationships

- `Role (1) -> (N) RolePermission`
- `Permission (1) -> (N) RolePermission`
- `Role (N) -> (N) Permission` *(via RolePermission)*

### Business Rules

- A permission can be assigned only once to a role.
- Denied permissions override allowed permissions when evaluated.
- Expired permissions are ignored by the authorization engine.
- System role permissions should only be modified by administrators.

### Accounting Impact

None.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/roles/{roleId}/permissions | List Role Permissions |
| PUT | /api/roles/{roleId}/permissions | Replace Permission Set |
| POST | /api/role-permissions | Assign Permission |
| DELETE | /api/role-permissions/{id} | Remove Assignment |

### Angular Screens

- Role Permission Matrix
- Role Permission Assignment Dialog
- Role Permission Audit

### SQL Notes

This is a pure junction table. Authorization should cache effective permissions for performance.

---

## 13 UserRole

### Purpose

Maps users to one or more roles. Supports multiple roles per user with effective date ranges.

### Database Schema

| Column | Data Type | PK | FK | Null | Default | Description |
|---|---|---|---|---|---|---|
| UserRoleId | BIGINT IDENTITY(1,1) | Yes | | No | | Primary Key |
| UserId | BIGINT | | User | No | | Assigned User |
| RoleId | BIGINT | | Role | No | | Assigned Role |
| DefaultRole | BIT | | | No | 0 | Default Role After Login |
| EffectiveFrom | DATETIME2 | | | Yes | | Start Date |
| EffectiveTo | DATETIME2 | | | Yes | | End Date |
| IsActive | BIT | | | No | 1 | Status |
| CreatedBy | BIGINT | | User | No | | Audit |
| CreatedDate | DATETIME2 | | | No | GETDATE() | Audit |
| ModifiedBy | BIGINT | | User | Yes | | Audit |
| ModifiedDate | DATETIME2 | | | Yes | | Audit |
| RowVersion | ROWVERSION | | | No | | Concurrency |

### Constraints

- `PK_UserRole`
- `FK_UserRole_User`
- `FK_UserRole_Role`
- `UQ_UserRole (UserId, RoleId)`
- `CHECK (EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom)`
- Only one `DefaultRole = 1` per User

### Indexes

| Type | Index |
|---|---|
| Clustered | PK on UserRoleId |
| Unique Composite | (UserId, RoleId) |
| Composite | (UserId, IsActive) |
| Composite | (RoleId, IsActive) |

### Relationships

- `User (1) -> (N) UserRole`
- `Role (1) -> (N) UserRole`
- `User (N) -> (N) Role` *(via UserRole)*

### Business Rules

- Users may have multiple active roles.
- Only one default role is allowed per user.
- Expired assignments are ignored by authorization.
- Roles inherited through UserRole are combined with UserPermission overrides.

### Accounting Impact

None.

### REST APIs

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/users/{userId}/roles | List User Roles |
| PUT | /api/users/{userId}/roles | Replace Role Assignments |
| POST | /api/user-roles | Assign Role |
| DELETE | /api/user-roles/{id} | Remove Role |

### Angular Screens

- User Role Assignment
- User Security Profile
- Role Membership View

### SQL Notes

A user's effective permissions are the union of all active roles plus UserPermission overrides.

---

## 14 Address

**Classification:** Shared Kernel — Core Entity

### Purpose

Stores reusable postal and physical addresses shared across Customer, Vendor, Company, Branch, Warehouse, Employee, Asset and other domains.

### Referenced By

- CustomerAddress
- VendorAddress
- CompanyAddress
- BranchAddress
- WarehouseAddress
- EmployeeAddress
- AssetAddress

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| AddressId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| AddressType | NVARCHAR(30) | No | | | | Billing/Shipping/Office/Site |
| AddressLine1 | NVARCHAR(250) | No | | | | Address Line 1 |
| AddressLine2 | NVARCHAR(250) | Yes | | | | Address Line 2 |
| CountryId | BIGINT | No | | | Country | Country |
| StateId | BIGINT | Yes | | | State | State |
| CityId | BIGINT | Yes | | | City | City |
| PostalCode | NVARCHAR(20) | Yes | | | | Postal Code |
| Latitude | DECIMAL(10,8) | Yes | | | | Latitude |
| Longitude | DECIMAL(11,8) | Yes | | | | Longitude |
| GooglePlaceId | NVARCHAR(100) | Yes | | | | Google Place Id |
| Remarks | NVARCHAR(500) | Yes | | | | Remarks |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Address`
- `FK_Address_Country`
- `FK_Address_State`
- `FK_Address_City`
- `FK_Address_CreatedBy`
- `FK_Address_ModifiedBy`
- `FK_Address_DeletedBy`
- `CHECK (Latitude BETWEEN -90 AND 90)`
- `CHECK (Longitude BETWEEN -180 AND 180)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Address |
| Non-Clustered | IX_Address_Country |
| Non-Clustered | IX_Address_State |
| Non-Clustered | IX_Address_City |
| Non-Clustered | IX_Address_PostalCode |

### Relationships

- `Country (1) -> (N) Address`
- `State (1) -> (N) Address`
- `City (1) -> (N) Address`
- `Address (1) -> (N) CustomerAddress`
- `Address (1) -> (N) VendorAddress`
- `Address (1) -> (N) BranchAddress`
- `Address (1) -> (N) WarehouseAddress`

### Business Rules

- Address is a reusable Shared Kernel entity.
- Address records must not contain CustomerId or VendorId.
- Address history is maintained by link tables.
- Soft delete only.

### Events Published

- `AddressCreated`
- `AddressUpdated`
- `AddressDeleted`

### Developer Notes

This is a shared entity. Business ownership resides in the consuming domain through bridge tables such as CustomerAddress and VendorAddress.

---

## 15 Contact

**Classification:** Shared Kernel — Core Entity

### Purpose

Stores reusable contact information shared across Customer, Vendor, Company, Branch, Employee and other domains.

### Referenced By

- CustomerContact
- VendorContact
- CompanyContact
- BranchContact
- EmployeeContact
- AssetContact

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ContactId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| Title | NVARCHAR(20) | Yes | | | | Mr./Mrs./Ms./Dr. |
| FirstName | NVARCHAR(100) | No | | | | First Name |
| MiddleName | NVARCHAR(100) | Yes | | | | Middle Name |
| LastName | NVARCHAR(100) | Yes | | | | Last Name |
| DisplayName | NVARCHAR(250) | No | | | | Display Name |
| Designation | NVARCHAR(150) | Yes | | | | Job Title |
| Department | NVARCHAR(150) | Yes | | | | Department |
| Email | NVARCHAR(255) | Yes | | | | Email Address |
| MobileNo | NVARCHAR(30) | Yes | | | | Mobile Number |
| PhoneNo | NVARCHAR(30) | Yes | | | | Phone Number |
| Extension | NVARCHAR(10) | Yes | | | | Extension |
| WhatsAppNo | NVARCHAR(30) | Yes | | | | WhatsApp |
| PreferredContactMethod | NVARCHAR(20) | No | Email | | | Email/Phone/SMS/WhatsApp |
| Remarks | NVARCHAR(500) | Yes | | | | Remarks |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Contact`
- `FK_Contact_CreatedBy`
- `FK_Contact_ModifiedBy`
- `FK_Contact_DeletedBy`
- `CHECK (Email IS NULL OR LEN(Email) > 5)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Contact |
| Non-Clustered | IX_Contact_DisplayName |
| Non-Clustered | IX_Contact_Email |
| Non-Clustered | IX_Contact_MobileNo |
| Non-Clustered | IX_Contact_Status |

### Relationships

- `Contact (1) -> (N) CustomerContact`
- `Contact (1) -> (N) VendorContact`
- `Contact (1) -> (N) CompanyContact`
- `Contact (1) -> (N) BranchContact`
- `Contact (1) -> (N) EmployeeContact`

### Business Rules

- Contact is a reusable Shared Kernel entity.
- Do not store CustomerId or VendorId in this table.
- Email and Mobile should be unique within the owning business context where required.
- Soft delete only.

### Events Published

- `ContactCreated`
- `ContactUpdated`
- `ContactDeleted`

### Developer Notes

Ownership is maintained through bridge tables such as CustomerContact and VendorContact. This table contains only reusable contact information.

---

## 16 Attachment

**Classification:** Shared Kernel — Core Entity

### Purpose

Stores reusable file metadata for documents, images, videos and other files. Physical files are stored in external storage or the configured file system.

### Referenced By

- CustomerAttachment
- VendorAttachment
- AssetAttachment
- CompanyAttachment
- BranchAttachment
- ServiceAttachment
- RentalAttachment

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| AttachmentId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| FileName | NVARCHAR(255) | No | | | | Original File Name |
| StoredFileName | NVARCHAR(255) | No | | | | Internal File Name |
| FileExtension | NVARCHAR(20) | No | | | | Extension |
| ContentType | NVARCHAR(100) | No | | | | MIME Type |
| FileSize | BIGINT | No | 0 | | | Bytes |
| StorageProvider | NVARCHAR(50) | No | Local | | | Local/Azure/S3/B2 |
| StoragePath | NVARCHAR(1000) | No | | | | Physical/Cloud Path |
| FileHash | NVARCHAR(128) | Yes | | | | SHA256 Hash |
| VersionNo | INT | No | 1 | | | Version |
| IsEncrypted | BIT | No | 0 | | | Encrypted |
| IsPublic | BIT | No | 0 | | | Public Access |
| Remarks | NVARCHAR(500) | Yes | | | | Remarks |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Attachment`
- `FK_Attachment_CreatedBy`
- `FK_Attachment_ModifiedBy`
- `FK_Attachment_DeletedBy`
- `CHECK (FileSize >= 0)`
- `CHECK (VersionNo > 0)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Attachment |
| Non-Clustered | IX_FileHash |
| Non-Clustered | IX_FileName |
| Non-Clustered | IX_ContentType |
| Non-Clustered | IX_Status |

### Relationships

- `Attachment (1) -> (N) CustomerAttachment`
- `Attachment (1) -> (N) VendorAttachment`
- `Attachment (1) -> (N) AssetAttachment`
- `Attachment (1) -> (N) ServiceAttachment`
- `Attachment (1) -> (N) RentalAttachment`

### Business Rules

- Binary data is not stored in this table.
- StoragePath identifies the physical or cloud location.
- FileHash should be used for duplicate detection.
- Attachments are reusable through bridge tables.
- Soft delete only.

### Events Published

- `AttachmentCreated`
- `AttachmentUpdated`
- `AttachmentDeleted`

### Developer Notes

Supports local storage, Azure Blob Storage, Amazon S3, Backblaze B2 and compatible object storage. Business ownership is maintained by bridge tables.

---

## 17 Note

**Classification:** Shared Kernel — Core Entity

### Purpose

Stores reusable textual notes that can be attached to any business entity through bridge tables.

### Referenced By

- CustomerNote
- VendorNote
- AssetNote
- ServiceNote
- RentalNote
- EmployeeNote

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| NoteId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| Title | NVARCHAR(200) | Yes | | | | Short Title |
| NoteText | NVARCHAR(MAX) | No | | | | Note Content |
| NoteType | NVARCHAR(30) | No | General | | | General/Internal/Reminder |
| Priority | SMALLINT | No | 1 | | | Priority |
| IsPinned | BIT | No | 0 | | | Pinned |
| IsConfidential | BIT | No | 0 | | | Restricted |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Note`
- `FK_Note_CreatedBy`
- `FK_Note_ModifiedBy`
- `FK_Note_DeletedBy`
- `CHECK (Priority BETWEEN 1 AND 5)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Note |
| Non-Clustered | IX_Note_Type |
| Non-Clustered | IX_Note_Status |
| Non-Clustered | IX_Note_CreatedDate |

### Relationships

- `Note (1) -> (N) CustomerNote`
- `Note (1) -> (N) VendorNote`
- `Note (1) -> (N) AssetNote`
- `Note (1) -> (N) ServiceNote`
- `Note (1) -> (N) RentalNote`

### Business Rules

- Note content is reusable through bridge tables.
- Confidential notes require authorization.
- Notes are never physically deleted.
- Soft delete only.

### Events Published

- `NoteCreated`
- `NoteUpdated`
- `NoteDeleted`

### Developer Notes

Business ownership is maintained by bridge tables (CustomerNote, VendorNote, etc.). The Note table stores only reusable note content.

---

## 18 Activity

**Classification:** Shared Kernel — Infrastructure Entity

### Purpose

Stores reusable activities such as calls, meetings, visits, emails, tasks and follow-ups that can be linked to any business entity.

### Referenced By

- CustomerActivity
- VendorActivity
- AssetActivity
- ServiceActivity
- RentalActivity
- EmployeeActivity

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| ActivityId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| ActivityType | NVARCHAR(30) | No | | | | Call/Meeting/Task/Visit/Email |
| Subject | NVARCHAR(250) | No | | | | Subject |
| Description | NVARCHAR(MAX) | Yes | | | | Details |
| StartDateTime | DATETIME2(7) | No | | | | Start |
| EndDateTime | DATETIME2(7) | Yes | | | | End |
| Priority | SMALLINT | No | 2 | | | 1-High 2-Medium 3-Low |
| AssignedTo | BIGINT | Yes | | | User | Assigned User |
| CompletedBy | BIGINT | Yes | | | User | Completed By |
| CompletedDate | DATETIME2(7) | Yes | | | | Completion Time |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Activity`
- `FK_Activity_AssignedTo`
- `FK_Activity_CompletedBy`
- `FK_Activity_CreatedBy`
- `FK_Activity_ModifiedBy`
- `FK_Activity_DeletedBy`
- `CHECK (EndDateTime IS NULL OR EndDateTime >= StartDateTime)`
- `CHECK (Priority BETWEEN 1 AND 3)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Activity |
| Non-Clustered | IX_Activity_Type |
| Non-Clustered | IX_Activity_AssignedTo |
| Non-Clustered | IX_Activity_StartDateTime |
| Non-Clustered | IX_Activity_Status |

### Relationships

- `Activity (1) -> (N) CustomerActivity`
- `Activity (1) -> (N) VendorActivity`
- `Activity (1) -> (N) AssetActivity`
- `Activity (1) -> (N) ServiceActivity`
- `Activity (1) -> (N) RentalActivity`

### Business Rules

- Activities are reusable through bridge tables.
- Completed activities should not be edited except by authorized users.
- Activities are never physically deleted.
- Soft delete only.

### Events Published

- `ActivityCreated`
- `ActivityUpdated`
- `ActivityCompleted`
- `ActivityDeleted`

### Developer Notes

Activity stores reusable operational work items. Ownership is maintained by bridge tables such as CustomerActivity and ServiceActivity.

---

## 19 Timeline

**Classification:** Shared Kernel — Infrastructure Entity

### Purpose

Stores a unified chronological history of significant business events. Timeline entries are generated by domain events and linked to business modules through bridge tables or reference fields.

### Referenced By

- CustomerTimeline
- VendorTimeline
- AssetTimeline
- ServiceTimeline
- RentalTimeline
- SalesTimeline
- PurchaseTimeline

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| TimelineId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| EventType | NVARCHAR(50) | No | | | | Created/Updated/Approved/etc. |
| Title | NVARCHAR(200) | No | | | | Timeline Title |
| Description | NVARCHAR(MAX) | Yes | | | | Event Details |
| ReferenceModule | NVARCHAR(50) | No | | | | Origin Module |
| ReferenceId | BIGINT | No | | | | Origin Record Id |
| ReferenceNo | NVARCHAR(50) | Yes | | | | Document Number |
| EventDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Event Timestamp |
| PerformedBy | BIGINT | No | | | User | Performed By |
| Severity | SMALLINT | No | 2 | | | 1-High 2-Medium 3-Low |
| IsSystemGenerated | BIT | No | 1 | | | Generated Automatically |
| StatusId | SMALLINT | No | 1 | | Status | Status |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_Timeline`
- `FK_Timeline_PerformedBy`
- `FK_Timeline_CreatedBy`
- `FK_Timeline_ModifiedBy`
- `FK_Timeline_DeletedBy`
- `CHECK (Severity BETWEEN 1 AND 3)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_Timeline |
| Non-Clustered | IX_Module_Reference (ReferenceModule, ReferenceId) |
| Non-Clustered | IX_EventDate (EventDate DESC) |
| Non-Clustered | IX_EventType |
| Non-Clustered | IX_Status |

### Relationships

- `Timeline (1) -> (N) CustomerTimeline`
- `Timeline (1) -> (N) VendorTimeline`
- `Timeline (1) -> (N) AssetTimeline`
- `Timeline (1) -> (N) ServiceTimeline`
- `Timeline (1) -> (N) RentalTimeline`

### Business Rules

- Timeline records are append-only.
- Business events automatically create timeline entries.
- Timeline records must never be physically deleted.
- Only administrators may amend entries when permitted.

### Events Published

- `TimelineRecorded`
- `TimelineCorrected`

### Developer Notes

Timeline is a reusable infrastructure entity that provides a unified audit/history feed across all ERP domains.

---

## 20 NumberSeries

**Classification:** Shared Kernel — Infrastructure Entity

### Purpose

Maintains configurable document numbering sequences for all ERP modules with support for prefixes, suffixes, fiscal years, branches and companies.

### Referenced By

- Customer
- Vendor
- Sales
- Purchase
- Rental
- Service
- Inventory
- Accounting
- Asset

### Database Schema

| Column | Data Type | Nullable | Default | PK | FK | Description |
|---|---|---|---|---|---|---|
| NumberSeriesId | BIGINT IDENTITY(1,1) | No | | Yes | | Primary Key |
| CompanyId | BIGINT | No | | | Company | Owner Company |
| BranchId | BIGINT | Yes | | | Branch | Optional Branch |
| ModuleName | NVARCHAR(100) | No | | | | Sales/Customer/etc. |
| DocumentType | NVARCHAR(100) | No | | | | Invoice/Order/etc. |
| SeriesName | NVARCHAR(100) | No | | | | Series Name |
| Prefix | NVARCHAR(20) | Yes | | | | Prefix |
| Suffix | NVARCHAR(20) | Yes | | | | Suffix |
| Separator | NVARCHAR(5) | No | - | | | Separator |
| NextNumber | BIGINT | No | 1 | | | Next Running Number |
| NumberLength | SMALLINT | No | 6 | | | Zero Padding |
| ResetPolicy | NVARCHAR(20) | No | Never | | | Never/Yearly/Monthly |
| FiscalYearId | BIGINT | Yes | | | FiscalYear | Fiscal Year |
| IsDefault | BIT | No | 1 | | | Default Series |
| IsActive | BIT | No | 1 | | | Active |
| CreatedBy | BIGINT | No | | | User | Audit |
| CreatedDate | DATETIME2(7) | No | SYSUTCDATETIME() | | | Audit |
| ModifiedBy | BIGINT | Yes | | | User | Audit |
| ModifiedDate | DATETIME2(7) | Yes | | | | Audit |
| DeletedBy | BIGINT | Yes | | | User | Audit |
| DeletedDate | DATETIME2(7) | Yes | | | | Audit |
| IsDeleted | BIT | No | 0 | | | Soft Delete |
| RowVersion | ROWVERSION | No | | | | Concurrency |

### Constraints

- `PK_NumberSeries`
- `FK_NumberSeries_Company`
- `FK_NumberSeries_Branch`
- `FK_NumberSeries_FiscalYear`
- `UQ (CompanyId, BranchId, ModuleName, DocumentType, SeriesName)`
- `CHECK (NextNumber > 0)`
- `CHECK (NumberLength BETWEEN 1 AND 20)`

### Indexes

| Type | Index |
|---|---|
| Clustered | PK_NumberSeries |
| Non-Clustered | IX_Module_Document (ModuleName, DocumentType) |
| Non-Clustered | IX_Company (CompanyId) |
| Non-Clustered | IX_Branch (BranchId) |
| Non-Clustered | IX_Active (IsActive) |

### Relationships

- `Company (1) -> (N) NumberSeries`
- `Branch (1) -> (N) NumberSeries`
- `FiscalYear (1) -> (N) NumberSeries`

### Business Rules

- One default series per Company/Branch/DocumentType.
- Numbers are generated atomically to avoid duplicates.
- Consumed numbers are never reused.
- Reset policy controls automatic sequence reset.

### Events Published

- `NumberSeriesCreated`
- `NumberSeriesUpdated`
- `NumberAllocated`
- `NumberSeriesReset`

### Developer Notes

All document numbers in the ERP must be generated through this table to ensure uniqueness, consistency and concurrency safety.

---


*RentalERP v1.0 — Foundation Module Documentation | Generated June 2026*
