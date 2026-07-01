# RentalERP — Claude Code Instructions

## Mandatory Reading
Before doing anything in any session, read these files:
- constitutions/Backend Constitution.md
- constitutions/Frontend Constitution.md
- constitutions/Project Context.md

## Project Structure
repository/
├── constitutions/        — source of truth for all decisions
│   └── Domains/          — individual domain schema files
├── backend/              — .NET 9 solution
└── frontend/             — Angular 20 application

## Architecture
- Backend: .NET 9, Clean Architecture, DDD, Modular Monolith
- Frontend: Angular 20, Standalone Components, PrimeNG
- Database: PostgreSQL 18

## Non-Negotiable Rules

### Naming
- C# classes and properties: PascalCase always
- Database tables and columns: snake_case via global 
  EF Core convention only
- Never use HasColumnName() manually
- Angular: camelCase for properties, PascalCase for classes

### Database
- All primary keys: long (BIGINT)
- Generation strategy: GENERATED ALWAYS AS IDENTITY
- All foreign keys: long to match primary key type
- Every foreign key must have an index
- snake_case via SnakeCaseNamingConvention globally

### Architecture
- Never place business logic in controllers
- Never place business logic in UI components
- Never duplicate functionality across modules
- Never introduce new libraries without approval
- Always inherit from correct SharedKernel base class:
  * AuditableEntity — needs soft delete
  * AggregateRoot — aggregate root without soft delete
  * Entity — simple child/junction entities
- ERP.SharedKernel must never reference EF Core
- ERP.SharedKernel must never contain persisted entities

### Code Quality
- No TODOs in production code
- No placeholder implementations
- No stubs — every class must be production ready
- Run dotnet build after every task and report result

## Current Build Status
- 29 projects — all compiling clean
- SharedKernel: locked, do not modify
- Group 1 entities: complete (Foundation, Security, 
  Customer, Vendor, Product, Warehouse)

## Domain Schema Files
All domain schemas are in constitutions/Domains/
Read the relevant domain file before generating 
any entity, configuration or application code.

## Current State

### Completed
- Solution structure: ✅ 30 projects, all compiling clean
- Shared Kernel: ✅ Locked, do not modify
- Group 1 entities: ✅ Foundation, Security, Customer, 
  Vendor, Product, Warehouse
- Group 2 entities: ✅ Inventory, Asset, Rental, Service, 
  Purchase, Sales, Accounting
- Group 3 entities: ✅ Report, Workflow, Notification, 
  Dashboard, Audit, Integration, Scheduler, 
  Configuration, Platform
- Group 1 EF Configs: ✅ Complete, build clean

### Next Task
EF Core Configurations — Group 2
Read constitutions/Prompts/Group2_EFConfig.md

### Do Not Touch
- ERP.SharedKernel — locked and complete
- Any already generated entity or config filess