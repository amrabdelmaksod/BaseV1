using FluentValidation;

namespace Hedaya.Application.Podcasts.Commands.Create
{
    public class CreatePodcastCommandValidator : AbstractValidator<CreatePodcastCommand>
    {
        public CreatePodcastCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
        }

        private bool BeAValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
