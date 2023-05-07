using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.Podcasts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Queries
{
    public class GetFavoritePodcastsQuery : IRequest<object>
    {
        public int PageNumber { get; set; } = 1;
        public string UserId { get; set; }

        public class GetFavoritePodcastsQueryHandler : IRequestHandler<GetFavoritePodcastsQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetFavoritePodcastsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetFavoritePodcastsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var PageSize = 10;
                    var traineeId = await _context.Trainees
                        .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                        .Select(a => a.Id)
                        .FirstOrDefaultAsync(cancellationToken);

                    var trainee = await _context.Trainees.FindAsync(traineeId);
                    if (trainee == null)
                    {
                        throw new ArgumentException("Invalid trainee ID");
                    }

                    var podcastsQuery = _context.Podcasts
                        .Where(p => p.PodcastFavourites.Any(f => f.TraineeId == traineeId))
                        .OrderByDescending(p => p.Id);

                    var totalCount = await podcastsQuery.CountAsync(cancellationToken);

                    var podcasts = await podcastsQuery
                        .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                        .ToListAsync(cancellationToken);

                    var podcastDtos = podcasts.Select(p => new PodcastDTO
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        IsFav = true,
                        Duration = p.Duration,
                        AudioUrl = p.AudioUrl,
                        PublishDate = p.PublishDate,
                        CreatedDate = p.CreationDate
                    }).ToList();

                    var favorites = new PaginatedList<PodcastDTO>(podcastDtos, totalCount, request.PageNumber, PageSize, (int)Math.Ceiling((double)totalCount / PageSize));
                    return new { Result = favorites };
                }
                catch (Exception ex)
                {

                    throw  new Exception(ex.Message);
                }
        
            }
        }
    }
}
