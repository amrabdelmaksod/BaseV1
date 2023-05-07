using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.Podcasts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Podcasts.Queries
{
    public class GetAllPodcastsQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
        public string UserId { get; set; }

        public class GetAllPodcastsQueryHandler : IRequestHandler<GetAllPodcastsQuery, object>
        {
            private readonly IApplicationDbContext _context;


            public GetAllPodcastsQueryHandler(IApplicationDbContext context)
            {
                _context = context;

            }

            public async Task<object> Handle(GetAllPodcastsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var traineeId = await _context.Trainees
       .Where(a => a.AppUserId == request.UserId && !a.Deleted)
       .Select(a => a.Id)
       .FirstOrDefaultAsync(cancellationToken);

                    var pageSize = 10;
                    var totalCount = await _context.Podcasts.CountAsync(cancellationToken);
                    var podcasts = await _context.Podcasts
                        .OrderByDescending(p => p.PublishDate)
                        .Skip((request.PageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Select(a => new PodcastDTO
                        {
                            AudioUrl = a.AudioUrl,
                            CreatedDate = a.CreationDate,
                            Description = a.Description,
                            Duration = a.Duration,
                            Id = a.Id,
                            PublishDate = a.PublishDate,
                            Title = a.Title,
                            IsFav = _context.PodcastFavourites.Any(f => f.PodcastId == a.Id && f.TraineeId == traineeId),

                        })
                        .ToListAsync(cancellationToken);

                    var PaginatedResult = new PaginatedList<PodcastDTO>(podcasts, totalCount, request.PageNumber, pageSize, (int)Math.Ceiling((decimal)totalCount / pageSize));

                    return new { Result = PaginatedResult } ;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

             
            }
        }

    }

}