namespace Hedaya.Application.Courses.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public bool IsFav { get; set; }
        public string InstructorName { get; set; }
        public string InstructorImageUrl { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int TrainingProgramId { get; set; }
    }
}
