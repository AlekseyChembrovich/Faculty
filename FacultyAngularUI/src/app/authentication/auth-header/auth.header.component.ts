import {Component} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth.header.component.html'
})
export class AuthHeaderComponent {
  public form: FormGroup = new FormGroup({ });
  public isAuth: boolean = false;
  public isAdmin: boolean = false;
  public login: string = "";

  constructor(private authService: AuthService,
              private router: Router,
              private translate: TranslateService)
  {
    this.isAuth = this.authService.isAuthenticated;
    this.isAdmin = this.authService.isAdmin;
    this.login = this.authService.getUserLogin ?? "";
  }

  public logout(){
    this.authService.logout();
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['/faculty/index']);
  }

  onChangeLanguage(option: any): void {
    let lang: string = option.value;
    this.translate.setDefaultLang(lang);
  }
}
