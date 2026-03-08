namespace OgrenciBilgiSistemiProject.Models
{ 
    public class Schedule : BaseEntity
    {
        public int CourseOfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;

        public string DayOfWeek { get; set; } = string.Empty; // "Monday", "Tuesday", etc.
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; } = string.Empty;
    }
}