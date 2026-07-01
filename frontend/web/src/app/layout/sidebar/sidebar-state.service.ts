import { Injectable, inject, signal } from '@angular/core';
import { StorageService } from '@infrastructure/storage';

@Injectable({ providedIn: 'root' })
export class SidebarStateService {
  private readonly storage = inject(StorageService);

  private readonly _isCollapsed = signal<boolean>(
    this.storage.get<boolean>('erp_sidebar_collapsed') ?? false
  );
  private readonly _isMobileOpen = signal<boolean>(false);

  readonly isCollapsed = this._isCollapsed.asReadonly();
  readonly isMobileOpen = this._isMobileOpen.asReadonly();

  toggle(): void {
    const next = !this._isCollapsed();
    this._isCollapsed.set(next);
    this.storage.set('erp_sidebar_collapsed', next);
  }

  collapse(): void {
    this._isCollapsed.set(true);
    this.storage.set('erp_sidebar_collapsed', true);
  }

  expand(): void {
    this._isCollapsed.set(false);
    this.storage.set('erp_sidebar_collapsed', false);
  }

  toggleMobile(): void {
    this._isMobileOpen.update(v => !v);
  }

  closeMobile(): void {
    this._isMobileOpen.set(false);
  }
}
