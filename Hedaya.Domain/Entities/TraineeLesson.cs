namespace Hedaya.Domain.Entities
{
    public class TraineeLesson
    {
        public int Id { get; set; }
        public string TraineeId { get; set; }
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Course Course { get; set; }
    }

}
