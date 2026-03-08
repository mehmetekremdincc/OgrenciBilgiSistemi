import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class AttendanceService {

 private apiUrl = 'https://localhost:7204/api/Attendance';

 constructor(private http: HttpClient) {}

 getAttendances(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addAttendance(attendance: any): Observable<any> {
   return this.http.post(this.apiUrl, attendance);
 }

}