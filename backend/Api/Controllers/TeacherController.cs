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
            var newUser = new User
            {
                Email = dto.Email,
                PasswordHash = "123456",
                RoleId = 2,
                PasswordSalt = Array.Empty<byte>()
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

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




        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile(int teacherId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null) return NotFound(new { message = "Öğretmen bulunamadı." });

            return Ok(new
            {
                FullName = teacher.FullName,
                Email = teacher.User != null ? teacher.User.Email : "Email yok"
            });
        }


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
        [HttpGet("dashboard-my-students")]
        public async Task<IActionResult> GetMyStudents(int teacherId)
        {
        
            var students = await _context.StudentCourseOfferings
                .Include(sco => sco.Student)
                .ThenInclude(s => s.User)
                .Where(sco => sco.CourseOffering.TeacherId == teacherId)
                .Select(sco => new
                {
                    Id = sco.Student.Id,
                    FullName = sco.Student.FullName,
                    Email = sco.Student.User != null ? sco.Student.User.Email : ""
                })
                .Distinct()
                .ToListAsync();

            return Ok(students);
        }

        [HttpGet("dashboard-students/{courseOfferingId}")]
        public async Task<IActionResult> GetDashboardStudents(int courseOfferingId)
        {
            var students = await _context.StudentCourseOfferings
                .Where(sco => sco.CourseOfferingId == courseOfferingId)
                .Include(sco => sco.Student)
                .Include(sco => sco.Grade)
                .Select(sco => new CourseStudentResponseDto
                {
                    Id = sco.Id,
                    StudentId = sco.Student.Id,
                    FullName = sco.Student.FullName,
                    Midterm = (double?)(sco.Grade != null ? sco.Grade.Midterm : 0),
                    Final = (double?)(sco.Grade != null ? sco.Grade.Final : 0)
                }).ToListAsync();
            return Ok(students);
        }

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
            return Ok(new { message = "Notlar başarıyla kaydedildi." });
        }
    }
}