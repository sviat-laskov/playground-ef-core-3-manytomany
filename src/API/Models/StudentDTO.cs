using System.Collections.Generic;

namespace API.Models
{
    public class StudentDTO : StudentWithoutCoursesDTO
    {
        public IEnumerable<CourseWithoutStudentsDTO> Courses { get; set; }
    }
}