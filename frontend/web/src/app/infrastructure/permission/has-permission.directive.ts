import { Directive, Input, TemplateRef, ViewContainerRef, inject } from '@angular/core';
import { PermissionService } from './permission.service';

@Directive({
  selector: '[hasPermission]',
  standalone: true,
})
export class HasPermissionDirective {
  private readonly templateRef = inject(TemplateRef<unknown>);
  private readonly viewContainer = inject(ViewContainerRef);
  private readonly permissionService = inject(PermissionService);
  private hasView = false;

  @Input() set hasPermission(value: string | string[]) {
    const permitted = Array.isArray(value)
      ? this.permissionService.hasAnyPermission(value)
      : this.permissionService.hasPermission(value);

    if (permitted && !this.hasView) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    } else if (!permitted && this.hasView) {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }
}
