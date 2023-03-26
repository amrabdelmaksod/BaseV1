namespace Hedaya.Domain.Entities
{
    public class Lesson 
    {
        public Lesson()
        {

        }
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string VideoFilePath { get; set; }
        public string Text { get; set; }
        public int CourseTopicId { get; set; }
        public TimeSpan Duration { get; set; }
        public int SortIndex { get; set; }
        public virtual CourseTopic CourseTopic { get; set; }

    }
}
