namespace Hedaya.Domain.Entities
{
    public class TraineeExplanationFavourite
    {
        public string TraineeId { get; set; }
        public int MethodologicalExplanationId { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual MethodologicalExplanation MethodologicalExplanation { get; set; }
    }
}
