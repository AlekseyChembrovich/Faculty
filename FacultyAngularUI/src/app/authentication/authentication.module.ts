import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {AuthService} from "./services/auth.service";
import {AuthInterceptor} from "./services/auth.interceptor";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import {RegistrationComponent} from "./registration/registration.component";
import {CommonModule} from "../shared/modules/common.module";
import {AuthHeaderComponent} from "./auth-header/auth.header.component";

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    AuthHeaderComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    CommonModule
  ],
  exports: [
    AuthHeaderComponent
  ],
  providers: [
    AuthService,
    AuthInterceptor
  ]
})
export class AuthenticationModule {
}
