using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformWorkAxis
{
    public class GetAllPlatformWorkAxesQuery : IRequest<PaginatedList<PlatformWorkAxesDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllPlatformWorkAxesQueryHandler : IRequestHandler<GetAllPlatformWorkAxesQuery, PaginatedList<PlatformWorkAxesDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllPlatformWorkAxesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<PlatformWorkAxesDto>> Handle(GetAllPlatformWorkAxesQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _context.PlatformWorkAxes.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var platformWorkAxesDtos = await _context.PlatformWorkAxes
                .OrderBy(pwa => pwa.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(pwa => new PlatformWorkAxesDto
                {
                    Id = pwa.Id,
                    Title = pwa.Title,
                    Description = pwa.Description
                })
                .ToListAsync();

            return new PaginatedList<PlatformWorkAxesDto>(platformWorkAxesDtos, totalCount, request.PageNumber, request.PageSize, totalPages);
        }
    }

}
