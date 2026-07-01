Read constitutions/Frontend Constitution.md thoroughly before doing anything.

We are building RentalERP. Phases F1 and F2 are complete.
The Angular 20 project is at frontend/web/.
Core layer is fully implemented under src/app/core/.
Infrastructure stubs exist for StorageService, LoadingService, NotificationService.

Your job is Phase F3: Full Infrastructure Layer.
Replace all stubs with production-ready implementations.
Use standalone components, strict TypeScript, inject() for DI, Angular Signals where appropriate.
Do not implement any UI pages or feature modules.

---

PART 1 — StorageService (replace stub)

Replace src/app/infrastructure/storage/storage.service.ts:

- providedIn: 'root'
- Support two storage backends: localStorage and sessionStorage
- Methods:
  - get<T>(key: string, storage?: 'local' | 'session'): T | null
  - set<T>(key: string, value: T, storage?: 'local' | 'session'): void
  - remove(key: string, storage?: 'local' | 'session'): void
  - clear(storage?: 'local' | 'session'): void
  - exists(key: string, storage?: 'local' | 'session'): boolean
- Default storage backend: localStorage
- Serialize/deserialize JSON automatically
- Wrap all operations in try/catch — never throw to callers
- On parse error: remove corrupted key and return null
- Never expose raw localStorage/sessionStorage outside this service

---

PART 2 — LoggerService

Create src/app/infrastructure/logger/logger.service.ts:

- providedIn: 'root'
- In production: suppress debug and info logs
- In development: log all levels with timestamps
- Methods:
  - debug(message: string, ...args: unknown[]): void
  - info(message: string, ...args: unknown[]): void
  - warn(message: string, ...args: unknown[]): void
  - error(message: string, error?: unknown, ...args: unknown[]): void
- Each log prefixed with: [RentalERP] [LEVEL] [timestamp]
- error() should also log the error stack if available
- Use environment.production to determine log level
- Never use console directly outside this service in the rest of the app

---

PART 3 — NotificationService (replace stub)

Replace src/app/infrastructure/notification/notification.service.ts:

- providedIn: 'root'
- Wrap PrimeNG MessageService — never expose MessageService outside infrastructure
- inject MessageService from primeng/api
- Methods:
  - success(message: string, title?: string, options?: INotificationOptions): void
  - error(message: string, title?: string, options?: INotificationOptions): void
  - warning(message: string, title?: string, options?: INotificationOptions): void
  - info(message: string, title?: string, options?: INotificationOptions): void
  - clear(): void
  - clearKey(key: string): void

INotificationOptions interface:
  - life?: number (default 4000ms for success/info/warning, 6000ms for error)
  - sticky?: boolean
  - closable?: boolean
  - key?: string (toast target key)
  - data?: unknown

- Duplicate prevention: do not show identical message+severity within 1000ms
- All notifications must be closable by default
- Error notifications default to sticky: false but life: 6000

---

PART 4 — LoadingService (replace stub)

Replace src/app/infrastructure/loading/loading.service.ts:

- providedIn: 'root'
- Support named loading keys for granular control
- Signals:
  - isGlobalLoading = computed signal: true if any global key is active
  - activeKeys = signal<Set<string>>(new Set())
- Methods:
  - show(key?: string): void — default key: 'global'
  - hide(key?: string): void — default key: 'global'
  - isLoading(key: string): Signal<boolean>
  - reset(): void — clears all active keys
- Key behavior:
  - Multiple show() calls for same key are reference counted
  - hide() decrements count, only removes key when count reaches 0
  - Prevents flickering for rapid show/hide cycles

---

PART 5 — ErrorService

Create src/app/infrastructure/error/error.service.ts:

- providedIn: 'root'
- IAppError interface:
  - type: ErrorType enum (Validation | Business | Authorization | Network | Server | Unexpected)
  - message: string
  - title?: string
  - statusCode?: number
  - validationErrors?: IValidationError[]
  - originalError?: unknown

- IValidationError interface:
  - field: string
  - message: string

- Methods:
  - handle(error: HttpErrorResponse): IAppError
  - mapHttpError(error: HttpErrorResponse): IAppError
  - getValidationErrors(error: HttpErrorResponse): IValidationError[]
  - formatValidationMessage(errors: IValidationError[]): string
  - isValidationError(error: IAppError): boolean
  - isAuthorizationError(error: IAppError): boolean
  - isNetworkError(error: IAppError): boolean

- Mapping rules:
  - status 0: Network error
  - status 400: Validation (extract errors from body)
  - status 401: Authorization
  - status 403: Authorization
  - status 422: Validation (extract field errors from body)
  - status 500+: Server error
  - Everything else: Unexpected

---

PART 6 — PermissionService

Create src/app/infrastructure/permission/permission.service.ts:

- providedIn: 'root'
- inject AuthService from core/auth
- Methods:
  - hasPermission(permission: string): boolean
  - hasRole(role: string): boolean
  - hasAnyPermission(permissions: string[]): boolean
  - hasAllPermissions(permissions: string[]): boolean
  - hasAnyRole(roles: string[]): boolean
  - canView(resource: string): boolean — checks '{resource}.view' permission
  - canCreate(resource: string): boolean — checks '{resource}.create'
  - canEdit(resource: string): boolean — checks '{resource}.edit'
  - canDelete(resource: string): boolean — checks '{resource}.delete'
  - canExport(resource: string): boolean — checks '{resource}.export'
  - canImport(resource: string): boolean — checks '{resource}.import'

Create src/app/infrastructure/permission/has-permission.directive.ts:
- Structural directive: *hasPermission="'customer.create'"
- Also support *hasPermission="['customer.create', 'customer.edit']" (any)
- Removes element from DOM if permission check fails
- Uses PermissionService

Create src/app/infrastructure/permission/has-role.directive.ts:
- Structural directive: *hasRole="'Admin'"
- Also support array: *hasRole="['Admin', 'Manager']"
- Removes element from DOM if role check fails

---

PART 7 — ThemeService

Create src/app/infrastructure/theme/theme.service.ts:

- providedIn: 'root'
- inject StorageService
- IThemeConfig interface:
  - mode: 'light' | 'dark'
  - primaryColor?: string
  - fontSize?: 'small' | 'medium' | 'large'

- Signals:
  - currentTheme = signal<IThemeConfig>
  - isDarkMode = computed(() => currentTheme().mode === 'dark')

- Methods:
  - initialize(): void — loads saved theme from storage, applies it
  - setMode(mode: 'light' | 'dark'): void
  - toggleDarkMode(): void
  - setPrimaryColor(color: string): void
  - setFontSize(size: 'small' | 'medium' | 'large'): void
  - private applyTheme(config: IThemeConfig): void — sets CSS variables on document root
  - private saveTheme(config: IThemeConfig): void — persists to StorageService

- On initialize(): read from StorageService, fall back to environment.defaultTheme
- applyTheme() must update CSS variables defined in _tokens.scss on :root

---

PART 8 — ConfigurationService

Create src/app/infrastructure/config/configuration.service.ts:

- providedIn: 'root'
- Exposes typed app configuration from environment
- Properties (all readonly signals or getters):
  - apiBaseUrl: string
  - appName: string
  - appVersion: string
  - defaultLanguage: string
  - isProduction: boolean
- Methods:
  - getApiUrl(path: string): string — concatenates apiBaseUrl + path cleanly

---

PART 9 — ApiClient

Create src/app/infrastructure/api/api-client.service.ts:

- providedIn: 'root'
- inject HttpClient
- inject ConfigurationService for base URL
- inject LoggerService
- All methods return Observable<T>
- Methods:
  - get<T>(path: string, params?: HttpParams | Record<string, unknown>, options?: IRequestOptions): Observable<T>
  - post<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T>
  - put<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T>
  - patch<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T>
  - delete<T>(path: string, options?: IRequestOptions): Observable<T>
  - postFormData<T>(path: string, formData: FormData, options?: IRequestOptions): Observable<T>

IRequestOptions interface:
  - skipLoading?: boolean — adds X-Skip-Loading header
  - skipErrorHandling?: boolean
  - responseType?: 'json' | 'blob' | 'text'
  - headers?: Record<string, string>

- Automatically builds full URL using ConfigurationService.getApiUrl()
- Logs all requests in development using LoggerService
- For skipLoading: add X-Skip-Loading: true header (interceptor reads this)
- params: convert Record to HttpParams automatically

---

PART 10 — DialogService

Create src/app/infrastructure/dialog/dialog.service.ts:

- providedIn: 'root'
- Wrap PrimeNG DynamicDialogService — never expose it outside infrastructure
- inject PrimeNG DialogService as PrimeDynamicDialogService
- inject ConfirmationService from primeng/api

IDialogOptions interface:
  - header?: string
  - width?: string
  - height?: string
  - modal?: boolean (default true)
  - closable?: boolean (default true)
  - closeOnEscape?: boolean (default true)
  - data?: unknown
  - styleClass?: string
  - maximizable?: boolean

Methods:
  - open<T>(component: Type<unknown>, options?: IDialogOptions): DynamicDialogRef
  - confirm(message: string, header?: string): Promise<boolean>
  - confirmDelete(entityName: string): Promise<boolean>
  - close(ref: DynamicDialogRef, result?: unknown): void

- confirmDelete should use a standardized message:
  "Are you sure you want to delete this {entityName}? This action cannot be undone."
- confirm() and confirmDelete() return Promise<boolean> (true = confirmed, false = cancelled)

---

PART 11 — NavigationService (Infrastructure)

Create src/app/infrastructure/navigation/navigation-history.service.ts:

- providedIn: 'root'
- Tracks recently visited pages
- Signal: recentPages = signal<IRecentPage[]>([])
- IRecentPage: { label: string; url: string; icon?: string; visitedAt: Date }
- Methods:
  - addRecentPage(page: IRecentPage): void — adds to front, max 10 items, no duplicates
  - clearRecentPages(): void
  - getRecentPages(): IRecentPage[]
- Persist recentPages to StorageService

---

PART 12 — Update app.config.ts

Update src/app/app.config.ts to add:
- MessageService from primeng/api (provide it)
- ConfirmationService from primeng/api (provide it)
- DynamicDialogModule providers if needed
- ThemeService.initialize() call via APP_INITIALIZER (runs after auth initializer)

---

PART 13 — Update Barrel Files

Update index.ts in every infrastructure subfolder:
- src/app/infrastructure/storage/
- src/app/infrastructure/logger/
- src/app/infrastructure/notification/
- src/app/infrastructure/loading/
- src/app/infrastructure/error/
- src/app/infrastructure/permission/
- src/app/infrastructure/theme/
- src/app/infrastructure/config/
- src/app/infrastructure/api/
- src/app/infrastructure/dialog/
- src/app/infrastructure/navigation/

---

PART 14 — Final Verification

Run `ng build` and confirm zero errors.
Report any warnings.
Do not generate any UI components, pages, layout or feature modules.