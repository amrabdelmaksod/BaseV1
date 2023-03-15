using Hedaya.Application.Auth.Models;

namespace Hedaya.Application.Auth.Validators
{
    using FluentValidation;
    using Hedaya.Domain.Entities.Authintication;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterModelValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(256).WithMessage("Full name cannot exceed 50 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
               .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian mobile number.")
                .MustAsync(BeUniquePhoneNumber).WithMessage("This phone number is already registered.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .MustAsync(BeUniqueEmail).WithMessage("This email address is already registered.");

            RuleFor(x => x.Country)
                .NotNull().WithMessage("Country is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }

        private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);
           
            return user == null;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user == null;
        }
    }


}
