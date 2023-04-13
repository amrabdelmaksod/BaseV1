using FluentValidation;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Create
{
    public class CreatePlatformFeatureCommandValidator : AbstractValidator<CreatePlatformFeatureCommand>
    {
        public CreatePlatformFeatureCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        }
    }
}

