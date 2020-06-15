using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class CourseStudentEntity
    {
        [Key]
        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public CourseEntity Course { get; set; }

        [Key]
        public Guid StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public StudentEntity Student { get; set; }
    }
}