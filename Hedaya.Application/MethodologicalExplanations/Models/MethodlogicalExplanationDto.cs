namespace Hedaya.Application.MethodologicalExplanations.Models
{
    public class MethodlogicalExplanationDto
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFav { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
    }
}
