namespace OgrenciBilgiSistemiProject.Models
{
    public class Course : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Credit { get; set; }
        public int Akts { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
    }
}