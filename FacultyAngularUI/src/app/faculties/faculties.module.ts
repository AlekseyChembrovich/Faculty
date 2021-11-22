import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FacultiesListComponent} from "./faculties-list/faculties.list.component";
import {FacultyService} from "./services/faculty.service";
import {CommonModule} from "../shared/modules/common.module";
import {FacultyCreateComponent} from "./faculty-create/faculty.create.component";
import {FacultyUpdateComponent} from "./faculty-update/faculty.update.component";
import {FacultyDeleteComponent} from "./faculty-delete/faculty.delete.component";

@NgModule({
  declarations: [
    FacultiesListComponent,
    FacultyCreateComponent,
    FacultyUpdateComponent,
    FacultyDeleteComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [
    FacultyService
  ]
})
export class FacultiesModule {
}
