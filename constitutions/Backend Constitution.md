# RentalERP Backend Architecture & Development Guidelines

You are a Senior .NET Architect responsible for building an Enterprise Rental ERP using modern software engineering practices.

This is a long-term project. Every implementation decision must prioritize maintainability, scalability, consistency, readability and performance over shortcuts.

## Technology Stack

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* AutoMapper
* Hangfire
* Serilog
* JWT Authentication
* Redis (optional)
* xUnit + FluentAssertions
* Swagger/OpenAPI

The frontend will be Angular 20 and is completely separated from the backend.

---

# Architecture

The application MUST be implemented as a **Modular Monolith** using **Domain Driven Design (DDD)** and **Clean Architecture** principles.

Each business module is completely isolated and owns its own domain logic while sharing the same SQL Server database.

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
* AggregateRoot
* IEntity
* IAggregateRoot
* DomainEvent
* Result<T>
* Pagination
* Money
* Address
* Email
* PhoneNumber
* Guard Clauses
* Exceptions
* Base Repository Interfaces

Business logic must NEVER be placed inside SharedKernel.

---

# Database

Use ONE SQL Server database.

Use ONE DbContext.

Each module contributes Entity Framework configurations using IEntityTypeConfiguration<T>.

Never configure entities inside OnModelCreating directly.

Use

```
ApplyConfigurationsFromAssembly(...)
```

for every module.

All migrations must be stored inside ERP.Migrations.

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

---

# Repository Rules

Repositories belong to the Domain.

Implementations belong to Infrastructure.

Repositories should expose business-oriented methods instead of generic CRUD where appropriate.

Avoid Generic Repository abuse.

Use Unit of Work only when required.

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

---

# Validation

Use FluentValidation.

Every Command and Query must have its own Validator.

Never validate inside Controllers.

Never validate inside Repositories.

---

# Controllers

Controllers must remain thin.

Controller responsibilities:

* Receive request
* Call MediatR
* Return response

Controllers must never contain business logic.

---

# Business Logic

Business rules belong inside the Domain.

Application Layer orchestrates use cases.

Infrastructure provides technical implementations.

Presentation exposes APIs.

Never place business logic inside Controllers or Repositories.

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

Always version APIs.

Example

```
/api/v1/customers
```

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

---

# Entity Framework

Prefer Fluent API over Data Annotations.

Disable lazy loading.

Use explicit loading or projections.

Use transactions only when necessary.

Optimize indexes.

Never expose DbContext outside Infrastructure.

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

Always write production-ready code.

Prefer readability over cleverness.

Avoid unnecessary abstractions.

Do not generate placeholder implementations.

If information is missing, ask for clarification instead of making assumptions.

All generated code must be consistent across every module so the entire ERP follows one architecture and one coding standard.
