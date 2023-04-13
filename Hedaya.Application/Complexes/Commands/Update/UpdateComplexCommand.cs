using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Complexes.Commands.Update
{
    public class UpdateComplexCommand : IRequest
    {

        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? AddressDescription { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? LandlinePhone { get; set; }
        public string? TermsAr { get; set; }
        public string? TermsEn { get; set; }
        public string? ConditionsAr { get; set; }
        public string? ConditionsEn { get; set; }
        public string? LogFilesAr { get; set; }
        public string? LogFilesEn { get; set; }
        public string? CookiesAr { get; set; }
        public string? CookiesEn { get; set; }
        public string? VisionAr { get; set; }
        public string? VisionEn { get; set; }
        public string? MissionAr { get; set; }
        public string? MissionEn { get; set; }
        public string? AboutPlatformVideoUrl { get; set; }

        public class Handler : IRequestHandler<UpdateComplexCommand>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateComplexCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Complexes.FirstOrDefaultAsync();


                if (entity == null)
                {
                    var complex = new Complex
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

                    await _context.Complexes.AddAsync(complex);
                    await _context.SaveChangesAsync();
                }
                if(!string.IsNullOrEmpty(request.TitleAr)) {
                    entity.TitleAr = request.TitleAr;
                }
                if (!string.IsNullOrEmpty(request.TitleEn))
                {
                    entity.TitleEn = request.TitleEn;
                }

                if (!string.IsNullOrEmpty(request.AddressDescription))
                {
                    entity.AddressDescription = request.AddressDescription;
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    entity.Email = request.Email;
                }

                if (!string.IsNullOrEmpty(request.Mobile))
                {
                    entity.Mobile = request.Mobile;
                }

                if (!string.IsNullOrEmpty(request.LandlinePhone))
                {
                    entity.LandlinePhone = request.LandlinePhone;
                }

                if (!string.IsNullOrEmpty(request.TermsAr))
                {
                    entity.TermsAr = request.TermsAr;
                }

                if (!string.IsNullOrEmpty(request.TermsEn))
                {
                    entity.TermsEn = request.TermsEn;
                }

                if (!string.IsNullOrEmpty(request.ConditionsAr))
                {
                    entity.ConditionsAr = request.ConditionsAr;
                }
                if (!string.IsNullOrEmpty(request.ConditionsEn))
                {
                    entity.ConditionsEn = request.ConditionsEn;
                }
                if (!string.IsNullOrEmpty(request.LogFilesAr))
                {
                    entity.LogFilesAr = request.LogFilesAr;
                }

                if (!string.IsNullOrEmpty(request.LogFilesEn))
                {
                    entity.LogFilesEn = request.LogFilesEn;
                }

                if (!string.IsNullOrEmpty(request.CookiesAr))
                {
                    entity.CookiesAr = request.CookiesAr;
                }

                if (!string.IsNullOrEmpty(request.CookiesEn))
                {
                    entity.CookiesEn = request.CookiesEn;
                }
                if (!string.IsNullOrEmpty(request.VisionAr))
                {
                    entity.VisionAr = request.VisionAr;
                }
                if (!string.IsNullOrEmpty(request.VisionEn))
                {
                    entity.VisionEn = request.VisionEn;
                }

                if (!string.IsNullOrEmpty(request.MissionAr))
                {
                    entity.MissionAr = request.MissionAr;
                }


                if (!string.IsNullOrEmpty(request.MissionEn))
                {
                    entity.MissionEn = request.MissionEn;
                }
                if (!string.IsNullOrEmpty(request.AboutPlatformVideoUrl))
                {
                    entity.AboutPlatformVideoUrl = request.AboutPlatformVideoUrl;
                }

               

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
