import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TooltipModule } from 'primeng/tooltip';
import { SidebarStateService } from './sidebar-state.service';

export interface IMenuItem {
  id: string;
  label: string;
  icon: string;
  route: string;
  badge?: number | string;
}

export interface IMenuGroup {
  id: string;
  label: string;
  items: IMenuItem[];
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, TooltipModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
})
export class SidebarComponent {
  readonly state = inject(SidebarStateService);

  readonly menuGroups: IMenuGroup[] = [
    {
      id: 'main',
      label: 'Main',
      items: [
        { id: 'dashboard', label: 'Dashboard', icon: 'pi pi-home', route: '/dashboard' },
      ],
    },
    {
      id: 'rental',
      label: 'Rental',
      items: [
        { id: 'rental-orders', label: 'Rental Orders', icon: 'pi pi-file-edit', route: '/rental/orders' },
        { id: 'fleet',         label: 'Fleet',         icon: 'pi pi-car',       route: '/rental/fleet' },
        { id: 'maintenance',   label: 'Maintenance',   icon: 'pi pi-wrench',    route: '/rental/maintenance' },
      ],
    },
    {
      id: 'operations',
      label: 'Operations',
      items: [
        { id: 'customers', label: 'Customers', icon: 'pi pi-users',    route: '/customers' },
        { id: 'vendors',   label: 'Vendors',   icon: 'pi pi-building', route: '/vendors' },
        { id: 'warehouse', label: 'Warehouse', icon: 'pi pi-box',      route: '/warehouse' },
      ],
    },
    {
      id: 'finance',
      label: 'Finance',
      items: [
        { id: 'sales',      label: 'Sales',      icon: 'pi pi-shopping-cart', route: '/sales' },
        { id: 'purchasing', label: 'Purchasing', icon: 'pi pi-cart-plus',     route: '/purchasing' },
        { id: 'accounting', label: 'Accounting', icon: 'pi pi-calculator',    route: '/accounting' },
      ],
    },
    {
      id: 'admin',
      label: 'Admin',
      items: [
        { id: 'reports',  label: 'Reports',  icon: 'pi pi-chart-bar', route: '/reports' },
        { id: 'settings', label: 'Settings', icon: 'pi pi-cog',       route: '/settings' },
      ],
    },
  ];
}
