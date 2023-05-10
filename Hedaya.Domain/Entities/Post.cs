namespace Hedaya.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostImages = new List<PostImage>();
            Likes = new HashSet<PostLike>();
        }

        public int Id { get; set; }

        public string Text { get; set; }
        public List<PostImage> PostImages { get; set; }
        public int ForumId { get; set; }
        public string TraineeId { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual Trainee Trainee { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> Likes { get; set; }
    }

}
