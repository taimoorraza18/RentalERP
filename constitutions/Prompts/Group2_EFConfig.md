Read these domain schema files before doing anything:
- constitutions/Domains/Inventory Domain.md
- constitutions/Domains/Asset Domain.md
- constitutions/Domains/Rental Domain.md
- constitutions/Domains/Service Domain.md
- constitutions/Domains/Purchase Domain.md
- constitutions/Domains/Sales Domain.md
- constitutions/Domains/Accounting Domain.md

Read every file completely before writing any code.
These files are the source of truth for all table names,
column types, indexes, constraints and relationships.
Do not invent any configuration not defined in these documents.

---

TASK: Generate EF Core IEntityTypeConfiguration<T> classes
for all Group 2 domain entities.

PLACEMENT RULES:
- Inventory configs → src/ERP.Inventory/Infrastructure/Configurations/
- Asset configs → src/ERP.Asset/Infrastructure/Configurations/
- Rental configs → src/ERP.Rental/Infrastructure/Configurations/
- Service configs → src/ERP.Service/Infrastructure/Configurations/
- Purchase configs → src/ERP.Purchase/Infrastructure/Configurations/
- Sales configs → src/ERP.Sales/Infrastructure/Configurations/
- Accounting configs → src/ERP.Accounting/Infrastructure/Configurations/

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
These domains reference entities from Group 1.
When configuring cross-domain relationships:
- Configure the FK property and index on the owning side
- Do NOT configure the relationship from the other side
  (that belongs in the other domain's configuration)
- Use HasIndex() for every cross-domain FK column

Known cross-domain FKs to expect:
- CustomerId → ERP.Customer
- VendorId → ERP.Vendor
- ProductId → ERP.Product
- WarehouseId → ERP.Warehouse
- Foundation entity FKs → ERP.Foundation

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
These domains had namespace clashes in Group 1.
Use explicit type aliases in every configuration file:
- using Inventory = ERP.Inventory.Domain.Entities.Inventory;
- using Asset = ERP.Asset.Domain.Entities.Asset;
Never use a wildcard namespace import that could clash
with the project namespace.

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
   that needs clarification before Group 3 configs