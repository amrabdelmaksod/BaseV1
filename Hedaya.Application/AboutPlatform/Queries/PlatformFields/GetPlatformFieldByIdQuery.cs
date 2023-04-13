using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Queries.PlatformFields
{
    public class GetPlatformFieldByIdQuery : IRequest<PlatformFieldDto>
    {
        public int Id { get; set; }
        public string WebRootPath { get; set; }
    }

    public class GetPlatformFieldByIdQueryHandler : IRequestHandler<GetPlatformFieldByIdQuery, PlatformFieldDto>
    {
        private readonly IApplicationDbContext _context;

        public GetPlatformFieldByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlatformFieldDto> Handle(GetPlatformFieldByIdQuery request, CancellationToken cancellationToken)
        {
            var platformField = await _context.PlatformFields.FindAsync(request.Id);

            if (platformField == null)
            {
                throw new NotFoundException(nameof(PlatformField));
            }

            var platformFieldDto = new PlatformFieldDto
            {
                Id = platformField.Id,
                Title = platformField.Title,
                Description = platformField.Description,
                IconUrl = Path.Combine(request.WebRootPath, "ImagePath", platformField.IconUrl ?? "1")
            };

            return platformFieldDto;
        }
    }
}
