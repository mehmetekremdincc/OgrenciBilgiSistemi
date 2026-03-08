using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemiProject.Data;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;
using AutoMapper;

namespace OgrenciBilgiSistemiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AttendanceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance(AttendanceCreateDto dto)
        {
            var attendance = new Attendance
            {
                StudentCourseOfferingId = dto.StudentCourseOfferingId,
                Date = dto.Date,
                Status = dto.Status,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
            return Ok("Attendance recorded");
        }
    }
}