using Hedaya.Domain.Enums;

namespace Hedaya.Domain.Entities
{
    public class CourseTest
    {
        public CourseTest()
        {
            Questions = new HashSet<Question>();
            TraineeAnswers = new HashSet<TraineeAnswer>();

        }

        public int Id { get; set; }
        public string Title{ get; set; }
        public int CourseId { get; set; }
        public CourseTestStatus Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TraineeAnswer> TraineeAnswers { get; set; }

    }
}
