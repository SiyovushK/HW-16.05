using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository(DataContext context) : IBaseRepository<Student, int>
{
    public async Task<int> AddAsync(Student entity)
    {
        await context.Students.AddAsync(entity);
        var result = await context.SaveChangesAsync();
        return result;
    }

    public async Task<int> DeleteAsync(Student entity)
    {
        context.Students.Remove(entity);
        var result = await context.SaveChangesAsync();
        return result;
    }

    public Task<IQueryable<Student>> GetAllAsync()
    {
        var students = context.Students.AsQueryable();
        return Task.FromResult(students);
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
        return student; 
    }

    public async Task<int> UpdateAsync(Student entity)
    {
        var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == entity.StudentId);

        student!.FirstName = entity.FirstName;
        student.LastName = entity.LastName;
        student.BirthDate = entity.BirthDate;

        var result = await context.SaveChangesAsync();
        return result;
    }
}