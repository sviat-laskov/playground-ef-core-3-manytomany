using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.Repositories.Interfaces;
using Domain.Models;
using Domain.Services.Interfaces;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Domain.Services
{
    public class CoursesService : ICoursesService, IAsyncDisposable
    {
        private readonly ICourseEntitiesRepository _courseEntitiesRepository;
        private readonly IMapper _mapper;

        public CoursesService(IMapper mapper, ICourseEntitiesRepository courseEntitiesRepository)
        {
            _mapper = mapper;
            _courseEntitiesRepository = courseEntitiesRepository;
        }

        public ValueTask DisposeAsync() => _courseEntitiesRepository.DisposeAsync();

        public async Task<IEnumerable<Course>> Get() => (await _courseEntitiesRepository.Get()).ProjectTo<Course>(_mapper.ConfigurationProvider);

        public async Task<Course?> GetById(Guid id, bool includeStudents = false)
        {
            CourseEntity? courseEntity = await _courseEntitiesRepository.GetById(id, includeStudents);
            Course? course = courseEntity != null ? _mapper.Map<Course>(courseEntity) : null;

            return course;
        }

        public async Task<bool> DoesExist(Guid id) => await _courseEntitiesRepository.DoesExist(id);

        public async Task<Course> Add(Course course)
        {
            course.GenerateIdIfDefault();
            Validator.ValidateObject(course, new ValidationContext(course), validateAllProperties: true);

            var courseEntity = _mapper.Map<CourseEntity>(course);
            await _courseEntitiesRepository.Add(courseEntity);

            return course;
        }

        public Task AssignStudentToCourse(Guid courseId, Guid studentId) => _courseEntitiesRepository.AssignStudentToCourse(courseId, studentId);
    }
}