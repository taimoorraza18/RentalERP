import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { finalize } from 'rxjs';
import { LoadingService } from '@infrastructure/loading';

const SKIP_LOADING_HEADER = 'X-Skip-Loading';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.headers.has(SKIP_LOADING_HEADER)) {
    return next(req.clone({ headers: req.headers.delete(SKIP_LOADING_HEADER) }));
  }

  const loading = inject(LoadingService);
  loading.show(req.url);

  return next(req).pipe(finalize(() => loading.hide(req.url)));
};
