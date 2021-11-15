import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {FacultyService} from "../services/faculty.service";
import {GroupService} from "../../groups/services/group.service";
import {StudentService} from "../../students/services/student.service";
import {CuratorsService} from "../../curators/services/curator.service";
import {StudentDto} from "../../students/models/student.dto";
import {CuratorDto} from "../../curators/models/curator.dto";
import {GroupDisplayDto} from "../../groups/models/group.display.dto";
import {FacultyDto} from "../models/faculty.dto";

@Component({
  selector: 'app-group-create',
  templateUrl: './faculty.create.component.html'
})
export class FacultyCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public groups: GroupDisplayDto[] = [];
  public students: StudentDto[] = [];
  public curators: CuratorDto[] = [];
  public nowDate: Date = new Date();

  constructor(private facultyService: FacultyService,
              private groupService: GroupService,
              private studentService: StudentService,
              private curatorsService: CuratorsService,
              private router: Router)
  {
    this.groupService.getGroups().subscribe(response => {
      response.forEach(data => this.groups.push(new GroupDisplayDto(data.name, data.specializationName, data.id)))
      console.log("Groups", this.groups);
    });

    this.studentService.getStudents().subscribe(response => {
      response.forEach(data => this.students.push(new StudentDto(data.surname, data.name, data.doublename, data.id)))
      console.log("Students", this.students);
    });

    this.curatorsService.getCurators().subscribe(response => {
      response.forEach(data => this.curators.push(new CuratorDto(data.surname, data.name, data.doublename, data.phone, data.id)))
      console.log("Curators", this.curators);
    });
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      "startDateEducation": new FormControl('',
        [ Validators.required ]),
      "countYearEducation": new FormControl('',
        [ Validators.required, Validators.max(5), Validators.min(3) ]),
      "groupId": new FormControl('',
        [ Validators.required ]),
      "studentId": new FormControl('',
        [ Validators.required ]),
      "curatorId": new FormControl('',
        [ Validators.required ]),
    });
  }

  submit() : void {
    if (this.form.invalid) return;
    let faculty: FacultyDto = new FacultyDto(this.form.value.startDateEducation, this.form.value.countYearEducation,
      this.form.value.studentId, this.form.value.groupId, this.form.value.curatorId, 0);
    this.facultyService.createFaculty(faculty).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/faculty/index');
    });
  }
}
