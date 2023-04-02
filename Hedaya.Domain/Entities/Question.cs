using Hedaya.Domain.Enums;

namespace Hedaya.Domain.Entities
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
            TraineeAnswers = new HashSet<TraineeAnswer>();

        }

        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public int CourseTestId { get; set; }
        public virtual CourseTest CourseTest { get; set; }
        public virtual ICollection<TraineeAnswer> TraineeAnswers { get; set; }

    }
}
