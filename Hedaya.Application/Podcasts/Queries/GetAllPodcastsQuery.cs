using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Application.Podcasts.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Podcasts.Queries
{
    public class GetAllPodcastsQuery : IRequest<object>
    {
        public int PageNumber { get; set; }

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
                    
                    var podcasts = await _context.Podcasts
                        .OrderByDescending(p => p.PublishDate)
                        .Skip((request.PageNumber - 1) * 10)
                        .Take(10).Select(a => new PodcastDTO
                        {
                            AudioUrl = a.AudioUrl,
                            CreatedDate = a.CreatedDate,
                            Description = a.Description,
                            Duration = a.Duration,
                            Id = a.Id,
                            PublishDate = a.PublishDate,
                            Title = a.Title
                        })
                        .ToListAsync(cancellationToken);

                    return new { Result = podcasts } ;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

             
            }
        }

    }

}