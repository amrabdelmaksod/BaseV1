using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformWorkAxes
{
    public class CreatePlatformWorkAxesCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreatePlatformWorkAxesCommandHandler : IRequestHandler<CreatePlatformWorkAxesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlatformWorkAxesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePlatformWorkAxesCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.PlatformWorkAxes
            {
                Title = request.Title,
                Description = request.Description,
                CreatedById = "HedayaAdmin",
                CreationDate = DateTime.UtcNow,
                Deleted = false
            };

            _context.PlatformWorkAxes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
