namespace Domain.DTOs.StudentDTOs;

public class CreateStudentDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}