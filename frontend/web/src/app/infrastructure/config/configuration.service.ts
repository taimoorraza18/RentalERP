import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable({ providedIn: 'root' })
export class ConfigurationService {
  readonly apiBaseUrl: string = environment.apiBaseUrl;
  readonly appName: string = environment.appName;
  readonly appVersion: string = environment.appVersion;
  readonly defaultLanguage: string = environment.defaultLanguage;
  readonly isProduction: boolean = environment.production;

  getApiUrl(path: string): string {
    const base = this.apiBaseUrl.replace(/\/$/, '');
    const segment = path.startsWith('/') ? path : `/${path}`;
    return `${base}${segment}`;
  }
}
