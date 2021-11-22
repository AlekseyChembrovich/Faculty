import {AbstractControl, FormArray, ValidationErrors, ValidatorFn} from "@angular/forms";

export const HasRoleValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const roles: FormArray = control.get('roles') as FormArray;
  return roles.length <= 0 ? { hasRoleValidator: true } : null;
};
