import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {StudentService} from "../services/student.service";
import {StudentModel} from "../models/student.model";

@Component({
  selector: 'app-student-create',
  templateUrl: './student.create.component.html'
})
export class StudentCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private studentService: StudentService,
              private router: Router){
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      "surname": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "name": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "doublename": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ])
    });
  }

  submit() : void {
    if (this.form.invalid) return;
    let student: StudentModel = new StudentModel(this.form.value.surname,
      this.form.value.name, this.form.value.doublename, 0);
    this.studentService.createStudent(student).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/student/index');
    });
  }
}
