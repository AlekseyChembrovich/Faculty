import {Component, OnInit} from '@angular/core';
import {CuratorDto} from "../models/curator.dto";
import {CuratorsService} from "../services/curator.service";
import {AuthService} from "../../authentication/services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-curators-list',
  templateUrl: './curators.list.component.html'
})
export class CuratorsListComponent implements OnInit {
  curators: CuratorDto[] = [];

  constructor(private router: Router,
              private curatorsService: CuratorsService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.curatorsService.getCurators().subscribe((data)=>{
      data.forEach(x => this.curators.push(new CuratorDto(x.surname, x.name, x.doublename, x.phone, x.id)))
      console.log("After", this.curators);
    });
  }

  public delete(id: number) : void {
    if (id === undefined) return;
    this.curatorsService.deleteCurator(id).subscribe(response => console.log(response) );
    this.curators = this.curators.filter(x => x.id != id);
  }
}
