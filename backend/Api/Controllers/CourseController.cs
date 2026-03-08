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
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CourseController(AppDbContext context, IMapper mapper) 
        { 
            _context = context; 
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses
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

            return Ok(courses);
        }

      

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto dto)
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
            return Ok("Course created");
        }
    }
}