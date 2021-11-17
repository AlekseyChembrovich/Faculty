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
              private authGuard: AuthGuard)
  {
    console.log("constructor");
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
      console.log("Guard False 1");
      return false;
    }

    if (!this.authService.isAdmin) {
      this.router.navigate(['/login']);
      console.log("Guard False 2");
      return false;
    }
    console.log("Guard True");
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
    console.log("canActivateChild");
    return this.canActivate(childRoute, state);
  }
}
