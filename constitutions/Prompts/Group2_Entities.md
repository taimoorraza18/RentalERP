Please read these files before doing anything:
- constitutions/Backed Constitution.md
- constitutions/Domains/Inventory Domain.md
- constitutions/Domains/Asset Domain.md
- constitutions/Domains/Rental Domain.md
- constitutions/Domains/Service Domain.md
- constitutions/Domains/Purchase Domain.md
- constitutions/Domains/Sales Domain.md
- constitutions/Domains/Accounting Domain.md

Read every file completely before writing any code.
These files are the source of truth for all entities, fields,
data types, relationships, indexes and constraints.
Do not invent any field or table not defined in these documents.

---

TASK: Generate ONLY EF Core entity classes for these 7 domains.

PLACEMENT RULES:
- Inventory Domain entities → src/ERP.Inventory/Domain/Entities/
- Asset Domain entities → src/ERP.Asset/Domain/Entities/
- Rental Domain entities → src/ERP.Rental/Domain/Entities/
- Service Domain entities → src/ERP.Service/Domain/Entities/
- Purchase Domain entities → src/ERP.Purchase/Domain/Entities/
- Sales Domain entities → src/ERP.Sales/Domain/Entities/
- Accounting Domain entities → src/ERP.Accounting/Domain/Entities/

CROSS-DOMAIN REFERENCE RULES:
These domains reference entities already generated in Group 1.
When referencing cross-domain entities:
- Add the FK property (long) in the entity
- Add the navigation property with correct type
- Add the project reference to .csproj if not already present
- Never duplicate an entity that already exists in another domain

Known cross-domain references to expect:
- Customer → ERP.Customer
- Vendor → ERP.Vendor  
- Product → ERP.Product
- Warehouse → ERP.Warehouse
- Foundation entities (Address, Contact, etc.) → ERP.Foundation

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

NAMING RULES — non negotiable:
- C# classes and properties: PascalCase always
- No snake_case anywhere in C# code
- No HasColumnName() calls — global convention handles it
- All primary keys are long, GENERATED ALWAYS AS IDENTITY
- All foreign key properties are long

After generating all entities:
1. Run dotnet build
2. Report build result — zero errors required
3. List every entity class created grouped by domain
4. Flag any cross-domain reference that was ambiguous
   or any field unclear in the schema files