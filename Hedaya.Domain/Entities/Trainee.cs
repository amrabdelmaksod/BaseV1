using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Enums;

namespace Hedaya.Domain.Entities
{
    public class Trainee : BaseEntity
    {
        public Trainee()
        {
            Certificates = new HashSet<Certificate>();
            Friends = new HashSet<Friendship>();
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public EducationalDegree?  EducationalDegree { get; set; }
        public string? JobTitle { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Whatsapp { get; set; }
        public string? Telegram { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Friendship> Friends { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
