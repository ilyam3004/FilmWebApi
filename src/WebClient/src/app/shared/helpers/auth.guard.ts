import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import {AccountService} from "../../core/services/account.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(
    private route: Router,
    private accountService: AccountService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = this.accountService.userValue;
    if (user) {
      return true;
    }

    this.route.navigate(['/account/login', { queryParams: {returnUrl: state.url}}]);
    return false;
  }
}
