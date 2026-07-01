import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

export enum ErrorType {
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

export interface IAppError {
  type: ErrorType;
  message: string;
  title?: string;
  statusCode?: number;
  validationErrors?: IValidationError[];
  originalError?: unknown;
}

@Injectable({ providedIn: 'root' })
export class ErrorService {
  handle(error: HttpErrorResponse): IAppError {
    return this.mapHttpError(error);
  }

  mapHttpError(error: HttpErrorResponse): IAppError {
    if (error.status === 0) {
      return {
        type: ErrorType.Network,
        message: 'Network error. Unable to reach the server.',
        title: 'Connection Error',
        statusCode: 0,
        originalError: error,
      };
    }

    if (error.status === 400) {
      const validationErrors = this.getValidationErrors(error);
      return {
        type: ErrorType.Validation,
        message: this.extractMessage(error.error, 'The request is invalid.'),
        title: 'Validation Error',
        statusCode: 400,
        validationErrors,
        originalError: error,
      };
    }

    if (error.status === 401) {
      return {
        type: ErrorType.Authorization,
        message: 'Your session has expired. Please log in again.',
        title: 'Session Expired',
        statusCode: 401,
        originalError: error,
      };
    }

    if (error.status === 403) {
      return {
        type: ErrorType.Authorization,
        message: 'You do not have permission to access this resource.',
        title: 'Access Denied',
        statusCode: 403,
        originalError: error,
      };
    }

    if (error.status === 422) {
      const validationErrors = this.getValidationErrors(error);
      return {
        type: ErrorType.Validation,
        message: this.extractMessage(error.error, 'Validation failed.'),
        title: 'Validation Error',
        statusCode: 422,
        validationErrors,
        originalError: error,
      };
    }

    if (error.status >= 500) {
      return {
        type: ErrorType.Server,
        message: 'A server error occurred. Please try again later.',
        title: 'Server Error',
        statusCode: error.status,
        originalError: error,
      };
    }

    if (error.status >= 400 && error.status < 500) {
      return {
        type: ErrorType.Business,
        message: this.extractMessage(error.error, 'The request could not be completed.'),
        title: 'Error',
        statusCode: error.status,
        originalError: error,
      };
    }

    return {
      type: ErrorType.Unexpected,
      message: 'An unexpected error occurred.',
      title: 'Error',
      statusCode: error.status,
      originalError: error,
    };
  }

  getValidationErrors(error: HttpErrorResponse): IValidationError[] {
    const body = error.error;
    if (!body || typeof body !== 'object') return [];

    const b = body as Record<string, unknown>;

    if (Array.isArray(b['errors'])) {
      return (b['errors'] as Record<string, string>[]).map(e => ({
        field: String(e['field'] ?? ''),
        message: String(e['message'] ?? ''),
      }));
    }

    if (b['errors'] && typeof b['errors'] === 'object' && !Array.isArray(b['errors'])) {
      return Object.entries(b['errors'] as Record<string, string | string[]>).flatMap(
        ([field, messages]) =>
          (Array.isArray(messages) ? messages : [String(messages)]).map(message => ({
            field,
            message,
          }))
      );
    }

    return [];
  }

  formatValidationMessage(errors: IValidationError[]): string {
    return errors.map(e => (e.field ? `${e.field}: ${e.message}` : e.message)).join('\n');
  }

  isValidationError(error: IAppError): boolean {
    return error.type === ErrorType.Validation;
  }

  isAuthorizationError(error: IAppError): boolean {
    return error.type === ErrorType.Authorization;
  }

  isNetworkError(error: IAppError): boolean {
    return error.type === ErrorType.Network;
  }

  private extractMessage(body: unknown, fallback: string): string {
    if (body && typeof body === 'object' && 'message' in body) {
      const msg = (body as Record<string, unknown>)['message'];
      if (typeof msg === 'string' && msg.trim()) return msg;
    }
    return fallback;
  }
}
