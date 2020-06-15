using System;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories.Interfaces.Base;

namespace Data.Repositories.Interfaces
{
    public interface ICourseEntitiesRepository : IGettableRepository<CourseEntity>, IExistingByIdRepository<CourseEntity>, IAddableRepository<CourseEntity>, IAsyncDisposable
    {
        public Task<CourseEntity?> GetById(Guid id, bool includeStudents = false);

        public Task AssignStudentToCourse(Guid courseId, Guid studentId);
    }
}