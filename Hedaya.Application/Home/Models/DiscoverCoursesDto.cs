namespace Hedaya.Application.Home.Models
{
    public class DiscoverCoursesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorImageUrl { get; set; }   
        public string CourseImageUrl { get; set; }   
        public string Description { get; set; }
        public bool IsFav { get; set; }
        public TimeSpan Duration { get; set; }
        public string StartDate { get; set; }

    }
}
