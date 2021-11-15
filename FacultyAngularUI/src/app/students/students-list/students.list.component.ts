import {Component, OnInit} from '@angular/core';
import {StudentDto} from "../models/student.dto";
import {StudentService} from "../services/student.service";

@Component({
  selector: 'app-students-list',
  templateUrl: './students.list.component.html'
})
export class StudentsListComponent implements OnInit {
  students: StudentDto[] = [];

  constructor(private studentService: StudentService){
  }

  ngOnInit() {
    this.studentService.getStudents().subscribe((data)=>{
      console.log("Before", this.students);
      data.forEach(x => this.students.push(new StudentDto(x.surname, x.name, x.doublename, x.id)))
      console.log("After", this.students);
    });
  }

  delete(id: number): void {
    if (id === undefined) return;
    this.studentService.deleteStudent(id).subscribe(response => console.log(response) );
    this.students = this.students.filter(x => x.id != id);
  }
}
