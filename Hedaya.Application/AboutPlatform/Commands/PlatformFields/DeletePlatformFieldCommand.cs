using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFields
{
    public class DeletePlatformFieldCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string WebRootPath { get; set; }
    }

    public class DeletePlatformFieldCommandHandler : IRequestHandler<DeletePlatformFieldCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeletePlatformFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePlatformFieldCommand request, CancellationToken cancellationToken)
        {
            var platformField = await _context.PlatformFields.FindAsync(request.Id);

            if (platformField == null)
            {
                throw new NotFoundException(nameof(PlatformField));
            }

            platformField.Deleted = true;
            platformField.ModifiedById = "HedayaAdmin";
            platformField.ModificationDate = DateTime.UtcNow;

            if(!string.IsNullOrEmpty(platformField.IconUrl))
            {
                var imagePath = Path.Combine(request.WebRootPath, platformField.IconUrl);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
          

            _context.PlatformFields.Update(platformField);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
