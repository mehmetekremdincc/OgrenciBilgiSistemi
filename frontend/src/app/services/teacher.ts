import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private apiUrl = 'https://localhost:7204/api/Teacher';

  constructor(private http: HttpClient) {}

  // --- ADMIN PANEL İÇİN KULLANILANLAR ---
  getTeachers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addTeacher(teacher: any): Observable<any> {
    return this.http.post(this.apiUrl, teacher);
  }

  // --- ÖĞRETMEN DASHBOARD İÇİN KULLANILANLAR ---
  
  getTeacherProfile(teacherId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/get-profile?teacherId=${teacherId}`);
  }

  getDashboardCourses(teacherId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/dashboard-courses?teacherId=${teacherId}`);
  }

  getDashboardMyStudents(teacherId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/dashboard-my-students?teacherId=${teacherId}`);
  }

  getDashboardStudents(courseId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/dashboard-students/${courseId}`);
  }

  updateDashboardGrades(grades: any[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/dashboard-bulk-update`, grades);
  }
}