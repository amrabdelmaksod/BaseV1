using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Complexes.Queries
{
    public class GetComplexDataForDashboardQuery : IRequest<ComplexDto>
    {
    }

    public class GetComplexDataQueryHandler : IRequestHandler<GetComplexDataForDashboardQuery, ComplexDto>
    {
        private readonly IApplicationDbContext _context;

        public GetComplexDataQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ComplexDto> Handle(GetComplexDataForDashboardQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complexes
                .Select(c => new ComplexDto
                {
                    Id = c.Id,
                    TitleAr = c.TitleAr,
                    TitleEn = c.TitleEn,
                    AddressDescription = c.AddressDescription,
                    Email = c.Email,
                    Mobile = c.Mobile,
                    LandlinePhone = c.LandlinePhone,
                   ConditionsAr = c.ConditionsAr,
                   ConditionsEn = c.ConditionsEn,
                   CookiesAr = c.CookiesAr,
                   CookiesEn = c.CookiesEn,
                   LogFilesAr = c.LogFilesAr,
                   LogFilesEn = c.LogFilesEn,
                   MissionAr = c.MissionAr,
                   MissionEn = c.MissionEn,
                   TermsAr = c.TermsAr,
                   TermsEn = c.TermsEn,
                   VisionAr = c.VisionAr,
                   VisionEn = c.VisionEn,
                    AboutPlatformVideoUrl = c.AboutPlatformVideoUrl
                })
                .FirstOrDefaultAsync(cancellationToken);

            return entity;
        }
    }
}
