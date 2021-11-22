import {Component, OnInit} from '@angular/core';
import {FacultyService} from "../services/faculty.service";
import {FacultyDisplayModel} from "../models/faculty.display.model";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-faculties-list',
  templateUrl: './faculties.list.component.html'
})
export class FacultiesListComponent implements OnInit {
  faculties: FacultyDisplayModel[] = [];

  constructor(private facultyService: FacultyService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.facultyService.getFaculties().subscribe((data) => {
      console.log("Before", this.faculties);
      data.forEach(x => this.faculties.push(new FacultyDisplayModel(x.startDateEducation,
        x.countYearEducation, x.studentSurname, x.groupName, x.curatorSurname, x.id)))
      console.log("After", this.faculties);
    });
  }
}
