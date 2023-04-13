using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserCommandValidator(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;


            RuleFor(x => x.FullName)
               .NotEmpty().WithMessage("Full Name is required.")
               .MaximumLength(256).WithMessage("Full Name cannot be longer than 256 characters.");


            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(15).WithMessage("Phone cannot be longer than 15 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(256).WithMessage("Email cannot be longer than 256 characters.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MustAsync(BeUniqueEmail).WithMessage("Email address is already taken.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password) // validate confirm password
           .WithMessage("Confirm password does not match the password.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role is required.")
                .MustAsync(BeValidRole).WithMessage("Invalid role selected.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }

        private async Task<bool> BeValidRole(string roleId, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return role != null;
        }
    }

}
