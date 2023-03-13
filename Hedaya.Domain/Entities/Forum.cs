namespace Hedaya.Domain.Entities
{
    public class Forum 
    {
        public Forum()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
