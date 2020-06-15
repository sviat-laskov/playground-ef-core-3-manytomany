using System.Collections.Generic;

namespace API.Models
{
    public class CourseDTO : CourseWithoutStudentsDTO
    {
        public IEnumerable<StudentWithoutCoursesDTO> Students { get; set; }
    }
}