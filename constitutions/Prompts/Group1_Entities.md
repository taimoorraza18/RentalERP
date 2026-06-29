Please read these files before doing anything:
- constitutions/Backed Constitution.md
- constitutions/Domains/Shared Kernel.md
- constitutions/Domains/Foundation Domain.md
- constitutions/Domains/Security Domain.md
- constitutions/Domains/Customer Domain.md
- constitutions/Domains/Vendor Domain.md
- constitutions/Domains/Product Domain.md
- constitutions/Domains/Warehouse Domain.md

Read every file completely before writing any code.
These files are the source of truth for all entities, fields,
data types, relationships, indexes and constraints.
Do not invent any field or table not defined in these documents.

---

TASK: Generate ONLY EF Core entity classes for these 6 domains.

PLACEMENT RULES:
- Foundation Domain entities → src/ERP.Foundation/Domain/Entities/
- Security Domain entities → src/ERP.Security/Domain/Entities/
- Customer Domain entities → src/ERP.Customer/Domain/Entities/
- Vendor Domain entities → src/ERP.Vendor/Domain/Entities/
- Product Domain entities → src/ERP.Product/Domain/Entities/
- Warehouse Domain entities → src/ERP.Warehouse/Domain/Entities/

BASE CLASS RULES:
Every entity must inherit from the correct base class 
from ERP.SharedKernel:
- AuditableEntity — entities needing soft delete 
  (IsDeleted, DeletedAt, DeletedBy)
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
4. Flag any field or relationship that was ambiguous
   in the schema files so I can clarify before Group 2