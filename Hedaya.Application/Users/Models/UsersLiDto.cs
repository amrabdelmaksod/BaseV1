namespace Hedaya.Application.Users.Models
{
    public class UsersLiDto
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
    
}
