namespace Hedaya.Application.Users.Models
{
    public class UserRolesDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<CheckBoxDto> Roles { get; set; }
    }
}
