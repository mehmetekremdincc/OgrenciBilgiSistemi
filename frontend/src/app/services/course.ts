import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class CourseService {

 private apiUrl = 'https://localhost:7204/api/Course';

 constructor(private http: HttpClient) {}

 getCourses(): Observable<any[]> {
   return this.http.get<any[]>(this.apiUrl);
 }

 addCourse(course: any): Observable<any> {
   return this.http.post(this.apiUrl, course);
 }

}