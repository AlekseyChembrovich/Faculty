import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {SpecializationService} from "../services/specialization.service";
import {SpecializationModel} from "../models/specialization.model";

@Component({
  selector: 'app-specialization-create',
  templateUrl: './specialization.create.component.html'
})
export class SpecializationCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private specializationService: SpecializationService,
              private router: Router){
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      "name": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ])
    });
  }

  submit() : void {
    if (this.form.invalid) return;
    let specialization: SpecializationModel = new SpecializationModel(this.form.value.name, 0);
    this.specializationService.createSpecialization(specialization).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/specialization/index');
    });
  }
}
