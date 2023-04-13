using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Org.BouncyCastle.Crypto;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Create
{
    public class CreatePlatformFeatureCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public class CreatePlatformFeatureCommandHandler : IRequestHandler<CreatePlatformFeatureCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreatePlatformFeatureCommandHandler(IApplicationDbContext context)
            {
                _context = context;

            }

            public async Task<int> Handle(CreatePlatformFeatureCommand request, CancellationToken cancellationToken)
            {
                var entity = new PlatformFeature
                {
                    Title = request.Title,
                    Description = request.Description,
                    CreatedById = "HedayaAdmin",
                    CreationDate = DateTime.Now,
                };

                _context.PlatformFeatures.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
