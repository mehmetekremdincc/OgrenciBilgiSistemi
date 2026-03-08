using AutoMapper;
using OgrenciBilgiSistemiProject.Models;
using OgrenciBilgiSistemiProject.DTOs;

namespace OgrenciBilgiSistemiProject.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentResponseDto>();
            CreateMap<StudentCreateDto, Student>();

            CreateMap<Course, CourseResponseDto>();
            CreateMap<CourseCreateDto, Course>();

            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<DepartmentCreateDto, Department>();

            CreateMap<Teacher, TeacherResponseDto>();
            CreateMap<TeacherCreateDto, Teacher>();

            CreateMap<CourseOffering, CourseOfferingResponseDto>();
            CreateMap<CourseOfferingCreateDto, CourseOffering>();

            CreateMap<StudentCourseOffering, StudentCourseOfferingResponseDto>();
            CreateMap<StudentCourseOfferingCreateDto, StudentCourseOffering>();

            CreateMap<Attendance, AttendanceResponseDto>();
            CreateMap<AttendanceCreateDto, Attendance>();

            CreateMap<Grade, GradeResponseDto>();
            CreateMap<GradeCreateDto, Grade>();
        }
    }
}