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
    public class StudentsService : IStudentsService, IAsyncDisposable
    {
        private readonly IMapper _mapper;
        private readonly IStudentEntitiesRepository _studentEntitiesRepository;

        public StudentsService(IMapper mapper, IStudentEntitiesRepository studentEntitiesRepository)
        {
            _mapper = mapper;
            _studentEntitiesRepository = studentEntitiesRepository;
        }

        public ValueTask DisposeAsync() => _studentEntitiesRepository.DisposeAsync();

        public async Task<IEnumerable<Student>> Get() => (await _studentEntitiesRepository.Get()).ProjectTo<Student>(_mapper.ConfigurationProvider);

        public async Task<Student?> GetById(Guid id, bool includeCourses = false)
        {
            StudentEntity? studentEntity = await _studentEntitiesRepository.GetById(id, includeCourses);
            Student? student = studentEntity != null ? _mapper.Map<Student>(studentEntity) : null;

            return student;
        }

        public async Task<bool> DoesExist(Guid id) => await _studentEntitiesRepository.DoesExist(id);

        public async Task<Student> Add(Student student)
        {
            student.GenerateIdIfDefault();
            Validator.ValidateObject(student, new ValidationContext(student), validateAllProperties: true);

            var studentEntity = _mapper.Map<StudentEntity>(student);
            await _studentEntitiesRepository.Add(studentEntity);

            return student;
        }
    }
}