import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class TeacherService {
 getMyStudents: any;
 getMyCourses:any;

 private apiUrl = 'https://localhost:7204/api/Teacher';
  getStudentsByCourse: any;
  updateGrades: any;

 constructor(private http: HttpClient) {}

 getTeachers(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addTeacher(teacher: any): Observable<any> {
   return this.http.post(this.apiUrl, teacher);
 }
// TeacherService sınıfının içine, en alta ekle:

getDashboardCourses(teacherId: number = 1): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/dashboard-courses?teacherId=${teacherId}`);
}

getDashboardStudents(courseId: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/dashboard-students/${courseId}`);
}

updateDashboardGrades(grades: any[]): Observable<any> {
  return this.http.post(`${this.apiUrl}/dashboard-bulk-update`, grades);
}

}