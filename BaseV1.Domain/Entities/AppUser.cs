using Microsoft.AspNetCore.Identity;

namespace BaseV1.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
