using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFields
{
    public class UpdatePlatformFieldCommand : IRequest<Unit>
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageIcon { get; set; }
        public string? WebRootPath { get; set; }
    }

    public class UpdatePlatformFieldCommandHandler : IRequestHandler<UpdatePlatformFieldCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePlatformFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePlatformFieldCommand request, CancellationToken cancellationToken)
        {
            var platformField = await _context.PlatformFields.FindAsync(request.Id);

            if (platformField == null)
            {
                throw new NotFoundException(nameof(PlatformField));
            }

            platformField.Title = request.Title ?? platformField.Title;
            platformField.Description = request.Description ?? platformField.Description;

            if (request.ImageIcon != null)
            {
                var imagePath = Path.Combine(request.WebRootPath, "ImagePath");

                if (platformField.IconUrl != null)
                {
                    var existingImagePath = Path.Combine(request.WebRootPath, platformField.IconUrl);

                   
                        File.Delete(existingImagePath);
                    
                }

                platformField.IconUrl = await ImageHelper.SaveImageAsync(request.ImageIcon, imagePath);
            }

            _context.PlatformFields.Update(platformField);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
