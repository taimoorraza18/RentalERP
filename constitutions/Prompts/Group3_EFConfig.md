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
These files are the source of truth for all table names,
column types, indexes, constraints and relationships.
Do not invent any configuration not defined in these documents.

---

TASK: Generate EF Core IEntityTypeConfiguration<T> classes
for all Group 3 domain entities.

PLACEMENT RULES:
- Report configs → src/ERP.Reporting/Infrastructure/Configurations/
- Workflow configs → src/ERP.Workflow/Infrastructure/Configurations/
- Notification configs → src/ERP.Notification/Infrastructure/Configurations/
- Dashboard configs → src/ERP.Dashboard/Infrastructure/Configurations/
- Audit configs → src/ERP.Audit/Infrastructure/Configurations/
- Integration configs → src/ERP.Integration/Infrastructure/Configurations/
- Scheduler configs → src/ERP.Scheduler/Infrastructure/Configurations/
- Configuration configs → src/ERP.SystemConfiguration/Infrastructure/Configurations/
- Platform configs → src/ERP.Platform/Infrastructure/Configurations/

Each configuration class must:
- Implement IEntityTypeConfiguration<TEntity>
- Be named exactly: {EntityName}Configuration
- Configure everything in the Configure() method

WHAT TO CONFIGURE FOR EVERY ENTITY:

1. TABLE NAME
   - Use ToTable("{table_name}") only if the snake_case
     convention does not produce the correct name
   - In most cases the global SnakeCaseNamingConvention
     handles this — do not set manually unless necessary

2. PRIMARY KEY
   - HasKey(x => x.Id)
   - UseIdentityAlwaysColumn() for PostgreSQL
     (GENERATED ALWAYS AS IDENTITY)

3. PROPERTIES
   - IsRequired() for non-nullable fields
   - HasMaxLength() for all string fields
   - HasColumnType("decimal(18,2)") for decimal fields
   - HasDefaultValue() where schema specifies a default
   - HasConversion() for enums stored as strings
   - ValueGeneratedOnAdd() for identity columns

4. RELATIONSHIPS
   - HasOne/HasMany with WithMany/WithOne
   - HasForeignKey() for every relationship
   - OnDelete() cascade behavior:
     * Restrict — default for most relationships
     * Cascade — only when child cannot exist without parent
     * SetNull — for optional relationships
   - IsRequired() on relationships where FK is not nullable

5. INDEXES
   - HasIndex() for every foreign key column
   - HasIndex().IsUnique() for unique constraints
   - HasIndex() for frequently queried columns
     as defined in schema
   - Name indexes as: ix_{table}_{column}

6. CONSTRAINTS
   - HasCheckConstraint() where schema defines check constraints
   - Name constraints as: chk_{table}_{rule}

CROSS-DOMAIN RELATIONSHIP RULES:
These domains reference entities from Group 1 and Group 2.
When configuring cross-domain relationships:
- Configure the FK property and index on the owning side
- Do NOT configure the relationship from the other side
- Use HasIndex() for every cross-domain FK column

Known cross-domain FKs to expect:
- CustomerId → ERP.Customer
- VendorId → ERP.Vendor
- ProductId → ERP.Product
- WarehouseId → ERP.Warehouse
- Foundation entity FKs → ERP.Foundation
- Inventory, Sales, Purchase, Rental, 
  Accounting entity FKs → respective projects

NAMING RULES — non negotiable:
- Configuration class names: PascalCase
- Never set HasColumnName() — global convention handles it
- Index names: ix_{table}_{column} in snake_case
- FK constraint names: fk_{table}_{referenced_table}
- Check constraint names: chk_{table}_{rule}
- Primary key names: pk_{table}

SOFT DELETE:
- Entities inheriting AuditableEntity have IsDeleted
- Add HasQueryFilter(x => !x.IsDeleted) for all
  AuditableEntity configurations
- This ensures soft deleted records are excluded
  from all queries automatically

NAMESPACE CLASH PREVENTION:
Use explicit type aliases in every configuration file
to avoid namespace clashes with project names.
Never use wildcard namespace imports.

DO NOT generate in this step:
- DbContext
- Migrations
- Repositories
- Application layer code
- Controllers
- DTOs

Only IEntityTypeConfiguration classes. Nothing else.

After generating all configurations:
1. Run dotnet build
2. Report build result — zero errors required
3. List every configuration class created grouped by domain
4. Flag any ambiguous cascade behavior or constraint
   that needs clarification before DbContext generation