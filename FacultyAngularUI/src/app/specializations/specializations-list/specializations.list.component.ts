import { Component, OnInit } from '@angular/core';
import { SpecializationModel } from "../models/specialization.model";
import { SpecializationService } from "../services/specialization.service";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-specializations-list',
  templateUrl: './specializations.list.component.html'
})
export class SpecializationsListComponent implements OnInit {
  specializations: SpecializationModel[] = [];

  constructor(private specializationService: SpecializationService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.specializationService.getSpecializations().subscribe((data)=>{
      console.log("Before", this.specializations);
      data.forEach(x => this.specializations.push(new SpecializationModel(x.name, x.id)));
      console.log("After", this.specializations);
    });
  }
}
