import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class GradeService {

 private apiUrl = 'https://localhost:7204/api/Grade';

 constructor(private http: HttpClient) {}

 getGrades(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addGrade(grade: any): Observable<any> {
   return this.http.post(this.apiUrl, grade);
 }

}