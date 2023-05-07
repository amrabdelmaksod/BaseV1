namespace Hedaya.Domain.Entities
{
    public class PodcastFavourite
    {

        public string TraineeId { get; set; }
        public int PodcastId { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual Podcast Podcast { get; set; }
    }
}
