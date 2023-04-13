using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hedaya.Application.AboutPlatform.Commands.PlatformFields
{
    public class CreatePlatformFieldCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageIcon { get; set; }
        public string? webrootpath { get; set; }
    }

    public class CreatePlatformFieldCommandHandler : IRequestHandler<CreatePlatformFieldCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlatformFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePlatformFieldCommand request, CancellationToken cancellationToken)
        {
            var platformField = new PlatformField
            {
                Title = request.Title,
                Description = request.Description,
                CreatedById = "HedayaAdmin",
                CreationDate = DateTime.Now,
            };

            if (request.ImageIcon != null)
            {
                var imagePath = Path.Combine(request.webrootpath, "ImagePath");

                platformField.IconUrl = await ImageHelper.SaveImageAsync(request.ImageIcon, imagePath);
            }

            _context.PlatformFields.Add(platformField);
            await _context.SaveChangesAsync(cancellationToken);

            return platformField.Id;
        }
    }

}
