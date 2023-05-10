namespace Hedaya.Application.Courses.Models
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public bool IsFav { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorImageUrl { get; set; }
        public string InstructorDescription { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string AboutCourse { get; set; }
        public string CourseSyllabus { get; set; }
        public string CourseFeatures { get; set; }
        public double CourseCompletionRate { get; set; }
        public int TrainingProgramId { get; set; }
        public bool IsEnrolled { get; set; }
        public List<CourseTopicDto> Topics { get; set; }
      
        public List<CourseTestDto> Tests { get; set; }

    }
}
