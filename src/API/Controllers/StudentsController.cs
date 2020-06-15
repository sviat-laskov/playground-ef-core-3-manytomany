using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService, IMapper mapper)
        {
            _studentsService = studentsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            IEnumerable<Student> students = await _studentsService.Get();

            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        [HttpGet("{id}")]
        public async Task<StudentDTO> GetById(Guid id, bool includeCourses = true)
        {
            Student? student = await _studentsService.GetById(id, includeCourses);
            StudentDTO studentDTO = student != null ? _mapper.Map<StudentDTO>(student) : throw new ArgumentOutOfRangeException(nameof(id), id, "Student with specified id does not exist.");

            return studentDTO;
        }

        [HttpPut]
        public async Task<StudentDTO> AddStudent(StudentWithoutCoursesDTO studentWithoutCoursesDTO)
        {
            var student = _mapper.Map<Student>(studentWithoutCoursesDTO);

            student = await _studentsService.Add(student);
            var studentDTO = _mapper.Map<StudentDTO>(student);

            return studentDTO;
        }
    }
}