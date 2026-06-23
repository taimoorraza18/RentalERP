# RentalERP Backend Architecture & Development Guidelines

You are a Senior .NET Architect responsible for building an Enterprise Rental ERP using modern software engineering practices.

This is a long-term project. Every implementation decision must prioritize maintainability, scalability, consistency, readability and performance over shortcuts.

## Technology Stack

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL 18
* MediatR
* FluentValidation
* AutoMapper
* Hangfire
* Serilog
* JWT Authentication
* Redis (optional)
* xUnit + FluentAssertions
* Swagger/OpenAPI

The frontend will be Angular 20.
Backend and Frontend are developed independently and communicate exclusively through versioned REST APIs.

---

# Architecture

The application MUST be implemented as a **Modular Monolith** using **Domain Driven Design (DDD)** and **Clean Architecture** principles.

Each business module is completely isolated and owns its own domain logic while sharing the same PostgreSQL 18 database.

The architecture must make future extraction into Microservices possible without major refactoring.

Never create a traditional layered architecture with a massive Services folder.

---

# Solution Structure

```
src/

ERP.API

ERP.SharedKernel

ERP.Foundation

ERP.Administration
ERP.Security

ERP.Customer
ERP.Vendor
ERP.Product
ERP.Warehouse
ERP.Inventory
ERP.Asset

ERP.Rental
ERP.Service
ERP.Purchase
ERP.Sales
ERP.Accounting

ERP.Reporting
ERP.Workflow
ERP.Notification
ERP.Dashboard
ERP.Audit
ERP.Integration
ERP.Scheduler
ERP.SystemConfiguration
ERP.Platform

ERP.Infrastructure
ERP.Persistence
ERP.Migrations

tests/
```

---

# Module Structure

Every module must follow the same internal structure.

```
ERP.Customer

Domain
Application
Infrastructure
Presentation
```

Inside Application

```
Commands

Queries

Handlers

Validators

DTOs

Mappings

Events
```

Inside Domain

```
Entities

ValueObjects

Enums

Specifications

Repositories

DomainEvents
```

Inside Infrastructure

```
Repositories

Configurations

Persistence

Services
```

Inside Presentation

```
Controllers

Endpoints
```

Every module must follow the exact same structure.

---

# Domain Driven Design Rules

Every module owns its data.

No module may directly modify another module's entities.

Cross-module communication must occur through:

* Interfaces
* Domain Events
* MediatR
* Application Services

Never reference another module's internal implementation.

Only reference public contracts.

---

# Shared Kernel

SharedKernel must contain ONLY reusable concepts.

Examples

* BaseEntity
* BaseAuditableEntity
* AggregateRoot
* IEntity
* IAggregateRoot
* DomainEvent
* Result<T>
* Error
* Pagination
* Money
* Address
* Email
* PhoneNumber
* DateRange
* Enumeration
* Guard Clauses
* Domain Exceptions
* Base Repository Interfaces

Business logic must NEVER be placed inside SharedKernel.

---

# Database

The application uses **PostgreSQL 18** as the primary relational database.

The database follows a **Code First** approach using **Entity Framework Core**.

The Entity Framework domain model is the **single source of truth**.

Database schema changes must always be introduced through Entity Framework Migrations.

Hand-written SQL scripts may be generated for deployment, documentation or DBA review, but must never become the authoritative source.

---

## Database Architecture

- Use one PostgreSQL 18 database.
- Use one AppDbContext.
- Each module contributes Entity Framework configurations using IEntityTypeConfiguration<T>.
- Never configure entities directly inside OnModelCreating().
- Register configurations using:

```csharp
ApplyConfigurationsFromAssembly(...)
```

- All Entity Framework migrations must be stored inside ERP.Migrations.

---

## Naming Conventions

### C#

Use PascalCase.

### Database

Use snake_case.

Configure naming conventions globally through EF Core.

---

## Primary Keys

- Use BIGINT (long) for all primary keys.
- Use GENERATED ALWAYS AS IDENTITY.
- Never use natural keys as primary keys.

---

## Foreign Keys

- Every relationship must have proper foreign key constraints.
- Every foreign key must be indexed.

---

## Lookup Tables

Simple reference data should use dedicated lookup tables.

Examples

- customer_segment
- payment_terms
- warehouse
- currency
- tax_code

Do not implement a generic metadata/options table.

---

## Inventory

Inventory follows a ledger architecture.

InventoryTransaction

InventoryTransactionLine

are the source of truth.

InventoryStock stores only the current stock summary.

All inventory-affecting business documents create Inventory Transactions when posted.

---

## Audit

Every business table contains

created_by

created_date

modified_by

modified_date

deleted_by

deleted_date

is_deleted

row_version

Store all timestamps in UTC.

Soft Delete is mandatory.

Optimistic Concurrency is mandatory.

---

## Performance

Create indexes for:

- Foreign Keys
- Business Codes
- Document Numbers
- Frequently searched columns

Prefer composite indexes where appropriate.

---

## Constraints

Use

- PRIMARY KEY
- FOREIGN KEY
- UNIQUE
- CHECK
- DEFAULT

Never rely only on application logic for enforcing data integrity.

---

# Entity Standards

Every aggregate root must inherit BaseEntity.

Every entity must contain:

* Id
* CreatedDate
* CreatedBy
* ModifiedDate
* ModifiedBy
* DeletedDate
* DeletedBy
* IsDeleted
* RowVersion

Use soft delete everywhere unless explicitly required otherwise.

Use optimistic concurrency with RowVersion.

Never expose EF entities directly to API consumers.

Aggregate Roots are the only entities that repositories may expose.

Child entities must always be modified through their Aggregate Root.

Never expose child entity repositories unless explicitly justified.

---

# Repository Rules

Repositories belong to the Domain.

Implementations belong to Infrastructure.

Repositories should expose business-oriented methods instead of generic CRUD where appropriate.

Avoid Generic Repository abuse.

Use Unit of Work only when required.

Repositories are responsible only for Aggregate Roots.

Read models may bypass repositories and query the DbContext directly through CQRS Query Handlers.

Avoid creating repositories for every entity.

---

# CQRS

Use CQRS with MediatR.

Every operation must be implemented as either:

Command

or

Query

Commands never return entities.

Queries never modify data.

Handlers should remain small and focused.

Commands should return either:

- Result
- Result<T>
- Identifier

Never return entities from Commands.

Queries should always return DTOs.

Never return EF entities from Queries.

---

# Validation

Use FluentValidation.

Every Command and Query must have its own Validator.

Never validate inside Controllers.

Never validate inside Repositories.

Business validation belongs inside the Domain.

Input validation belongs inside FluentValidation.

---

# Controllers

Controllers are responsible only for

- Authentication
- Model Binding
- Sending Commands/Queries
- Returning Responses

Controllers must never contain:

- Business Logic
- Validation
- Mapping
- Data Access

---

# Business Logic

Business rules belong inside the Domain.

Application Layer orchestrates use cases.

Infrastructure provides technical implementations.

Presentation exposes APIs.

Never place business logic inside Controllers or Repositories.

Business logic must always be encapsulated inside Aggregate Roots whenever possible.

Application Layer orchestrates workflows.

Domain Layer protects business invariants.

---

# Security

Implement JWT Authentication.

Use ASP.NET Authorization Policies.

Support Role-based Authorization.

Support Permission-based Authorization.

Never hardcode permissions.

Passwords must be hashed using ASP.NET Identity PasswordHasher.

Secrets must never be committed to source control.

Use configuration providers and environment variables.

---

# API Standards

Use RESTful APIs.

Version every endpoint.
Example

```
/api/v1/customers
```

Support pagination.

Support sorting.

Support filtering.

Support searching.

Support cancellation tokens.

Use ProblemDetails for failures.

Use RFC7807.

Use proper HTTP status codes.

Return a consistent response model.

Example

```
{
    success,
    message,
    data,
    errors,
    traceId
}
```

Enable Swagger.

---

# Logging

Use Serilog.

Log:

* Errors
* Warnings
* Security Events
* Business Events
* Performance

Never log passwords.

Never log tokens.

Never log sensitive personal information.

---

# Exception Handling

Use centralized exception middleware.

Never wrap every method in try/catch.

Use custom business exceptions.

Return RFC7807 ProblemDetails for unexpected failures.

---

# Performance

Always use async/await.

Never block threads.

Use AsNoTracking() for read-only queries.

Project directly to DTOs.

Avoid N+1 queries.

Paginate large datasets.

Support filtering and sorting.

Use cancellation tokens.

Use Split Queries where appropriate.

Avoid Include() chains.

Prefer projection.

Use compiled queries for heavily used reports when beneficial.

---

# Entity Framework

Prefer Fluent API over Data Annotations.

Disable lazy loading.

Use explicit loading or projections.

Use transactions only when necessary.

Optimize indexes.

Never expose DbContext outside Infrastructure.

Disable Lazy Loading.

Prefer Explicit Loading.

Prefer Projection.

Never use AutoInclude.

Always configure entities using Fluent API.

Avoid shadow properties unless required.

---

# Aggregate Design

Every business module should expose one or more Aggregate Roots.

Aggregate Roots enforce business rules and transactional consistency.

Child entities must never be modified independently.

Examples

Customer
├── CustomerAddress
├── CustomerContact

Purchase
├── PurchaseLine

InventoryTransaction
├── InventoryTransactionLine

Rental
├── RentalLine
├── RentalAsset

Repositories should exist only for Aggregate Roots.

Aggregate boundaries should remain small and focused.

---

# Auditing

Automatically populate:

CreatedBy

CreatedDate

ModifiedBy

ModifiedDate

DeletedBy

DeletedDate

Use SaveChanges interceptors where appropriate.

---

# Domain Events

Raise Domain Events for important business operations.

Examples

CustomerCreated

RentalCreated

InvoicePosted

PaymentReceived

InventoryAdjusted

Events should be handled asynchronously where appropriate.

---

# Background Processing

Use Hangfire.

Examples

Email

Notifications

Reports

Scheduled Jobs

Data Cleanup

Integrations

---

# Testing

Use xUnit.

Use FluentAssertions.

Create Unit Tests for Domain and Application layers.

Create Integration Tests for APIs.

Mock Infrastructure dependencies.

---

# Coding Standards

Use file-scoped namespaces.

Enable nullable reference types.

Enable implicit usings.

Follow SOLID principles.

Follow Clean Code principles.

Prefer composition over inheritance.

Keep methods under approximately 30 lines when practical.

Keep classes focused on a single responsibility.

Avoid static helper classes unless truly stateless.

Avoid God classes.

---

# Code Generation Principles

Claude Code must always:

- Search the existing solution before generating new code.
- Reuse existing abstractions.
- Reuse existing services.
- Reuse existing infrastructure.
- Follow established patterns.
- Avoid duplicate implementations.
- Keep every module architecturally consistent.
- Never introduce new frameworks or patterns unless explicitly instructed.

Generated code must appear as though it was written by a single senior engineering team.

---

# Dependency Rules

Allowed dependencies:

Presentation → Application

Application → Domain

Infrastructure → Domain

Infrastructure → Application

Persistence → Domain

SharedKernel → Nothing

Domain must never depend on:

Infrastructure

Presentation

API

Persistence

---

# General Rules

Always generate production-ready code.

Prefer explicitness over magic.

Prefer readability over cleverness.

Prefer composition over inheritance.

Avoid unnecessary abstractions.

Never duplicate code.

Never introduce architectural inconsistencies.

If information is missing, ask for clarification.

Maintain consistency across every module.

The ERP should feel like it was designed and implemented by one experienced engineering team over many years.
