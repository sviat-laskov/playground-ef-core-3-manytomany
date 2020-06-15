using System;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories.Interfaces.Base;

namespace Data.Repositories.Interfaces
{
    public interface IStudentEntitiesRepository : IGettableRepository<StudentEntity>, IExistingByIdRepository<StudentEntity>, IAddableRepository<StudentEntity>, IAsyncDisposable
    {
        public Task<StudentEntity?> GetById(Guid id, bool includeCourses = false);
    }
}