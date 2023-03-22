using FluentValidation;
using Hedaya.Application.Auth.Models;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Application.Auth.Validators
{
    public class UpdateProfileModelValidator : AbstractValidator<UpdateProfileModel>
    {
        private readonly UserManager<AppUser> _userManager;
        public UpdateProfileModelValidator(UserManager<AppUser> userManager)
        {
             _userManager = userManager;

            RuleFor(x => x.FullName)
             
                .MaximumLength(256).WithMessage("Full name cannot exceed 50 characters.");

            RuleFor(x => x.Phone)

               .Matches(@"^(01)[0-9]{9}$").WithMessage("Please enter a valid Egyptian mobile number.");



            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
              

            RuleFor(x => x.Whatsapp)

               .MaximumLength(15).WithMessage("Whatsapp cannot exceed 15 characters.");
           
            RuleFor(x => x.Facebook)

              .MaximumLength(200).WithMessage("Facebok cannot exceed 200 characters.");

            RuleFor(x => x.Twitter)

          .MaximumLength(200).WithMessage("Twitter cannot exceed 200 characters.");

            RuleFor(x => x.JobTitle)

        .MaximumLength(200).WithMessage("JobTitle cannot exceed 200 characters.");

            RuleFor(x => x.Telegram)

      .MaximumLength(15).WithMessage("Telegram cannot exceed 15 characters.");

        }

   
    }
}
