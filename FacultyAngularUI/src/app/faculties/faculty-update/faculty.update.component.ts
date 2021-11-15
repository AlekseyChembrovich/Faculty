import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {GroupDisplayDto} from "../../groups/models/group.display.dto";
import {FacultyService} from "../services/faculty.service";
import {StudentService} from "../../students/services/student.service";
import {CuratorsService} from "../../curators/services/curator.service";
import {GroupService} from "../../groups/services/group.service";
import {FacultyDto} from "../models/faculty.dto";
import {StudentDto} from "../../students/models/student.dto";
import {CuratorDto} from "../../curators/models/curator.dto";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-faculty-update',
  templateUrl: './faculty.update.component.html'
})
export class FacultyUpdateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public groups: GroupDisplayDto[] = [];
  public students: StudentDto[] = [];
  public curators: CuratorDto[] = [];
  public faculty: FacultyDto | undefined;

  constructor(private facultyService: FacultyService,
              private groupService: GroupService,
              private studentService: StudentService,
              private curatorsService: CuratorsService,
              private activatedRoute: ActivatedRoute,
              private router: Router)
  {
    this.groupService.getGroups().subscribe(response => {
      response.forEach(data => this.groups.push(new GroupDisplayDto(data.name, data.specializationName, data.id)));
      console.log("Groups", this.groups);
    });

    this.studentService.getStudents().subscribe(response => {
      response.forEach(data => this.students.push(new StudentDto(data.surname, data.name, data.doublename, data.id)));
      console.log("Students", this.students);
    });

    this.curatorsService.getCurators().subscribe(response => {
      response.forEach(data => this.curators.push(new CuratorDto(data.surname, data.name, data.doublename, data.phone, data.id)));
      console.log("Groups", this.groups);
    });
  }

  ngOnInit() : void {
    let id = 0;
    this.activatedRoute.params.subscribe(params => {
      console.log(params);
      id = params['id'];
    });

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

    this.facultyService.getFaculty(id).subscribe(response => {
      this.faculty = response;
      console.log("Faculty", this.faculty)
      this.form.patchValue({
        startDateEducation: this.faculty.startDateEducation,
        countYearEducation: this.faculty.countYearEducation
      });
    });
  }

  submit(): void {
    let faculty: FacultyDto = new FacultyDto(this.form.value.startDateEducation, this.form.value.countYearEducation,
      this.form.value.studentId, this.form.value.groupId, this.form.value.curatorId, this.form.value.id);
    this.facultyService.updateFaculty(faculty).subscribe(response => {
      console.log("Response", response);
      this.router.navigateByUrl('/faculty/index');
    });
  }
}
