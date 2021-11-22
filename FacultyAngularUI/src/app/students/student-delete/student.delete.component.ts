import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {StudentModel} from "../models/student.model";
import {StudentService} from "../services/student.service";

@Component({
  selector: 'app-student-delete',
  templateUrl: './student.delete.component.html'
})
export class StudentDeleteComponent implements OnInit {
  public student: StudentModel | undefined;

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

    this.studentService.getStudent(id).subscribe(response => {
      this.student = response;
    });
  }

  public delete() : void {
    let id = this.student?.id;
    if (id === undefined) return;
    this.studentService.deleteStudent(id).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/student/index');
    });
  }
}
