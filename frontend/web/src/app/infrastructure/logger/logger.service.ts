import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

const PREFIX = '[RentalERP]';

@Injectable({ providedIn: 'root' })
export class LoggerService {
  private timestamp(): string {
    return new Date().toISOString();
  }

  debug(message: string, ...args: unknown[]): void {
    if (environment.production) return;
    console.debug(`${PREFIX} [DEBUG] [${this.timestamp()}]`, message, ...args);
  }

  info(message: string, ...args: unknown[]): void {
    if (environment.production) return;
    console.info(`${PREFIX} [INFO] [${this.timestamp()}]`, message, ...args);
  }

  warn(message: string, ...args: unknown[]): void {
    console.warn(`${PREFIX} [WARN] [${this.timestamp()}]`, message, ...args);
  }

  error(message: string, error?: unknown, ...args: unknown[]): void {
    console.error(`${PREFIX} [ERROR] [${this.timestamp()}]`, message, ...args);
    if (error instanceof Error && error.stack) {
      console.error(error.stack);
    } else if (error !== undefined) {
      console.error(error);
    }
  }
}
