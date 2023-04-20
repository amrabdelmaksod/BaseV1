using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Hedaya.Application.Podcasts.Commands.Create
{
    // Define the create command
    public class CreatePodcastCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile AudioFile { get; set; }
    }

    // Define the command handler
    public class CreatePodcastCommandHandler : IRequestHandler<CreatePodcastCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreatePodcastCommandHandler(IApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<int> Handle(CreatePodcastCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Save the audio file
                string audioFilePath = await PodcastHelper.SavePodcastAudio(request.AudioFile, _hostingEnvironment);
                var duration = PodcastHelper.GetAudioDuration(audioFilePath);
                // Create the new podcast entity
                var podcast = new Podcast
                {
                    Title = request.Title,
                    Description = request.Description,
                    AudioUrl = audioFilePath,
                    PublishDate = DateTime.UtcNow,
                    Duration = TimeSpan.Zero,
                    CreatedById = "Hedaya Admin", // set the created by id as appropriate
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };

                var x = PodcastHelper.GetAudioDuration(audioFilePath);

                // Add the new podcast entity to the database and save changes
                _context.Podcasts.Add(podcast);
                await _context.SaveChangesAsync();

                // Return the ID of the newly created podcast entity
                return podcast.Id;
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            
         
        }
    }

}
