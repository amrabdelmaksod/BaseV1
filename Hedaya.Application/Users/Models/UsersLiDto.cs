using Hedaya.Domain.Enums;

namespace Hedaya.Application.Users.Models
{
    public class UsersLiDto
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public IEnumerable<string> Roles { get; set; }     
        public IEnumerable<string> RolesIds { get; set; }     
        public Nationality Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
    
}
