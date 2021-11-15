import {NgModule} from "@angular/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import {RouterModule} from "@angular/router";

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule
  ]
})
export class CommonModule {
}
