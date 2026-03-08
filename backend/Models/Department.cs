namespace OgrenciBilgiSistemiProject.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}