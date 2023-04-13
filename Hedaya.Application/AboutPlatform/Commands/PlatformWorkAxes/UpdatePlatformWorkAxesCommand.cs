using Hedaya.Application.Interfaces;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformWorkAxes
{
    public class UpdatePlatformWorkAxesCommand : IRequest<Unit>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePlatformWorkAxesCommandHandler : IRequestHandler<UpdatePlatformWorkAxesCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePlatformWorkAxesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePlatformWorkAxesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlatformWorkAxes.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlatformWorkAxes));
            }

            entity.Title = request.Title ?? entity.Title;
            entity.Description = request.Description ?? entity.Description;
            entity.ModifiedById = "HedayaAdmin";
            entity.ModificationDate = DateTime.UtcNow;

            _context.PlatformWorkAxes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

