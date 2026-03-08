namespace OgrenciBilgiSistemiProject.DTOs;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}

public class DepartmentCreateDto
{
    public string Name { get; set; }
}