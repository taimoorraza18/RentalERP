import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable({ providedIn: 'root' })
export class AppConfigService {
  readonly apiBaseUrl: string = environment.apiBaseUrl;
  readonly appName: string = environment.appName;
  readonly appVersion: string = environment.appVersion;
  readonly defaultLanguage: string = environment.defaultLanguage;
  readonly defaultTheme: string = environment.defaultTheme;
  readonly production: boolean = environment.production;
}
