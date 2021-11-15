import {NgModule} from "@angular/core";
import {Routes, RouterModule} from "@angular/router";
import {FacultiesListComponent} from "./faculties/faculties-list/faculties.list.component";
import {CuratorsListComponent} from "./curators/curators-list/curators.list.component";
import {GroupsListComponent} from "./groups/groups-list/groups.list.component";
import {SpecializationsListComponent} from "./specializations/specializations-list/specializations.list.component";
import {StudentsListComponent} from "./students/students-list/students.list.component";
import {LayoutComponent} from "./shared/components/layout/layout.component";
import {CuratorCreateComponent} from "./curators/curator-create/curator.create.component";
import {CuratorUpdateComponent} from "./curators/curator-update/curator.update.component";
import {AdminGuard} from "./auth.guard/admin.guard";
import {RegistrationComponent} from "./authentication/registration/registration.component";
import {LoginComponent} from "./authentication/login/login.component";
import {GroupCreateComponent} from "./groups/group-create/group.create.component";
import {GroupUpdateComponent} from "./groups/group-update/group.update.component";
import {FacultyCreateComponent} from "./faculties/faculty-create/faculty.create.component";
import {FacultyUpdateComponent} from "./faculties/faculty-update/faculty.update.component";
import {SpecializationCreateComponent} from "./specializations/specialization-create/specialization.create.component";
import {SpecializationUpdateComponent} from "./specializations/specialization-update/specialization.update.component";
import {StudentCreateComponent} from "./students/student-create/student.create.component";
import {StudentUpdateComponent} from "./students/student-update/student.update.component";

const appRoutes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', redirectTo: 'faculty/index', pathMatch: 'full' },
      { path: '', component: FacultiesListComponent },
      { path: 'faculty/index', component: FacultiesListComponent },
      { path: 'faculty/create', component: FacultyCreateComponent },
      { path: 'faculty/edit/:id', component: FacultyUpdateComponent },

      { path: 'group/index', component: GroupsListComponent },
      { path: 'group/create', component: GroupCreateComponent },
      { path: 'group/edit/:id', component: GroupUpdateComponent },

      { path: 'specialization/index', component: SpecializationsListComponent },
      { path: 'specialization/create', component: SpecializationCreateComponent },
      { path: 'specialization/edit/:id', component: SpecializationUpdateComponent },

      { path: 'student/index', component: StudentsListComponent },
      { path: 'student/create', component: StudentCreateComponent },
      { path: 'student/edit/:id', component: StudentUpdateComponent },

      { path: 'curator/index', component: CuratorsListComponent },
      { path: 'curator/create', component: CuratorCreateComponent, canActivateChild: [ AdminGuard ] },
      { path: 'curator/edit/:id', component: CuratorUpdateComponent, canActivateChild: [ AdminGuard ] },

      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegistrationComponent }
    ]
  }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(appRoutes)]
})
export class AppRoutingModule {
}
