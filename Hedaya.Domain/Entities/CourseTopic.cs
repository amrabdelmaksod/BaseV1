namespace Hedaya.Domain.Entities
{
    public class CourseTopic
    {
        public CourseTopic()
        {
            Lessons = new HashSet<Lesson>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }

   

    }
}
