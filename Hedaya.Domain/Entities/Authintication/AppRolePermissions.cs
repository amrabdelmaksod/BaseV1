namespace Hedaya.Domain.Entities.Authintication
{
    public class AppRolePermissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public int PermissionId { get; set; }
        public virtual AppPermission AppPermission { get; set; }
    }
}
