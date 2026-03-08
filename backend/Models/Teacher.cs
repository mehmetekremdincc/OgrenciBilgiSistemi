namespace OgrenciBilgiSistemiProject.Models
{
    public class Teacher : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public string FullName { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
    }
}