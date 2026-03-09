import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'https://localhost:7204/api/Student';

  constructor(private http: HttpClient) {}

  // --- ADMIN PANEL İÇİN KULLANILANLAR ---
  getStudents(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addStudent(student: any): Observable<any> {
    return this.http.post(this.apiUrl, student);
  }

  deleteStudent(id: number) {
    // Admin panelinde silme işlemi için burayı doldurabilirsin
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  // --- ÖĞRENCİ DASHBOARD İÇİN KULLANILANLAR ---
  
  getStudentProfile(studentId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/get-profile?studentId=${studentId}`);
  }

  getMyGrades(studentId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/my-grades?studentId=${studentId}`);
  }
}