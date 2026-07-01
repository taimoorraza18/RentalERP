import { inject } from '@angular/core';
import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
  HttpErrorResponse,
} from '@angular/common/http';
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '@core/auth';

let isRefreshing = false;
const refreshToken$ = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (isPublicEndpoint(req.url)) {
    return next(req);
  }

  const token = auth.getAccessToken();
  const authorizedReq = token ? attachToken(req, token) : req;

  return next(authorizedReq).pipe(
    catchError((error: unknown) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return handleRefresh(req, next, auth, router);
      }
      return throwError(() => error);
    })
  );
};

function attachToken(req: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
  return req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
}

function isPublicEndpoint(url: string): boolean {
  return url.includes('/auth/login') || url.includes('/auth/refresh');
}

function handleRefresh(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
  auth: AuthService,
  router: Router
) {
  if (!isRefreshing) {
    isRefreshing = true;
    refreshToken$.next(null);

    return auth.refreshToken().pipe(
      switchMap(response => {
        isRefreshing = false;
        refreshToken$.next(response.accessToken);
        return next(attachToken(req, response.accessToken));
      }),
      catchError(err => {
        isRefreshing = false;
        auth.logout();
        router.navigate(['/auth/login']);
        return throwError(() => err);
      })
    );
  }

  return refreshToken$.pipe(
    filter((token): token is string => token !== null),
    take(1),
    switchMap(token => next(attachToken(req, token)))
  );
}
