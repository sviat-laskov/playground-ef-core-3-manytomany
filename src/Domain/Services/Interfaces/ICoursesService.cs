using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ICoursesService
    {
        public Task<IEnumerable<Course>> Get();

        public Task<Course?> GetById(Guid id, bool includeStudents = false);

        public Task<bool> DoesExist(Guid id);

        public Task<Course> Add(Course domainEntity);

        public Task AssignStudentToCourse(Guid courseId, Guid studentId);
    }
}