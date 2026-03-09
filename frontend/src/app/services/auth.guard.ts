import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // 1. Kullanıcı giriş yapmış mı?
    if (!this.authService.isLoggedIn()) {
      alert("Lütfen önce giriş yapın!");
      this.router.navigate(['/login']);
      return false;
    }

    // 2. Sayfanın gerektirdiği rol ile kullanıcının rolünü karşılaştır
    const expectedRole = route.data['role']; 
    const currentRole = this.authService.getRole();

    if (expectedRole && expectedRole !== currentRole) {
      alert("Bu sayfaya erişim yetkiniz bulunmamaktadır!");
      
      // Kendi yetkisine göre ait olduğu sayfaya geri yönlendir
      if (currentRole === 'Admin') {
        this.router.navigate(['/admin-panel']);
      } else if (currentRole === 'Teacher') {
        this.router.navigate(['/teacher-dashboard']);
      } else if (currentRole === 'Student') {
        this.router.navigate(['/student-dashboard']);
      } else {
        this.router.navigate(['/login']);
      }
      
      return false;
    }

    // Her şey tamamsa (giriş yapılmış ve rol doğruysa) geçişe izin ver
    return true;
  }
}