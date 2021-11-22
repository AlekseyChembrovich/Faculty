import {NgModule} from "@angular/core";
import {GroupsListComponent} from "./groups-list/groups.list.component";
import {GroupCreateComponent} from "./group-create/group.create.component";
import {GroupUpdateComponent} from "./group-update/group.update.component";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "../shared/modules/common.module";
import {GroupService} from "./services/group.service";
import {GroupDeleteComponent} from "./group-delete/group.delete.component";

@NgModule({
  declarations: [
    GroupsListComponent,
    GroupCreateComponent,
    GroupUpdateComponent,
    GroupDeleteComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [
    GroupService
  ]
})
export class GroupsModule {
}
