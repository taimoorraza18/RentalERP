import { inject } from '@angular/core';
import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { NotificationService } from '@infrastructure/notification';

export enum ErpErrorType {
  Validation = 'Validation',
  Business = 'Business',
  Authorization = 'Authorization',
  Network = 'Network',
  Server = 'Server',
  Unexpected = 'Unexpected',
}

export interface IValidationError {
  field: string;
  message: string;
}

export interface IErpError {
  type: ErpErrorType;
  message: string;
  validationErrors?: IValidationError[];
  statusCode?: number;
  original?: unknown;
}

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notification = inject(NotificationService);
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: unknown) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return throwError(() => error);
      }

      const erpError = mapToErpError(error);
      dispatchErrorFeedback(erpError, notification, router);
      return throwError(() => erpError);
    })
  );
};

function dispatchErrorFeedback(
  erpError: IErpError,
  notification: NotificationService,
  router: Router
): void {
  switch (erpError.type) {
    case ErpErrorType.Authorization:
      if (erpError.statusCode === 403) {
        router.navigate(['/unauthorized']);
      }
      break;
    case ErpErrorType.Network:
      notification.error(
        'Network error. Please check your connection and try again.',
        'Connection Error'
      );
      break;
    case ErpErrorType.Server:
      notification.error('A server error occurred. Please try again later.', 'Server Error');
      break;
    case ErpErrorType.Business:
      notification.error(erpError.message, 'Error');
      break;
    case ErpErrorType.Validation:
      notification.warning(
        'Please correct the validation errors and try again.',
        'Validation Error'
      );
      break;
    default:
      notification.error('An unexpected error occurred.', 'Error');
  }
}

function mapToErpError(error: unknown): IErpError {
  if (!(error instanceof HttpErrorResponse)) {
    return {
      type: ErpErrorType.Unexpected,
      message: 'An unexpected error occurred.',
      original: error,
    };
  }

  if (error.status === 0) {
    return {
      type: ErpErrorType.Network,
      message: 'Network error. Unable to reach the server.',
      statusCode: 0,
      original: error,
    };
  }

  if (error.status === 403) {
    return {
      type: ErpErrorType.Authorization,
      message: 'You do not have permission to access this resource.',
      statusCode: 403,
      original: error,
    };
  }

  if (error.status === 422) {
    return {
      type: ErpErrorType.Validation,
      message: extractMessage(error.error, 'Validation failed.'),
      validationErrors: extractValidationErrors(error.error),
      statusCode: 422,
      original: error,
    };
  }

  if (error.status >= 400 && error.status < 500) {
    return {
      type: ErpErrorType.Business,
      message: extractMessage(error.error, 'The request could not be completed.'),
      statusCode: error.status,
      original: error,
    };
  }

  if (error.status >= 500) {
    return {
      type: ErpErrorType.Server,
      message: 'A server error occurred.',
      statusCode: error.status,
      original: error,
    };
  }

  return {
    type: ErpErrorType.Unexpected,
    message: 'An unexpected error occurred.',
    statusCode: error.status,
    original: error,
  };
}

function extractMessage(body: unknown, fallback: string): string {
  if (body && typeof body === 'object' && 'message' in body) {
    return String((body as Record<string, unknown>)['message']) || fallback;
  }
  return fallback;
}

function extractValidationErrors(body: unknown): IValidationError[] {
  if (!body || typeof body !== 'object') {
    return [];
  }
  const b = body as Record<string, unknown>;

  if (Array.isArray(b['errors'])) {
    return (b['errors'] as Record<string, string>[]).map(e => ({
      field: String(e['field'] ?? ''),
      message: String(e['message'] ?? ''),
    }));
  }

  if (b['errors'] && typeof b['errors'] === 'object' && !Array.isArray(b['errors'])) {
    return Object.entries(b['errors'] as Record<string, string[]>).flatMap(([field, messages]) =>
      (Array.isArray(messages) ? messages : [String(messages)]).map(message => ({
        field,
        message,
      }))
    );
  }

  return [];
}
