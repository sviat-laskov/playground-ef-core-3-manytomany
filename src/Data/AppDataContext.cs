using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<CourseEntity> Courses { get; set; }

        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<CourseStudentEntity> CourseStudents { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentConfiguration

            modelBuilder.Entity<CourseStudentEntity>().HasKey(courseStudentEntity => new
            {
                courseStudentEntity.StudentId,
                courseStudentEntity.CourseId
            });

            #endregion

            #region SeedConfiguration

            CourseEntity mathCourseEntity = new CourseEntity
                         {
                             Id = Guid.NewGuid(),
                             Title = "Math"
                         },
                         programmingCourseEntity = new CourseEntity
                         {
                             Id = Guid.NewGuid(),
                             Title = "Programming"
                         };

            StudentEntity laskovStudentEntity = new StudentEntity
                          {
                              Id = Guid.NewGuid(),
                              FirstName = "Sviat",
                              LastName = "Laskov"
                          },
                          petrovStudentEntity = new StudentEntity
                          {
                              Id = Guid.NewGuid(),
                              FirstName = "Ivan",
                              LastName = "Petrov"
                          };

            CourseStudentEntity mathCoursePetrovStudentEntity = new CourseStudentEntity
                                {
                                    CourseId = mathCourseEntity.Id,
                                    StudentId = petrovStudentEntity.Id
                                },
                                programmingCourseLaskovStudentEntity = new CourseStudentEntity
                                {
                                    CourseId = programmingCourseEntity.Id,
                                    StudentId = laskovStudentEntity.Id
                                },
                                programmingCoursePetrovStudentEntity = new CourseStudentEntity
                                {
                                    CourseId = programmingCourseEntity.Id,
                                    StudentId = petrovStudentEntity.Id
                                };

            modelBuilder.Entity<CourseEntity>().HasData(mathCourseEntity, programmingCourseEntity);
            modelBuilder.Entity<StudentEntity>().HasData(laskovStudentEntity, petrovStudentEntity);
            modelBuilder.Entity<CourseStudentEntity>().HasData(mathCoursePetrovStudentEntity, programmingCourseLaskovStudentEntity, programmingCoursePetrovStudentEntity);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}