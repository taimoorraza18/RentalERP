import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { Observable } from 'rxjs';

export interface IUnsavedChanges {
  hasUnsavedChanges(): boolean;
}

export const unsavedChangesGuard: CanDeactivateFn<IUnsavedChanges> = component => {
  if (!component.hasUnsavedChanges()) {
    return true;
  }

  const confirmationService = inject(ConfirmationService);

  return new Observable<boolean>(observer => {
    confirmationService.confirm({
      message: 'You have unsaved changes. Are you sure you want to leave?',
      header: 'Unsaved Changes',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        observer.next(true);
        observer.complete();
      },
      reject: () => {
        observer.next(false);
        observer.complete();
      },
    });
  });
};
