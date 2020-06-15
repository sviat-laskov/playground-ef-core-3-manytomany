using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IStudentsService
    {
        public Task<IEnumerable<Student>> Get();

        public Task<Student?> GetById(Guid id, bool includeCourses = false);

        public Task<bool> DoesExist(Guid id);

        public Task<Student> Add(Student domainEntity);
    }
}