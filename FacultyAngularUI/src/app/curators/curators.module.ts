import {NgModule} from "@angular/core";
import {CuratorsListComponent} from "./curators-list/curators.list.component";
import {CuratorCreateComponent} from "./curator-create/curator.create.component";
import {CuratorUpdateComponent} from "./curator-update/curator.update.component";
import {CuratorsService} from "./services/curator.service";
import {CommonModule} from "../shared/modules/common.module";
import {CuratorDeleteComponent} from "./curator-delete/curator.delete.component";

@NgModule({
  declarations: [
    CuratorsListComponent,
    CuratorCreateComponent,
    CuratorUpdateComponent,
    CuratorDeleteComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [
    CuratorsService
  ]
})
export class CuratorsModule {
}
