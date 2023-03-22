namespace Hedaya.Domain.Entities
{
    public class SubCategory
    {
        public SubCategory()
        {
            MassCultures = new HashSet<MassCulture>();
            MethodologicalExplanations = new HashSet<MethodologicalExplanation>();
        }
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImgIconUrl { get; set; }
        public int MainCategoryId { get; set; } 
        public virtual MainCategory MainCategory { get; set; }
        public virtual ICollection<MassCulture> MassCultures { get; set; }
        public virtual ICollection<MethodologicalExplanation> MethodologicalExplanations { get; set; }
    }
}
