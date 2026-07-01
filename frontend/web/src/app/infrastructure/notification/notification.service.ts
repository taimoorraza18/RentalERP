import { Injectable, inject } from '@angular/core';
import { MessageService } from 'primeng/api';

export interface INotificationOptions {
  life?: number;
  sticky?: boolean;
  closable?: boolean;
  key?: string;
  data?: unknown;
}

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private readonly messageService = inject(MessageService);
  private readonly recentMessages = new Map<string, number>();

  success(message: string, title?: string, options?: INotificationOptions): void {
    this.emit('success', message, title, { life: 4000, ...options });
  }

  error(message: string, title?: string, options?: INotificationOptions): void {
    this.emit('error', message, title, { life: 6000, sticky: false, ...options });
  }

  warning(message: string, title?: string, options?: INotificationOptions): void {
    this.emit('warn', message, title, { life: 4000, ...options });
  }

  info(message: string, title?: string, options?: INotificationOptions): void {
    this.emit('info', message, title, { life: 4000, ...options });
  }

  clear(): void {
    this.messageService.clear();
  }

  clearKey(key: string): void {
    this.messageService.clear(key);
  }

  private emit(
    severity: string,
    detail: string,
    summary?: string,
    options?: INotificationOptions
  ): void {
    if (this.isDuplicate(severity, detail)) return;

    this.messageService.add({
      severity,
      summary: summary ?? this.defaultTitle(severity),
      detail,
      life: options?.life,
      sticky: options?.sticky ?? false,
      closable: options?.closable ?? true,
      key: options?.key,
      data: options?.data,
    });
  }

  private isDuplicate(severity: string, message: string): boolean {
    const key = `${severity}:${message}`;
    const now = Date.now();
    const last = this.recentMessages.get(key);
    if (last !== undefined && now - last < 1000) {
      return true;
    }
    this.recentMessages.set(key, now);
    this.pruneRecentMessages();
    return false;
  }

  private pruneRecentMessages(): void {
    if (this.recentMessages.size <= 30) return;
    const cutoff = Date.now() - 5000;
    for (const [key, time] of this.recentMessages) {
      if (time < cutoff) this.recentMessages.delete(key);
    }
  }

  private defaultTitle(severity: string): string {
    const titles: Record<string, string> = {
      success: 'Success',
      error: 'Error',
      warn: 'Warning',
      info: 'Information',
    };
    return titles[severity] ?? '';
  }
}
