using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.Complexes.Commands.Create
{
    public class CreateComplexCommand : IRequest<int>
    {
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

        public class Handler : IRequestHandler<CreateComplexCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateComplexCommand request, CancellationToken cancellationToken)
            {
                var entity = new Complex
                {
                    Title = request.Title,
                    AddressDescription = request.AddressDescription,
                    Email = request.Email,
                    Mobile = request.Mobile,
                    LandlinePhone = request.LandlinePhone,
                    Terms = request.Terms,
                    Conditions = request.Conditions,
                    LogFiles = request.LogFiles,
                    Cookies = request.Cookies,
                    Vision = request.Vision,
                    Mission = request.Mission,
                    AboutPlatformVideoUrl = request.AboutPlatformVideoUrl,
                };

                _context.Complexes.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }

}
