namespace OgrenciBilgiSistemiProject.Models
{
    public class Attendance : BaseEntity
    {
        public int StudentCourseOfferingId { get; set; }
        public StudentCourseOffering StudentCourseOffering { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; } // 0: Yok, 1: Var, 2: Raporlu
    }
}