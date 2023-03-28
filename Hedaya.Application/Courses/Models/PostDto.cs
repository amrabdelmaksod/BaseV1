namespace Hedaya.Application.Courses.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public string Text { get; set; }
        public string? ImageUrl { get; set; }
        public List<CommentDto> Coments { get; set; }

    }
}
