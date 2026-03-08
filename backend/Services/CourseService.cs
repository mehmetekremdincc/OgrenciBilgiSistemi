using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.DTOs;
using OgrenciBilgiSistemiProject.Models;

namespace OgrenciBilgiSistemiProject.Services
{
    public class CourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseResponseDto>> GetAll()
        {
            return await _context.Courses
                .Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Credit = c.Credit,
                    Akts = c.Akts,
                    DepartmentId = c.DepartmentId,
                    IsDeleted = !c.IsActive
                }).ToListAsync();
        }

        public async Task Create(CourseCreateDto dto)
        {
            var course = new Course
            {
                Code = dto.Code,
                Name = dto.Name,
                Credit = dto.Credit,
                Akts = dto.Akts,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
    }
}