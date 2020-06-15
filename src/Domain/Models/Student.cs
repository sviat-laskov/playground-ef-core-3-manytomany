using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models.Base;

namespace Domain.Models
{
    public class Student : BaseGuid
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string LastName { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}