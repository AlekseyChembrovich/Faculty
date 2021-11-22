import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {FacultyModel} from "../models/faculty.model";
import {StudentModel} from "../../students/models/student.model";
import {CuratorModel} from "../../curators/models/curator.model";
import {FacultyService} from "../services/faculty.service";
import {StudentService} from "../../students/services/student.service";
import {CuratorsService} from "../../curators/services/curator.service";
import {GroupModel} from "../../groups/models/group.model";
import {GroupService} from "../../groups/services/group.service";

@Component({
  selector: 'app-faculty-delete',
  templateUrl: './faculty.delete.component.html'
})
export class FacultyDeleteComponent implements OnInit {
  public faculty: FacultyModel | undefined;
  public group: GroupModel | undefined;
  public student: StudentModel | undefined;
  public curator: CuratorModel | undefined;

  constructor(private facultyService: FacultyService,
              private groupService: GroupService,
              private studentService: StudentService,
              private curatorsService: CuratorsService,
              private activatedRoute: ActivatedRoute,
              private router: Router)
  {
  }

  ngOnInit() : void {
    let id = 0;
    this.activatedRoute.params.subscribe(params => {
      console.log(params);
      id = params['id'];
    });

    this.facultyService.getFaculty(id).subscribe(response => {
      this.faculty = response;
      this.groupService.getGroup(this.faculty.groupId).subscribe(response => {
        this.group = response;
      });

      this.studentService.getStudent(this.faculty.studentId).subscribe(response => {
        this.student = response;
      });

      this.curatorsService.getCurator(this.faculty.curatorId).subscribe(response => {
        this.curator = response;
      });
    });
  }

  public delete() : void {
    let id = this.faculty?.id;
    if (id === undefined) return;
    this.facultyService.deleteFaculty(id).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/faculty/index');
    });
  }
}
