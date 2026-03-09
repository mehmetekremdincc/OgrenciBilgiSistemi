import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student';

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student-dashboard.html',
  styleUrls: ['./student-dashboard.scss']
})
export class StudentDashboardComponent implements OnInit {
  activeTab: string = 'overview';
  
  studentId: number = 0; 
  userName: string = 'Yükleniyor...'; 
  studentNumber: string = '';
  departmentName: string = '';

  myCoursesAndGrades: any[] = [];
  
  totalAkts: number = 0;
  averageGrade: number = 0;

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    const currentUserStr = localStorage.getItem('currentUser');
    
    if (currentUserStr) {
      const currentUser = JSON.parse(currentUserStr);
      this.studentId = currentUser.studentId || currentUser.id; // Login yapısına göre ayarlarsın
      this.loadData(); 
    } else {
      console.warn("Kullanıcı girişi bulunamadı! Test amacıyla Student ID = 1 olarak ayarlanıyor.");
      this.studentId = 1; 
      this.loadData();
    }
  }

  loadData() {
    // 1. Öğrenci Profil Bilgisini Getir
    this.studentService.getStudentProfile(this.studentId).subscribe({
      next: (profile) => {
        this.userName = profile.fullName;
        this.studentNumber = profile.studentNumber;
        this.departmentName = profile.departmentName;
      },
      error: (err) => console.error('Profil yükleme hatası:', err)
    });

    // 2. Öğrencinin Derslerini ve Notlarını Getir
    this.studentService.getMyGrades(this.studentId).subscribe({
      next: (data) => {
        this.myCoursesAndGrades = data;
        this.calculateStats();
      },
      error: (err) => console.error('Not yükleme hatası:', err)
    });
  }

  setTab(tab: string) {
    this.activeTab = tab;
  }

  private calculateStats() {
    if (this.myCoursesAndGrades.length === 0) return;

    let totalPoints = 0;
    this.totalAkts = 0;

    this.myCoursesAndGrades.forEach(course => {
      this.totalAkts += course.akts;
      // Vize %40, Final %60
      const courseAvg = (course.midterm * 0.4) + (course.final * 0.6);
      totalPoints += courseAvg;
    });

    this.averageGrade = totalPoints / this.myCoursesAndGrades.length;
  }

  logout() {
    localStorage.removeItem('currentUser');
    alert('Çıkış yapıldı.');
  }
}