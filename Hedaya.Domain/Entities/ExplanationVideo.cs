namespace Hedaya.Domain.Entities
{
    public class ExplanationVideo
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Description { get; set; }
        public int MethodologicalExplanationId { get; set; }
        public virtual MethodologicalExplanation MethodologicalExplanation { get; set; }

    }
}
