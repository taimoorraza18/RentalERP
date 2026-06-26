# Shared Kernel

## Overview

The **Shared Kernel** contains the fundamental building blocks shared by every module in RentalERP.

It must remain **pure**, lightweight, framework-agnostic, and independent of persistence technologies.

The Shared Kernel **must not** contain business modules, persisted entities, repositories, database configurations, or infrastructure implementations.

It should have **no dependency on Entity Framework Core** or any other persistence framework.

---

# Purpose

The Shared Kernel provides common abstractions and domain primitives that are used consistently throughout the entire application.

Its primary responsibilities are:

* Base domain abstractions
* Common interfaces
* Value objects
* Domain primitives
* Shared enumerations
* Common result models
* Domain events
* Guard clauses
* Exceptions
* Cross-module contracts

---

# Design Principles

* Keep the Shared Kernel dependency-free.
* Never reference Entity Framework Core.
* Never reference Infrastructure.
* Never reference PostgreSQL.
* Never reference ASP.NET Core.
* Never implement business logic.
* Never implement persistence.
* Never introduce module-specific concepts.

The Shared Kernel should be reusable in any .NET application.

---

# What Belongs in Shared Kernel

## Base Classes

* BaseEntity
* BaseAuditableEntity
* AggregateRoot

---

## Interfaces

* IEntity
* IAggregateRoot
* IRepository
* IUnitOfWork
* IDomainEvent
* ICurrentUser
* IDateTimeProvider

---

## Domain Primitives

* Money
* Email
* PhoneNumber
* DateRange

---

## Enumerations

Strongly typed Enumeration base class.

Examples:

* CurrencyType
* Gender
* Status

---

## Result Pattern

* Result
* Result<T>
* Error

---

## Domain Events

Base classes and interfaces for Domain Events.

---

## Exceptions

* DomainException
* ValidationException
* NotFoundException
* BusinessRuleException

---

## Guard Clauses

Reusable validation helpers.

Examples:

* Guard.NotNull()
* Guard.NotEmpty()
* Guard.Positive()
* Guard.ValidEmail()

---

## Common Utilities

* Pagination
* SortDefinition
* SearchCriteria
* FilterCriteria

---

# What Does NOT Belong in Shared Kernel

The following reusable persisted entities belong to **ERP.Foundation**, not Shared Kernel:

* Address
* Contact
* Attachment
* Note
* Activity
* Timeline
* NumberSeries

These entities require persistence, Entity Framework Core configurations, repositories, and database tables. They are foundational business components rather than pure domain primitives.

---

# Dependency Rules

Shared Kernel may be referenced by:

* ERP.Foundation
* ERP.Infrastructure
* ERP.Persistence
* Every Business Module

Shared Kernel must never reference:

* Foundation
* Infrastructure
* Persistence
* API
* Business Modules
* Entity Framework Core

---

# Project References

```text
ERP.SharedKernel

↑

Referenced by

ERP.Foundation

ERP.Infrastructure

ERP.Persistence

ERP.API

All Business Modules
```

---

# Folder Structure

```text
ERP.SharedKernel

├── Abstractions
├── Base
├── Common
├── Constants
├── DomainEvents
├── Enumerations
├── Exceptions
├── Guards
├── Interfaces
├── Models
├── Results
├── Utilities
└── ValueObjects
```

---

# Architecture Rule

The Shared Kernel represents the foundation of the domain model.

If a class requires:

* Database persistence
* Entity Framework Core configuration
* Repository implementation
* Navigation properties
* Business workflows

then it does **not** belong in Shared Kernel.

Such components belong in **ERP.Foundation** or the appropriate business module.

---

# Goal

Keep the Shared Kernel:

* Small
* Stable
* Framework-independent
* Persistence-independent
* Easy to understand
* Highly reusable

It should rarely change after the initial architecture has been established.
