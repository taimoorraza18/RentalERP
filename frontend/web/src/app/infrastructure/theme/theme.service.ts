import { Injectable, Signal, computed, inject, signal } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { environment } from '@environments/environment';
import { StorageService } from '@infrastructure/storage';

export interface IThemeConfig {
  mode: 'light' | 'dark';
  primaryColor?: string;
  fontSize?: 'small' | 'medium' | 'large';
}

const THEME_STORAGE_KEY = 'erp_theme';

const FONT_SIZE_MAP: Record<string, string> = {
  small: '13px',
  medium: '14px',
  large: '16px',
};

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private readonly document = inject(DOCUMENT);
  private readonly storage = inject(StorageService);

  private readonly _currentTheme = signal<IThemeConfig>({
    mode: (environment.defaultTheme as 'light' | 'dark') ?? 'light',
  });

  readonly currentTheme: Signal<IThemeConfig> = this._currentTheme.asReadonly();
  readonly isDarkMode: Signal<boolean> = computed(() => this._currentTheme().mode === 'dark');

  initialize(): void {
    const saved = this.storage.get<IThemeConfig>(THEME_STORAGE_KEY);
    const config: IThemeConfig = saved ?? {
      mode: (environment.defaultTheme as 'light' | 'dark') ?? 'light',
    };
    this._currentTheme.set(config);
    this.applyTheme(config);
  }

  setMode(mode: 'light' | 'dark'): void {
    this.updateTheme({ mode });
  }

  toggleDarkMode(): void {
    this.setMode(this._currentTheme().mode === 'dark' ? 'light' : 'dark');
  }

  setPrimaryColor(color: string): void {
    this.updateTheme({ primaryColor: color });
  }

  setFontSize(size: 'small' | 'medium' | 'large'): void {
    this.updateTheme({ fontSize: size });
  }

  private updateTheme(partial: Partial<IThemeConfig>): void {
    const config = { ...this._currentTheme(), ...partial };
    this._currentTheme.set(config);
    this.applyTheme(config);
    this.saveTheme(config);
  }

  private applyTheme(config: IThemeConfig): void {
    const root = this.document.documentElement;

    if (config.mode === 'dark') {
      root.classList.add('erp-dark');
    } else {
      root.classList.remove('erp-dark');
    }

    if (config.primaryColor) {
      root.style.setProperty('--primary-color', config.primaryColor);
    }

    if (config.fontSize) {
      root.style.setProperty('--erp-font-size', FONT_SIZE_MAP[config.fontSize]);
    }
  }

  private saveTheme(config: IThemeConfig): void {
    this.storage.set<IThemeConfig>(THEME_STORAGE_KEY, config);
  }
}
