import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { CuratorDto } from "../../../dto/curator/curator.dto";
import { CuratorsService } from "../../../services/curator.service";

@Component({
  selector: 'app-curator-create',
  templateUrl: './curator.create.component.html'
})
export class CuratorCreateComponent implements OnInit {
  public form: FormGroup = new FormGroup({ });

  constructor(private curatorsService: CuratorsService){
  }

  ngOnInit() : void {
    this.form = new FormGroup({
      surname: new FormControl(''),
      name: new FormControl(''),
      doublename: new FormControl(''),
      phone: new FormControl('')
    });
  }

  submit() : void {
    const formValue = {...this.form.value};
    let curator: CuratorDto = new CuratorDto(1, formValue.surname,
      formValue.name, formValue.doublename, formValue.phone);
    this.curatorsService.createCurator(curator);
    console.log('Send curator')
  }
}
