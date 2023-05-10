using Hedaya.Application.Courses.Models;

namespace Hedaya.Application.TrainingPrograms.Models
{
    public class MyProgramsDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int CourseCount { get; set; }
        public int TestsCount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float CompletionRate { get; set; }
        public List<MyCoursesDto> Courses { get; set; }
    }
}
