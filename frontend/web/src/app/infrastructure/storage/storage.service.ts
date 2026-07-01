import { Injectable } from '@angular/core';

type StorageBackend = 'local' | 'session';

@Injectable({ providedIn: 'root' })
export class StorageService {
  private backend(storage?: StorageBackend): Storage {
    return storage === 'session' ? sessionStorage : localStorage;
  }

  get<T>(key: string, storage?: StorageBackend): T | null {
    try {
      const raw = this.backend(storage).getItem(key);
      if (raw === null) return null;
      return JSON.parse(raw) as T;
    } catch {
      this.remove(key, storage);
      return null;
    }
  }

  set<T>(key: string, value: T, storage?: StorageBackend): void {
    try {
      this.backend(storage).setItem(key, JSON.stringify(value));
    } catch {
      // Quota exceeded or storage unavailable — fail silently
    }
  }

  remove(key: string, storage?: StorageBackend): void {
    try {
      this.backend(storage).removeItem(key);
    } catch {
      // Ignore
    }
  }

  clear(storage?: StorageBackend): void {
    try {
      this.backend(storage).clear();
    } catch {
      // Ignore
    }
  }

  exists(key: string, storage?: StorageBackend): boolean {
    try {
      return this.backend(storage).getItem(key) !== null;
    } catch {
      return false;
    }
  }
}
