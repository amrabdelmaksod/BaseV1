namespace Hedaya.Domain.Entities
{
    public class MassCulture
    {
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
