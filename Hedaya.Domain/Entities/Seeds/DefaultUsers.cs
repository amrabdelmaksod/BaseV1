using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hedaya.Domain.Entities.Seeds
{
    public static class DefaultUsers
    {
       
        public static async  Task SeedBasicUserAsync(UserManager<AppUser> userManager)
        {
            var defaultUser = new AppUser
            {
                Name = "basicUser",
                UserName = "basicuser@hedaya.com",
                Email = "basicuser@hedaya.com",
                EmailConfirmed = true,
            };

            var user =await userManager.FindByEmailAsync(defaultUser.Email);
            if (user is null) {
                await userManager.CreateAsync(defaultUser, "P@ssword123");
                await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
            }
        }

        public static async Task SeedSuperAdminUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new AppUser
            {
                Name = "superadmin",
                UserName = "superadmin@hedaya.com",
                Email = "superadmin@hedaya.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "P@ssword123");
                await userManager.AddToRolesAsync(defaultUser, new List<string> { Roles.Support.ToString(), Roles.SuperAdmin.ToString(), Roles.User.ToString() });
            }
            await roleManger.SeedClaimsForSuperUser();
           
        }


        private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddPermissionClaims(adminRole, "Products");
        }


        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsList(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
