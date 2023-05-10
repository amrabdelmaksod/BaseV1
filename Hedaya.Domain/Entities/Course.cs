namespace Hedaya.Domain.Entities
{
    public class Course  : BaseEntity
    {
        public Course()
        {
            CourseTopics = new HashSet<CourseTopic>();
            Favorites = new HashSet<TraineeCourseFavorite>();
            CourseTests = new HashSet<CourseTest>();
        }
        public int Id { get; set; }
        public required string TitleAr { get; set; }
        public  string TitleEn { get; set; }
        public  string Description { get; set; }
        public  string AboutCourse { get; set; }
        public  string CourseSyllabus { get; set; }
        public  string CourseFeatures { get; set; }
        public  string ImageUrl { get; set; }
        public  string VideoUrl { get; set; }
        public  DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
        public  TimeSpan Duration { get; set; }
        public string InstructorId { get; set; }
        public int SubCategoryId { get; set; }
        public int ForumId { get; set; }
        public int TrainingProgramId { get; set; }


        public virtual TrainingProgram TrainingProgram { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
        public virtual ICollection<TraineeCourseFavorite> Favorites { get; set; }
        public virtual ICollection<CourseTest> CourseTests { get; set; }
    }
}
