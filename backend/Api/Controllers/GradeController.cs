using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;
using AutoMapper;

namespace OgrenciBilgiSistemiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GradeController(AppDbContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(GradeCreateDto dto)
        {
            var grade = new Grade
            {
                StudentCourseOfferingId = dto.StudentCourseOfferingId,
                Midterm = dto.Midterm,
                Final = dto.Final,
                Average = dto.Average,
                LetterGrade = dto.LetterGrade,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok("Grade added");
        }
    }
}