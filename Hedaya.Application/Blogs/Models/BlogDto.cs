using MediatR;

namespace Hedaya.Application.Blogs.Models
{
    public class BlogDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
