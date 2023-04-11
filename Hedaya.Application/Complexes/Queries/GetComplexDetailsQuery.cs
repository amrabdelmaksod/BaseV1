using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Complexes.Queries.GetComplex
{
    public class GetComplexQuery : IRequest<ComplexDetailsDto>
    {
    }

    public class GetComplexQueryHandler : IRequestHandler<GetComplexQuery, ComplexDetailsDto>
    {
        private readonly IApplicationDbContext _context;

        public GetComplexQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ComplexDetailsDto> Handle(GetComplexQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complexes
                .Select(c => new ComplexDetailsDto
                {
                    Id = c.Id,
                    Title =CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.TitleAr : c.TitleEn,
                    AddressDescription = c.AddressDescription,
                    Email = c.Email,
                    Mobile = c.Mobile,
                    LandlinePhone = c.LandlinePhone,
                    Terms =CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.TermsAr : c.TermsEn,
                    Conditions = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.ConditionsAr : c.ConditionsEn,
                    LogFiles = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.LogFilesAr : c.LogFilesEn,
                    Cookies = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.CookiesAr : c.ConditionsEn,
                    Vision = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.VisionAr : c.VisionEn,
                    Mission = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.MissionAr : c.MissionEn,
                    AboutPlatformVideoUrl = c.AboutPlatformVideoUrl
                })
                .FirstOrDefaultAsync(cancellationToken);

            return entity;
        }
    }
}
