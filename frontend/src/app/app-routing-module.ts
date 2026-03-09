import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel';
import { TeacherDashboardComponent } from './components/teacher-dashboard/teacher-dashboard';
import { StudentDashboardComponent } from './components/student-dashboard/student-dashboard';
import { AuthGuard } from './services/auth.guard'; // Yolu klasör yapına göre kontrol et

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { 
    path: 'admin-panel', 
    component: AdminPanelComponent, 
    canActivate: [AuthGuard], 
    data: { role: 'Admin' } 
  }, 
  { 
    path: 'teacher-dashboard', 
    component: TeacherDashboardComponent, 
    canActivate: [AuthGuard], 
    data: { role: 'Teacher' } 
  }, 
  { 
    path: 'student-dashboard', 
    component: StudentDashboardComponent, 
    canActivate: [AuthGuard], 
    data: { role: 'Student' } 
  }, 
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' } // Rastgele saçma bir URL girilirse login'e atar
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }