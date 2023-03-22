namespace Hedaya.Application.MassCultures.Models
{
    public class MassCultureDetailsDto
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFav { get; set; }
        public string Duration { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Telegram { get; set; }
        public string Youtube { get; set; }
    }
}
