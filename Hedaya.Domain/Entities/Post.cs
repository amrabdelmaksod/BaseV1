using Microsoft.EntityFrameworkCore;

namespace Hedaya.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }

        public string Text { get; set; }
        public string ImagePath { get; set; }      
        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
