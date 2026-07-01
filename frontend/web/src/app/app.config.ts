import { APP_INITIALIZER, ApplicationConfig, inject, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideTranslateService } from '@ngx-translate/core';
import { provideTranslateHttpLoader } from '@ngx-translate/http-loader';
import { providePrimeNG } from 'primeng/config';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import Aura from '@primeuix/themes/aura';
import { routes } from './app.routes';
import { authInterceptor, loadingInterceptor, errorInterceptor } from '@core/interceptors';
import { appInitializerFactory } from '@core/config';
import { ThemeService } from '@infrastructure/theme';

function themeInitializerFactory(): () => Promise<void> {
  const theme = inject(ThemeService);
  return () =>
    new Promise<void>(resolve => {
      theme.initialize();
      resolve();
    });
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([authInterceptor, loadingInterceptor, errorInterceptor])),
    provideAnimationsAsync(),
    provideRouter(routes),
    provideTranslateService({
      lang: 'en',
      fallbackLang: 'en',
    }),
    provideTranslateHttpLoader({
      prefix: './assets/i18n/',
      suffix: '.json',
    }),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: '.erp-dark',
        },
      },
    }),
    MessageService,
    ConfirmationService,
    DialogService,
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFactory,
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: themeInitializerFactory,
      multi: true,
    },
  ],
};
