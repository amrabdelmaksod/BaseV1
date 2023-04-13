using Hedaya.Application.Interfaces;
using MediatR;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Delete
{
    public class DeletePlatformFeatureCommand : IRequest
    {
        public int Id { get; set; }

        public class DeletePlatformFeatureCommandHandler : IRequestHandler<DeletePlatformFeatureCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeletePlatformFeatureCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePlatformFeatureCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlatformFeatures.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new ApplicationException($"Platform feature with Id={request.Id} not found.");
                }

                entity.Deleted = true;
                entity.ModifiedById = "HedayaAdmin";
                entity.ModificationDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
