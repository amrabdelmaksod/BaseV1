using FluentValidation;
using Hedaya.Application.Auth.Models;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Auth.Validators
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        private readonly UserManager<AppUser> _userManager;
        public ChangePasswordModelValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
            RuleFor(x => x.NewPassword)
             .NotEmpty().WithMessage("Password is required.")
             .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Password and confirmation password do not match.");
        }
    }
}
