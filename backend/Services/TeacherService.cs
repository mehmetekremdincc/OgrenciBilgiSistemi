using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.DTOs;
using OgrenciBilgiSistemiProject.Models;

namespace OgrenciBilgiSistemiProject.Services
{
    public class TeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherResponseDto>> GetAll()
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Select(t => new TeacherResponseDto
                {
                    Id = t.Id,
                    Email = t.User.Email,
                    DepartmentId = t.DepartmentId,
                    IsDeleted = !t.IsActive
                }).ToListAsync();
        }

        public async Task Create(TeacherCreateDto dto)
        {
            var teacher = new Teacher
            {
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }
    }
}   