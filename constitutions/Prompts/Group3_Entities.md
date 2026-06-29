Read these domain schema files before doing anything:
- constitutions/Domains/Report Domain.md
- constitutions/Domains/Workflow Domain.md
- constitutions/Domains/Notification Domain.md
- constitutions/Domains/Dashboard Domain.md
- constitutions/Domains/Audit Domain.md
- constitutions/Domains/Integration Domain.md
- constitutions/Domains/Scheduler Domain.md
- constitutions/Domains/Configuration Domain.md
- constitutions/Domains/Platform Domain.md

Read every file completely before writing any code.
These files are the source of truth for all entities, fields,
data types, relationships, indexes and constraints.
Do not invent any field or table not defined in these documents.

---

TASK: Generate ONLY EF Core entity classes for these 9 domains.

PLACEMENT RULES:
- Report Domain entities → src/ERP.Reporting/Domain/Entities/
- Workflow Domain entities → src/ERP.Workflow/Domain/Entities/
- Notification Domain entities → src/ERP.Notification/Domain/Entities/
- Dashboard Domain entities → src/ERP.Dashboard/Domain/Entities/
- Audit Domain entities → src/ERP.Audit/Domain/Entities/
- Integration Domain entities → src/ERP.Integration/Domain/Entities/
- Scheduler Domain entities → src/ERP.Scheduler/Domain/Entities/
- Configuration Domain entities → src/ERP.SystemConfiguration/Domain/Entities/
- Platform Domain entities → src/ERP.Platform/Domain/Entities/

CROSS-DOMAIN REFERENCE RULES:
These domains may reference entities from Group 1 and Group 2.
When referencing cross-domain entities:
- Add the FK property (long) in the entity
- Add the navigation property with correct type
- Add project reference to .csproj if not already present
- Never duplicate an entity that already exists in another domain

Known cross-domain references to expect:
- Customer → ERP.Customer
- Vendor → ERP.Vendor
- Product → ERP.Product
- Warehouse → ERP.Warehouse
- Foundation entities → ERP.Foundation
- Inventory entities → ERP.Inventory
- Sales entities → ERP.Sales
- Purchase entities → ERP.Purchase
- Rental entities → ERP.Rental
- Accounting entities → ERP.Accounting

BASE CLASS RULES:
Every entity must inherit from the correct base class
from ERP.SharedKernel:
- AuditableEntity — entities needing soft delete
- AggregateRoot — aggregate roots without soft delete
- Entity — simple child/junction entities

PROPERTY TYPE MAPPING:
- BIGINT → long
- VARCHAR/TEXT → string
- BOOLEAN → bool
- DECIMAL/NUMERIC → decimal
- TIMESTAMP/TIMESTAMPTZ → DateTime
- INTEGER → int
- DATE → DateOnly
- All foreign keys → long (must match primary key type)
- All primary keys → long (inherited from base Entity)

NAVIGATION PROPERTIES:
- Add navigation properties for all relationships
- Use ICollection<T> for one-to-many collections
- Initialize collections in constructor: = new List<T>()
- Single navigation for many-to-one references

DO NOT generate in this step:
- EF Core IEntityTypeConfiguration files
- DbContext
- Migrations
- Repositories
- Application layer code
- Controllers
- DTOs
- Any other file type

Only entity classes. Nothing else.

After generating all entities:
1. Run dotnet build
2. Report build result — zero errors required
3. List every entity class created grouped by domain
4. Flag any cross-domain reference that was ambiguous
   or any field unclear in the schema files