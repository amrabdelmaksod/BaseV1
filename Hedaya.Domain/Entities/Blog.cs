namespace Hedaya.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public  string?  Facebook { get; set; }
        public  string? Twitter { get; set; }
        public  string? Youtube { get; set; }
        public  string? Instagram { get; set; }
        public  string? Whatsapp { get; set; }
        public bool Deleted { get; set; }
    }
}
