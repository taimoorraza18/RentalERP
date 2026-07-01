import { Component, inject, signal, viewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Menu } from 'primeng/menu';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { AuthService } from '@core/auth';
import { ThemeService } from '@infrastructure/theme';
import { SidebarStateService } from '../sidebar/sidebar-state.service';
import { BreadcrumbComponent } from '../breadcrumb/breadcrumb.component';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [MenuModule, BreadcrumbComponent],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.scss',
})
export class TopbarComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);
  readonly theme = inject(ThemeService);
  readonly sidebar = inject(SidebarStateService);

  readonly currentUser = this.auth.currentUser;
  readonly isDarkMode = this.theme.isDarkMode;
  readonly notificationCount = signal<number>(0);

  readonly userMenuRef = viewChild<Menu>('userMenuRef');

  readonly userMenuItems: MenuItem[] = [
    {
      label: 'My Profile',
      icon: 'pi pi-user',
      command: () => this.router.navigate(['/profile']),
    },
    {
      label: 'Settings',
      icon: 'pi pi-cog',
      command: () => this.router.navigate(['/settings']),
    },
    { separator: true },
    {
      label: 'Sign Out',
      icon: 'pi pi-sign-out',
      command: () => this.signOut(),
    },
  ];

  toggleSidebar(): void {
    this.sidebar.toggle();
  }

  toggleMobileSidebar(): void {
    this.sidebar.toggleMobile();
  }

  toggleTheme(): void {
    this.theme.toggleDarkMode();
  }

  openUserMenu(event: MouseEvent): void {
    this.userMenuRef()?.toggle(event);
  }

  private signOut(): void {
    this.auth.logout();
    this.router.navigate(['/auth/login']);
  }
}
