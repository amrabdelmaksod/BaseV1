using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformFeatures
{
    public class GetPlatformFeatureByIdQuery : IRequest<PlatformFeaturesDto>
    {
        public int Id { get; set; }

        public class GetPlatformFeatureByIdQueryHandler : IRequestHandler<GetPlatformFeatureByIdQuery, PlatformFeaturesDto>
        {
            private readonly IApplicationDbContext _context;

            public GetPlatformFeatureByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PlatformFeaturesDto> Handle(GetPlatformFeatureByIdQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlatformFeatures
                .AsNoTracking()
                    .Where(x => x.Id == request.Id && !x.Deleted)
                    .Select(x => new PlatformFeaturesDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new ApplicationException($"Platform feature with Id={request.Id} not found.");
                }

                return entity;
            }
        }
    }
}
