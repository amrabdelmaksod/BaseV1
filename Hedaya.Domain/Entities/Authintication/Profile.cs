namespace Hedaya.Domain.Entities.Authintication
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        public int Gender { get; set; }
        public DateTime DOB { get; set; }
        public string JobTitle { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public virtual AppUser User { get; set; }

    }
}
