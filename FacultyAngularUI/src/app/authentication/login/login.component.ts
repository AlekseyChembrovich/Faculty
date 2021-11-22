import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {AuthUserModel} from "../models/auth.user.model";
import {Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {Subject} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public error$: Subject<string> = new Subject<string>();

  constructor(public authService: AuthService,
              private router: Router){
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      login: new FormControl('',
        [ Validators.required, Validators.maxLength(30), Validators.minLength(10) ]),
      password: new FormControl('',
        [ Validators.required, Validators.maxLength(50), Validators.minLength(8) ])
    });
  }

  submit() : void {
    let loginUser: AuthUserModel = new AuthUserModel(this.form.value.login, this.form.value.password);
    this.authService.login(loginUser).subscribe(response => {
        console.log("Response", response);
      },
      error => {
        console.log("Error", error)
        if (error instanceof HttpErrorResponse)  {
          if (error.status == 400){
            this.error$.next('ServerError.400');
          }
        }
      },
      () => {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate(['/faculty/index']);
      });
  }
}
