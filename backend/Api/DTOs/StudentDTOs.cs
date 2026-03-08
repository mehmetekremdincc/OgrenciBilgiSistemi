namespace OgrenciBilgiSistemiProject.DTOs
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class StudentCreateDto
    {
        public string StudentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string FullName { get; set; } = string.Empty;
    }

    public class StudentUpdateDto
    {
        public string StudentNumber { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}