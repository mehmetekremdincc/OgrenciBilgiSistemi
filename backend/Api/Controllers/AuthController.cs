using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemiProject.DTOs;
using OgrenciBilgiSistemiProject.Services;
using OgrenciBilgiSistemiProject.Data; // Eklendi
using System.Linq;

namespace OgrenciBilgiSistemiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AppDbContext _context; // Veri çekmek için eklendi

        public AuthController(AuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _authService.Login(request);
            if (result == null)
                return Unauthorized(new { message = "Email veya şifre hatalı" });

            return Ok(result);
        }

        // Admin Paneli İçin Eksik Olan 404 Veren Yollar Buraya Eklendi:

        [HttpGet("admin/courses")]
        public IActionResult GetCourses() => Ok(_context.Courses.ToList());

        [HttpGet("admin/teachers")]
        public IActionResult GetTeachers() => Ok(_context.Teachers.ToList());

        [HttpGet("admin/students")]
        public IActionResult GetStudents() => Ok(_context.Students.ToList());

        [Authorize(Roles = "Teacher")]
        [HttpGet("adminpanel")]
        public IActionResult AdminPanel()
        {
            return Ok("Sadece Teacher rolü erişebilir.");
        }
    }
}