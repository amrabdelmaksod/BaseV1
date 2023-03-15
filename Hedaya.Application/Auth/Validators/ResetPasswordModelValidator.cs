namespace Hedaya.Application.Auth.Validators
{
    using FluentValidation;
    using Hedaya.Application.Auth.Models;
    using Hedaya.Domain.Entities.Authintication;
    using Microsoft.AspNetCore.Identity;
    using System.Globalization;

    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordModelValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ?"من فضلك ادخل رقم الموبايل." : "Mobile number is required.")
               .Matches(@"^(01)[0-9]{9}$").WithMessage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "الرجاء إدخال رقم هاتف مصري صحيح." : "Please enter a valid Egyptian mobile number.")
                .MustAsync(BeUniqueMobile).WithMessage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "عفوا لايوجد مستخدم بهذا الرقم!" : "User with this mobile is not found");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Password and confirmation password do not match.");
        }

        private async Task<bool> BeUniqueMobile(string mobileNumber, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(mobileNumber);
            return user != null;
        }
    }

}
