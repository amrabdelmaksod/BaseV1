using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformFeatures
{
    public class GetAllPlatformFeaturesQuery : IRequest<PaginatedList<PlatformFeaturesDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetAllPlatformFeaturesQueryHandler : IRequestHandler<GetAllPlatformFeaturesQuery, PaginatedList<PlatformFeaturesDto>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllPlatformFeaturesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PaginatedList<PlatformFeaturesDto>> Handle(GetAllPlatformFeaturesQuery request, CancellationToken cancellationToken)
            {
                var query = _context.PlatformFeatures
                    .AsNoTracking()
                    .Where(x => !x.Deleted)
                    .OrderByDescending(x => x.CreationDate)
                    .Select(x => new PlatformFeaturesDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description
                    });

                int totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                int pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

                return new PaginatedList<PlatformFeaturesDto>(items, totalCount, request.PageNumber,request.PageSize, pageCount);
            }
        }
    }

}
