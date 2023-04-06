using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Complexes.Commands.Update
{
    public class UpdateComplexCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AddressDescription { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string LandlinePhone { get; set; }
        public string Terms { get; set; }
        public string Conditions { get; set; }
        public string LogFiles { get; set; }
        public string Cookies { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string AboutPlatformVideoUrl { get; set; }

        public class Handler : IRequestHandler<UpdateComplexCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateComplexCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Complexes.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Complex));
                }

                entity.Title = request.Title;
                entity.AddressDescription = request.AddressDescription;
                entity.Email = request.Email;
                entity.Mobile = request.Mobile;
                entity.LandlinePhone = request.LandlinePhone;
                entity.Terms = request.Terms;
                entity.Conditions = request.Conditions;
                entity.LogFiles = request.LogFiles;
                entity.Cookies = request.Cookies;
                entity.Vision = request.Vision;
                entity.Mission = request.Mission;
                entity.AboutPlatformVideoUrl = request.AboutPlatformVideoUrl;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
