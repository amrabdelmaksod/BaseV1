namespace Hedaya.Application.Courses.Models
{
    public class CourseTopicDto
    {
        public string Title { get; set; }
        public List<LessonDto> Lessons { get; set; }
    }
}
