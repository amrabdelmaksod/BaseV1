using BaseV1.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BaseV1.Domain.Entities.Authintication
{
    public class AppUser : IdentityUser
    {
        public required string Name { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
