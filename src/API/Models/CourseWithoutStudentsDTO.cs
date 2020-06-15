using System.ComponentModel.DataAnnotations;
using Common.Models.Base;

namespace API.Models
{
    public class CourseWithoutStudentsDTO : BaseGuid
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Title { get; set; }
    }
}