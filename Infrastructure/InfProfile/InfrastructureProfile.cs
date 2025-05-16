using AutoMapper;
using Domain.DTOs.StudentDTOs;
using Domain.Entities;

namespace Infrastructure.InfProfile;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Student, GetStudentDTO>();
        CreateMap<CreateStudentDTO, Student>();
    }
}