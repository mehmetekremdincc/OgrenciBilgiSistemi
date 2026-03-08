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

  activeTab: string = 'students';

  // MODELLER
  newDepartment = { name: '' };
  newCourse = { name: '', code: '', akts: 0, credit: 0, departmentId: 0 };
  newTeacher = { fullName: '', email: '', departmentId: 0 };
  newStudent = { fullName: '', studentNumber: '', email: '', departmentId: 0 };

  // DATA LİSTELERİ
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
    if (!this.newTeacher.fullName || !this.newTeacher.email || this.newTeacher.departmentId == 0) {
      alert("Lütfen tüm alanları doldurun!");
      return;
    }
    const dto = {
      fullName: this.newTeacher.fullName,
      email: this.newTeacher.email,
      departmentId: Number(this.newTeacher.departmentId)
    };
    this.teacherService.addTeacher(dto).subscribe({
      next: () => {
        this.newTeacher = { fullName: '', email: '', departmentId: 0 };
        this.getTeachers();
        alert("Öğretmen eklendi!");
      },
      error: err => console.error("Teacher eklenemedi:", err)
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

  addStudent() {
    if (!this.newStudent.fullName || !this.newStudent.studentNumber || !this.newStudent.email || this.newStudent.departmentId == 0) {
      alert("Lütfen Ad Soyad, Öğrenci No, E-posta ve Bölüm girin!");
      return;
    }
    const dto = {
      fullName: this.newStudent.fullName,
      studentNumber: this.newStudent.studentNumber,
      email: this.newStudent.email,
      departmentId: Number(this.newStudent.departmentId)
    };
    this.studentService.addStudent(dto).subscribe({
      next: () => {
        this.newStudent = { fullName: '', studentNumber: '', email: '', departmentId: 0 };
        this.getStudents();
        alert("Öğrenci eklendi!");
      },
      error: err => {
        console.error("Student eklenemedi:", err);
        alert("Eklenirken hata oluştu!");
      }
    });
  }

  updateStudent(id: any) {
    console.log("Güncellenecek Öğrenci ID:", id);
  }

  deleteStudent(id: any) {
    console.log("Silinecek Öğrenci ID:", id);
  }
}