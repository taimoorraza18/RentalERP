import { Routes } from '@angular/router';
import { authGuard } from '@core/guards';
import { ShellComponent } from './layout/shell/shell.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: ShellComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () =>
          import(
            './features/foundation/dashboard/pages/dashboard.page'
          ).then(m => m.DashboardPage),
      },
      {
        path: 'unauthorized',
        loadComponent: () =>
          import('./features/unauthorized/unauthorized.page').then(
            m => m.UnauthorizedPage
          ),
      },
    ],
  },
  {
    path: 'auth/login',
    loadComponent: () =>
      import('./core/auth/pages/login.page').then(m => m.LoginPage),
  },
  {
    path: '**',
    redirectTo: 'dashboard',
  },
];
