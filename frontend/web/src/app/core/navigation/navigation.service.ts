import { Injectable, inject, signal } from '@angular/core';
import { NavigationEnd, NavigationExtras, Router } from '@angular/router';
import { Location } from '@angular/common';
import { filter } from 'rxjs';

export interface IBreadcrumb {
  label: string;
  url?: string;
  icon?: string;
}

@Injectable({ providedIn: 'root' })
export class NavigationService {
  private readonly router = inject(Router);
  private readonly location = inject(Location);

  readonly currentUrl = signal<string>('');
  readonly previousUrl = signal<string>('');
  readonly breadcrumbs = signal<IBreadcrumb[]>([]);

  constructor() {
    this.router.events
      .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
      .subscribe(event => {
        this.previousUrl.set(this.currentUrl());
        this.currentUrl.set(event.urlAfterRedirects);
      });
  }

  navigateTo(url: string, extras?: NavigationExtras): void {
    this.router.navigate([url], extras);
  }

  navigateBack(): void {
    this.location.back();
  }

  navigateToLogin(): void {
    this.router.navigate(['/auth/login']);
  }

  navigateToUnauthorized(): void {
    this.router.navigate(['/unauthorized']);
  }

  setBreadcrumbs(breadcrumbs: IBreadcrumb[]): void {
    this.breadcrumbs.set(breadcrumbs);
  }
}
