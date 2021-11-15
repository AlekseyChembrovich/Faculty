import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
  RouterStateSnapshot,
  UrlTree
} from "@angular/router";
import {Observable} from "rxjs";
import {AuthService} from "../authentication/services/auth.service";
import {Injectable} from "@angular/core";
import {AuthGuard} from "./auth.guard";

@Injectable()
export class AdminGuard implements CanActivate, CanActivateChild {
  constructor(private authService: AuthService,
              private router: Router,
              private authGuard: AuthGuard) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):
    Observable<boolean | UrlTree> |
    Promise<boolean | UrlTree> |
    boolean |
    UrlTree

  {
    if(!this.authGuard.canActivate(route, state)) {
      this.router.navigate(['/login']);
      return false;
    }

    if (!this.authService.isAdmin) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):
    Observable<boolean | UrlTree> |
    Promise<boolean | UrlTree> |
    boolean |
    UrlTree

  {
    return this.canActivate(childRoute, state);
  }
}
