using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Podcasts.Commands.Delete
{
    public class DeletePodcastCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePodcastCommandHandler : IRequestHandler<DeletePodcastCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePodcastCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePodcastCommand request, CancellationToken cancellationToken)
        {
            var podcast = await _context.Podcasts.FindAsync(request.Id);

            if (podcast == null)
            {
                throw new NotFoundException(nameof(Podcast));
            }

            podcast.Deleted = true;
            podcast.ModificationDate = DateTime.UtcNow;
            podcast.ModifiedById = "Hedaya Admin";

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
