Read constitutions/Frontend Constitution.md thoroughly before doing anything.

We are building RentalERP. Phase F1 (workspace setup) is complete. 
The Angular 20 project is at frontend/web/. All packages are installed. 
Folder structure exists with barrel index.ts files.

Your job is Phase F2: Core Layer. Implement everything under src/app/core/.
Use standalone components, strict TypeScript, inject() for DI, and Angular Signals where appropriate.
Do not implement any UI or business logic. This is infrastructure only.

---

PART 1 — Auth Models

Create src/app/core/auth/auth.models.ts with the following interfaces and types:

- ILoginRequest: { username: string; password: string; rememberMe: boolean }
- ILoginResponse: { accessToken: string; refreshToken: string; expiresIn: number; user: IAuthUser }
- IAuthUser: { id: number; username: string; email: string; fullName: string; roles: string[]; permissions: string[]; companyId: number; branchId: number | null; avatarUrl: string | null }
- ITokenPayload: { sub: string; email: string; roles: string[]; permissions: string[]; companyId: number; exp: number; iat: number }
- IRefreshTokenRequest: { refreshToken: string }
- IAuthState: { user: IAuthUser | null; isAuthenticated: boolean; isLoading: boolean; error: string | null }

---

PART 2 — Auth Service

Create src/app/core/auth/auth.service.ts:

- Use Angular Signals for state: authState signal of type IAuthState
- Computed signals: currentUser, isAuthenticated, isLoading, userPermissions, userRoles
- Methods:
  - login(request: ILoginRequest): Observable<ILoginResponse>
  - logout(): void
  - refreshToken(): Observable<ILoginResponse>
  - hasPermission(permission: string): boolean
  - hasRole(role: string): boolean
  - hasAnyPermission(permissions: string[]): boolean
  - hasAnyRole(roles: string[]): boolean
  - initializeFromStorage(): void — reads token from storage on app start
  - private decodeToken(token: string): ITokenPayload
  - private isTokenExpired(token: string): boolean

- Use StorageService from @infrastructure/storage for token storage (inject it, do not use localStorage directly)
- Use environment variables for token keys
- Do not make HTTP calls directly — delegate to a core ApiService (create src/app/core/auth/auth-api.service.ts separately)
- Update authState signal on every state change

Create src/app/core/auth/auth-api.service.ts:
- login(request: ILoginRequest): Observable<ILoginResponse>
- refreshToken(request: IRefreshTokenRequest): Observable<ILoginResponse>
- logout(): Observable<void>
- Use HttpClient injected via inject()
- Use environment apiBaseUrl for URLs — never hardcode

---

PART 3 — Guards

Create src/app/core/guards/auth.guard.ts:
- Functional guard using inject()
- Redirects to /auth/login if not authenticated
- Preserves the attempted URL for redirect after login
- Uses AuthService.isAuthenticated signal

Create src/app/core/guards/permission.guard.ts:
- Functional guard using inject()
- Reads required permissions from route data: data: { permissions: string[] }
- Uses AuthService.hasAnyPermission()
- Redirects to /unauthorized if permission check fails

Create src/app/core/guards/unsaved-changes.guard.ts:
- Functional guard using inject()
- Interface IUnsavedChanges: { hasUnsavedChanges(): boolean }
- Checks if component implements IUnsavedChanges
- Shows confirmation before leaving if unsaved changes exist
- Use PrimeNG ConfirmationService for the dialog

---

PART 4 — Interceptors

Create src/app/core/interceptors/auth.interceptor.ts:
- Functional HTTP interceptor
- Attaches Authorization: Bearer <token> header to every outgoing request
- Skips auth header for requests to /auth/login and /auth/refresh
- On 401 response: attempts token refresh once, retries original request
- On refresh failure: calls AuthService.logout() and redirects to login
- Prevents multiple simultaneous refresh attempts using a refresh lock

Create src/app/core/interceptors/loading.interceptor.ts:
- Functional HTTP interceptor
- Calls LoadingService.show() on request start
- Calls LoadingService.hide() on response or error
- Supports a custom header X-Skip-Loading to bypass loading for silent requests

Create src/app/core/interceptors/error.interceptor.ts:
- Functional HTTP interceptor
- Catches all HTTP errors
- Maps to typed error categories: Validation, Business, Authorization, Network, Server, Unexpected
- For 422: extracts validation errors from response body
- For 401: triggers auth refresh flow
- For 403: redirects to /unauthorized
- For 0/network error: shows network error notification
- Uses NotificationService for user-facing error messages
- Re-throws typed error for feature services to handle if needed

---

PART 5 — Configuration

Create src/app/core/config/app-config.service.ts:
- Loads runtime configuration from environment
- Exposes typed config properties
- Provides apiBaseUrl, appName, appVersion, defaultLanguage, defaultTheme

Create src/app/core/config/app-initializer.ts:
- Factory function for APP_INITIALIZER
- Calls AuthService.initializeFromStorage()
- Returns a Promise

---

PART 6 — Navigation Service

Create src/app/core/navigation/navigation.service.ts:
- inject Router
- Signal: currentUrl = signal<string>('')
- Signal: previousUrl = signal<string>('')
- Signal: breadcrumbs = signal<IBreadcrumb[]>([])
- IBreadcrumb interface: { label: string; url?: string; icon?: string }
- Methods:
  - navigateTo(url: string, extras?: NavigationExtras): void
  - navigateBack(): void
  - navigateToLogin(): void
  - navigateToUnauthorized(): void
  - setBreadcrumbs(breadcrumbs: IBreadcrumb[]): void
  - Listen to Router events and update currentUrl and previousUrl signals

---

PART 7 — Infrastructure Stubs (Minimal)

The interceptors and auth service depend on infrastructure services not yet built.
Create minimal stub versions so the core layer compiles cleanly:

src/app/infrastructure/storage/storage.service.ts (stub):
- get(key: string): string | null
- set(key: string, value: string): void
- remove(key: string): void
- clear(): void
- Use localStorage internally for now — will be replaced in F3

src/app/infrastructure/loading/loading.service.ts (stub):
- show(key?: string): void
- hide(key?: string): void
- isLoading = signal<boolean>(false)

src/app/infrastructure/notification/notification.service.ts (stub):
- success(message: string, title?: string): void
- error(message: string, title?: string): void
- warning(message: string, title?: string): void
- info(message: string, title?: string): void
- Use console.log for now — will be replaced in F3

---

PART 8 — Update app.config.ts

Update src/app/app.config.ts to include:
- withInterceptors([authInterceptor, loadingInterceptor, errorInterceptor])
- APP_INITIALIZER using the appInitializerFactory
- provideRouter with routes from app.routes.ts
- Keep existing providers from F1

---

PART 9 — Update Barrel Files

Update index.ts barrel files in:
- src/app/core/auth/
- src/app/core/guards/
- src/app/core/interceptors/
- src/app/core/config/
- src/app/core/navigation/
- src/app/infrastructure/storage/
- src/app/infrastructure/loading/
- src/app/infrastructure/notification/

---

PART 10 — Final Verification

Run `ng build` and confirm zero errors.
Report any warnings.
Do not generate any UI components, pages or feature modules.