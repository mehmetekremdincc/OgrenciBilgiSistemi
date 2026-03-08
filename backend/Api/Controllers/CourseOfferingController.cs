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
    public class CourseOfferingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CourseOfferingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offerings = await _context.CourseOfferings
                .Select(o => new CourseOfferingResponseDto
                {
                    Id = o.Id,
                    CourseId = o.CourseId,
                    TeacherId = o.TeacherId,
                    Year = o.Year,
                    TermType = o.TermType,
                    IsDeleted = !o.IsActive
                }).ToListAsync();
            return Ok(offerings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseOfferingCreateDto dto)
        {
            var offering = new CourseOffering
            {
                CourseId = dto.CourseId,
                TeacherId = dto.TeacherId,
                Year = dto.Year,
                TermType = dto.TermType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.CourseOfferings.Add(offering);
            await _context.SaveChangesAsync();
            return Ok("Course offering created");
        }
    }
}