import {AbstractControl, FormArray, ValidationErrors, ValidatorFn} from "@angular/forms";

export const HasRoleValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const roles: FormArray = control.get('roles') as FormArray;
  if (roles.length <= 0) {
    roles.setErrors({"should has one and more roles": true});
    return { hasRoleValidator: true }
  }

  roles.setErrors(null);
  return null;
};
