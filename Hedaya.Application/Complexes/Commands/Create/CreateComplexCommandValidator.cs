using FluentValidation;
using Hedaya.Application.Complexes.Commands.Create;

namespace Hedaya.Application.Complexes.Commands
{
    public class CreateComplexCommandValidator : AbstractValidator<CreateComplexCommand>
    {
        public CreateComplexCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.AddressDescription)
                .NotEmpty().WithMessage("Address description is required.")
                .MaximumLength(500).WithMessage("Address description must not exceed 500 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Mobile)
               .MaximumLength(15);
            RuleFor(x => x.LandlinePhone).MaximumLength(10);
               

            RuleFor(x => x.Terms)
                .NotEmpty().WithMessage("Terms are required.")
                .MaximumLength(500).WithMessage("Terms must not exceed 500 characters.");

            RuleFor(x => x.Conditions)
                .NotEmpty().WithMessage("Conditions are required.")
                .MaximumLength(500).WithMessage("Conditions must not exceed 500 characters.");

            RuleFor(x => x.Vision)
                .MaximumLength(500).WithMessage("Vision must not exceed 500 characters.");

            RuleFor(x => x.Mission)
                .MaximumLength(500).WithMessage("Mission must not exceed 500 characters.");

            RuleFor(x => x.AboutPlatformVideoUrl)
                .MaximumLength(500).WithMessage("About platform video URL must not exceed 500 characters.");
        }
    }

}