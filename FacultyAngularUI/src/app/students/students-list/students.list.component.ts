import {Component, OnInit} from '@angular/core';
import {StudentModel} from "../models/student.model";
import {StudentService} from "../services/student.service";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-students-list',
  templateUrl: './students.list.component.html'
})
export class StudentsListComponent implements OnInit {
  students: StudentModel[] = [];

  constructor(private studentService: StudentService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.studentService.getStudents().subscribe((data)=>{
      console.log("Before", this.students);
      data.forEach(x => this.students.push(new StudentModel(x.surname, x.name, x.doublename, x.id)))
      console.log("After", this.students);
    });
  }
}
