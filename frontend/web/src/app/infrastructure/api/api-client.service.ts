import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ConfigurationService } from '@infrastructure/config';
import { LoggerService } from '@infrastructure/logger';

export interface IRequestOptions {
  skipLoading?: boolean;
  skipErrorHandling?: boolean;
  responseType?: 'json' | 'blob' | 'text';
  headers?: Record<string, string>;
}

const SKIP_LOADING_HEADER = 'X-Skip-Loading';

@Injectable({ providedIn: 'root' })
export class ApiClientService {
  private readonly http = inject(HttpClient);
  private readonly config = inject(ConfigurationService);
  private readonly logger = inject(LoggerService);

  get<T>(
    path: string,
    params?: HttpParams | Record<string, unknown>,
    options?: IRequestOptions
  ): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`GET ${url}`);
    return this.http
      .get<T>(url, {
        params: this.toHttpParams(params),
        headers: this.buildHeaders(options),
        responseType: (options?.responseType ?? 'json') as 'json',
      })
      .pipe(tap({ error: err => this.logger.error(`GET ${url} failed`, err) }));
  }

  post<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`POST ${url}`);
    return this.http
      .post<T>(url, body, {
        headers: this.buildHeaders(options),
        responseType: (options?.responseType ?? 'json') as 'json',
      })
      .pipe(tap({ error: err => this.logger.error(`POST ${url} failed`, err) }));
  }

  put<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`PUT ${url}`);
    return this.http
      .put<T>(url, body, {
        headers: this.buildHeaders(options),
        responseType: (options?.responseType ?? 'json') as 'json',
      })
      .pipe(tap({ error: err => this.logger.error(`PUT ${url} failed`, err) }));
  }

  patch<T>(path: string, body: unknown, options?: IRequestOptions): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`PATCH ${url}`);
    return this.http
      .patch<T>(url, body, {
        headers: this.buildHeaders(options),
        responseType: (options?.responseType ?? 'json') as 'json',
      })
      .pipe(tap({ error: err => this.logger.error(`PATCH ${url} failed`, err) }));
  }

  delete<T>(path: string, options?: IRequestOptions): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`DELETE ${url}`);
    return this.http
      .delete<T>(url, {
        headers: this.buildHeaders(options),
        responseType: (options?.responseType ?? 'json') as 'json',
      })
      .pipe(tap({ error: err => this.logger.error(`DELETE ${url} failed`, err) }));
  }

  postFormData<T>(path: string, formData: FormData, options?: IRequestOptions): Observable<T> {
    const url = this.config.getApiUrl(path);
    this.logger.debug(`POST (form-data) ${url}`);
    const headers = this.buildHeaders(options);
    return this.http
      .post<T>(url, formData, { headers })
      .pipe(tap({ error: err => this.logger.error(`POST form-data ${url} failed`, err) }));
  }

  private buildHeaders(options?: IRequestOptions): HttpHeaders {
    let headers = new HttpHeaders(options?.headers ?? {});
    if (options?.skipLoading) {
      headers = headers.set(SKIP_LOADING_HEADER, 'true');
    }
    return headers;
  }

  private toHttpParams(
    params?: HttpParams | Record<string, unknown>
  ): HttpParams | undefined {
    if (!params) return undefined;
    if (params instanceof HttpParams) return params;

    return Object.entries(params).reduce((p, [key, value]) => {
      if (value === null || value === undefined) return p;
      return p.set(key, String(value));
    }, new HttpParams());
  }
}
