Read constitutions/Frontend Constitution.md thoroughly before doing anything.

We are setting up the RentalERP Angular 20 frontend. The Angular project was created with `ng new web` using SCSS and no routing. It is located at frontend/web/.

Your job is Phase F1: complete workspace setup. Do not generate any components, services, or business logic yet.

---

PART 1 — Install All Required Packages

Install the following packages exactly. Do not introduce any other libraries.

Dependencies:
- primeng (latest)
- primeicons (latest)
- @primeuix/themes (latest)
- @angular/cdk (latest)
- apexcharts
- ng-apexcharts
- @ngx-translate/core
- @ngx-translate/http-loader
- xlsx
- jspdf
- jspdf-autotable

DevDependencies:
- tailwindcss
- @tailwindcss/forms
- postcss
- autoprefixer

After installing, verify the Angular project still builds successfully with `ng build`.

---

PART 2 — TypeScript Configuration

Update tsconfig.json with the following:
- strict: true
- strictNullChecks: true
- strictPropertyInitialization: true
- noImplicitAny: true
- noImplicitReturns: true
- noFallthroughCasesInSwitch: true
- strictTemplates: true (in angularCompilerOptions)
- forceConsistentCasingInFileNames: true
- paths alias: @core/* → src/app/core/*, @shared/* → src/app/shared/*, @features/* → src/app/features/*, @infrastructure/* → src/app/infrastructure/*, @layout/* → src/app/layout/*, @environments/* → src/environments/*

---

PART 3 — Tailwind Setup

Configure Tailwind CSS for Angular:
- Create tailwind.config.js with content paths covering all .html and .ts files under src/
- Add @tailwind base, components, utilities to styles.scss
- Do not use Tailwind to override PrimeNG component styles
- Do not create any custom Tailwind components

---

PART 4 — Environment Files

Create the following environment files under src/environments/:

environment.ts (development):
- production: false
- apiBaseUrl: 'http://localhost:5000/api'
- appVersion: '1.0.0'
- appName: 'RentalERP'
- defaultLanguage: 'en'
- defaultTheme: 'light'
- tokenKey: 'erp_token'
- refreshTokenKey: 'erp_refresh_token'
- userPreferencesKey: 'erp_preferences'

environment.prod.ts (production):
- production: true
- apiBaseUrl: '/api'
- same other fields

Register both environment files in angular.json under fileReplacements for the production build.

---

PART 5 — Folder Structure

Create the complete folder structure under src/app/ as follows. Create only index.ts barrel files and .gitkeep where needed — no implementation files yet.

src/app/
├── core/
│   ├── auth/
│   ├── guards/
│   ├── interceptors/
│   ├── config/
│   └── navigation/
├── infrastructure/
│   ├── api/
│   ├── loading/
│   ├── notification/
│   ├── dialog/
│   ├── error/
│   ├── permission/
│   ├── theme/
│   ├── storage/
│   ├── logger/
│   └── navigation/
├── shared/
│   ├── components/
│   │   ├── erp-table/
│   │   ├── erp-toolbar/
│   │   ├── erp-page-header/
│   │   ├── erp-filters/
│   │   ├── erp-file-upload/
│   │   ├── erp-attachment-viewer/
│   │   ├── erp-timeline/
│   │   ├── erp-confirm-dialog/
│   │   ├── erp-empty-state/
│   │   └── erp-skeleton/
│   ├── directives/
│   ├── pipes/
│   ├── models/
│   └── utils/
├── layout/
│   ├── shell/
│   ├── sidebar/
│   ├── topbar/
│   └── breadcrumb/
└── features/
    ├── foundation/
    │   ├── administration/
    │   ├── security/
    │   ├── dashboard/
    │   └── system-configuration/
    ├── masters/
    │   ├── customer/
    │   ├── vendor/
    │   ├── product/
    │   ├── warehouse/
    │   └── asset/
    ├── operations/
    │   ├── rental/
    │   ├── service/
    │   ├── purchase/
    │   ├── sales/
    │   └── inventory/
    ├── finance/
    │   ├── accounting/
    │   └── reporting/
    └── platform/
        ├── workflow/
        ├── notification/
        ├── audit/
        ├── integration/
        └── scheduler/

---

PART 6 — Styles Structure

Create the following SCSS structure under src/styles/:

styles/
├── _tokens.scss        — all CSS custom properties (design tokens)
├── _typography.scss    — font families, sizes, weights, line heights
├── _animations.scss    — transition durations and keyframes
├── _mixins.scss        — reusable SCSS mixins
├── _reset.scss         — minimal resets on top of Tailwind base
├── _primeng.scss       — PrimeNG global overrides (empty for now, ready for Phase F4)
└── _utilities.scss     — any global utility classes not covered by Tailwind

Import all partials into the main styles.scss in the correct order:
tokens → reset → typography → animations → mixins → primeng → utilities → tailwind directives

In _tokens.scss define CSS variables for:
- --color-primary, --color-primary-hover, --color-primary-light
- --color-secondary
- --color-success, --color-warning, --color-danger, --color-info
- --color-surface, --color-background, --color-border
- --color-text-primary, --color-text-secondary, --color-text-muted
- --border-radius-sm, --border-radius-md, --border-radius-lg, --border-radius-xl
- --spacing-xs, --spacing-sm, --spacing-md, --spacing-lg, --spacing-xl
- --shadow-sm, --shadow-md, --shadow-lg
- --duration-fast (150ms), --duration-normal (200ms), --duration-slow (250ms)
- --font-family-base, --font-size-base, --font-weight-normal, --font-weight-medium, --font-weight-bold

Use sensible default values for a professional enterprise ERP (neutral grays, blue primary, proper surface colors).

---

PART 7 — app.config.ts

Set up app.config.ts with:
- provideHttpClient with withInterceptorsFromDi()
- provideAnimationsAsync()
- provideRouter([]) — empty routes for now, routing will be added in a later phase
- provideTranslateModule setup (ngx-translate with HttpLoader reading from assets/i18n/)
- PrimeNG provider configuration

---

PART 8 — i18n

Create assets/i18n/en.json with an empty object {} as placeholder.

---

PART 9 — Final Verification

After all parts are complete:
1. Run `ng build` and confirm zero errors
2. Report any warnings
3. Confirm folder structure was created correctly
4. List all installed package versions

Do not generate any components, pages, services, guards or business logic. This phase is workspace preparation only.