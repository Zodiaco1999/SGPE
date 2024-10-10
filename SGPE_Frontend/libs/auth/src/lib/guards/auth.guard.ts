import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '@sgpe-ws/services';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const url: string = state.url;
  const router = inject(Router);
  const authService = inject(AuthService);
  const isLoggedIn = authService.isLoggedIn();
  //const hasExpiredToken = authService.expirationToken();
  console.log(url);

  if (url === '/auth') {
    authService.removeTokens();
    return true;
  }
  else if (url === "/" && !isLoggedIn) {
    router.navigateByUrl('/auth');
  }

  return authService.autorizaUrl(url) ? true : router.parseUrl('/noautorizado');
};
