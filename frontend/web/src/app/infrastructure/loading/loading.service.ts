import { Injectable, Signal, computed, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private readonly _keyCounts = signal<Map<string, number>>(new Map());

  readonly activeKeys: Signal<Set<string>> = computed(
    () => new Set(this._keyCounts().keys())
  );

  readonly isGlobalLoading: Signal<boolean> = computed(
    () => this._keyCounts().size > 0
  );

  show(key = 'global'): void {
    this._keyCounts.update(map => {
      const next = new Map(map);
      next.set(key, (next.get(key) ?? 0) + 1);
      return next;
    });
  }

  hide(key = 'global'): void {
    this._keyCounts.update(map => {
      const count = map.get(key) ?? 0;
      if (count <= 0) return map;
      const next = new Map(map);
      if (count === 1) {
        next.delete(key);
      } else {
        next.set(key, count - 1);
      }
      return next;
    });
  }

  isLoading(key: string): Signal<boolean> {
    return computed(() => (this._keyCounts().get(key) ?? 0) > 0);
  }

  reset(): void {
    this._keyCounts.set(new Map());
  }
}
