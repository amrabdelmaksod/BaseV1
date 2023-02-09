namespace BaseV1.Domain.Entities
{
    public class EducationalCourse : BaseEntity
    {
        public EducationalCourse()
        {
            CourseTopics = new HashSet<CourseTopic>();
        }
        public int Id { get; set; }
        public required string Title { get; set; }
        public byte CategoryId { get; set; }
        public required string Description { get; set; }
        public string Objectives { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StudyDuration { get; set; }     
        public string Pros { get; set; }

       public virtual ICollection<CourseTopic> CourseTopics { get; set; }



    }
}
