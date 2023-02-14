namespace Hedaya.Domain.Entities
{
    public class Forum : BaseEntity
    {
        public Forum()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public int EducationalCourseId { get; set; }
        public virtual EducationalCourse EducationalCourse { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
