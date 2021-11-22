import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpecializationService} from "../services/specialization.service";
import {SpecializationModel} from "../models/specialization.model";

@Component({
  selector: 'app-specialization-delete',
  templateUrl: './specialization.delete.component.html'
})
export class SpecializationDeleteComponent implements OnInit {
  public specialization: SpecializationModel | undefined;

  constructor(private specializationService: SpecializationService,
              private activatedRoute: ActivatedRoute,
              private router: Router){
  }

  ngOnInit() : void {
    let id = 0;
    this.activatedRoute.params.subscribe(params => {
      console.log(params);
      id = params['id'];
    });

    this.specializationService.getSpecialization(id).subscribe(response => {
      this.specialization = response;
    });
  }

  public delete() : void {
    let id = this.specialization?.id;
    if (id === undefined) return;
    this.specializationService.deleteSpecialization(id).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/specialization/index');
    });
  }
}
