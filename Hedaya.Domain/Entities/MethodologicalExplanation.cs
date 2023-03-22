namespace Hedaya.Domain.Entities
{
    public class MethodologicalExplanation
    {
        public MethodologicalExplanation()
        {
            ExplanationVideos = new HashSet<ExplanationVideo>();
            ExplanationNotes = new HashSet<ExplanationNote>();
        }
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Whatsapp { get; set; }
        public string Youtube { get; set; }
        public string Telegram { get; set; }
        public string ImageUrl { get; set; }
      
        public TimeSpan Duration { get; set; }
        public int SubCategoryId { get; set; }
        public string InstructorId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<ExplanationVideo> ExplanationVideos { get; set; }
        public virtual ICollection<ExplanationNote> ExplanationNotes { get; set; }
    }
}
