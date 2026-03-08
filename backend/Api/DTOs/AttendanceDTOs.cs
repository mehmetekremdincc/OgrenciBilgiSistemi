namespace OgrenciBilgiSistemiProject.DTOs;

public class AttendanceResponseDto 
{
    public int Id { get; set; }
    public int StudentCourseOfferingId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
}

public class AttendanceCreateDto
{
    public int StudentCourseOfferingId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; } // 0: Yok, 1: Var, 2: Raporlu
}