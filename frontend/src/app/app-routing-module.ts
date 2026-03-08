import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel';
import { TeacherDashboardComponent } from './components/teacher-dashboard/teacher-dashboard';
import { StudentDashboardComponent } from './components/student-dashboard/student-dashboard';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin-panel', component: AdminPanelComponent }, // Role ID: 0
  { path: 'teacher-dashboard', component: TeacherDashboardComponent }, // Role ID: 1
  { path: 'student-dashboard', component: StudentDashboardComponent }, // Role ID: 2
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }