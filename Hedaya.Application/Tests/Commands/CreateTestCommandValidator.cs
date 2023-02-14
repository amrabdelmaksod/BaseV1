using FluentValidation;

namespace Hedaya.Application.Tests.Commands
{
    public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(a=>a.Name).NotEmpty().WithMessage("Please Enter The Name").MaximumLength(256).WithMessage($"Maximum Length Of Name : 256");
        }
    }
}
