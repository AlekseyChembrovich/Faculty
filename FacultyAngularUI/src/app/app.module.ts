import {NgModule, Provider} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthInterceptor} from "./authentication/services/auth.interceptor";

import {AppRoutingModule} from "./app.routing.module";
import {CuratorsModule} from "./curators/curators.module";
import {AuthenticationModule} from "./authentication/authentication.module";
import {FacultiesModule} from "./faculties/faculties.module";
import {GroupsModule} from "./groups/groups.module";
import {SpecializationsModule} from "./specializations/specializations.module";
import {StudentsModule} from "./students/students.module";
import {LayoutComponent} from "./shared/components/layout/layout.component";
import {FooterComponent} from "./shared/components/footer/footer.component";
import {HeaderComponent} from "./shared/components/header/header.component";

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LayoutComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CuratorsModule,
    FacultiesModule,
    GroupsModule,
    SpecializationsModule,
    StudentsModule,
    AuthenticationModule
  ],
  providers: [
    INTERCEPTOR_PROVIDER
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}
