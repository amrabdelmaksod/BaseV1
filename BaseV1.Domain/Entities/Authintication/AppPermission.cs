namespace BaseV1.Domain.Entities.Authintication
{
    public class AppPermission
    {
        public AppPermission()
        {
            AppRolePermissions = new HashSet<AppRolePermissions>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AppRolePermissions> AppRolePermissions { get; set; }
    }
}
