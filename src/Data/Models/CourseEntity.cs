using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models.Base;

namespace Data.Models
{
    public class CourseEntity : BaseGuid
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Title { get; set; }

        public IEnumerable<CourseStudentEntity> CourseStudents { get; set; }
    }
}