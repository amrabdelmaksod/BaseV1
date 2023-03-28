namespace Hedaya.Domain.Entities
{
    public class Comment 
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public required string Text { get; set; }
        public string? ImagePath { get; set; }
        public int PostId { get; set; }
        public string TraineeId { get; set; }
        public virtual Post Post { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
      

    }
}
