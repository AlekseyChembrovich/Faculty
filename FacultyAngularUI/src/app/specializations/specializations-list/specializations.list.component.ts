import { Component, OnInit } from '@angular/core';
import { SpecializationDto } from "../models/specialization.dto";
import { SpecializationService } from "../services/specialization.service";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-specializations-list',
  templateUrl: './specializations.list.component.html'
})
export class SpecializationsListComponent implements OnInit {
  specializations: SpecializationDto[] = [];

  constructor(private specializationService: SpecializationService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.specializationService.getSpecializations().subscribe((data)=>{
      console.log("Before", this.specializations);
      data.forEach(x => this.specializations.push(new SpecializationDto(x.name, x.id)));
      console.log("After", this.specializations);
    });
  }

  public delete(id: number) : void {
    if (id === undefined) return;
    this.specializationService.deleteSpecialization(id).subscribe(response => console.log(response) );
    this.specializations = this.specializations.filter(x => x.id != id);
  }
}
