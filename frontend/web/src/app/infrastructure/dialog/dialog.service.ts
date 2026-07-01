import { Injectable, Type, inject } from '@angular/core';
import { DialogService as PrimeDynamicDialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';

export interface IDialogOptions {
  header?: string;
  width?: string;
  height?: string;
  modal?: boolean;
  closable?: boolean;
  closeOnEscape?: boolean;
  data?: unknown;
  styleClass?: string;
  maximizable?: boolean;
}

@Injectable({ providedIn: 'root' })
export class ErpDialogService {
  private readonly primeDynamicDialog = inject(PrimeDynamicDialogService);
  private readonly confirmationService = inject(ConfirmationService);

  open<T>(component: Type<T>, options?: IDialogOptions): DynamicDialogRef | null {
    return this.primeDynamicDialog.open(component, {
      header: options?.header,
      width: options?.width ?? '50vw',
      height: options?.height,
      modal: options?.modal ?? true,
      closable: options?.closable ?? true,
      closeOnEscape: options?.closeOnEscape ?? true,
      data: options?.data,
      styleClass: options?.styleClass,
      maximizable: options?.maximizable ?? false,
    });
  }

  confirm(message: string, header?: string): Promise<boolean> {
    return new Promise<boolean>(resolve => {
      this.confirmationService.confirm({
        message,
        header: header ?? 'Confirm',
        icon: 'pi pi-question-circle',
        accept: () => resolve(true),
        reject: () => resolve(false),
      });
    });
  }

  confirmDelete(entityName: string): Promise<boolean> {
    return this.confirm(
      `Are you sure you want to delete this ${entityName}? This action cannot be undone.`,
      'Delete Confirmation'
    );
  }

  close(ref: DynamicDialogRef | null, result?: unknown): void {
    ref?.close(result);
  }
}
