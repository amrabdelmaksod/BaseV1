namespace Hedaya.Domain.Entities
{
    public class MainCategory
    {
        public MainCategory()
        {
            SubCategories = new HashSet<SubCategory>();
        }
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImgIconUrl { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
