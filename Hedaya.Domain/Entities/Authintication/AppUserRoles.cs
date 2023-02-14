namespace Hedaya.Domain.Entities.Authintication
{
    public class AppUserRoles
    {
        public int RoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
