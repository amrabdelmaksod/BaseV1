namespace Hedaya.Domain.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
