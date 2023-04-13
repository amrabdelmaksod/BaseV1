using Hedaya.Application.Interfaces;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformWorkAxes
{
    public class DeletePlatformWorkAxesCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePlatformWorkAxesCommandHandler : IRequestHandler<DeletePlatformWorkAxesCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePlatformWorkAxesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePlatformWorkAxesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlatformWorkAxes.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlatformWorkAxes));
            }

            entity.Deleted = true;
            entity.ModifiedById = "HedayaAdmin";
            entity.ModificationDate = DateTime.UtcNow;

            _context.PlatformWorkAxes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

