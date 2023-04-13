using Hedaya.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Domain.Entities.Authintication
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Notifications = new HashSet<Notification>();
        }
        public string? FullName { get; set; }
        public UserType UserType { get; set; }
        public Nationality Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? SecurityCode { get; set; }
        public bool Deleted { get; set; }
        public bool IsActive { get; set; }
        public string? DeletedReason { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }




    }
}
