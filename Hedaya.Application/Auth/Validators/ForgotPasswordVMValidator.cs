using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Hedaya.Application.Auth.Models;
using Hedaya.Domain.Entities.Authintication;

namespace Hedaya.Application.Auth.Validators
{


    public class ForgotPasswordVMValidator : AbstractValidator<ForgotPasswordVM>
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordVMValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile number is required.")
               .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian mobile number.")
                .MustAsync(IsExistedMobile).WithMessage("User with this mobile is not found");
        }


        //to return not valid if the user with this mobile is not exists
        private async Task<bool> IsExistedMobile(string mobileNumber, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(mobileNumber);
            return user != null;
        }
    }



}
