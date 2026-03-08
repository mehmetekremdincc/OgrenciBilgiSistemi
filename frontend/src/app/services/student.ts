import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class StudentService {
 deleteStudent(id: number) {
   throw new Error('Method not implemented.');
 }

 private apiUrl = 'https://localhost:7204/api/Student';

 constructor(private http: HttpClient) {}

 getStudents(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addStudent(student: any): Observable<any> {
   return this.http.post(this.apiUrl, student);
 }

}