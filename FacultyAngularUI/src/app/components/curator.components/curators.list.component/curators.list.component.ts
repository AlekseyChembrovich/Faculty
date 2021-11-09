import {Component, OnInit} from '@angular/core';
import { CuratorDto } from "../../../dto/curator/curator.dto";
import {CuratorsService} from "../../../services/curator.service";

@Component({
  selector: 'app-curators-list',
  templateUrl: './curators.list.component.html'
})
export class CuratorsListComponent implements OnInit {
  curators: CuratorDto[] = [];

  constructor(private curatorsService: CuratorsService){
  }

  ngOnInit() {
    this.curatorsService.getCurators().subscribe((data)=>{
      console.log("Before", this.curators);
      data.forEach(x => this.curators.push(new CuratorDto(x.id, x.surname, x.name, x.doublename, x.phone)))
      console.log("After", this.curators);
    });
  }
}
