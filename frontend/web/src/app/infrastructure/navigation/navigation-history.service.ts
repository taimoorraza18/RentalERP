import { Injectable, inject, signal } from '@angular/core';
import { StorageService } from '@infrastructure/storage';

export interface IRecentPage {
  label: string;
  url: string;
  icon?: string;
  visitedAt: Date;
}

const RECENT_PAGES_KEY = 'erp_recent_pages';
const MAX_RECENT_PAGES = 10;

@Injectable({ providedIn: 'root' })
export class NavigationHistoryService {
  private readonly storage = inject(StorageService);

  readonly recentPages = signal<IRecentPage[]>(this.loadFromStorage());

  addRecentPage(page: IRecentPage): void {
    this.recentPages.update(pages => {
      const filtered = pages.filter(p => p.url !== page.url);
      const next = [page, ...filtered].slice(0, MAX_RECENT_PAGES);
      this.storage.set<IRecentPage[]>(RECENT_PAGES_KEY, next);
      return next;
    });
  }

  clearRecentPages(): void {
    this.storage.remove(RECENT_PAGES_KEY);
    this.recentPages.set([]);
  }

  getRecentPages(): IRecentPage[] {
    return this.recentPages();
  }

  private loadFromStorage(): IRecentPage[] {
    const saved = this.storage.get<IRecentPage[]>(RECENT_PAGES_KEY);
    if (!saved) return [];
    return saved.map(p => ({ ...p, visitedAt: new Date(p.visitedAt) }));
  }
}
