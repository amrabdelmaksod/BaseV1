namespace Hedaya.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public int Id { get; set; }


        public int CourseId { get; set; }

        public required string TraineeId { get; set; }
        public string? ImageUrl { get; set; }
        public string CertificateType { get; set; }

        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
 

    }
}
