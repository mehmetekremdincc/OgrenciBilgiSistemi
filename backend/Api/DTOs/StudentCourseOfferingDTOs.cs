namespace OgrenciBilgiSistemiProject.DTOs;

public class StudentCourseOfferingResponseDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseOfferingId { get; set; }
    public bool IsDeleted { get; set; } 
}

public class StudentCourseOfferingCreateDto
{
    public int StudentId { get; set; }
    public int CourseOfferingId { get; set; }
}