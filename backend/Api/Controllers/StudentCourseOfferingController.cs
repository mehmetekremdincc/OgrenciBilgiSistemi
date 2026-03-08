using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;
using AutoMapper;

namespace OgrenciBilgiSistemiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseOfferingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentCourseOfferingController(AppDbContext context, IMapper mapper) 
        { 
            _context = context; 
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(StudentCourseOfferingCreateDto dto)
        {
            var enrollment = new StudentCourseOffering
            {
                StudentId = dto.StudentId,
                CourseOfferingId = dto.CourseOfferingId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.StudentCourseOfferings.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok("Student enrolled to course");
        }
    }
}