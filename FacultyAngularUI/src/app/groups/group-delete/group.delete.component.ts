import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {GroupModel} from "../models/group.model";
import {SpecializationModel} from "../../specializations/models/specialization.model";
import {GroupService} from "../services/group.service";
import {SpecializationService} from "../../specializations/services/specialization.service";

@Component({
  selector: 'app-group-delete',
  templateUrl: './group.delete.component.html'
})
export class GroupDeleteComponent implements OnInit {
  public group: GroupModel | undefined;
  public specialization: SpecializationModel | undefined;

  constructor(private groupService: GroupService,
              private specializationService: SpecializationService,
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

    this.groupService.getGroup(id).subscribe(response => {
      this.group = response;
      this.specializationService.getSpecialization(this.group.specializationId).subscribe(response => {
        this.specialization = response;
      });
    });
  }

  public delete() : void {
    let id = this.group?.id;
    if (id === undefined) return;
    this.groupService.deleteGroup(id).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/group/index');
    });
  }
}
