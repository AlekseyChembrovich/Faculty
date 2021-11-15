import {NgModule} from "@angular/core";
import {StudentService} from "./services/student.service";
import {StudentsListComponent} from "./students-list/students.list.component";
import {BrowserModule} from "@angular/platform-browser";
import {StudentCreateComponent} from "./student-create/student.create.component";
import {StudentUpdateComponent} from "./student-update/student.update.component";
import {CommonModule} from "../shared/modules/common.module";

@NgModule({
  declarations: [
    StudentsListComponent,
    StudentCreateComponent,
    StudentUpdateComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [
    StudentService,
  ]
})
export class StudentsModule {
}
