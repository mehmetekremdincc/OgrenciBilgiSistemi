namespace OgrenciBilgiSistemiProject.Models
{
    public class StudentCourseOffering : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseOfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;

        public Grade? Grade { get; set; } // Öğrencinin notu olmayabilir

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}