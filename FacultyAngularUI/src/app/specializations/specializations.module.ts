import {NgModule} from "@angular/core";
import {SpecializationsListComponent} from "./specializations-list/specializations.list.component";
import {SpecializationService} from "./services/specialization.service";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "../shared/modules/common.module";
import {SpecializationCreateComponent} from "./specialization-create/specialization.create.component";
import {SpecializationUpdateComponent} from "./specialization-update/specialization.update.component";

@NgModule({
  declarations: [
    SpecializationsListComponent,
    SpecializationCreateComponent,
    SpecializationUpdateComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [
    SpecializationService
  ]
})
export class SpecializationsModule {
}
