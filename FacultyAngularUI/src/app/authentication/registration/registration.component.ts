import {Component, OnInit} from "@angular/core";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {AuthUserDto} from "../models/auth.user.dto";

@Component({
  selector: 'app-register',
  templateUrl: './registration.component.html'
})
export class RegistrationComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private authService: AuthService,
              private router: Router,
              private formBuilder: FormBuilder){
  }

  ngOnInit() : void {
    this.form = this.formBuilder.group({
      login: new FormControl('',
        [ Validators.required, Validators.maxLength(30), Validators.minLength(10) ]),
      password: new FormControl('',
        [ Validators.required, Validators.maxLength(50), Validators.minLength(8) ]),
      confirmPassword: new FormControl('', [ Validators.required ])
    },
      {
        validators: this.MustMatch('password', 'confirmPassword')
      });
  }

  submit() : void {
    let authUser: AuthUserDto = new AuthUserDto(this.form.value.login, this.form.value.password);
    this.authService.register(authUser);
    this.router.navigate(['/login'])
  }

  private MustMatch(targetControlName: string, matchingControlName: string) {
    return (formGroup : FormGroup) => {
      let targetControl = formGroup.controls[targetControlName];
      let matchingControl = formGroup.controls[matchingControlName];
      if (matchingControl.errors && !matchingControl.errors['MustMatch']) {
        return;
      }

      if (matchingControl.value !== targetControl.value) {
        matchingControl.setErrors({MustMatch:true});
      }
      else {
        matchingControl.setErrors(null);
      }
    };
  }
}
