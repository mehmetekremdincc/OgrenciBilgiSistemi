using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;
using AutoMapper;

namespace OgrenciBilgiSistemiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // --- ADMIN PANEL METOTLARI (DOKUNULMADI) ---

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .Include(s => s.User)
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    StudentNumber = s.StudentNumber,
                    Email = s.User != null ? s.User.Email : "",
                    DepartmentId = s.DepartmentId,
                    IsDeleted = !s.IsActive,
                    FullName = s.FullName
                }).ToListAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    StudentNumber = s.StudentNumber,
                    Email = s.User != null ? s.User.Email : "",
                    DepartmentId = s.DepartmentId,
                    IsDeleted = !s.IsActive,
                    FullName = s.FullName
                }).FirstOrDefaultAsync();

            if (student == null) return NotFound("Öğrenci bulunamadı");
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Önce Kullanıcı Oluştur
                var newUser = new User
                {
                    Email = dto.Email,
                    PasswordHash = "123456",
                    RoleId = 2, // Buradaki sayıyı veritabanındaki doğru Role ID ile değiştir
                    PasswordSalt = Array.Empty<byte>()
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // 2. Sonra Öğrenci Oluştur
                var student = new Student
                {
                    StudentNumber = dto.StudentNumber,
                    UserId = newUser.Id, // Oluşan User ID'yi bağla
                    DepartmentId = dto.DepartmentId,
                    FullName = dto.FullName,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync(); // İkisini birden başarıyla kaydet
                return Ok(new { message = "Öğrenci ve Kullanıcı başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var innerError = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = "Veritabanı Hatası: " + innerError });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound("Öğrenci bulunamadı");

            student.StudentNumber = dto.StudentNumber;
            student.DepartmentId = dto.DepartmentId;
            student.UpdatedAt = DateTime.UtcNow;
            student.FullName = dto.FullName;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Öğrenci güncellendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound("Öğrenci bulunamadı");

            student.IsActive = false;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Öğrenci silindi" });
        }

        // --- ÖĞRENCİ DASHBOARD İÇİN YENİ EKLENEN METOTLAR ---

        // api/Student/get-profile?studentId=1
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return NotFound(new { message = "Öğrenci bulunamadı." });

            return Ok(new
            {
                FullName = student.FullName,
                StudentNumber = student.StudentNumber,
                Email = student.User != null ? student.User.Email : "Email yok",
                DepartmentName = student.Department != null ? student.Department.Name : "Departman Yok"
            });
        }

        // api/Student/my-grades?studentId=1
        [HttpGet("my-grades")]
        public async Task<IActionResult> GetMyGrades(int studentId)
        {
            var grades = await _context.StudentCourseOfferings
                .Where(sco => sco.StudentId == studentId && sco.IsActive)
                .Include(sco => sco.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(sco => sco.CourseOffering)
                    .ThenInclude(co => co.Teacher)
                .Include(sco => sco.Grade)
                .Select(sco => new
                {
                    CourseName = sco.CourseOffering.Course.Name,
                    CourseCode = sco.CourseOffering.Course.Code,
                    TeacherName = sco.CourseOffering.Teacher.FullName,
                    Akts = sco.CourseOffering.Course.Akts,
                    Midterm = sco.Grade != null ? sco.Grade.Midterm : 0,
                    Final = sco.Grade != null ? sco.Grade.Final : 0
                }).ToListAsync();

            return Ok(grades);
        }
    }
}