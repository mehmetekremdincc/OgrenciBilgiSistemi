namespace OgrenciBilgiSistemiProject.DTOs;

public class CourseOfferingResponseDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public int Year { get; set; }
    public int TermType { get; set; }
    public bool IsDeleted { get; set; }
}

public class CourseOfferingCreateDto
{
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public int Year { get; set; }
    public int TermType { get; set; }
}