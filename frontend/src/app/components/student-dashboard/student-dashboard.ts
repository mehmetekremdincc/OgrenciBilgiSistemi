import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <div class="p-5 bg-success text-white rounded-3 shadow">
        <h1>Öğrenci Paneli</h1>
        <p class="lead">Notlarınızı, devamsızlıklarınızı ve ders programınızı takip edin.</p>
        <hr class="my-4">
        <button class="btn btn-light text-success fw-bold">Transkript Görüntüle</button>
      </div>
    </div>
  `
})
export class StudentDashboardComponent {}