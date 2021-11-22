import {Component, OnInit} from '@angular/core';
import {CuratorModel} from "../models/curator.model";
import {CuratorsService} from "../services/curator.service";
import {AuthService} from "../../authentication/services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-curators-list',
  templateUrl: './curators.list.component.html'
})
export class CuratorsListComponent implements OnInit {
  curators: CuratorModel[] = [];

  constructor(private router: Router,
              private curatorsService: CuratorsService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.curatorsService.getCurators().subscribe((data)=>{
      data.forEach(x => this.curators.push(new CuratorModel(x.surname, x.name, x.doublename, x.phone, x.id)))
      console.log("After", this.curators);
    });
  }
}
