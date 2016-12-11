import { Injectable } from '@angular/core';

import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService,
    private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    if (this.auth.isLoggedIn()) {
      return true;
    }

    this.router.navigate(['login']);
  }
}

@Injectable()
export class AdminGuard implements CanActivate {
  constructor(private auth: AuthService,
    private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.auth.isAdmin()) {
      alert('To access this page you need to be admin');
      return false;
    }

    return true;
  }


}