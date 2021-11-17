import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "../shared/modules/common.module";
import {UserService} from "./services/user.service";
import {UsersListComponent} from "./users-list/users.list.component";
import {UserCreateComponent} from "./user-create/user.create.component";
import {UserUpdatePasswordComponent} from "./user-update-password/user.update.password.component";
import {UserUpdateComponent} from "./user-update/user.update.component";

@NgModule({
  declarations: [
    UsersListComponent,
    UserCreateComponent,
    UserUpdateComponent,
    UserUpdatePasswordComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [
    UserService
  ]
})
export class UsersModule {
}
