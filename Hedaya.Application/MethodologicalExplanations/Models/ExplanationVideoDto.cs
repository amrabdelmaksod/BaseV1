namespace Hedaya.Application.MethodologicalExplanations.Models
{
    public class ExplanationVideoDto
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }

}
