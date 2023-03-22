using FluentValidation;
using Hedaya.Application.Auth.Models;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Hedaya.Application.Auth.Validators
{
    public class UpdateProfilePictureModelValidator : AbstractValidator<UpdateProfilePictureModel>
    {
        private readonly UserManager<AppUser> _userManager;
        public UpdateProfilePictureModelValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            RuleFor(a => a.ProfilePicture).NotEmpty().WithMessage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "من فضلك اختر صورة" : "Please Select Profile Picture!");
        }
    }
}
