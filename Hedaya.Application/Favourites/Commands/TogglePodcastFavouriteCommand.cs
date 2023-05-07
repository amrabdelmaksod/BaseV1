using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Commands
{
    public class TogglePodcastFavouriteCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsFavourite { get; set; }
        public string UserId { get; set; }

        public class TogglePodcastFavouriteCommandHandler : IRequestHandler<TogglePodcastFavouriteCommand>
        {
            private readonly IApplicationDbContext _context;

            public TogglePodcastFavouriteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(TogglePodcastFavouriteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var podcast = await _context.Podcasts.FindAsync(request.Id);
                    if (podcast == null)
                    {
                        throw new ArgumentException("Invalid podcast ID");
                    }

                    var traineeId = await _context.Trainees
                                       .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                                       .Select(a => a.Id)
                                       .FirstOrDefaultAsync(cancellationToken);

                    var trainee = await _context.Trainees.FindAsync(traineeId);
                    if (trainee == null)
                    {
                        throw new ArgumentException("Invalid trainee ID");
                    }

                    var favouritePodcast = await _context.PodcastFavourites
                        .FirstOrDefaultAsync(f => f.TraineeId == traineeId && f.PodcastId == request.Id);

                    if (request.IsFavourite && favouritePodcast == null)
                    {
                        _context.PodcastFavourites.Add(new PodcastFavourite
                        {
                            TraineeId = traineeId,
                            PodcastId = request.Id
                        });
                    }
                    else if (!request.IsFavourite && favouritePodcast != null)
                    {
                        _context.PodcastFavourites.Remove(favouritePodcast);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
