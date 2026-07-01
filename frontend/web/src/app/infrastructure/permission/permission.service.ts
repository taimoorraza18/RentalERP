import { Injectable, inject } from '@angular/core';
import { AuthService } from '@core/auth';

@Injectable({ providedIn: 'root' })
export class PermissionService {
  private readonly auth = inject(AuthService);

  hasPermission(permission: string): boolean {
    return this.auth.hasPermission(permission);
  }

  hasRole(role: string): boolean {
    return this.auth.hasRole(role);
  }

  hasAnyPermission(permissions: string[]): boolean {
    return this.auth.hasAnyPermission(permissions);
  }

  hasAllPermissions(permissions: string[]): boolean {
    return permissions.every(p => this.auth.hasPermission(p));
  }

  hasAnyRole(roles: string[]): boolean {
    return this.auth.hasAnyRole(roles);
  }

  canView(resource: string): boolean {
    return this.hasPermission(`${resource}.view`);
  }

  canCreate(resource: string): boolean {
    return this.hasPermission(`${resource}.create`);
  }

  canEdit(resource: string): boolean {
    return this.hasPermission(`${resource}.edit`);
  }

  canDelete(resource: string): boolean {
    return this.hasPermission(`${resource}.delete`);
  }

  canExport(resource: string): boolean {
    return this.hasPermission(`${resource}.export`);
  }

  canImport(resource: string): boolean {
    return this.hasPermission(`${resource}.import`);
  }
}
