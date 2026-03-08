  import { Component, OnInit } from '@angular/core';
  import { CommonModule } from '@angular/common';
  import { FormsModule } from '@angular/forms';
  import { TeacherService } from '../../services/teacher';

  @Component({
    selector: 'app-teacher-dashboard',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './teacher-dashboard.html',
    styleUrls: ['./teacher-dashboard.scss']
  })
  export class TeacherDashboardComponent implements OnInit {
    activeTab: string = 'overview';
    userName: string = 'Ekrem Hoca';
    teacherId: number = 1; // Veritabanındaki öğretmen ID'niz

    myCourses: any[] = [];
    myStudents: any[] = [];
    selectedCourseId: number = 0;
    selectedCourseStudents: any[] = [];
    
    totalStudents: number = 0;
    nextLesson: any = null;

    constructor(private teacherService: TeacherService) {}

    ngOnInit(): void {
      this.loadData();
    }

    loadData() {
      // 1. Dersleri Getir
      this.teacherService.getDashboardCourses(this.teacherId).subscribe({
        next: (data) => {
          this.myCourses = data;
          this.prepareOverviewData();
        },
        error: (err) => console.error('Ders yükleme hatası:', err)
      });

      // 2. Tüm Öğrencileri Getir (Öğrencilerim sekmesi için)
      // Eğer backend'de dashboard-all-students yoksa getTeachers veya benzeri bir metodla geçici dolduralım
      // Şimdilik hata vermemesi için boş dizi atayalım veya mevcut metodu çağıralım
      this.teacherService.getTeachers().subscribe({
        next: (data) => {
          this.myStudents = data;
          this.totalStudents = data.length;
        }
      });
    }

    setTab(tab: string) {
      this.activeTab = tab;
      if (tab === 'grades') {
        this.onCourseSelected();
      }
    }

    // Not Girişi sekmesinde ders seçilince çalışır
    onCourseSelected() {
      if (!this.selectedCourseId || this.selectedCourseId == 0) {
        this.selectedCourseStudents = [];
        return;
      }

      this.teacherService.getDashboardStudents(Number(this.selectedCourseId)).subscribe({
        next: (data) => {
          this.selectedCourseStudents = data;
        },
        error: (err) => console.error('Öğrenci listesi hatası:', err)
      });
    }

    saveGrades() {
      if (!this.selectedCourseId || this.selectedCourseId == 0) return;

      const payload = this.selectedCourseStudents.map(s => ({
        scoId: s.id, // Backend'in beklediği SCO ID
        midterm: s.midterm,
        final: s.final
      }));

      this.teacherService.updateDashboardGrades(payload).subscribe({
        next: () => alert('Notlar başarıyla kaydedildi!'),
        error: (err) => alert('Kaydetme hatası: ' + err.message)
      });
    }

    private prepareOverviewData() {
      if (this.myCourses.length > 0) {
        this.nextLesson = { 
          course: this.myCourses[0].name, 
          time: '14:00' 
        };
      }
    }

    logout() {
      alert('Çıkış yapılıyor...');
    }
  }