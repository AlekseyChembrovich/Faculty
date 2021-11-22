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
import {AdminGuard} from "./authentication/auth-guard/admin.guard";
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
import {UsersListComponent} from "./users/users-list/users.list.component";
import {UserCreateComponent} from "./users/user-create/user.create.component";
import {UserUpdateComponent} from "./users/user-update/user.update.component";
import {UserUpdatePasswordComponent} from "./users/user-update-password/user.update.password.component";
import {AuthenticatedGuard} from "./authentication/auth-guard/authenticated.guard";
import {ServerError} from "./shared/components/errors/server-error/server.error";
import {NotFoundError} from "./shared/components/errors/not-found-error/not.found.error";
import {CommonModule} from "./shared/modules/common.module";
import {CuratorDeleteComponent} from "./curators/curator-delete/curator.delete.component";
import {FacultyDeleteComponent} from "./faculties/faculty-delete/faculty.delete.component";
import {GroupDeleteComponent} from "./groups/group-delete/group.delete.component";
import {SpecializationDeleteComponent} from "./specializations/specialization-delete/specialization.delete.component";
import {StudentDeleteComponent} from "./students/student-delete/student.delete.component";

const appRoutes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', redirectTo: 'faculty/index', pathMatch: 'full' },
      { path: '', component: FacultiesListComponent },
      { path: 'faculty/index', component: FacultiesListComponent },
      { path: 'faculty/create', component: FacultyCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'faculty/edit/:id', component: FacultyUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'faculty/delete/:id', component: FacultyDeleteComponent, canActivate: [ AdminGuard ] },

      { path: 'group/index', component: GroupsListComponent },
      { path: 'group/create', component: GroupCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'group/edit/:id', component: GroupUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'group/delete/:id', component: GroupDeleteComponent, canActivate: [ AdminGuard ] },

      { path: 'specialization/index', component: SpecializationsListComponent },
      { path: 'specialization/create', component: SpecializationCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'specialization/edit/:id', component: SpecializationUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'specialization/delete/:id', component: SpecializationDeleteComponent, canActivate: [ AdminGuard ] },

      { path: 'student/index', component: StudentsListComponent },
      { path: 'student/create', component: StudentCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'student/edit/:id', component: StudentUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'student/delete/:id', component: StudentDeleteComponent, canActivate: [ AdminGuard ] },

      { path: 'curator/index', component: CuratorsListComponent },
      { path: 'curator/create', component: CuratorCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'curator/edit/:id', component: CuratorUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'curator/delete/:id', component: CuratorDeleteComponent, canActivate: [ AdminGuard ] },

      { path: 'user/index', component: UsersListComponent, canActivate: [ AdminGuard ] },
      { path: 'user/create', component: UserCreateComponent, canActivate: [ AdminGuard ] },
      { path: 'user/edit/:id', component: UserUpdateComponent, canActivate: [ AdminGuard ] },
      { path: 'user/edit-password/:id', component: UserUpdatePasswordComponent, canActivate: [ AdminGuard ] },

      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegistrationComponent },
      { path: 'notfound-error', component: NotFoundError },
      { path: 'server-error', component: ServerError },
      { path: '**', redirectTo: '/notfound-error' }
    ]
  }
];

@NgModule({
  declarations: [
    ServerError,
    NotFoundError
  ],
  exports: [
    RouterModule
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    AuthenticatedGuard,
    AdminGuard
  ]
})
export class AppRoutingModule {
}
