import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginObj = {
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  onLogin() {
    this.authService.login(this.loginObj).subscribe({
      next: (res: any) => {

        console.log("Backend'den gelen yanıt:", res);

        this.authService.saveToken(res.token);

        const role = res.role; // ✅ DOĞRUSU BU

        if (role === "Admin") {
          this.router.navigate(['/admin-panel']);
        }
        else if (role === "Ogretmen") {
          this.router.navigate(['/teacher-dashboard']);
        }
        else if (role === "Ogrenci") {
          this.router.navigate(['/student-dashboard']);
        }
        else {
          console.error("Tanımsız Rol geldi:", role);
        }
      },
      error: (err) => {
        console.error("Giriş hatası detayları:", err);
        alert("Giriş yapılamadı. Bilgilerinizi kontrol edin.");
      }
    });
  }
}