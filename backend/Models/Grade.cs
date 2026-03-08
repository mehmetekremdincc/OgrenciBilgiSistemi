namespace OgrenciBilgiSistemiProject.Models
{
    public class Grade : BaseEntity
    {
        public int StudentCourseOfferingId { get; set; }
        public StudentCourseOffering StudentCourseOffering { get; set; } = null!;

        public double Value { get; set; } // 0-100 arası
        public decimal Midterm { get; set; }
        public decimal Final { get; set; }
        public decimal Average { get; set; }
        public string LetterGrade { get; set; } = string.Empty; // Harf notu
    }
}