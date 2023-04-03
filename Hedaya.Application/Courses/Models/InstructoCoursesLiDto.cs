namespace Hedaya.Application.Courses.Models
{
    public class InstructoCoursesLiDto
    {
        public InstructorDto Instructor { get; set; }
        public List<InstructorCoursesDto> Courses { get; set; }
    }
}
