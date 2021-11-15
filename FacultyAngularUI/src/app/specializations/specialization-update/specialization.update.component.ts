import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {SpecializationService} from "../services/specialization.service";
import {SpecializationDto} from "../models/specialization.dto";

@Component({
  selector: 'app-specialization-update',
  templateUrl: './specialization.update.component.html'
})
export class SpecializationUpdateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });
  private specialization: SpecializationDto | undefined;

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

    this.form = new FormGroup({
      name: new FormControl('',
        [ Validators.required, Validators.maxLength(50) ])
    });

    this.specializationService.getSpecialization(id).subscribe(response => {
      this.specialization = response;
      this.form.patchValue({
        name: this.specialization.name
      });
    });
  }

  submit() : void {
    let specialization: SpecializationDto = new SpecializationDto(this.form.value.name, this.specialization?.id);
    this.specializationService.updateSpecialization(specialization).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/specialization/index');
    });
  }
}
