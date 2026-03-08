namespace OgrenciBilgiSistemiProject.DTOs;

public class CourseResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Credit { get; set; }
    public int Akts { get; set; }
    public int DepartmentId { get; set; }
    public bool IsDeleted { get; set; }
}

public class CourseCreateDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Credit { get; set; }
    public int Akts { get; set; }
    public int DepartmentId { get; set; }
}