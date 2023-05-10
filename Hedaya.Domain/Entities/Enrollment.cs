namespace Hedaya.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int TrainingProgramId { get; set; }
        public string TraineeId { get; set; }
        public int CourseId { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public virtual Course Course { get; set; }  
        public virtual Trainee Trainee { get; set; }
    }
}

