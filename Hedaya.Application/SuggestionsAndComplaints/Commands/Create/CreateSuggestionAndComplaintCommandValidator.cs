using FluentValidation;

namespace Hedaya.Application.SuggestionsAndComplaints.Commands.Create
{
    public class CreateSuggestionAndComplaintCommandValidator : AbstractValidator<CreateSuggestionAndComplaintCommand>
    {
        public CreateSuggestionAndComplaintCommandValidator()
        {
            RuleFor(x => x.TraineeName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Phone)
            .Must(IsValidEgyptianPhoneNumberFormat)
            .WithMessage("The phone number must be in Egyptian phone number format.");

            RuleFor(x => x.Email).MaximumLength(50).EmailAddress();
            RuleFor(x => x.Subject).MaximumLength(100);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
        }

        private bool IsValidEgyptianPhoneNumberFormat(string phoneNumber)
        {
            // Check if the phone number starts with the Egyptian phone code
            return phoneNumber.StartsWith("01") && phoneNumber.Length == 11;
        }
    }


}
