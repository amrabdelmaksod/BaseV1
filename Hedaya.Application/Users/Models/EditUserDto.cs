using Hedaya.Domain.Enums;

namespace Hedaya.Application.Users.Models
{
    public class EditUserDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public UserType? UserType { get; set; }
        public Nationality? Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? RoleId { get; set; }
    }
}
