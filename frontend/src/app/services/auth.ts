import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7204/api/Auth'; 

  constructor(private http: HttpClient) { }

  login(credentials: any): Observable<any> {
    const loginRequest = {
      Email: credentials.email,
      Password: credentials.password
    };
    return this.http.post(`${this.apiUrl}/login`, loginRequest);
  }

  saveToken(token: string): void {
    if (typeof window !== 'undefined') {
      localStorage.setItem('token', token);
    }
  }

  saveUserInfo(role: string, username: string): void {
    if (typeof window !== 'undefined') {
      localStorage.setItem('role', role);
      localStorage.setItem('username', username);
    }
  }

  getToken(): string | null {
    if (typeof window !== 'undefined') {
      return localStorage.getItem('token');
    }
    return null;
  }

  logout(): void {
    if (typeof window !== 'undefined') {
      localStorage.clear();
    }
  }
}