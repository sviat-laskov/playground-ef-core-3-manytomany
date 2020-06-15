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
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        private readonly IMapper _mapper;

        public CoursesController(ICoursesService coursesService, IMapper mapper)
        {
            _coursesService = coursesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            IEnumerable<Course> courses = await _coursesService.Get();

            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        [HttpGet("{id}")]
        public async Task<CourseDTO> GetById(Guid id, bool includeStudents = true)
        {
            Course? course = await _coursesService.GetById(id, includeStudents);
            CourseDTO courseDTO = course != null ? _mapper.Map<CourseDTO>(course) : throw new ArgumentOutOfRangeException(nameof(id), id, "Course with specified id does not exist.");

            return courseDTO;
        }

        [HttpPut]
        public async Task<CourseDTO> AddCourse(CourseWithoutStudentsDTO courseWithoutStudentsDTO)
        {
            var course = _mapper.Map<Course>(courseWithoutStudentsDTO);

            course = await _coursesService.Add(course);
            var courseDTO = _mapper.Map<CourseDTO>(course);

            return courseDTO;
        }

        [HttpPost("students")]
        public Task AssignStudentToCourse(Guid courseId, Guid studentId) => _coursesService.AssignStudentToCourse(courseId, studentId);
    }
}