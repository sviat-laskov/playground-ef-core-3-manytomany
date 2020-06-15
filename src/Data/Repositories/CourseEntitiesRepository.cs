using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CourseEntitiesRepository : ICourseEntitiesRepository
    {
        private readonly AppDataContext _appDataContext;
        private readonly DbSet<CourseEntity> _courseEntities;
        private readonly IStudentEntitiesRepository _studentEntitiesRepository;

        public CourseEntitiesRepository(AppDataContext appDataContext, IStudentEntitiesRepository studentEntitiesRepository)
        {
            _appDataContext = appDataContext;
            _courseEntities = appDataContext.Courses;
            _studentEntitiesRepository = studentEntitiesRepository;
        }

        public Task<IQueryable<CourseEntity>> Get() => Task.FromResult(_courseEntities.AsNoTracking());

        public async Task<CourseEntity?> GetById(Guid id, bool includeStudents = false)
        {
            IQueryable<CourseEntity> courseEntities = await Get();
            if (includeStudents)
                courseEntities = courseEntities
                    .Include(entity => entity.CourseStudents)
                    .ThenInclude(courseStudentEntity => courseStudentEntity.Student);

            CourseEntity? courseEntity = await courseEntities.SingleOrDefaultAsync(entity => entity.Id == id);

            return courseEntity;
        }

        public async Task<bool> DoesExist(Guid id) => await (await Get()).Select(entity => entity.Id).ContainsAsync(id);

        public async Task<CourseEntity> Add(CourseEntity courseEntity)
        {
            await _courseEntities.AddAsync(courseEntity);
            try
            {
                await _appDataContext.SaveChangesAsync();
            }
            catch
            {
                if (await DoesExist(courseEntity.Id)) throw new ArgumentOutOfRangeException(nameof(courseEntity), courseEntity, "Course with specified id already exists.");
                throw;
            }

            return courseEntity;
        }

        public async Task AssignStudentToCourse(Guid courseId, Guid studentId)
        {
            var courseStudentEntity = new CourseStudentEntity
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await _appDataContext.CourseStudents.AddAsync(courseStudentEntity);
            try
            {
                await _appDataContext.SaveChangesAsync();
            }
            catch
            {
                if (await _appDataContext.CourseStudents.SingleOrDefaultAsync(entity => entity.CourseId == courseId && entity.StudentId == studentId) != null) throw new ArgumentOutOfRangeException(nameof(studentId), studentId, "Specified student is already assigned to this course.");

                if (!await DoesExist(courseId)) throw new ArgumentOutOfRangeException(nameof(courseId), courseId, "Course with specified id does not exist.");
                if (!await _studentEntitiesRepository.DoesExist(studentId)) throw new ArgumentOutOfRangeException(nameof(studentId), studentId, "Student with specified id does not exist.");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _appDataContext.DisposeAsync();
            await _studentEntitiesRepository.DisposeAsync();
        }
    }
}