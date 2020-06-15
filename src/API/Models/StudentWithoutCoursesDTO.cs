using System.ComponentModel.DataAnnotations;
using Common.Models.Base;

namespace API.Models
{
    public class StudentWithoutCoursesDTO : BaseGuid
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string LastName { get; set; }
    }
}