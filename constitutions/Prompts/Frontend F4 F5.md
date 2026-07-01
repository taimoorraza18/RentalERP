Read constitutions/Frontend Constitution.md thoroughly before doing anything.

We are building RentalERP. Phases F1, F2, F3 are complete.
The Angular 20 project is at frontend/web/.
Full infrastructure layer is implemented under src/app/infrastructure/.

Your job is Phase F4 (Design System) and Phase F5 (Layout Shell).
Use standalone components, strict TypeScript, inject() for DI, Angular Signals.
PrimeNG is the only UI component library.
Tailwind is for layout only — never use it to override PrimeNG styles.

---

PART 1 — Design Tokens (_tokens.scss)

Replace src/styles/_tokens.scss with a complete professional enterprise token set.

Light theme tokens on :root:

Primary palette:
--color-primary: #2563EB
--color-primary-hover: #1D4ED8
--color-primary-light: #EFF6FF
--color-primary-dark: #1E40AF
--color-primary-contrast: #FFFFFF

Secondary palette:
--color-secondary: #64748B
--color-secondary-hover: #475569
--color-secondary-light: #F1F5F9
--color-secondary-contrast: #FFFFFF

Semantic colors:
--color-success: #16A34A
--color-success-light: #F0FDF4
--color-success-contrast: #FFFFFF
--color-warning: #D97706
--color-warning-light: #FFFBEB
--color-warning-contrast: #FFFFFF
--color-danger: #DC2626
--color-danger-light: #FEF2F2
--color-danger-contrast: #FFFFFF
--color-info: #0891B2
--color-info-light: #ECFEFF
--color-info-contrast: #FFFFFF

Surface and background:
--color-background: #F8FAFC
--color-surface: #FFFFFF
--color-surface-hover: #F8FAFC
--color-surface-secondary: #F1F5F9
--color-surface-tertiary: #E2E8F0

Border:
--color-border: #E2E8F0
--color-border-strong: #CBD5E1
--color-border-focus: #2563EB

Text:
--color-text-primary: #0F172A
--color-text-secondary: #475569
--color-text-muted: #94A3B8
--color-text-disabled: #CBD5E1
--color-text-inverse: #FFFFFF
--color-text-link: #2563EB
--color-text-link-hover: #1D4ED8

Sidebar specific:
--color-sidebar-bg: #0F172A
--color-sidebar-text: #CBD5E1
--color-sidebar-text-active: #FFFFFF
--color-sidebar-item-hover: #1E293B
--color-sidebar-item-active: #2563EB
--color-sidebar-icon: #64748B
--color-sidebar-icon-active: #FFFFFF
--color-sidebar-border: #1E293B
--color-topbar-bg: #FFFFFF
--color-topbar-border: #E2E8F0
--color-topbar-text: #0F172A

Border radius:
--radius-xs: 2px
--radius-sm: 4px
--radius-md: 6px
--radius-lg: 8px
--radius-xl: 12px
--radius-2xl: 16px
--radius-full: 9999px

Spacing:
--space-1: 4px
--space-2: 8px
--space-3: 12px
--space-4: 16px
--space-5: 20px
--space-6: 24px
--space-8: 32px
--space-10: 40px
--space-12: 48px
--space-16: 64px

Shadows:
--shadow-xs: 0 1px 2px 0 rgb(0 0 0 / 0.05)
--shadow-sm: 0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)
--shadow-md: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)
--shadow-lg: 0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1)
--shadow-xl: 0 20px 25px -5px rgb(0 0 0 / 0.1), 0 8px 10px -6px rgb(0 0 0 / 0.1)

Animation durations:
--duration-fast: 150ms
--duration-normal: 200ms
--duration-slow: 250ms
--duration-page: 200ms
--duration-dialog: 150ms
--duration-drawer: 250ms
--duration-toast: 250ms
--duration-accordion: 150ms
--easing-default: cubic-bezier(0.4, 0, 0.2, 1)
--easing-in: cubic-bezier(0.4, 0, 1, 1)
--easing-out: cubic-bezier(0, 0, 0.2, 1)

Typography:
--font-family-base: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif
--font-family-mono: 'JetBrains Mono', 'Fira Code', monospace
--font-size-xs: 0.75rem
--font-size-sm: 0.875rem
--font-size-base: 0.9375rem
--font-size-lg: 1.0625rem
--font-size-xl: 1.25rem
--font-size-2xl: 1.5rem
--font-size-3xl: 1.875rem
--font-weight-normal: 400
--font-weight-medium: 500
--font-weight-semibold: 600
--font-weight-bold: 700
--line-height-tight: 1.25
--line-height-normal: 1.5
--line-height-relaxed: 1.625

Layout:
--sidebar-width: 260px
--sidebar-collapsed-width: 64px
--topbar-height: 60px
--content-max-width: 1600px
--page-padding: var(--space-6)

Also add dark theme overrides inside [data-theme="dark"] selector
with appropriate dark surface and text colors — keep primary colors same.

---

PART 2 — Typography (_typography.scss)

Update src/styles/_typography.scss:
- Import Inter font from Google Fonts via @import in styles.scss
- Set base font-family, font-size, line-height on html and body using tokens
- Heading styles h1–h6 using tokens
- Paragraph, label, small, code styles
- Link styles using --color-text-link tokens
- Selection color using primary token

---

PART 3 — Animations (_animations.scss)

Update src/styles/_animations.scss:
- @keyframes fadeIn, fadeOut
- @keyframes slideInLeft, slideOutLeft (for sidebar drawer)
- @keyframes slideInRight, slideOutRight
- @keyframes slideInUp, slideOutDown
- @keyframes scaleIn (for dialogs)
- @keyframes pulse (for skeleton loading)
- Utility classes: .animate-fade-in, .animate-slide-in-left, .animate-scale-in
- @media (prefers-reduced-motion: reduce) — disable all animations

---

PART 4 — Mixins (_mixins.scss)

Update src/styles/_mixins.scss with:
- @mixin flex-center
- @mixin flex-between
- @mixin flex-start
- @mixin truncate (single line ellipsis)
- @mixin line-clamp($lines)
- @mixin card-base (surface bg, border, border-radius-lg, shadow-sm)
- @mixin focus-ring (accessible focus outline using primary color)
- @mixin respond-to($breakpoint) — supports: mobile, tablet, laptop, desktop
- @mixin visually-hidden
- @mixin scrollbar-thin (thin styled scrollbar using tokens)

---

PART 5 — PrimeNG Theme Overrides (_primeng.scss)

Update src/styles/_primeng.scss with global PrimeNG overrides:

Configure PrimeNG to use Aura theme preset with primary color matching --color-primary.

Override these to match design tokens:
- .p-button border-radius uses --radius-md
- .p-inputtext border-radius uses --radius-md, border uses --color-border
- .p-inputtext:focus border uses --color-border-focus
- .p-card uses --color-surface, --shadow-sm, --radius-lg
- .p-dialog .p-dialog-header uses --color-surface, border-bottom uses --color-border
- .p-datatable .p-datatable-header uses --color-surface
- .p-datatable tbody tr:hover uses --color-surface-hover
- .p-toast border-radius uses --radius-lg
- .p-tag border-radius uses --radius-full
- .p-badge border-radius uses --radius-full
- .p-sidebar, .p-drawer uses --color-surface
- General: all PrimeNG components inherit --font-family-base
- Scrollbars: apply scrollbar-thin mixin globally

---

PART 6 — Shell Layout Component

Create src/app/layout/shell/shell.component.ts:

- Standalone component
- Selector: app-shell
- Template structure:
  <div class="erp-shell" [attr.data-theme]="themeService.currentTheme().mode">
    <app-sidebar />
    <div class="erp-main" [class.sidebar-collapsed]="sidebarCollapsed()">
      <app-topbar />
      <main class="erp-content">
        <router-outlet />
      </main>
    </div>
  </div>

- inject ThemeService, NavigationService
- Signal: sidebarCollapsed = signal<boolean>(false)
- Method: toggleSidebar(): void
- Listen to sidebar collapse events from SidebarComponent via a shared SidebarStateService

- SCSS (shell.component.scss):
  .erp-shell: display flex, height 100vh, overflow hidden, background --color-background
  .erp-main: flex 1, display flex, flex-direction column, overflow hidden, transition width using --duration-drawer
  .erp-content: flex 1, overflow-y auto, padding --page-padding, use @include scrollbar-thin

---

PART 7 — SidebarStateService

Create src/app/layout/sidebar/sidebar-state.service.ts:
- providedIn: 'root'
- Signal: isCollapsed = signal<boolean>(false)
- Signal: isMobileOpen = signal<boolean>(false)
- Methods:
  - toggle(): void
  - collapse(): void
  - expand(): void
  - toggleMobile(): void
  - closeMobile(): void
- Persist collapsed state to StorageService with key 'erp_sidebar_collapsed'
- On init: restore from StorageService

---

PART 8 — Sidebar Component

Create src/app/layout/sidebar/sidebar.component.ts:

Sidebar must support:
- Collapsed mode (icons only, 64px wide)
- Expanded mode (icons + labels, 260px wide)
- Mobile overlay mode
- Active route highlighting
- Nested menu items (one level deep only)
- Tooltip on collapsed icons using PrimeNG pTooltip

IMenuItem interface:
  - id: string
  - label: string
  - icon: string (PrimeIcons class)
  - route?: string
  - children?: IMenuItem[]
  - permission?: string
  - badge?: string | number
  - separator?: boolean

Menu structure (hardcoded for now, will be driven by permissions later):
Group: Foundation
  - Dashboard (pi pi-home) → /dashboard
  - Administration (pi pi-building) → /administration
  - Security (pi pi-shield) → /security
  - System Config (pi pi-cog) → /system-configuration

Group: Masters
  - Customers (pi pi-users) → /customers
  - Vendors (pi pi-truck) → /vendors
  - Products (pi pi-box) → /products
  - Warehouses (pi pi-warehouse) → /warehouses
  - Assets (pi pi-server) → /assets

Group: Operations
  - Rentals (pi pi-calendar) → /rentals
  - Services (pi pi-wrench) → /services
  - Purchases (pi pi-shopping-cart) → /purchases
  - Sales (pi pi-dollar) → /sales
  - Inventory (pi pi-list) → /inventory

Group: Finance
  - Accounting (pi pi-calculator) → /accounting
  - Reporting (pi pi-chart-bar) → /reporting

Group: Platform
  - Workflow (pi pi-sitemap) → /workflow
  - Notifications (pi pi-bell) → /notifications
  - Audit (pi pi-eye) → /audit
  - Scheduler (pi pi-clock) → /scheduler

Sidebar SCSS:
- Width transitions between collapsed and expanded using --duration-drawer
- Active item: background --color-sidebar-item-active, text --color-sidebar-text-active
- Hover: background --color-sidebar-item-hover
- Group labels: font-size --font-size-xs, color --color-sidebar-icon, uppercase, letter-spacing
- Logo area at top: 60px height matching topbar, border-bottom --color-sidebar-border
- Scrollable menu area with @include scrollbar-thin
- Mobile: position fixed, overlay with backdrop, z-index 1000

---

PART 9 — Topbar Component

Create src/app/layout/topbar/topbar.component.ts:

Elements (left to right):
- Hamburger/toggle button → calls SidebarStateService.toggle()
- Breadcrumb component (app-breadcrumb)
- Spacer (flex: 1)
- Global search button (pi pi-search) — placeholder, opens search panel later
- Notifications button (pi pi-bell) with badge count signal
- Theme toggle button (pi pi-sun / pi pi-moon) → ThemeService.toggleDarkMode()
- User avatar/menu button → shows dropdown with: Profile, Settings, Divider, Logout

User menu dropdown using PrimeNG Menu component:
- Profile → /profile
- Settings → /settings
- Logout → calls AuthService.logout()

Topbar SCSS:
- Height: --topbar-height (60px)
- Background: --color-topbar-bg
- Border-bottom: 1px solid --color-topbar-border
- Box-shadow: --shadow-xs
- Display flex, align-items center, padding 0 --space-4
- Position sticky, top 0, z-index 100

---

PART 10 — Breadcrumb Component

Create src/app/layout/breadcrumb/breadcrumb.component.ts:

- Standalone component
- inject NavigationService from core
- Display breadcrumbs signal from NavigationService
- Use PrimeNG p-breadcrumb component
- Home icon always first item
- Truncate long breadcrumb labels on mobile
- SCSS: font-size --font-size-sm, color --color-text-secondary

---

PART 11 — App Routes

Update src/app/app.routes.ts:

- Default route '' → redirect to /dashboard
- Route '' with ShellComponent as layout wrapper using children:
  - /dashboard → lazy load placeholder component
  - /unauthorized → inline unauthorized component
  - All other feature routes will be added in later phases
- Route /auth/login → lazy load placeholder login component
- Use authGuard on all shell children routes

Create a minimal placeholder for:
- src/app/features/foundation/dashboard/pages/dashboard.page.ts
  Simple standalone component showing "Dashboard - Coming Soon" using PrimeNG Card
  Sets breadcrumb to [{ label: 'Dashboard', icon: 'pi pi-home' }]

- src/app/core/auth/pages/login.page.ts
  Simple standalone component showing a centered PrimeNG Card with:
  Title: "RentalERP"
  Subtitle: "Sign in to your account"
  Username input (p-inputtext)
  Password input (p-password)
  Login button (p-button) — calls AuthService.login(), shows loading state
  Error message display
  Uses reactive form, inject AuthService, inject Router
  On success: navigate to /dashboard
  No styling beyond Tailwind layout utilities

- src/app/features/unauthorized/unauthorized.page.ts
  Simple page: "403 - Access Denied" with a back button

---

PART 12 — Update app.component.ts

Update src/app/app.component.ts:
- Remove default Angular content
- Simple: template with only <router-outlet />
- No styles needed on app component itself

---

PART 13 — Final Verification

Run `ng build` and confirm zero errors.
Run `ng serve` and confirm the app loads in the browser showing the shell with sidebar and topbar.
Report what is visible when navigating to http://localhost:4200.
Report any warnings.