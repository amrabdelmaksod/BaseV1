namespace Hedaya.Application.Certificates.Models
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public required string TraineeName { get; set; }
        public required string InstructorName { get; set; }
        public required string CourseTitle { get; set; }
        public string TraineeCode { get; set; }
        public string? ImageUrl { get; set; }

    }
}
