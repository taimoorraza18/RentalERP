import { Injectable, inject, signal, computed } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '@environments/environment';
import { StorageService } from '@infrastructure/storage';
import { AuthApiService } from './auth-api.service';
import {
  IAuthState,
  IAuthUser,
  ILoginRequest,
  ILoginResponse,
  ITokenPayload,
} from './auth.models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly storage = inject(StorageService);
  private readonly authApi = inject(AuthApiService);

  private readonly _authState = signal<IAuthState>({
    user: null,
    isAuthenticated: false,
    isLoading: false,
    error: null,
  });

  readonly authState = this._authState.asReadonly();
  readonly currentUser = computed(() => this._authState().user);
  readonly isAuthenticated = computed(() => this._authState().isAuthenticated);
  readonly isLoading = computed(() => this._authState().isLoading);
  readonly userPermissions = computed(() => this._authState().user?.permissions ?? []);
  readonly userRoles = computed(() => this._authState().user?.roles ?? []);

  login(request: ILoginRequest): Observable<ILoginResponse> {
    this._authState.update(s => ({ ...s, isLoading: true, error: null }));
    return this.authApi.login(request).pipe(
      tap({
        next: response => this.handleAuthSuccess(response, request.rememberMe),
        error: (err: unknown) => {
          const message = this.extractErrorMessage(err, 'Login failed');
          this._authState.update(s => ({ ...s, isLoading: false, error: message }));
        },
      })
    );
  }

  logout(): void {
    this.storage.remove(environment.tokenKey);
    this.storage.remove(environment.refreshTokenKey);
    this._authState.set({ user: null, isAuthenticated: false, isLoading: false, error: null });
  }

  refreshToken(): Observable<ILoginResponse> {
    const refreshToken = this.storage.get<string>(environment.refreshTokenKey) ?? '';
    return this.authApi.refreshToken({ refreshToken }).pipe(
      tap({
        next: response => this.handleAuthSuccess(response, true),
        error: () => this.logout(),
      })
    );
  }

  hasPermission(permission: string): boolean {
    return this.userPermissions().includes(permission);
  }

  hasRole(role: string): boolean {
    return this.userRoles().includes(role);
  }

  hasAnyPermission(permissions: string[]): boolean {
    return permissions.some(p => this.hasPermission(p));
  }

  hasAnyRole(roles: string[]): boolean {
    return roles.some(r => this.hasRole(r));
  }

  initializeFromStorage(): void {
    const token = this.storage.get<string>(environment.tokenKey);
    if (!token || this.isTokenExpired(token)) {
      this.logout();
      return;
    }
    try {
      const payload = this.decodeToken(token);
      const user: IAuthUser = {
        id: parseInt(payload.sub, 10),
        username: payload.sub,
        email: payload.email,
        fullName: '',
        roles: payload.roles,
        permissions: payload.permissions,
        companyId: payload.companyId,
        branchId: null,
        avatarUrl: null,
      };
      this._authState.set({ user, isAuthenticated: true, isLoading: false, error: null });
    } catch {
      this.logout();
    }
  }

  getAccessToken(): string | null {
    return this.storage.get<string>(environment.tokenKey);
  }

  getRefreshToken(): string | null {
    return this.storage.get<string>(environment.refreshTokenKey);
  }

  private handleAuthSuccess(response: ILoginResponse, persist: boolean): void {
    this.storage.set(environment.tokenKey, response.accessToken);
    if (persist) {
      this.storage.set(environment.refreshTokenKey, response.refreshToken);
    }
    this._authState.set({
      user: response.user,
      isAuthenticated: true,
      isLoading: false,
      error: null,
    });
  }

  private decodeToken(token: string): ITokenPayload {
    const parts = token.split('.');
    if (parts.length !== 3) {
      throw new Error('Invalid JWT format');
    }
    const base64 = parts[1].replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload) as ITokenPayload;
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = this.decodeToken(token);
      return payload.exp * 1000 < Date.now();
    } catch {
      return true;
    }
  }

  private extractErrorMessage(err: unknown, fallback: string): string {
    if (err && typeof err === 'object' && 'error' in err) {
      const e = (err as { error: unknown }).error;
      if (e && typeof e === 'object' && 'message' in e) {
        return String((e as { message: unknown }).message);
      }
    }
    return fallback;
  }
}
