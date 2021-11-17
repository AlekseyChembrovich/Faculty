import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../services/user.service";
import {UserEditPasswordDto} from "../models/user.edit.password.dto";

@Component({
  selector: 'app-user-update-password',
  templateUrl: './user.update.password.component.html'
})
export class UserUpdatePasswordComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public idUser: string = '';

  constructor(private userService: UserService,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit() : void {
    this.activatedRoute.params.subscribe(params => {
      this.idUser = params['id'];
      console.log(params['id']);
    });

    this.form = new FormGroup({
      newPassword: new FormControl('',
        [ Validators.required, Validators.maxLength(50), Validators.minLength(8) ])
    });
  }

  submit() : void {
    let user: UserEditPasswordDto = new UserEditPasswordDto(this.form.value.newPassword, this.idUser);
    this.userService.updatePasswordUser(user).subscribe(response => {
      console.log("Response", response);
      this.router.navigateByUrl('/user/index');
    });
  }
}
