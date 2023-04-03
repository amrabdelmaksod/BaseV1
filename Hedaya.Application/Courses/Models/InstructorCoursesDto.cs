namespace Hedaya.Application.Courses.Models
{
    public class InstructorCoursesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFav { get; set; }
        public string Category { get; set; }
    }
}
