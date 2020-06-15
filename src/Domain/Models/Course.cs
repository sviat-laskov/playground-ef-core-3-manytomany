using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models.Base;

namespace Domain.Models
{
    public class Course : BaseGuid
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Title { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}