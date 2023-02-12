namespace BaseV1.Domain.Entities
{
    public class AppRolePermissions
    {
        public int RoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public int PermissionId { get; set; }
        public virtual AppPermission AppPermission { get; set; }
    }
}
