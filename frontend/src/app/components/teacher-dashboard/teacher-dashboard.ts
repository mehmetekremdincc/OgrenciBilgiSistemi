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
  
  // Statik 1 yerine başlangıçta 0 veriyoruz, ngOnInit içinde dinamik dolacak
  teacherId: number = 0; 
  userName: string = 'Yükleniyor...'; 

  myCourses: any[] = [];
  myStudents: any[] = [];
  selectedCourseId: number = 0;
  selectedCourseStudents: any[] = [];
  
  totalStudents: number = 0;
  nextLesson: any = null;

  constructor(private teacherService: TeacherService) {}

  ngOnInit(): void {
    // 1. Tarayıcı hafızasından giriş yapan kullanıcıyı al
    const currentUserStr = localStorage.getItem('currentUser');
    
    if (currentUserStr) {
      // Eğer biri giriş yapmışsa veriyi JSON'a çevir
      const currentUser = JSON.parse(currentUserStr);
      
      // Kullanıcının ID'sini teacherId'ye ata
      this.teacherId = currentUser.teacherId; 
      
      // Hocanın ID'sini bulduğumuza göre artık verilerini çekebiliriz
      this.loadData(); 
    } else {
      // Eğer kimse giriş yapmamışsa (localStorage boşsa)
      console.warn("Kullanıcı girişi bulunamadı! Test amacıyla Teacher ID = 1 olarak ayarlanıyor.");
      
      // Şimdilik sistem çökmesin diye test ID'mizi verip devam edelim
      this.teacherId = 1; 
      this.loadData();
    }
  }

  loadData() {
    // 1. Öğretmenin Profil Bilgisini (Adı vb.) Getir
    this.teacherService.getTeacherProfile(this.teacherId).subscribe({
      next: (profile) => {
        this.userName = profile.fullName;
      },
      error: (err) => console.error('Profil yükleme hatası:', err)
    });

    // 2. Öğretmenin Kendi Derslerini Getir
    this.teacherService.getDashboardCourses(this.teacherId).subscribe({
      next: (data) => {
        this.myCourses = data;
        this.prepareOverviewData();
      },
      error: (err) => console.error('Ders yükleme hatası:', err)
    });

    // 3. Öğretmenin Tüm Öğrencilerini Getir (Öğrencilerim sekmesi için)
    this.teacherService.getDashboardMyStudents(this.teacherId).subscribe({
      next: (data) => {
        this.myStudents = data;
        this.totalStudents = data.length;
      },
      error: (err) => console.error('Öğrenci yükleme hatası:', err)
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
      error: (err) => console.error('Seçili dersin öğrenci listesi hatası:', err)
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
    // Çıkış yaparken tarayıcıdaki kullanıcı bilgilerini temizle
    localStorage.removeItem('currentUser');
    alert('Çıkış yapıldı. Giriş sayfasına yönlendirilmeniz gerekiyor.');
    
    // İleride buraya router.navigate(['/login']) ekleyerek login'e atarsın
    // this.router.navigate(['/login']);
  }
}