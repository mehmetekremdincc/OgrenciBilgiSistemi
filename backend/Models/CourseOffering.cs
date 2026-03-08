namespace OgrenciBilgiSistemiProject.Models
{
    public class CourseOffering : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int Year { get; set; }
        public int TermType { get; set; } // 1: Fall, 2: Spring, 3: Summer  

        public ICollection<StudentCourseOffering> StudentCourseOfferings { get; set; } = new List<StudentCourseOffering>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}