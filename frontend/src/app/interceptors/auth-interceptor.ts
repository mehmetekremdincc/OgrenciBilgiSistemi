import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();

    // Token ekleme
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    // Body varsa ve object ise PascalCase'e çevir
    if (request.body && typeof request.body === 'object') {
      request = request.clone({
        body: this.toPascalCase(request.body)
      });
    }

    return next.handle(request);
  }

  private toPascalCase(obj: any): any {
    if (Array.isArray(obj)) {
      return obj.map(item => this.toPascalCase(item));
    } else if (obj !== null && typeof obj === 'object') {
      const newObj: any = {};
      Object.keys(obj).forEach(key => {
        const pascalKey = key.charAt(0).toUpperCase() + key.slice(1);
        newObj[pascalKey] = this.toPascalCase(obj[key]);
      });
      return newObj;
    }
    return obj;
  }
}