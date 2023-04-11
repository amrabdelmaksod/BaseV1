using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;

namespace Hedaya.Application.Complexes.Commands.Create
{
    public class CreateComplexCommand : IRequest<int>
    {
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string AddressDescription { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string LandlinePhone { get; set; }
        public string TermsAr { get; set; }
        public string TermsEn { get; set; }
        public string ConditionsAr { get; set; }
        public string ConditionsEn { get; set; }
        public string LogFilesAr { get; set; }
        public string LogFilesEn { get; set; }
        public string CookiesAr { get; set; }
        public string CookiesEn { get; set; }
        public string VisionAr { get; set; }
        public string VisionEn { get; set; }
        public string MissionAr { get; set; }
        public string MissionEn { get; set; }
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
                    TitleAr = request.TitleAr,
                    TitleEn = request.TitleEn,
                    AddressDescription = request.AddressDescription,
                    Email = request.Email,
                    Mobile = request.Mobile,
                    LandlinePhone = request.LandlinePhone,
                    TermsAr = request.TermsAr,
                    TermsEn = request.TermsEn,
                    ConditionsAr = request.ConditionsAr,
                    ConditionsEn = request.ConditionsEn,
                    LogFilesAr = request.LogFilesAr,
                    LogFilesEn = request.LogFilesEn,
                    CookiesAr = request.CookiesAr,
                    CookiesEn = request.CookiesEn,
                    VisionAr = request.VisionAr,
                    VisionEn = request.VisionEn,
                    MissionAr = request.MissionAr,
                    MissionEn = request.MissionEn,
                    AboutPlatformVideoUrl = request.AboutPlatformVideoUrl,
                };

                _context.Complexes.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }

}
