import {Component, OnInit} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth.header.component.html'
})
export class AuthHeaderComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public isAuth: boolean = false;
  public isAdmin: boolean = false;
  public login: string = "";

  constructor(private authService: AuthService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.isAuth = this.authService.isAuth;
    this.isAdmin = this.authService.isAdmin;
    this.login = this.authService.getUserLogin ?? "";
  }

  public logout(){
    this.authService.logout();
    window.location.href = "/";
  }
}
