import { Component, OnInit, inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { NavigationService } from '@core/navigation';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [CardModule],
  template: `
    <div class="dashboard animate-fade-in">
      <div class="dashboard__header">
        <h1>Dashboard</h1>
        <p>Welcome to RentalERP</p>
      </div>

      <div class="dashboard__grid">
        <p-card header="Active Rentals" subheader="Today">
          <p class="stat">0</p>
        </p-card>
        <p-card header="Fleet Available" subheader="Assets ready to rent">
          <p class="stat">0</p>
        </p-card>
        <p-card header="Monthly Revenue" subheader="This month">
          <p class="stat">$0.00</p>
        </p-card>
        <p-card header="Pending Maintenance" subheader="Open tasks">
          <p class="stat">0</p>
        </p-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard__header {
      margin-bottom: var(--space-6);

      h1 { margin-bottom: var(--space-1); }
      p { color: var(--color-text-secondary); }
    }

    .dashboard__grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
      gap: var(--space-4);
    }

    .stat {
      font-size: var(--font-size-2xl);
      font-weight: var(--font-weight-bold);
      color: var(--color-text-primary);
      margin: var(--space-2) 0 0;
    }
  `],
})
export class DashboardPage implements OnInit {
  private readonly navService = inject(NavigationService);

  ngOnInit(): void {
    this.navService.setBreadcrumbs([{ label: 'Dashboard' }]);
  }
}
