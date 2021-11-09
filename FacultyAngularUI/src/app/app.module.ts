import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { Routes, RouterModule } from '@angular/router';
const appRoutes: Routes = [
  { path: '', component: CuratorsListComponent },
  { path: 'curator/index', component: CuratorsListComponent },
  { path: 'curator/create', component: CuratorCreateComponent },
  { path: '**', component: CuratorsListComponent }
];

import { AppComponent } from './app.component';
import { CuratorsListComponent } from "./components/curator.components/curators.list.component/curators.list.component";
import { HeaderComponent } from "./components/header.component/header.component";
import { FooterComponent } from "./components/footer.component/footer.component";
import { HttpClientModule } from "@angular/common/http";
import { CuratorsService } from "./services/curator.service";
import { CuratorCreateComponent } from "./components/curator.components/curator.create.component/curator.create.component";

@NgModule({
  declarations: [
    AppComponent,
    CuratorsListComponent,
    CuratorCreateComponent,
    HeaderComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [ CuratorsService ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
