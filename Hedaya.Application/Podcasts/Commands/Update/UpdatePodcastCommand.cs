using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Podcasts.Commands.Update
{
    public class UpdatePodcastCommand : IRequest
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? AudioFile { get; set; }
    }

    public class UpdatePodcastCommandHandler : IRequestHandler<UpdatePodcastCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UpdatePodcastCommandHandler(IApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Unit> Handle(UpdatePodcastCommand request, CancellationToken cancellationToken)
        {
            var podcast = await _context.Podcasts.FindAsync(request.Id);

            if (podcast == null)
            {
                throw new NotFoundException(nameof(Podcast));
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                podcast.Title = request.Title;

            }


            if (!string.IsNullOrEmpty(request.Description))
            {
                podcast.Description = request.Description;

            }

            if (request.AudioFile != null)
            {
                var audioPath = await PodcastHelper.SavePodcastAudio(request.AudioFile, _hostingEnvironment);

                podcast.AudioUrl = audioPath;
                podcast.Duration = TimeSpan.Zero; /* PodcastHelper.GetAudioDuration(audioPath);*/
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }

}
