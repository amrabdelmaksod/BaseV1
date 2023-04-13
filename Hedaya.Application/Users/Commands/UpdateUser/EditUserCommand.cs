namespace Hedaya.Application.Users.Commands.UpdateUser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Hedaya.Domain.Entities.Authintication;
    using global::Hedaya.Domain.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    namespace Hedaya.Application.Users.Commands.EditUser
    {
        public class EditUserCommand : IRequest
        {
            public string Id { get; set; }
            public string? FullName { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public UserType? UserType { get; set; }
            public Nationality? Nationality { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public Gender? Gender { get; set; }
            public string? RoleId { get; set; }
        }

        public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public EditUserCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    throw new ApplicationException($"User with ID {request.Id} not found.");
                }

                if (!string.IsNullOrWhiteSpace(request.FullName))
                {
                    user.FullName = request.FullName;
                }

                if (!string.IsNullOrWhiteSpace(request.Phone))
                {
                    user.UserName = request.Phone;
                }

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    user.Email = request.Email;
                }

                if (request.UserType.HasValue)
                {
                    user.UserType = request.UserType.Value;
                }

                if (request.Nationality.HasValue)
                {
                    user.Nationality = request.Nationality.Value;
                }

                if (request.DateOfBirth.HasValue)
                {
                    user.DateOfBirth = request.DateOfBirth.Value;
                }

                if (request.Gender.HasValue)
                {
                    user.Gender = request.Gender.Value;
                }

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new ApplicationException($"Unable to update user: {errors}");
                }

            

                if (!string.IsNullOrWhiteSpace(request.RoleId))
                {
                    // Remove the user from all roles and then add them to the specified role
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                    var newRole = await _roleManager.FindByIdAsync(request.RoleId);
                    if (newRole == null)
                    {
                        throw new ApplicationException($"Role with ID {request.RoleId} not found.");
                    }

                    await _userManager.AddToRoleAsync(user, newRole.Name);
                }

                return Unit.Value;
            }
        }
    }

}
