using Hedaya.Application.Users.Models;
using Hedaya.Domain.Entities.Authintication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Commands.UpdateRoles
{
    public class UpdateRolesCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public List<CheckBoxDto> Roles { get; set; }
        public class Handler : IRequestHandler<UpdateRolesCommand, string>
        {

            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {

                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<string> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(request.UserId);

                    if (user == null)
                        return null;

                    var userRoles = await _userManager.GetRolesAsync(user);

                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, request.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

                    return "Updated Successfully";

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
