import {Component, OnInit} from '@angular/core';
import {GroupDisplayDto} from "../models/group.display.dto";
import {GroupService} from "../services/group.service";
import {AuthService} from "../../authentication/services/auth.service";

@Component({
  selector: 'app-groups-list',
  templateUrl: './groups.list.component.html'
})
export class GroupsListComponent implements OnInit {
  groups: GroupDisplayDto[] = [];

  constructor(private groupService: GroupService,
              public authService: AuthService){
  }

  ngOnInit() {
    this.groupService.getGroups().subscribe((data)=>{
      console.log("Before", this.groups);
      data.forEach(x => this.groups.push(new GroupDisplayDto(x.name,
        x.specializationName, x.id)));
      console.log("After", this.groups);
    });
  }

  delete(id: number) {
    if (id === undefined) return;
    this.groupService.deleteGroup(id).subscribe(response => console.log(response) );
    this.groups = this.groups.filter(x => x.id != id);
  }
}
