namespace Hedaya.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public Lesson()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string VideoFilePath { get; set; }
        public string Text { get; set; }
        public int CourseTopicId { get; set; }
        public virtual CourseTopic CourseTopic { get; set; }

    }
}
