import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {StudentModel} from "../models/student.model";
import {StudentService} from "../services/student.service";

@Component({
  selector: 'app-student-update',
  templateUrl: './student.update.component.html'
})
export class StudentUpdateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  private student: StudentModel | undefined;

  constructor(private studentService: StudentService,
              private activatedRoute: ActivatedRoute,
              private router: Router){
  }

  ngOnInit() : void {
    let id = 0;
    this.activatedRoute.params.subscribe(params => {
      console.log(params);
      id = params['id'];
    });

    this.form = new FormGroup({
      surname: new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      name: new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      doublename: new FormControl('',
        [ Validators.required, Validators.maxLength(50) ])
    });

    this.studentService.getStudent(id).subscribe(response => {
      this.student = response;
      this.form.patchValue({
        surname: this.student.surname,
        name: this.student.name,
        doublename: this.student.doublename
      });
    });
  }

  submit() : void {
    let student: StudentModel = new StudentModel(this.form.value.surname,
      this.form.value.name, this.form.value.doublename, this.student?.id);
    this.studentService.updateStudent(student).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/student/index');
    });
  }
}
