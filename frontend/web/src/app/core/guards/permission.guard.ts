import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '@core/auth';

export const permissionGuard: CanActivateFn = (route, _state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const required = route.data['permissions'] as string[] | undefined;
  if (!required || required.length === 0 || auth.hasAnyPermission(required)) {
    return true;
  }

  return router.createUrlTree(['/unauthorized']);
};
