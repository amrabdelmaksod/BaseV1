namespace Hedaya.Domain.Entities
{
    public class TraineeFavouriteProgram
    {

        public string TraineeId { get; set; }
        public int TrainingProgramId { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
    }
}
