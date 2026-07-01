import { Component, computed, inject } from '@angular/core';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { NavigationService } from '@core/navigation';

@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [BreadcrumbModule],
  template: `
    <p-breadcrumb [model]="breadcrumbItems()" [home]="homeItem" />
  `,
  styleUrl: './breadcrumb.component.scss',
})
export class BreadcrumbComponent {
  private readonly navService = inject(NavigationService);

  readonly homeItem: MenuItem = { icon: 'pi pi-home', routerLink: '/dashboard' };

  readonly breadcrumbItems = computed<MenuItem[]>(() =>
    this.navService.breadcrumbs().map(b => ({
      label: b.label,
      routerLink: b.url,
      icon: b.icon,
    }))
  );
}
