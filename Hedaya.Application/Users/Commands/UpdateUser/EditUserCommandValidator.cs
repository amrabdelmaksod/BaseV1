using FluentValidation;
using Hedaya.Application.Users.Commands.UpdateUser.Hedaya.Application.Users.Commands.EditUser;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Commands.UpdateUser
{


    namespace Hedaya.Application.Users.Commands.UpdateUser
    {
        public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public EditUserCommandValidator(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;

                RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Id is required.");

                RuleFor(x => x.FullName)
             .MaximumLength(256).WithMessage("Full Name cannot be longer than 256 characters.");


                RuleFor(x => x.Phone)
                    .MaximumLength(15).WithMessage("Phone cannot be longer than 15 characters.");

                RuleFor(x => x.Email)
                    .MaximumLength(256).WithMessage("Email cannot be longer than 256 characters.")
                    .EmailAddress().WithMessage("Invalid email address.")
                    .MustAsync(BeUniqueEmail).WithMessage("Email address is already taken.");

                RuleFor(x => x.RoleId)
                    .MustAsync(BeValidRole).WithMessage("Invalid role selected.");
            }

           
            private async Task<bool> BeUniqueEmail(EditUserCommand model, string email, CancellationToken cancellationToken)
            {
                if (model.Email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    return user == null || user.Id == model.Id;
                }
               return true;
            }

            private async Task<bool> BeValidRole(string roleId, CancellationToken cancellationToken)
            {
                if (roleId != null)
                {
                    var role = await _roleManager.FindByIdAsync(roleId);
                    return role != null;
                }
                return true;
       
            }
        }
    }
}
