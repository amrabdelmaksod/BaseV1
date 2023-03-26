namespace Hedaya.Domain.Entities
{
    public class CourseTopic
    {
        public CourseTopic()
        {
            Lessons = new HashSet<Lesson>();
        }
        public int Id { get; set; }
        public required string NameAr { get; set; }
        public required string NameEn { get; set; }
        public int SortIndex { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }

   

    }
}
