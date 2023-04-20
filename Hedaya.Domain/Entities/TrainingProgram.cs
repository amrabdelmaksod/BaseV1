namespace Hedaya.Domain.Entities
{
    public class TrainingProgram : BaseEntity
    {
        public TrainingProgram()
        {
            TraineeFavouritePrograms = new HashSet<TraineeFavouriteProgram>();
            Courses = new HashSet<Course>();
            TrainingProgramNotes = new HashSet<TrainingProgramNote>();
            Enrollments = new HashSet<Enrollment>();
        }
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImgUrl { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<TraineeFavouriteProgram> TraineeFavouritePrograms { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<TrainingProgramNote> TrainingProgramNotes { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }


    }
}
