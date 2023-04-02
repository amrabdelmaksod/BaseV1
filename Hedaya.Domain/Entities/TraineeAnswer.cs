namespace Hedaya.Domain.Entities
{
    public class TraineeAnswer
    {
        public int Id { get; set; }
        public string SelectedAnswers { get; set; }

        public int Score { get; set; }

        public string TraineeId { get; set; }

        public int CourseTestId { get; set; }
        public int QuestionId { get; set; }
        public virtual Trainee Trainee { get; set; }

        public virtual CourseTest CourseTest { get; set; }

        public virtual Question Question { get; set; }

       
    }
}
