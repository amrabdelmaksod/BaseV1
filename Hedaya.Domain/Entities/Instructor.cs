using Hedaya.Domain.Entities.Authintication;

namespace Hedaya.Domain.Entities
{
    public class Instructor : BaseEntity
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
            MethodologicalExplanations = new HashSet<MethodologicalExplanation>();
        }
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public required string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MethodologicalExplanation> MethodologicalExplanations { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
