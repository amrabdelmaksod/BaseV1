namespace Hedaya.Domain.Entities
{
    public class TraineeCourseFavorite
    {
        public string TraineeId { get; set; }
        public int CourseId { get; set; }

        public virtual Trainee Trainee { get; set; }
        public virtual Course Course { get; set; }
    }

}
