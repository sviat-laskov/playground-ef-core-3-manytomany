using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StudentEntitiesRepository : IStudentEntitiesRepository
    {
        private readonly AppDataContext _appDataContext;
        private readonly DbSet<StudentEntity> _studentEntities;

        public StudentEntitiesRepository(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
            _studentEntities = appDataContext.Students;
        }

        public Task<IQueryable<StudentEntity>> Get() => Task.FromResult(_studentEntities.AsNoTracking());

        public async Task<StudentEntity?> GetById(Guid id, bool includeCourses = false)
        {
            IQueryable<StudentEntity> studentEntities = await Get();
            if (includeCourses)
                studentEntities = studentEntities
                    .Include(entity => entity.CourseStudents)
                    .ThenInclude(courseStudentEntity => courseStudentEntity.Course);

            StudentEntity? studentEntity = await studentEntities.SingleOrDefaultAsync(entity => entity.Id == id);

            return studentEntity;
        }

        public async Task<bool> DoesExist(Guid id) => await (await Get()).Select(entity => entity.Id).ContainsAsync(id);

        public async Task<StudentEntity> Add(StudentEntity studentEntity)
        {
            await _studentEntities.AddAsync(studentEntity);
            try
            {
                await _appDataContext.SaveChangesAsync();
            }
            catch
            {
                if (await DoesExist(studentEntity.Id)) throw new ArgumentOutOfRangeException(nameof(studentEntity), studentEntity, "Student with specified id already exists.");
                throw;
            }

            return studentEntity;
        }

        public ValueTask DisposeAsync() => _appDataContext.DisposeAsync();
    }
}