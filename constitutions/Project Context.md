# RentalERP - Project Context

## Project Overview

RentalERP is a modern, enterprise-grade, web-based ERP (Enterprise Resource Planning) system designed for organizations that manage rental operations, inventory, sales, purchasing, accounting, assets, services and business administration.

The application is intended to become a complete business management platform rather than a simple rental management system.

The primary goals are:

* Productivity
* Maintainability
* Scalability
* Performance
* Configurability
* Enterprise Readiness

This is a long-term project expected to evolve over many years.

Every implementation should prioritize long-term maintainability over short-term convenience.

---

# Project Objectives

The system should provide a single platform for managing:

* Company Administration
* Security & Authorization
* Customers
* Vendors
* Products
* Warehouses
* Inventory
* Assets
* Rentals
* Services
* Purchases
* Sales
* Accounting
* Reports
* Workflow
* Notifications
* Dashboards
* Audit Logs
* System Configuration
* Integrations
* Background Jobs
* Platform Administration

The application is intended for internal business users such as:

* Administrators
* Managers
* Sales Staff
* Inventory Staff
* Purchase Staff
* Accountants
* Warehouse Staff
* Customer Service Representatives

The application is not intended to be a public consumer application.

User productivity is always more important than visual appearance.

---

# Architecture Overview

The backend follows:

* Modular Monolith
* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* MediatR
* Repository Pattern
* Entity Framework Core

The frontend follows:

* Feature-Based Architecture
* Infrastructure Layer
* Shared Components
* PrimeNG
* Tailwind CSS
* Angular Signals

Backend and frontend architectures are documented separately in:

* BACKEND_CONSTITUTION.md
* FRONTEND_CONSTITUTION.md

These documents are mandatory and must always be followed.

---

# Technology Stack

## Backend

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* AutoMapper
* Serilog
* Hangfire
* JWT Authentication

## Frontend

* Angular 20
* Standalone Components
* PrimeNG
* Tailwind CSS
* SCSS
* Angular Signals
* RxJS
* ApexCharts
* ngx-translate

---

# Development Philosophy

Every implementation should follow these principles:

* Build once, reuse everywhere.
* Keep architecture consistent.
* Prefer composition over duplication.
* Follow existing patterns.
* Keep code readable.
* Keep code testable.
* Keep features isolated.
* Avoid unnecessary abstractions.
* Avoid premature optimization.
* Always think long-term.

If multiple implementation approaches are possible, choose the one that produces a more maintainable enterprise application.

---

# Business Domains

The application consists of the following business domains:

## Foundation

Shared entities used throughout the system.

## Administration

Company configuration and administration.

## Security

Authentication, authorization, roles and permissions.

## Customer

Customer management.

## Vendor

Vendor management.

## Product

Product catalog.

## Warehouse

Warehouse management.

## Inventory

Inventory control and stock movement.

## Asset

Company asset management.

## Rental

Rental contracts and rental lifecycle.

## Service

Repairs, maintenance and customer service.

## Purchase

Purchase orders and procurement.

## Sales

Sales quotations, orders and invoicing.

## Accounting

Financial transactions and reporting.

## Reporting

Business intelligence and reporting.

## Workflow

Approval workflows.

## Notification

Emails, SMS, WhatsApp and system notifications.

## Dashboard

Business dashboards and KPIs.

## Audit

Audit logs and activity tracking.

## Integration

External system integrations.

## Scheduler

Background jobs and scheduled tasks.

## System Configuration

Application configuration.

## Platform

Cross-cutting platform services.

---

# Frontend Principles

The frontend should provide a consistent enterprise user experience.

Primary principles:

* Fast
* Responsive
* Consistent
* Predictable
* Keyboard Friendly
* Accessible

PrimeNG is the primary UI component library.

Tailwind CSS is used for layout and responsive utilities.

Never introduce another UI framework.

Reusable infrastructure should always be preferred over feature-specific implementations.

---

# Backend Principles

Business logic belongs inside the domain.

Controllers remain thin.

Repositories remain focused.

Application layer orchestrates use cases.

Infrastructure provides technical implementations.

Never place business logic inside controllers or UI components.

---

# User Experience Principles

The application is used for daily business operations.

Users may spend many hours each day working inside the system.

The interface should therefore prioritize:

* Efficiency
* Clarity
* Speed
* Minimal clicks
* Consistency

Avoid unnecessary animations.

Provide immediate feedback for user actions.

Always display meaningful loading states.

Never leave users wondering whether the application is working.

---

# Reusable Infrastructure

The application provides centralized infrastructure for:

* Loading
* Notifications
* Dialogs
* Permissions
* Navigation
* Error Handling
* Configuration
* Logging
* API Communication
* Theme Management

Do not create duplicate implementations inside feature modules.

---

# Development Rules

Always follow the existing project architecture.

Always reuse existing infrastructure.

Never invent new architectural patterns.

Never introduce new libraries without approval.

Never duplicate functionality.

Always generate production-ready code.

Maintain consistency across every module.

When requirements are unclear, ask for clarification instead of making assumptions.

---

# Success Criteria

Every module should appear as if it was developed by the same engineering team.

Code should be:

* Consistent
* Reusable
* Maintainable
* Scalable
* Performant
* Easy to understand

The long-term quality of the application is more important than the speed of implementation.

This document provides project context only.

Implementation details are defined in the Backend Constitution and Frontend Constitution, which must always take precedence during development.
