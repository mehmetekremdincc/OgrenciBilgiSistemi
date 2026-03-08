namespace OgrenciBilgiSistemiProject.DTOs;

public class GradeResponseDto 
{
    public int Id { get; set; }
    public int StudentCourseOfferingId { get; set; }
    public decimal Midterm { get; set; }
    public decimal Final { get; set; }
    public decimal Average { get; set; }
    public string LetterGrade { get; set; }
    public bool IsDeleted { get; set; }
}

public class GradeCreateDto
{
    public int StudentCourseOfferingId { get; set; }
    public decimal Midterm { get; set; }
    public decimal Final { get; set; }
    public decimal Average { get; set; }
    public string LetterGrade { get; set; } = string.Empty;
}

public class GradeUpdateDto
{
    public decimal Midterm { get; set; }
    public decimal Final { get; set; }
    public int ScoId { get; set; }
}
public class CourseStudentResponseDto
{
    public int Id { get; set; } // Bu ScoId olacak (Frontend'in güncelleme yapabilmesi için)
    public int StudentId { get; set; }
    public string FullName { get; set; }
    public double? Midterm { get; set; }
    public double? Final { get; set; }
}