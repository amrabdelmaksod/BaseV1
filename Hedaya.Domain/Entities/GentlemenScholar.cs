namespace Hedaya.Domain.Entities
{
    public class GentlemenScholar
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
    }
}
