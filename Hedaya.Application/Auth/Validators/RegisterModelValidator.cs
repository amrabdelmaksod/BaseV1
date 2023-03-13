using FluentValidation;
using Hedaya.Application.Auth.Models;

namespace Hedaya.Application.Auth.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(m => m.FullName).NotEmpty().WithMessage("Please enter your full name.");

            RuleFor(m => m.PhoneNumber).NotEmpty().WithMessage("Please enter your phone number.")
                .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian phone number.");

            RuleFor(m => m.Email).NotEmpty().WithMessage("Please enter your email address.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(m => m.Country).NotEmpty().WithMessage("Please select your country.");

            RuleFor(m => m.Password).NotEmpty().WithMessage("Please enter a password.")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$")


                .WithMessage("The password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");

            RuleFor(m => m.ConfirmPassword).Equal(m => m.Password)
                .WithMessage("The password and confirmation password do not match.");
        }
    }

}
