Read these files before doing anything:
- constitutions/Domains/Foundation Domain.md
- constitutions/Domains/Security Domain.md
- constitutions/Domains/Customer Domain.md
- constitutions/Domains/Vendor Domain.md
- constitutions/Domains/Product Domain.md
- constitutions/Domains/Warehouse Domain.md

Read every file completely before writing any code.
These files are the source of truth for all table names,
column types, indexes, constraints and relationships.

---

TASK: Generate EF Core IEntityTypeConfiguration<T> classes
for all Group 1 domain entities.

PLACEMENT RULES:
- Foundation configs → src/ERP.Foundation/Infrastructure/Configurations/
- Security configs → src/ERP.Security/Infrastructure/Configurations/
- Customer configs → src/ERP.Customer/Infrastructure/Configurations/
- Vendor configs → src/ERP.Vendor/Infrastructure/Configurations/
- Product configs → src/ERP.Product/Infrastructure/Configurations/
- Warehouse configs → src/ERP.Warehouse/Infrastructure/Configurations/

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
   that needs clarification