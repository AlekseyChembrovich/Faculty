import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {AuthUserDto} from "../models/auth.user.dto";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private authService: AuthService){
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
    let loginUser: AuthUserDto = new AuthUserDto(this.form.value.login, this.form.value.password);
    this.authService.login(loginUser);
    window.location.href="/";
  }
}
