using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.DTOs;
using OgrenciBilgiSistemiProject.Models;

namespace OgrenciBilgiSistemiProject.Services
{
    public class DepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentResponseDto>> GetAll()
        {
            return await _context.Departments
                .Select(d => new DepartmentResponseDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    IsDeleted = !d.IsActive
                }).ToListAsync();
        }

        public async Task Create(DepartmentCreateDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }
    }
}