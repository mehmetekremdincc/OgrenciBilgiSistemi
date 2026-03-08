namespace OgrenciBilgiSistemiProject.Models
{
    public class Student : BaseEntity
    {
        public string StudentNumber { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string FullName { get; set; }

        public ICollection<StudentCourseOffering> StudentCourseOfferings { get; set; } = new List<StudentCourseOffering>();
    }
}