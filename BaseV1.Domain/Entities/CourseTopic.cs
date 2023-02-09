namespace BaseV1.Domain.Entities
{
    public class CourseTopic : BaseEntity
    {
        public CourseTopic()
        {
            Lessons = new HashSet<Lesson>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public int EducationalCourseId { get; set; }
        public virtual EducationalCourse EducationalCourse { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }

   

    }
}
