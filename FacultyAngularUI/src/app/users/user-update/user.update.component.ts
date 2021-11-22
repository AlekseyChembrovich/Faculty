import {Component, OnInit} from "@angular/core";
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../services/user.service";
import {UserModel} from "../models/user.model";
import {HasRoleValidator} from "../../shared/validations/has.role.validation";
import {Subject} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-user-update',
  templateUrl: './user.update.component.html'
})
export class UserUpdateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public roles: Array<string> = [];
  public user: UserModel | undefined;
  public error$: Subject<string> = new Subject<string>();

  constructor(private userService: UserService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private formBuilder: FormBuilder) {
  }

  ngOnInit() : void {
    let id = '';
    this.activatedRoute.params.subscribe(params => {
      id = params['id'];
    });

    this.userService.getRoles().subscribe(response => {
      this.roles = response;
    });

    this.form = new FormGroup({
      login: new FormControl('',
        [ Validators.required, Validators.maxLength(30), Validators.minLength(10) ]),
      roles: new FormArray([]),
      birthday: new FormControl('',
        [ Validators.required ])
    },
      {
        validators: HasRoleValidator
      });

    const formArray: FormArray = this.form.get('roles') as FormArray;
    this.userService.getUser(id).subscribe(response => {
      this.user = response;
      this.user?.roles.forEach(x => { formArray.push(new FormControl(x)); });
      this.form.patchValue({
        login: this.user.login,
        birthday: this.user.birthday
      });
    });
  }

  submit() : void {
    let formArray: FormArray =  this.form.get('roles') as FormArray;
    let namesRole: Array<string> = [];
    formArray.controls.forEach(x => namesRole.push(x.value));
    let user: UserModel = new UserModel(this.form.value.login,
      namesRole, this.form.value.birthday, this.user?.id);
    this.userService.updateUser(user).subscribe(response => {
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

  private MustHasValue(targetControlName: string) {
    return (formGroup : FormGroup) => {
      const formArray: FormArray = this.form.get('roles') as FormArray;
      console.log("formArray", formArray);
      if (formArray?.errors && !formArray.errors['MustHasValue']) {
        return;
      }

      if (formArray.controls.length <= 0) {
        formArray.setErrors({ MustHasValue:true });
      }
      else {
        formArray.setErrors(null);
      }
    };
  }
}
