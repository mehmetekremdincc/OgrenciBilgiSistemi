using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.DTOs;
using OgrenciBilgiSistemiProject.Models;

namespace OgrenciBilgiSistemiProject.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentResponseDto>> GetAll()
        {
            return await _context.Students
                .Include(s => s.User)
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    StudentNumber = s.StudentNumber,
                    Email = s.User.Email,
                    DepartmentId = s.DepartmentId,
                    IsDeleted = !s.IsActive
                }).ToListAsync();
        }

        public async Task<StudentResponseDto?> GetById(int id)
        {
            return await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    StudentNumber = s.StudentNumber,
                    Email = s.User.Email,
                    DepartmentId = s.DepartmentId,
                    IsDeleted = !s.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task Create(StudentCreateDto dto)
        {
            var student = new Student
            {
                StudentNumber = dto.StudentNumber,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, StudentUpdateDto dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null) return;

            student.StudentNumber = dto.StudentNumber;
            student.DepartmentId = dto.DepartmentId;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null) return;

            student.IsActive = false;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}