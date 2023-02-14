namespace BaseV1.Domain.Entities.Authintication
{
    public class AppRole 
    {
        public AppRole()
        {
            AppRolePermissions = new HashSet<AppRolePermissions>();
            AppUserRoles = new HashSet<AppUserRoles>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<AppRolePermissions> AppRolePermissions { get; set; }
        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
