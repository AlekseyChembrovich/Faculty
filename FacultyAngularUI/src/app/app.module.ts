import {NgModule, Provider} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
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
import {UsersModule} from "./users/users.module";
import {ErrorInterceptor} from "./shared/services/error.interceptor";
import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {environment} from "../environments/environment";

const AUTH_INTERCEPTOR: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
}

const ERROR_INTERCEPTOR: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
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
    AuthenticationModule,
    UsersModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      defaultLanguage: 'en'
    })
  ],
  providers: [
    AUTH_INTERCEPTOR,
    ERROR_INTERCEPTOR
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}

console.log("--> App starts");
console.log("--> Url authentication server - " + environment.authApiUrl);
console.log("--> Url resource server - " + environment.resourceApiUrl);
