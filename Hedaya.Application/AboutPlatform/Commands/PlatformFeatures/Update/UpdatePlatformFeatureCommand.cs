using Hedaya.Application.Interfaces;
using MediatR;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Update
{
    public class UpdatePlatformFeatureCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class UpdatePlatformFeatureCommandHandler : IRequestHandler<UpdatePlatformFeatureCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePlatformFeatureCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePlatformFeatureCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlatformFeatures.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new ApplicationException($"Platform feature with Id={request.Id} not found.");
                }

                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.ModifiedById = "HedayaAdmin";
                entity.ModificationDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

