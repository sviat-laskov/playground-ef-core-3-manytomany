using API.Models;
using AutoMapper;
using Domain.Models;

namespace API
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<Student, StudentWithoutCoursesDTO>().ReverseMap();
            CreateMap<Course, CourseWithoutStudentsDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().IncludeBase<Student, StudentWithoutCoursesDTO>();
            CreateMap<Course, CourseDTO>().IncludeBase<Course, CourseWithoutStudentsDTO>();
        }
    }
}