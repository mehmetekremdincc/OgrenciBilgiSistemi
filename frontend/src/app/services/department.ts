import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class DepartmentService {

 private apiUrl = 'https://localhost:7204/api/Department';

 constructor(private http: HttpClient) {}

 getDepartments(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addDepartment(department: any): Observable<any> {
   return this.http.post(this.apiUrl, department);
 }

 deleteDepartment(id: number): Observable<any> {
   return this.http.delete(`${this.apiUrl}/${id}`);
 }

 updateDepartment(id: number, department: any): Observable<any> {
   return this.http.put(`${this.apiUrl}/${id}`, department);
 }

}