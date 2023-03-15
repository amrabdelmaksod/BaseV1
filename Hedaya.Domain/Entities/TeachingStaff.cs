namespace Hedaya.Domain.Entities
{
    public class TeachingStaff
    {
        public string Id { get; set; }
        public string ImageURL { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
