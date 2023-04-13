using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformWorkAxis
{
    public class GetPlatformWorkAxesByIdQuery : IRequest<PlatformWorkAxesDto>
    {
        public int Id { get; set; }
    }

    public class GetPlatformWorkAxesByIdQueryHandler : IRequestHandler<GetPlatformWorkAxesByIdQuery, PlatformWorkAxesDto>
    {
        private readonly IApplicationDbContext _context;

        public GetPlatformWorkAxesByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlatformWorkAxesDto> Handle(GetPlatformWorkAxesByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlatformWorkAxes.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlatformWorkAxes));
            }

            return new PlatformWorkAxesDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
        }
    }
}
