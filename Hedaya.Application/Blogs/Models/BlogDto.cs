using MediatR;

namespace Hedaya.Application.Blogs.Models
{
    public class BlogDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public string? Facebook { get; set; }
        public string? Youtube { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Whatsapp { get; set; }
    }
}
