import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NavigationService } from '@core/navigation';

@Component({
  selector: 'app-unauthorized-page',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="unauthorized">
      <div class="unauthorized__card animate-scale-in">
        <div class="unauthorized__icon">
          <i class="pi pi-lock"></i>
        </div>
        <h1>403</h1>
        <h2>Access Denied</h2>
        <p>You don't have permission to access this page.</p>
        <div class="unauthorized__actions">
          <a routerLink="/dashboard" class="unauthorized__btn">
            <i class="pi pi-home"></i>
            Back to Dashboard
          </a>
          <button type="button" class="unauthorized__btn unauthorized__btn--secondary" (click)="goBack()">
            <i class="pi pi-arrow-left"></i>
            Go Back
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .unauthorized {
      min-height: 100vh;
      display: flex;
      align-items: center;
      justify-content: center;
      background: var(--color-background);
      padding: var(--space-4);
    }

    .unauthorized__card {
      text-align: center;
      max-width: 420px;
      width: 100%;
      background: var(--color-surface);
      border: 1px solid var(--color-border);
      border-radius: var(--radius-xl);
      padding: var(--space-10);
      box-shadow: var(--shadow-md);
    }

    .unauthorized__icon {
      width: 80px;
      height: 80px;
      border-radius: 50%;
      background: var(--color-danger-light);
      display: flex;
      align-items: center;
      justify-content: center;
      margin: 0 auto var(--space-4);

      .pi {
        font-size: 2rem;
        color: var(--color-danger);
      }
    }

    h1 {
      font-size: 3.5rem;
      font-weight: var(--font-weight-bold);
      color: var(--color-danger);
      line-height: 1;
      margin-bottom: var(--space-2);
    }

    h2 {
      font-size: var(--font-size-xl);
      color: var(--color-text-primary);
      margin-bottom: var(--space-3);
    }

    p {
      color: var(--color-text-secondary);
      margin-bottom: var(--space-6);
    }

    .unauthorized__actions {
      display: flex;
      flex-direction: column;
      gap: var(--space-3);
      align-items: center;
    }

    .unauthorized__btn {
      display: inline-flex;
      align-items: center;
      gap: var(--space-2);
      background: var(--color-primary);
      color: white;
      padding: var(--space-3) var(--space-6);
      border-radius: var(--radius-md);
      font-size: var(--font-size-sm);
      font-weight: var(--font-weight-medium);
      text-decoration: none;
      border: none;
      cursor: pointer;
      transition: background var(--duration-fast) var(--easing-default);
      width: 200px;
      justify-content: center;

      &:hover {
        background: var(--color-primary-hover);
        text-decoration: none;
        color: white;
      }

      &--secondary {
        background: var(--color-surface-secondary);
        color: var(--color-text-primary);

        &:hover {
          background: var(--color-surface-tertiary);
        }
      }
    }
  `],
})
export class UnauthorizedPage {
  private readonly navService = inject(NavigationService);

  goBack(): void {
    this.navService.navigateBack();
  }
}
