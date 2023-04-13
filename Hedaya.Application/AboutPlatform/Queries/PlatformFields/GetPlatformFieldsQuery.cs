using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformFields
{
    public class GetPlatformFieldsQuery : IRequest<PaginatedList<PlatformFieldDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string WebRootPath { get; set; }
    }

    public class GetPlatformFieldsQueryHandler : IRequestHandler<GetPlatformFieldsQuery, PaginatedList<PlatformFieldDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPlatformFieldsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<PlatformFieldDto>> Handle(GetPlatformFieldsQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _context.PlatformFields.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var platformFields = await _context.PlatformFields
                .OrderBy(pf => pf.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(pf => new PlatformFieldDto
                {
                    Id = pf.Id,
                    Title = pf.Title,
                    Description = pf.Description,
                    IconUrl = Path.Combine(request.WebRootPath, "ImagePath", pf.IconUrl??"1")
        })
                .ToListAsync();

            return new PaginatedList<PlatformFieldDto>(platformFields, totalCount, request.PageNumber, request.PageSize, totalPages);
        }
    }
}
