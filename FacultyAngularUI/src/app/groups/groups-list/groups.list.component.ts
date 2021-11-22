import {Component, OnInit} from '@angular/core';
import {GroupDisplayModel} from "../models/group.display.model";
import {GroupService} from "../services/group.service";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-groups-list',
  templateUrl: './groups.list.component.html'
})
export class GroupsListComponent implements OnInit {
  groups: GroupDisplayModel[] = [];

  constructor(private groupService: GroupService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.groupService.getGroups().subscribe((data)=>{
      console.log("Before", this.groups);
      data.forEach(x => this.groups.push(new GroupDisplayModel(x.name,
        x.specializationName, x.id)));
      console.log("After", this.groups);
    });
  }
}
