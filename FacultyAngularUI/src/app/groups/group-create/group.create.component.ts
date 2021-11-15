import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {GroupService} from "../services/group.service";
import {SpecializationDto} from "../../specializations/models/specialization.dto";
import {SpecializationService} from "../../specializations/services/specialization.service";
import {GroupDto} from "../models/group.dto";

@Component({
  selector: 'app-group-create',
  templateUrl: './group.create.component.html'
})
export class GroupCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  public specializations: SpecializationDto[] = [];

  constructor(private groupService: GroupService,
              private specializationService: SpecializationService,
              private router: Router){
  }

  ngOnInit() : void {
    this.specializationService.getSpecializations().subscribe(response => {
      response.forEach(data => this.specializations.push(new SpecializationDto(data.name, data.id)))
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
    let group: GroupDto = new GroupDto(this.form.value.name, this.form.value.specializationId, 0);
    this.groupService.createGroup(group).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/group/index');
    });
  }
}
