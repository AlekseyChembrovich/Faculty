import {Component, OnInit} from '@angular/core';
import {UserDto} from "../models/user.dto";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-users-list',
  templateUrl: './users.list.component.html'
})
export class UsersListComponent implements OnInit {
  users: UserDto[] = [];

  constructor(private userService: UserService){
  }

  ngOnInit() {
    this.userService.getUsers().subscribe((data)=>{
      console.log("Before", this.users);
      data.forEach(x => this.users.push(new UserDto(x.login, x.roles, x.birthday, x.id)))
      console.log("After", this.users);
    });
  }

  delete(id: string): void {
    if (id === undefined) return;
    this.userService.deleteUser(id).subscribe(response => console.log(response) );
    this.users = this.users.filter(x => x.id != id);
  }
}
