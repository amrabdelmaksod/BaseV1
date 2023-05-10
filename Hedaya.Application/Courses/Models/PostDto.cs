using Hedaya.Domain.Entities;

namespace Hedaya.Application.Courses.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public string Text { get; set; }
        public string TraineeImage { get; set; }
        public int NumberOfLikes { get; set; }
        public bool Liked { get; set; }
        public List<CommentDto> Coments { get; set; }
        public List<string> PostImages { get; set; }

    }
}
