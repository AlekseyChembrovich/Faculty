import {NgModule} from "@angular/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import {RouterModule} from "@angular/router";
import {TranslateModule} from "@ngx-translate/core";

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    TranslateModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    TranslateModule
  ]
})
export class CommonModule {
}
