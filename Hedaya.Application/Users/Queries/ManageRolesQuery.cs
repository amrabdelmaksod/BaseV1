using Hedaya.Application.Users.Models;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Users.Queries
{
    public class ManageRolesQuery : IRequest<UserRolesDto>
    {
        public string userId { get; set; }
        public class Handler : IRequestHandler<ManageRolesQuery, UserRolesDto>
        {

            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {

                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<UserRolesDto> Handle(ManageRolesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(request.userId);

                    if (user == null)
                        return null;

                    var roles = await _roleManager.Roles.ToListAsync();

                    var data = new UserRolesDto 
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Roles = roles.Select(role => new CheckBoxDto
                        {
                            DisplayValue = role.Name,
                            IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                        }).ToList()
                    };

                    return data;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
