# RentalERP — Backend

Enterprise Rental ERP backend. **Modular Monolith** built with **Domain Driven Design**, **Clean Architecture**, and **CQRS** on **.NET 9 / ASP.NET Core / PostgreSQL 18**.

---

## Solution Structure

```
backend/
├── RentalERP.slnx
├── Directory.Build.props
├── src/
│   ├── ERP.API                    # ASP.NET Core Web API host
│   ├── ERP.SharedKernel           # Base types, value objects, interfaces (no business logic)
│   │
│   ├── ERP.Foundation             # Shared reference data (currencies, countries, etc.)
│   ├── ERP.Administration         # Company administration
│   ├── ERP.Security               # Authentication, roles, permissions (JWT)
│   │
│   ├── ERP.Customer               # Customer management
│   ├── ERP.Vendor                 # Vendor management
│   ├── ERP.Product                # Product catalog
│   ├── ERP.Warehouse              # Warehouse management
│   ├── ERP.Inventory              # Inventory control and stock movement (ledger architecture)
│   ├── ERP.Asset                  # Company asset management
│   │
│   ├── ERP.Rental                 # Rental contracts and lifecycle
│   ├── ERP.Service                # Repairs, maintenance, customer service
│   ├── ERP.Purchase               # Purchase orders and procurement
│   ├── ERP.Sales                  # Sales quotations, orders, invoicing
│   ├── ERP.Accounting             # Financial transactions and reporting
│   │
│   ├── ERP.Reporting              # Business intelligence and reports
│   ├── ERP.Workflow               # Approval workflows
│   ├── ERP.Notification           # Email, SMS, WhatsApp, system notifications
│   ├── ERP.Dashboard              # Dashboards and KPIs
│   ├── ERP.Audit                  # Audit logs and activity tracking
│   ├── ERP.Integration            # External system integrations
│   ├── ERP.Scheduler              # Background jobs and scheduled tasks (Hangfire)
│   ├── ERP.SystemConfiguration    # Application configuration
│   ├── ERP.Platform               # Cross-cutting platform services
│   │
│   ├── ERP.Infrastructure         # Cross-cutting: Serilog, Hangfire, MediatR behaviors, interceptors
│   ├── ERP.Persistence            # AppDbContext, owns all module EF configurations
│   └── ERP.Migrations             # All EF Core database migrations
│
└── tests/
    ├── ERP.UnitTests              # Unit tests — Domain and Application layers
    └── ERP.IntegrationTests       # Integration tests — API endpoints
```

---

## Module Internal Structure

Every business module follows the **same** internal folder layout:

```
ERP.<Module>/
├── Domain/
│   ├── Entities/          # Aggregate roots and child entities
│   ├── ValueObjects/      # Immutable value types
│   ├── Enums/             # Domain enumerations
│   ├── Specifications/    # Query specifications
│   ├── Repositories/      # Repository interfaces (contracts only)
│   └── DomainEvents/      # Domain event definitions
│
├── Application/
│   ├── Commands/          # MediatR command definitions
│   ├── Queries/           # MediatR query definitions
│   ├── Handlers/          # Command and query handlers
│   ├── Validators/        # FluentValidation validators
│   ├── DTOs/              # Data transfer objects
│   ├── Mappings/          # AutoMapper profiles
│   └── Events/            # Integration event definitions
│
├── Infrastructure/
│   ├── Repositories/      # Repository implementations
│   ├── Configurations/    # EF Core IEntityTypeConfiguration<T>
│   ├── Persistence/       # Module-specific persistence helpers
│   └── Services/          # Infrastructure service implementations
│
└── Presentation/
    ├── Controllers/       # ASP.NET Core API controllers
    └── Endpoints/         # Minimal API endpoints (if used)
```

---

## Project Dependencies

```
ERP.SharedKernel        ──► (nothing)

ERP.<Module>            ──► ERP.SharedKernel

ERP.Infrastructure      ──► ERP.SharedKernel

ERP.Persistence         ──► ERP.SharedKernel
                        ──► ERP.Infrastructure
                        ──► ERP.<all 23 modules>     (discovers EF configurations)

ERP.Migrations          ──► ERP.Persistence

ERP.API                 ──► ERP.<all 23 modules>     (registers Presentation layer)
                        ──► ERP.Infrastructure
                        ──► ERP.Persistence
```

Cross-module communication is permitted only through:
- Public interfaces defined in `ERP.SharedKernel`
- Domain Events dispatched via `MediatR`
- Application Service contracts

Direct references between business modules are **prohibited**.

---

## NuGet Package Assignments

| Project | Packages |
|---------|----------|
| `ERP.SharedKernel` | *(none — pure .NET abstractions)* |
| All 23 business modules | `MediatR` · `FluentValidation` · `AutoMapper` · `Microsoft.EntityFrameworkCore` |
| `ERP.Security` (extra) | `Microsoft.AspNetCore.Authentication.JwtBearer` |
| `ERP.Scheduler` (extra) | `Hangfire.Core` |
| `ERP.Infrastructure` | `MediatR` · `FluentValidation` · `Microsoft.EntityFrameworkCore` · `Serilog` · `Serilog.AspNetCore` · `Serilog.Sinks.Console` · `Serilog.Sinks.File` · `Hangfire.Core` · `Hangfire.PostgreSql` |
| `ERP.Persistence` | `Microsoft.EntityFrameworkCore` · `Npgsql.EntityFrameworkCore.PostgreSQL` |
| `ERP.Migrations` | `Microsoft.EntityFrameworkCore.Design` · `Npgsql.EntityFrameworkCore.PostgreSQL` |
| `ERP.API` | `Microsoft.AspNetCore.Authentication.JwtBearer` · `Swashbuckle.AspNetCore` · `Hangfire.AspNetCore` · `Serilog.AspNetCore` |
| `ERP.UnitTests` | `xunit` · `FluentAssertions` · `NSubstitute` · `coverlet.collector` |
| `ERP.IntegrationTests` | `xunit` · `FluentAssertions` · `Microsoft.AspNetCore.Mvc.Testing` · `coverlet.collector` |

---

## Technology Stack

| Concern | Technology |
|---------|-----------|
| Runtime | .NET 9 / ASP.NET Core |
| ORM | Entity Framework Core 9 (Code First) |
| Database | PostgreSQL 18 |
| CQRS | MediatR 12 |
| Validation | FluentValidation 11 |
| Mapping | AutoMapper 13 |
| Background Jobs | Hangfire 1.8 |
| Logging | Serilog 4 |
| Auth | JWT Bearer / ASP.NET Authorization Policies |
| API Docs | Swashbuckle / OpenAPI |
| Testing | xUnit · FluentAssertions · NSubstitute |

---

## Getting Started

```bash
# Restore all packages
dotnet restore RentalERP.slnx

# Build the solution
dotnet build RentalERP.slnx

# Add a migration (run from backend/)
dotnet ef migrations add InitialCreate \
  --project src/ERP.Migrations \
  --startup-project src/ERP.API

# Apply migrations
dotnet ef database update \
  --project src/ERP.Migrations \
  --startup-project src/ERP.API

# Run tests
dotnet test RentalERP.slnx
```

> **Connection string**: configure `ConnectionStrings:Default` in `src/ERP.API/appsettings.json` or via environment variable `ConnectionStrings__Default`.

---

## Key Design Rules

- **One database**, one `AppDbContext` in `ERP.Persistence`. Each module contributes EF configurations via `ApplyConfigurationsFromAssembly`.
- **Soft delete everywhere** — `IsDeleted` flag; no hard deletes unless explicitly required.
- **Optimistic concurrency** — `RowVersion` on every aggregate root.
- **UTC timestamps** — all audit columns stored in UTC.
- **snake_case database** — configured globally via EF Core naming convention.
- **BIGINT primary keys** — `GENERATED ALWAYS AS IDENTITY`; never natural keys.
- **No lazy loading** — disabled globally; use explicit loading or projection.
- **API versioning** — all endpoints prefixed `/api/v1/`.
- **RFC 7807 ProblemDetails** — unified error response model.
