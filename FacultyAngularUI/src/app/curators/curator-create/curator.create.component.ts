import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CuratorDto} from "../models/curator.dto";
import {CuratorsService} from "../services/curator.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-curator-create',
  templateUrl: './curator.create.component.html'
})
export class CuratorCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private curatorsService: CuratorsService,
              private router: Router){
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      "surname": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "name": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "doublename": new FormControl('',
        [ Validators.required, Validators.maxLength(50) ]),
      "phone": new FormControl('',
        [ Validators.required, Validators.pattern("^\\+375-[0-9]{2}-[0-9]{3}-[0-9]{2}-[0-9]{2}$") ])
    });
  }

  submit() : void {
    if (this.form.invalid) return;
    let curator: CuratorDto = new CuratorDto(this.form.value.surname,
      this.form.value.name, this.form.value.doublename, this.form.value.phone, 0);
    this.curatorsService.createCurator(curator).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/curator/index');
    });
  }
}
