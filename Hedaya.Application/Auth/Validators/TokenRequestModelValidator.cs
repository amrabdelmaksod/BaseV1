using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Hedaya.Application.Auth.Models;
using Hedaya.Domain.Entities.Authintication;

namespace Hedaya.Application.Auth.Validators
{


    public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
    {
        private readonly UserManager<AppUser> _userManager;

        public TokenRequestModelValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian mobile number.")
                .MustAsync(BeRegisteredUser).WithMessage("User not found.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MustAsync(BeValidPassword).WithMessage("Invalid password.");
        }

        private async Task<bool> BeRegisteredUser(string mobile, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(mobile);
            return user != null;
        }

        private async Task<bool> BeValidPassword(TokenRequestModel model, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(model.Mobile);
            if (user == null)
            {
                return true; // User not found, don't validate password
            }

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, password);
            return passwordIsValid;
        }
    }


}
