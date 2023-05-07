namespace Hedaya.Application.Courses.Models
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string VideoUrl { get; set; }
        public bool IsCompleeted { get; set; }
    }
}
