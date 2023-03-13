using FluentValidation;
using Hedaya.Application.Auth.Models;

namespace Hedaya.Application.Auth.Validators
{
    public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidator()
        {
            RuleFor(m => m.Mobile).NotEmpty().WithMessage("Please enter your mobile number.")
                .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian mobile number.");

            RuleFor(m => m.Password).NotEmpty().WithMessage("Please enter your password.");
        }
    }

}
