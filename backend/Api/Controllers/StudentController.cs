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
    }
}