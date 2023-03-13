namespace Hedaya.Domain.Entities
{
    public class Course 
    {
        public Course()
        {
            CourseTopics = new HashSet<CourseTopic>();
        }
        public int Id { get; set; }
        public required string Title { get; set; }
        public string InstructorId { get; set; }


        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }


        
    }
}
