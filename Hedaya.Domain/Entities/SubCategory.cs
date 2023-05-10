namespace Hedaya.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        public SubCategory()
        {
            MassCultures = new HashSet<MassCulture>();
            MethodologicalExplanations = new HashSet<MethodologicalExplanation>();
            Courses = new HashSet<Course>();
            TrainingPrograms = new HashSet<TrainingProgram>();
        }
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImgIconUrl { get; set; }
        public int MainCategoryId { get; set; } 
        public virtual MainCategory MainCategory { get; set; }
        public virtual ICollection<MassCulture> MassCultures { get; set; }
        public virtual ICollection<MethodologicalExplanation> MethodologicalExplanations { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; }
    }
}
