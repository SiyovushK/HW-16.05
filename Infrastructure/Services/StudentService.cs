using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(IBaseRepository<Student, int> studentRepository, IMapper mapper) : IStudentService
{
    public async Task<Response<GetStudentDTO>> CreateStudent(CreateStudentDTO createStudent)
    {
        var student = mapper.Map<Student>(createStudent);

        var result = await studentRepository.AddAsync(student);

        var getStudentDto = mapper.Map<GetStudentDTO>(student);

        return result == 0
            ? new Response<GetStudentDTO>(HttpStatusCode.BadRequest, "Student created")
            : new Response<GetStudentDTO>(getStudentDto);
    }

    public async Task<Response<GetStudentDTO>> UpdateStudent(int studentId, GetStudentDTO updateStudent)
    {
        var student = await studentRepository.GetByIdAsync(studentId);
        if (student == null)
            return new Response<GetStudentDTO>(HttpStatusCode.NotFound, "Student is not found");
        
        student.FirstName = updateStudent.FirstName;
        student.LastName = updateStudent.LastName;
        student.BirthDate = updateStudent.BirthDate;

        var result = await studentRepository.UpdateAsync(student);

        var getStudentDto = mapper.Map<GetStudentDTO>(student);

        return result == 0
            ? new Response<GetStudentDTO>(HttpStatusCode.BadRequest, "Student not updated")
            : new Response<GetStudentDTO>(getStudentDto);
    }

    public async Task<Response<string>> DeleteStudent(int studentId)
    {
        var student = await studentRepository.GetByIdAsync(studentId);
        if (student == null)
            return new Response<string>(HttpStatusCode.NotFound, "Student is not found");

        var result = await studentRepository.DeleteAsync(student);

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Student wansn't deleted")
            : new Response<string>("Student deleted successfully");
    }

    public async Task<Response<List<GetStudentDTO>>> GetAllStudents()
    {
        var studentsQuery = await studentRepository.GetAllAsync();
        var allStudents = await studentsQuery.ToListAsync();

        if (allStudents.Count == 0)
            return new Response<List<GetStudentDTO>>(HttpStatusCode.NotFound, "No students found");

        var getStudentsDto = mapper.Map<List<GetStudentDTO>>(allStudents);
        
        return new Response<List<GetStudentDTO>>(getStudentsDto);
    }

    public async Task<Response<List<GetStudentDTO>>> GetAllAsync(StudentFilter filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var studentQuery = await studentRepository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            var nameFilter = filter.Name.ToLower();
            studentQuery = studentQuery.Where(s => 
                (s.FirstName + " " + s.LastName).ToLower().Contains(nameFilter));
        }

        if (filter.From != null)
        {
            var year = DateTime.UtcNow.Year;
            studentQuery = studentQuery.Where(s => year - s.BirthDate.Year >= filter.From);
        }

        if (filter.To != null)
        {
            var year = DateTime.UtcNow.Year;
            studentQuery = studentQuery.Where(s => year - s.BirthDate.Year <= filter.From);
        }

        var totalRecords = await studentQuery.CountAsync();

        var student = await studentQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var studentDtos = mapper.Map<List<GetStudentDTO>>(student);

        return new PagedResponse<List<GetStudentDTO>>(studentDtos, pageNumber, pageSize, totalRecords);
    }
}