namespace Hedaya.Domain.Entities
{
    public class Course 
    {
        public Course()
        {
            CourseTopics = new HashSet<CourseTopic>();
        }
        public int Id { get; set; }
        public required string TitleAr { get; set; }
        public  string TitleEn { get; set; }
        public  string Description { get; set; }
        public  string ImageUrl { get; set; }
        public  DateTime StartDate { get; set; }
        public  TimeSpan Duration { get; set; }
        public string InstructorId { get; set; }
        public int SubCategoryId { get; set; }


        public virtual Instructor Instructor { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }


        
    }
}
