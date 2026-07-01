import { inject } from '@angular/core';
import { AuthService } from '@core/auth';

export function appInitializerFactory(): () => Promise<void> {
  const auth = inject(AuthService);
  return () =>
    new Promise<void>(resolve => {
      auth.initializeFromStorage();
      resolve();
    });
}
