using System.Linq;
using AutoMapper;
using Data.Models;
using Domain.Models;

namespace Domain
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<StudentEntity, Student>()
                .ForMember(dest => dest.Courses, options => options
                    .MapFrom(src => src.CourseStudents
                        .Where(courseStudentEntity => courseStudentEntity.StudentId == src.Id)
                        .Select(courseStudentEntity => courseStudentEntity.Course)))
                .ReverseMap();
            CreateMap<CourseEntity, Course>()
                .ForMember(dest => dest.Students, options => options
                    .MapFrom(src => src.CourseStudents
                        .Where(courseStudentEntity => courseStudentEntity.CourseId == src.Id)
                        .Select(courseStudentEntity => courseStudentEntity.Student)))
                .ReverseMap();
        }
    }
}