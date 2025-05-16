using Domain.DTOs.StudentDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<GetStudentDTO>> CreateStudent(CreateStudentDTO createStudent);
    Task<Response<GetStudentDTO>> UpdateStudent(int studentId, GetStudentDTO updateStudent);
    Task<Response<string>> DeleteStudent(int StudentId);
    Task<Response<List<GetStudentDTO>>> GetAllStudents();
    Task<Response<List<GetStudentDTO>>> GetAllAsync(StudentFilter filter);
}