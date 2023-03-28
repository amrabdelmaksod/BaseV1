namespace Hedaya.Domain.Entities
{
    public class CourseTest
    {
        public CourseTest()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Title{ get; set; }
 
        public virtual ICollection<Question> Questions { get; set; }
    }
}
