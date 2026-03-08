import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DepartmentService } from '../../services/department';
import { CourseService } from '../../services/course';
import { TeacherService } from '../../services/teacher';
import { StudentService } from '../../services/student';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel.html',
  styleUrls: ['./admin-panel.scss']
})
export class AdminPanelComponent implements OnInit {
deleteDepartment(arg0: any) {
throw new Error('Method not implemented.');
}
updateDepartment(arg0: any) {
throw new Error('Method not implemented.');
}
updateCourse(arg0: any) {
throw new Error('Method not implemented.');
}
deleteCourse(arg0: any) {
throw new Error('Method not implemented.');
}
deleteTeacher(arg0: any) {
throw new Error('Method not implemented.');
}
updateTeacher(arg0: any) {
throw new Error('Method not implemented.');
}

  activeTab: string = 'departments';

  // -------------------------
  // MODELLER (Eksiksiz)
  // -------------------------

  newDepartment = { name: '' };

  newCourse = {
    name: '',
    code: '',
    akts: 0,
    credit: 0,
    departmentId: 0
  };

  newTeacher = {
    fullName: '',
    email: '',
    departmentId: 0
  };

  newStudent = {
    fullName: '',
    studentNumber: '',
    userId: 0,
    departmentId: 0,
    email: ''
  };

  // -------------------------
  // DATA LİSTELERİ
  // -------------------------
  departments: any[] = [];
  courses: any[] = [];
  teachers: any[] = [];
  students: any[] = [];

  constructor(
    private departmentService: DepartmentService,
    private courseService: CourseService,
    private teacherService: TeacherService,
    private studentService: StudentService
  ) {}

  ngOnInit() {
    this.getDepartments();
    this.getCourses();
    this.getTeachers();
    this.getStudents();
  }

  setTab(tab: string) {
    this.activeTab = tab;
  }

  // =========================
  // DEPARTMENTS
  // =========================
  getDepartments() {
    this.departmentService.getDepartments().subscribe({
      next: data => this.departments = data,
      error: err => console.error("Departman hatası:", err)
    });
  }

  addDepartment() {
    if (!this.newDepartment.name) return;
    const dto = { Name: this.newDepartment.name };
    this.departmentService.addDepartment(dto).subscribe({
      next: () => {
        this.newDepartment.name = '';
        this.getDepartments();
      },
      error: err => console.error("Departman eklenemedi:", err)
    });
  }

  // =========================
  // COURSES
  // =========================
  getCourses() {
    this.courseService.getCourses().subscribe({
      next: data => this.courses = data,
      error: err => console.error("Course hatası:", err)
    });
  }

  addCourse() {
    if (!this.newCourse.name) return;
    const dto = {
      Name: this.newCourse.name,
      Code: this.newCourse.code,
      Akts: this.newCourse.akts,
      DepartmentId: this.newCourse.departmentId
    };
    this.courseService.addCourse(dto).subscribe({
      next: () => {
        this.newCourse = { name: '', code: '', akts: 0, credit: 0, departmentId: 0 };
        this.getCourses();
      },
      error: err => console.error("Course eklenemedi:", err)
    });
  }

  // =========================
  // TEACHERS
  // =========================
  getTeachers() {
    this.teacherService.getTeachers().subscribe({
      next: data => this.teachers = data,
      error: err => console.error("Teacher hatası:", err)
    });
  }

  addTeacher() {
  // 1. Alanların dolu olup olmadığını kontrol ediyoruz
  if (!this.newTeacher.fullName || !this.newTeacher.email || this.newTeacher.departmentId == 0) {
    alert("Lütfen Ad Soyad, E-posta girin ve geçerli bir Bölüm seçin!");
    return;
  }

  // 2. C# Backend'ine göndereceğimiz veriyi hazırlıyoruz (Değişken isimleri Backend ile birebir aynı olmalı)
  const dto = {
    fullName: this.newTeacher.fullName,
    email: this.newTeacher.email,
    departmentId: Number(this.newTeacher.departmentId) // Select'ten metin gelme ihtimaline karşı garantiye alıp sayıya çeviriyoruz
  };

  console.log("Backend'e yollamaya çalıştığımız veri:", dto); // Test için konsola yazdırıyoruz

  // 3. Servis üzerinden API'ye yolluyoruz
  this.teacherService.addTeacher(dto).subscribe({
    next: (response) => {
      alert("Öğretmen başarıyla eklendi!");
      // Formu temizle ve listeyi yenile
      this.newTeacher = { fullName: '', email: '', departmentId: 0 };
      this.getTeachers();
    },
    error: (err ) => {
      // Hata durumunda detaylı log basıyoruz ki sorunu bulabilelim
      console.error("Ekleme işlemi başarısız. Detay:", err);
      alert("Eklenirken hata oluştu! Lütfen klavyeden F12'ye basıp Console sekmesindeki kırmızı yazıyı kontrol et.");
    }
  });
}

  // =========================
  // STUDENTS
  // =========================
  getStudents() {
    this.studentService.getStudents().subscribe({
      next: data => this.students = data,
      error: err => console.error("Student hatası:", err)
    });
  }

 // admin-panel.component.ts içinde addStudent metodu
addStudent() {
    const dto = {
        studentNumber: this.newStudent.studentNumber,
        email: this.newStudent.email,
        departmentId: Number(this.newStudent.departmentId), // Sayı olduğundan emin ol
        fullName: this.newStudent.fullName
    };

    console.log("Backend'e giden veri:", dto); // Bunu konsolda kontrol et

    this.studentService.addStudent(dto).subscribe({
        next: (res) => {
            alert("Öğrenci eklendi!");
            this.getStudents();
        },
        error: (err) => {
            // Backend'den gelen detaylı hata mesajını burada göreceğiz
            console.error("Hata detayı:", err.error.message);
            alert(err.error.message); 
        }
    });
} 
  updateStudent(id: any) {
    // Güncelleme mantığı buraya
  }

  deleteStudent(id: any) {
    // Silme mantığı buraya
  }
}