namespace Domain.DTOs.StudentDTOs;

public class StudentsWithCourseCount
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CourseCount { get; set; }
}