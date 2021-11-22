import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {GroupService} from "../services/group.service";
import {SpecializationModel} from "../../specializations/models/specialization.model";
import {SpecializationService} from "../../specializations/services/specialization.service";
import {GroupModel} from "../models/group.model";

@Component({
  selector: 'app-group-create',
  templateUrl: './group.create.component.html'
})
export class GroupCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public specializations: SpecializationModel[] = [];

  constructor(private groupService: GroupService,
              private specializationService: SpecializationService,
              private router: Router){
  }

  ngOnInit() : void {
    this.specializationService.getSpecializations().subscribe(response => {
      response.forEach(data => this.specializations.push(new SpecializationModel(data.name, data.id)))
      console.log("Specializations", this.specializations);
    })

    this.form = new FormGroup({
      "name": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "specializationId": new FormControl('',
        [ Validators.required ]),
    });
  }

  submit() : void {
    if (this.form.invalid) return;
    let group: GroupModel = new GroupModel(this.form.value.name, this.form.value.specializationId, 0);
    this.groupService.createGroup(group).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/group/index');
    });
  }
}
