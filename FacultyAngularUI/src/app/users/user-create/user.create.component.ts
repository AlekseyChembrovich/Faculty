import {Component, OnInit} from "@angular/core";
import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";
import {Router} from "@angular/router";
import {UserAddModel} from "../models/user.add.model";
import {UserService} from "../services/user.service";
import {HasRoleValidator} from "../../shared/validations/has.role.validation";
import {HttpErrorResponse} from "@angular/common/http";
import {Subject} from "rxjs";

@Component({
  selector: 'app-user-create',
  templateUrl: './user.create.component.html'
})
export class UserCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public nowDate: Date = new Date();
  public roles: Array<string> = [];
  public error$: Subject<string> = new Subject<string>();

  constructor(private userService: UserService,
              private router: Router) {
  }

  ngOnInit() : void {
    this.userService.getRoles().subscribe(response => {
      this.roles = response;
    });

    this.form = new FormGroup({
      login: new FormControl('',
        [ Validators.required, Validators.maxLength(30), Validators.minLength(10) ]),
      password: new FormControl('',
        [ Validators.required, Validators.maxLength(50), Validators.minLength(8) ]),
      roles: new FormArray([]),
      birthday: new FormControl('',
        [ Validators.required ])
    }, { validators: HasRoleValidator });
  }

  submit() : void {
    let formArray: FormArray =  this.form.get('roles') as FormArray;
    let namesRole: Array<string> = [];
    formArray.controls.forEach(x => namesRole.push(x.value));
    let user: UserAddModel = new UserAddModel(this.form.value.login,
      namesRole, this.form.value.birthday, this.form.value.password);
    this.userService.createUser(user).subscribe(response => {
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
        this.router.navigateByUrl('/user/index');
      });
  }

  onCheckChange(event: any) {
    const formArray: FormArray = this.form.get('roles') as FormArray;
    if(event.target.checked){
      formArray.push(new FormControl(event.target.value));
    }
    else {
      let i: number = 0;
      formArray.controls.forEach((ctrl: AbstractControl) => {
        if(ctrl.value == event.target.value) {
          formArray.removeAt(i);
          return;
        }
        i++;
      });
    }
    console.log(formArray);
  }
}
