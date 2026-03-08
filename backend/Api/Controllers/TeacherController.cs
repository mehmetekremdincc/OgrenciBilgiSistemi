using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;

namespace OgrenciBilgiSistemiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _context.Teachers
                .Include(t => t.User)
                .Select(t => new TeacherResponseDto
                {
                    Id = t.Id,
                    Email = t.User != null ? t.User.Email : "",
                    FullName = t.FullName,
                    DepartmentId = t.DepartmentId,
                    IsDeleted = !t.IsActive
                }).ToListAsync();

            return Ok(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateDto dto)
        {
            // 1. Önce User tablosuna ekliyoruz (Giriş bilgileri)
            var newUser = new User
            {
                Email = dto.Email,
                PasswordHash = "123456", // Sabit şifre
                RoleId = 2, // Öğretmen Rolü
                PasswordSalt = Array.Empty<byte>() // Boş bırakılmaz
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // 2. Teacher tablosuna ekliyoruz (Profil bilgileri)
            var teacher = new Teacher
            {
                UserId = newUser.Id,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                FullName = dto.FullName
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Öğretmen başarıyla eklendi." });
        }


        // api/Teacher/dashboard-courses?teacherId=1
        [HttpGet("dashboard-courses")]
        public async Task<IActionResult> GetDashboardCourses(int teacherId)
        {
            var courses = await _context.CourseOfferings
                .Where(co => co.TeacherId == teacherId && co.IsActive)
                .Include(co => co.Course)
                .Select(co => new {
                    Id = co.Id,
                    Name = co.Course.Name,
                    Code = co.Course.Code,
                    Akts = co.Course.Akts
                }).ToListAsync();
            return Ok(courses);
        }

        // api/Teacher/dashboard-students/{courseOfferingId}
        [HttpGet("dashboard-students/{courseOfferingId}")]
        public async Task<IActionResult> GetDashboardStudents(int courseOfferingId)
        {
            var students = await _context.StudentCourseOfferings
                .Where(sco => sco.CourseOfferingId == courseOfferingId)
                .Include(sco => sco.Student)
                .Include(sco => sco.Grade)
                .Select(sco => new CourseStudentResponseDto
                {
                    Id = sco.Id, // SCO ID
                    StudentId = sco.Student.Id,
                    FullName = sco.Student.FullName,
                    Midterm = (double?)(sco.Grade != null ? sco.Grade.Midterm : 0),
                    Final = (double?)(sco.Grade != null ? sco.Grade.Final : 0)
                }).ToListAsync();
            return Ok(students);
        }

        // api/Teacher/dashboard-bulk-update
        [HttpPost("dashboard-bulk-update")]
        public async Task<IActionResult> DashboardBulkUpdate([FromBody] List<GradeUpdateDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var grade = await _context.Grades
                    .FirstOrDefaultAsync(g => g.StudentCourseOfferingId == dto.ScoId);
                if (grade != null)
                {
                    grade.Midterm = dto.Midterm;
                    grade.Final = dto.Final;
                }
                else
                {
                    _context.Grades.Add(new Grade { StudentCourseOfferingId = dto.ScoId, Midterm = dto.Midterm, Final = dto.Final });
                }
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "Başarılı" });
        }


    }
}