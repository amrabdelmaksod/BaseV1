namespace Hedaya.Domain.Entities
{
    public class Podcast : BaseEntity
    {
        public Podcast()
        {
            PodcastFavourites = new HashSet<PodcastFavourite>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<PodcastFavourite> PodcastFavourites { get; set; }


    }



}
