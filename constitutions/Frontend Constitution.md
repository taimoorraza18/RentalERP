# RentalERP Frontend Constitution v1.0

This document defines the mandatory frontend architecture, development standards, UI framework, coding conventions and UX principles for RentalERP.

Every developer and AI assistant (Claude Code, ChatGPT, Copilot, etc.) must follow this document. Do not deviate unless explicitly instructed.

---

# 1. Project Overview

RentalERP is an enterprise business application used by organizations to manage:

- Administration
- Security
- Customers
- Vendors
- Products
- Inventory
- Warehouse
- Assets
- Rentals
- Services
- Purchases
- Sales
- Accounting
- Reporting
- Workflow
- Notifications
- Dashboard
- Audit
- Integration
- Scheduler
- System Configuration
- Platform Management

The application is designed for long-term maintainability, scalability and productivity.

User productivity is always more important than visual effects.

---

# 2. Technology Stack

Frontend Framework

- Angular 20
- Standalone Components
- TypeScript (Strict Mode)

UI Framework

- PrimeNG
- PrimeIcons

Styling

- Tailwind CSS
- SCSS
- CSS Variables (Design Tokens)

State Management

- Angular Signals
- RxJS only when appropriate

Charts

- ApexCharts

Internationalization

- ngx-translate

Documents

- SheetJS
- jsPDF

Backend

- .NET 9 Web API

---

# 3. Development Philosophy

The frontend must be:

- Modular
- Reusable
- Configurable
- Maintainable
- Responsive
- Enterprise Ready

Avoid unnecessary abstractions.

Avoid duplicate implementations.

Reuse existing infrastructure whenever possible.

Every feature must follow the same architecture.

Consistency is more important than cleverness.

---

# 4. Application Architecture

The application follows Feature Based Architecture.

Never organize the application using global folders such as:

Components

Pages

Services

Models

Instead every business module owns everything related to itself.

Example

Customer

Inventory

Purchase

Sales

Accounting

Each feature is isolated.

Business logic must never leak into another feature.

---

# 5. Folder Structure

src/

app/

core/

infrastructure/

shared/

layout/

features/

administration/

security/

customer/

vendor/

product/

warehouse/

inventory/

asset/

rental/

service/

purchase/

sales/

accounting/

reporting/

workflow/

notification/

dashboard/

audit/

integration/

scheduler/

system-configuration/

platform/

assets/

environments/

---

# 6. Core Layer

Core contains application-wide functionality.

Examples

Authentication

Authorization

Guards

Interceptors

Configuration

Navigation

Application Bootstrap

Core must never contain business logic.

---

# 7. Infrastructure Layer

Infrastructure contains reusable application services.

LoadingService

NotificationService

DialogService

PermissionService

NavigationService

ApiClient

ErrorService

ThemeService

ConfigurationService

StorageService

LoggerService

Feature modules must always use these services.

Never create duplicate implementations.

---

# 8. Shared Layer

Shared contains reusable UI components.

Examples

ERPTable

ERPFileUpload

ERPTimeline

ERPAttachmentViewer

ERPConfirmDialog

ERPEmptyState

ERPSkeleton

ERPToolbar

ERPPageHeader

ERPFilters

Shared components must never contain business-specific logic.

---

# 9. UI Framework

PrimeNG is the only UI component library.

Use PrimeNG for:

Buttons

Inputs

Dropdowns

AutoComplete

Dialogs

Tables

Tree

TreeTable

Calendar

Menu

Panel

Card

Toolbar

Toast

Sidebar

Drawer

Stepper

Timeline

FileUpload

ContextMenu

Accordion

Splitter

Progress

Skeleton

BlockUI

VirtualScroller

Do not introduce another UI library.

---

# 10. Tailwind Usage

Tailwind is responsible for:

Layout

Grid

Flex

Spacing

Sizing

Responsive Design

Alignment

Visibility

Positioning

Typography utilities

Do not use Tailwind to redesign PrimeNG components.

Use Tailwind to compose pages, not replace component styling.

---

# 11. SCSS Usage

SCSS is used only for:

Theme

Design Tokens

Reusable Mixins

Animations

PrimeNG Theme Overrides

Global Styles

Avoid writing layout CSS that can be achieved using Tailwind.

---

# 12. Design System

Never hardcode colors.

Never hardcode spacing.

Use Design Tokens.

Examples

Primary

Secondary

Success

Warning

Danger

Info

Surface

Background

Border

Text

Border Radius

Spacing

Elevation

Animation Duration

All themes must use these tokens.

---

# 13. General Principles

Prefer readability over cleverness.

Prefer reuse over duplication.

Prefer configuration over hardcoding.

Keep components focused on one responsibility.

Write production-ready code.

Never generate placeholder implementations.

If requirements are unclear, ask for clarification instead of making assumptions.

# 14. ERP Table Framework

Every business list must use a standardized ERPTable component.

Feature modules must never use PrimeNG p-table directly.

Example

Customer List

Vendor List

Inventory

Purchase Orders

Sales Orders

Rental Contracts

Accounting Journals

All must use ERPTable.

ERPTable internally uses PrimeNG p-table.

---

## ERPTable Features

ERPTable must support:

- Server Side Paging
- Server Side Sorting
- Server Side Filtering
- Global Search
- Multi Column Search
- Saved Filters
- User Preferences
- Column Resize
- Column Reorder
- Column Visibility
- Frozen Columns
- Row Selection
- Multi Selection
- Context Menu
- Inline Editing
- Row Expansion
- Row Grouping
- Group Header
- Group Footer
- Export Excel
- Export CSV
- Print
- Skeleton Loading
- Empty State
- Loading Overlay
- Permission Based Actions

---

## Responsive Table Strategy

Desktop

Show complete table.

Laptop

Hide lowest priority columns.

Tablet

Hidden columns move into expandable detail rows.

Mobile

Automatically switch to Card View.

Never use horizontal scrolling as the primary responsive solution.

---

## Column Configuration

Every column should support:

Field

Header

Width

Priority

Sortable

Filterable

Editable

Visible

Frozen

Permission

Formatter

Template

Columns should be configurable and reusable.

---

## User Preferences

Users should be able to save:

Column Order

Column Width

Visible Columns

Filters

Sorting

Page Size

Preferences should be restored automatically.

---

# 15. Forms Framework

Use Reactive Forms only.

Never use Template Driven Forms.

Every form must support:

Create Mode

Edit Mode

View Mode

One component should support all three modes.

Do not duplicate forms.

---

## Form Layout

Large forms should use:

Tabs

Sections

Cards

Accordions

Logical grouping

Avoid very long single-page forms.

---

## Validation

Frontend validation improves user experience.

Backend remains the source of truth.

Display validation errors close to the field.

Show summary validation only when necessary.

---

## Form Behavior

Prevent duplicate submission.

Disable Save while processing.

Warn before leaving unsaved changes.

Auto-focus first invalid field.

Support keyboard navigation.

---

# 16. Lookup Framework

Every lookup should support:

Server Search

Pagination

Lazy Loading

Multi Select

Single Select

Keyboard Navigation

Dialog Lookup

Recent Items

Favorites (future ready)

Large datasets must never be fully loaded.

---

# 17. Dialog Framework

Use Prime Dynamic Dialog.

Do not open dialogs directly throughout the application.

Use DialogService.

Supported dialog types:

Create

Edit

View

Delete Confirmation

Side Panel

Wizard

Full Screen

Responsive

---

## Dialog Behavior

Dialogs should:

Load data before interaction.

Display loading state.

Support ESC to close when safe.

Warn for unsaved changes.

Be responsive.

Avoid nested dialogs.

---

# 18. Loading Framework

Every long-running operation must provide visual feedback.

Never leave users wondering whether the application is working.

---

## Loading Types

Global Loader

Application startup.

Authentication.

Refresh token.

---

Page Loader

Entire page loading.

Use Skeletons instead of spinners whenever possible.

---

Section Loader

Individual cards.

Tabs.

Panels.

Widgets.

---

Grid Loader

Displayed inside ERPTable.

---

Button Loader

Save

Update

Delete

Import

Export

Buttons should display loading state and prevent double clicks.

---

Dialog Loader

Dialogs must load required data before becoming interactive.

---

## Loading Rules

Every API call longer than approximately 300ms should display an appropriate loader.

Hide loaders only after UI is fully rendered.

Never flash loaders for extremely short operations.

---

# 19. Notification Framework

Use NotificationService only.

Never call Prime MessageService directly from feature modules.

---

## Notification Types

Success

Information

Warning

Error

Loading

Progress

---

## Notification Behavior

Support:

Position

Duration

Sticky

Action Buttons

HTML Content

Icons

Progress Bar

Duplicate Prevention

Queue

---

Examples

Customer created successfully.

Retry

Undo

View Details

---

# 20. Error Framework

Use centralized error handling.

Categorize errors as:

Validation

Business

Authorization

Network

Server

Unexpected

Display user-friendly messages.

Never expose backend exception details.

---

# 21. API Framework

Every feature owns its own API service.

Example

CustomerApiService

InventoryApiService

PurchaseApiService

Never create one huge ApiService.

---

## API Rules

Always use typed DTOs.

Support cancellation tokens.

Use pagination for large datasets.

Avoid unnecessary API calls.

Cache configuration data when appropriate.

Never hardcode URLs.

Use environment configuration.

# 24. Permission Framework

The frontend must implement permission-aware UI.

Permissions are enforced by the backend, but the frontend must hide or disable inaccessible features to improve user experience.

---

## Permission Types

Module Permission

Page Permission

Action Permission

Field Permission

Column Permission

Widget Permission

Report Permission

---

## UI Behavior

Buttons

Menus

Tabs

Actions

Dialogs

Fields

Columns

Widgets

must automatically respect permissions.

Example

Create Customer

↓

User has no Create permission

↓

Hide button

Example

Delete Customer

↓

User has Read permission only

↓

Delete action hidden

Never rely on the frontend for security.

Backend authorization is always mandatory.

---

# 25. Theme Framework

The application must support centralized theming.

Never hardcode colors or fonts.

Everything must come from Design Tokens.

---

## Theme Tokens

Primary

Secondary

Success

Warning

Danger

Info

Surface

Background

Border

Text Primary

Text Secondary

Border Radius

Spacing

Shadow

Animation Duration

Typography

---

## Theme Requirements

Support:

Light Theme

Dark Theme (future ready)

Company Branding (future ready)

User Preferences

High Contrast (future ready)

---

# 26. Responsive Framework

Every page must work on:

Desktop

Laptop

Tablet

Mobile

---

## Layout Rules

Desktop

Maximum information density.

---

Laptop

Minor spacing adjustments.

---

Tablet

Responsive layouts.

Tables become expandable.

Dialogs become wider.

Navigation collapses.

---

Mobile

Cards instead of tables.

Single-column layouts.

Full-screen dialogs.

Touch-friendly controls.

---

Never create separate pages for desktop and mobile.

Use adaptive layouts.

---

# 27. Dashboard Standards

Dashboard widgets must be reusable.

Each widget should support:

Refresh

Configuration

Permissions

Loading

Empty State

Error State

Filters

Export

Widgets must not directly call APIs.

They should communicate through their feature services.

---

# 28. Charts

Use ApexCharts.

Charts must be reusable.

Supported chart types:

Line

Bar

Column

Area

Pie

Donut

Radar

Heatmap

Timeline

Charts must support:

Export

Fullscreen

Refresh

Filters

Responsive resizing

---

# 29. File Upload Standards

Use Prime FileUpload.

Support:

Drag & Drop

Browse

Multiple Files

Image Preview

Progress

Validation

Retry

Large Files

Future Ready:

Chunk Upload

Resumable Upload

Virus Scan Integration

---

# 30. Accessibility

The application must support:

Keyboard Navigation

Screen Readers

ARIA Labels

Focus Indicators

Tab Navigation

Accessible Dialogs

Accessible Forms

Accessible Tables

Accessibility should be considered from the beginning, not added later.

---

# 31. Performance Standards

Always use lazy loading.

Lazy load feature routes.

Lazy load dialogs when practical.

Use virtual scrolling for large datasets.

Use OnPush change detection where beneficial.

Avoid unnecessary subscriptions.

Destroy subscriptions properly.

Use trackBy for rendered lists.

Minimize change detection.

Avoid unnecessary DOM updates.

Cache static lookup data.

---

# 32. User Experience Principles

The application is built for productivity.

Every screen should help users complete work quickly.

---

## General Rules

Show meaningful empty states.

Never leave blank pages.

Always display loading indicators.

Prevent duplicate actions.

Remember user preferences.

Provide keyboard shortcuts where appropriate.

Use confirmation dialogs only for destructive actions.

Avoid excessive animations.

Animations should enhance the experience, not slow it down.

---

## Forms

Auto-focus first editable field.

Move focus to first invalid field.

Keep labels visible.

Avoid placeholder-only inputs.

---

## Tables

Remember filters.

Remember sorting.

Remember pagination.

Support double-click where useful.

Support keyboard navigation.

Support context menus.

---

## Feedback

Every user action should receive immediate feedback.

Success

Warning

Error

Progress

Loading

Never leave the user guessing.

---

# 33. Coding Standards

Enable strict TypeScript.

Use standalone components.

Use inject() instead of constructor injection where appropriate.

Prefer Signals.

Avoid any.

Avoid deep inheritance.

Prefer composition.

Keep components focused.

Separate UI from business logic.

Keep methods small and readable.

Avoid duplicated code.

Use meaningful names.

Follow SOLID principles where applicable.

---

# 34. Naming Conventions

Folders

kebab-case

Example

customer-detail

purchase-order

system-configuration

---

Components

customer-list.component.ts

customer-detail.component.ts

purchase-order-dialog.component.ts

---

Services

customer-api.service.ts

customer-state.service.ts

loading.service.ts

notification.service.ts

---

Interfaces

ICustomer

IPurchaseOrder

---

Enums

CustomerStatus

OrderType

PaymentMethod

---

Constants

UPPER_SNAKE_CASE

---

# 35. Development Rules

Build reusable components.

Avoid premature optimization.

Avoid unnecessary abstractions.

Keep architecture consistent.

Reuse existing infrastructure.

If functionality already exists, extend it instead of creating another implementation.

Think long-term.

Every feature should feel like it was developed by the same developer.

---

# 36. Claude Code Rules

Claude Code must follow these principles when generating code.

Always:

Follow the existing project structure.

Reuse existing services.

Generate production-ready code.

Use typed models.

Use strict TypeScript.

Generate responsive layouts.

Use PrimeNG components correctly.

Use Tailwind only for layout.

Use feature-based architecture.

Respect the Loading Framework.

Respect the Notification Framework.

Respect the Dialog Framework.

Respect the Permission Framework.

Respect the ERP Table Framework.

Generate maintainable code.

If requirements are unclear, ask instead of assuming.

---

# 37. Never Allowed

Never use template-driven forms.

Never use inline styles.

Never hardcode colors.

Never hardcode URLs.

Never hardcode permissions.

Never duplicate loading implementations.

Never duplicate notification implementations.

Never duplicate dialog implementations.

Never call HttpClient directly from components.

Never put business logic inside components.

Never bypass infrastructure services.

Never manipulate the DOM directly.

Never use 'any' without explicit justification.

Never disable strict mode.

Never create feature-specific utility classes inside global styles.

Never create multiple implementations of the same feature.

Never sacrifice maintainability for short-term convenience.

---

# Final Principle

RentalERP is an enterprise application intended to evolve for many years.

Every implementation decision must prioritize:

Consistency

Maintainability

Scalability

Performance

Accessibility

Reusability

Developer Experience

User Productivity

When multiple implementation approaches are possible, always choose the one that produces a consistent, maintainable and enterprise-ready application.

---

# 22. State Management

Prefer Angular Signals.

Use RxJS only when streams provide clear benefits.

Each feature owns its own state.

Avoid global state unless truly shared.

Keep state localized.

---

# 23. Navigation

Support:

Breadcrumbs

Favorites

Recent Pages

Global Search

Module Search

Pinned Menu

Collapsed Menu

Navigation should remain consistent across all modules.

# 38. Feature Module Standard

Every business module must follow exactly the same folder structure.

Example

customer/

pages/

components/

dialogs/

services/

store/

models/

interfaces/

enums/

validators/

utils/

routes.ts

customer-api.service.ts

customer-state.service.ts

Each feature owns everything related to itself.

Never create global business folders.

---

# 39. Component Classification

Every component must belong to one of the following categories.

---

## Page Component

Responsible for:

Routing

Page Layout

Loading Data

Permissions

Toolbar

Never contain business logic.

---

## Feature Component

Represents a reusable part of a feature.

Examples

Customer Summary

Customer Address List

Rental Timeline

Purchase Items

Inventory Stock Card

Feature components should receive data through Inputs and emit Outputs.

---

## Dialog Component

Dialogs should only contain dialog-specific UI.

Dialogs must never perform API calls directly.

Data loading should occur before or through feature services.

---

## Shared Component

Reusable across multiple modules.

Examples

ERPTable

ERPToolbar

ERPPageHeader

ERPSkeleton

ERPEmptyState

ERPAttachmentViewer

---

## Infrastructure Component

Owned by Infrastructure.

Examples

Loading Overlay

Toast Container

Permission Directive

Global Search

Navigation

---

# 40. CRUD Page Standard

Every CRUD module should follow the same page structure.

------------------------------------------------

Page Header

Toolbar

Quick Filters

Advanced Filters

ERPTable

Paginator

------------------------------------------------

Toolbar should contain:

Create

Refresh

Export

Import (if applicable)

Column Settings

Saved Filters

Search

Actions

The layout should remain consistent across all modules.

---

# 41. Detail Page Standard

Every entity detail page should follow the same structure.

------------------------------------------------

Page Header

Summary Card

Quick Actions

Tabs

Timeline

Attachments

Notes

Activity

Audit

------------------------------------------------

Examples

Customer

Vendor

Product

Rental

Purchase

Sales

Inventory

Asset

Consistency is mandatory.

---

# 42. Tab Standard

Detail pages should organize information using tabs.

Typical order:

General

Contacts

Addresses

Items

Documents

Attachments

Timeline

Activity

Audit

Do not invent different tab orders for different modules unless required.

---

# 43. Filter Framework

Every ERPTable should support:

Quick Filters

Advanced Filters

Saved Filters

Reset Filters

Recent Filters

Shared Filters (future ready)

Filters should be reusable.

Avoid implementing filtering differently in each module.

---

# 44. Search Framework

Support multiple search types.

Global Search

Module Search

Lookup Search

Table Search

Search should support keyboard interaction.

Debounce search requests.

Never perform API requests on every keystroke.

---

# 45. Lookup Framework

Every lookup should behave consistently.

Supported lookup types:

Dropdown

Autocomplete

Lookup Dialog

Multi Select

Entity Picker

Large datasets should use server-side searching.

Never preload thousands of records.

---

# 46. Entity Selection Standard

Whenever selecting business entities:

Customer

Vendor

Product

Warehouse

Asset

Employee

Vehicle

Use:

Autocomplete for small datasets.

Lookup Dialog for large datasets.

Support:

Recent Selections

Search

Keyboard Navigation

Quick Create (future ready)

---

# 47. Wizard Framework

Complex workflows should use wizards.

Examples

Rental Creation

Purchase Creation

Sales Creation

Inventory Adjustment

Imports

Wizard structure:

Step Navigation

Validation

Progress

Save Draft

Previous

Next

Finish

Never lose entered data when moving between steps.

---

# 48. Empty State Standard

Every page should provide meaningful empty states.

Instead of

"No Records"

Display:

Illustration/Icon

Friendly Message

Primary Action

Example

"No Customers Found"

Create Customer

Import Customers

Avoid blank screens.

---

# 49. Skeleton Standards

Prefer Skeletons over spinners.

Skeletons should represent the final layout.

Supported skeletons:

Table

Card

Form

Dialog

Dashboard Widget

Timeline

Attachment List

Never replace every loading state with a spinner.

---

# 50. Keyboard Shortcuts

Enterprise users should be productive without relying entirely on the mouse.

Recommended shortcuts:

Ctrl + N

New Record

Ctrl + S

Save

Ctrl + F

Focus Search

Esc

Close Dialog

Delete

Delete Selected

F5

Refresh Data

Arrow Keys

Table Navigation

Tab

Next Field

Shift + Tab

Previous Field

Future modules may extend shortcuts.

---

# 51. File Naming Standards

Folders

kebab-case

Examples

customer-detail

purchase-order

system-configuration

Files

customer-list.page.ts

customer-detail.page.ts

customer-form.component.ts

customer-summary.component.ts

customer-api.service.ts

customer-state.service.ts

customer.routes.ts

customer.model.ts

customer.interface.ts

customer.enum.ts

customer.validator.ts

Maintain consistent naming throughout the project.

---

# 52. Layout Standards

Default page spacing

24px

Card spacing

16px

Section spacing

24px

Grid gap

16px

Form spacing

16px

Dialog padding

24px

Avoid arbitrary spacing values.

Use design tokens whenever possible.

---

# 53. Animation Standards

Animations should improve usability.

Never distract users.

Standard durations:

Page Transition

200ms

Dialog

150ms

Drawer

250ms

Toast

250ms

Accordion

150ms

Tables

No unnecessary animations.

Respect reduced motion preferences where possible.

---

# 54. User Personalization

Users should be able to personalize:

Theme

Sidebar State

Table Columns

Column Width

Filters

Sorting

Page Size

Dashboard Layout (future)

Preferences should persist between sessions.

---

# 55. Offline & Network Handling

Detect network failures gracefully.

Display user-friendly messages.

Support retry actions.

Avoid data loss.

Warn users before leaving unsaved changes.

---

# 56. Final Development Principles

Every feature should appear as if it was developed by the same team.

Prioritize:

Consistency

Predictability

Reusability

Maintainability

Performance

Accessibility

Developer Experience

User Productivity

When uncertain, follow existing patterns instead of inventing new ones.

This document is the single source of truth for all RentalERP frontend development.