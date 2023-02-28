using Hedaya.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Domain.Entities.Seeds
{
    public static class DefaultRoles
    {

        
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Support.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }
         
        }
    }
}
